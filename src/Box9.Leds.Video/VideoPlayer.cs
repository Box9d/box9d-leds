using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Box9.Leds.Core.Configuration;
using Box9.Leds.Core.LedLayouts;
using Box9.Leds.Core.Messages.UpdatePixels;
using Box9.Leds.Core.Multitasking;
using Box9.Leds.Core.Patterns;
using Box9.Leds.DataStorage;
using Box9.Leds.FcClient;
using Box9.Leds.Video.Storage;
using DBreeze;

namespace Box9.Leds.Video
{
    public class VideoPlayer : IDisposable
    {
        private readonly IClientWrapper fcClient;
        private readonly VideoData videoData;
        private readonly ServerConfiguration serverConfiguration;

        private ChunkedConcurrentQueue<FrameVideoData> videoFrameQueue;
        private IChunkedStorageClient<int, FrameVideoData> storageClient;
        private VideoStatus currentStatus;

        private int loadedStorageKeys;

        public delegate void ChangeVideoStatus(VideoStatus status);
        public event ChangeVideoStatus VideoStatusChanged;

        public VideoPlayer(IClientWrapper fcClient, IChunkedStorageClient<int, FrameVideoData> storageClient, VideoData videoData, ServerConfiguration serverConfiguration)
        {
            this.fcClient = fcClient;
            this.videoData = videoData;
            this.serverConfiguration = serverConfiguration;
            this.storageClient = storageClient;

            this.videoFrameQueue = new ChunkedConcurrentQueue<FrameVideoData>();

            LoadBuffer(BufferReadyForStorageKeys());

            VideoStatusChanged += VideoStatusChangedHandle;
            VideoStatusChanged(VideoStatus.ReadyToPlay);
        }

        public async Task Play(CancellationToken cancellationToken)
        {
            if (currentStatus != VideoStatus.ReadyToPlay)
            {
                throw new InvalidOperationException("Load a video before trying to play");
            }

            await fcClient.ConnectAsync();

            VideoStatusChanged(VideoStatus.Playing);

            var playStopwatch = new Stopwatch();
            playStopwatch.Start();

            int loadedChunkStorageKey = 1;
            int currentFrame = 1;
            int totalPlayTime = (int)Math.Round((double)((double)videoData.TotalNumberOfFrames / (double)videoData.Framerate) * 1000, 0);

            FrameVideoData[] loadedChunk = null;
            while (playStopwatch.ElapsedMilliseconds < totalPlayTime && !cancellationToken.IsCancellationRequested)
            {
                double secondsPassed = (double)playStopwatch.ElapsedMilliseconds / 1000;

                currentFrame = (int)Math.Round(secondsPassed * videoData.Framerate, 0) + 1;
                if (currentFrame > videoData.TotalNumberOfFrames)
                {
                    break;
                }

                // Load the current chunk into memory to save IO reads from storage
                int storageKeyForCurrentFrame = 1;
                while (currentFrame > storageKeyForCurrentFrame * videoData.FramesPerStorageKey)
                {
                    storageKeyForCurrentFrame++;
                }

                if (loadedChunk == null || storageKeyForCurrentFrame != loadedChunkStorageKey)
                {
                    loadedChunk = videoFrameQueue.DequeueChunk().ToArray();
                    loadedChunkStorageKey = storageKeyForCurrentFrame;
                }

                var currentFrameInChunk = currentFrame - ((loadedChunkStorageKey - 1) * videoData.FramesPerStorageKey);
                var frameData = loadedChunk.Skip(currentFrameInChunk - 1).Take(1).SingleOrDefault();

                if (frameData != null)
                {
                    await fcClient.SendPixelUpdates(new UpdatePixelsRequest
                    {
                        PixelUpdates = frameData.PixelInfo
                    });
                }

                var readyForStorageKeys = BufferReadyForStorageKeys();
                Task.Run(() => LoadBuffer(readyForStorageKeys));
            }

            var allPixelsBlack = Block.GeneratePattern(
                Color.Black, serverConfiguration, serverConfiguration.XPixels, serverConfiguration.YPixels, 0, 0);

            await fcClient.SendPixelUpdates(new UpdatePixelsRequest
            {
                PixelUpdates = allPixelsBlack
            });

            await fcClient.CloseAsync();
            loadedStorageKeys = 0;

            LoadBuffer(BufferReadyForStorageKeys());
            VideoStatusChanged(VideoStatus.ReadyToPlay);     
        }

        private IEnumerable<int> BufferReadyForStorageKeys()
        {
            while (VideoSettings.BufferInSeconds * videoData.Framerate > videoFrameQueue.NumberOfItemsInAllChunks
               && loadedStorageKeys < videoData.FrameStorageKeys.Count())
            {
                loadedStorageKeys++;
                yield return loadedStorageKeys;
            }
        }

        private void LoadBuffer(IEnumerable<int> storageKeys)
        {
            storageKeys = storageKeys.OrderBy(k => k).ToArray();

            foreach (var storageKey in storageKeys)
            {
                IEnumerable<FrameVideoData> chunk = null;
                if (storageClient.LoadIfExists(storageKey, out chunk))
                {
                    videoFrameQueue.EnqueueChunk(chunk);
                }
            }
        }

        private void VideoStatusChangedHandle(VideoStatus status)
        {
            currentStatus = status;
        }

        public void Dispose()
        {
            this.fcClient.Dispose();
        }
    }
}
