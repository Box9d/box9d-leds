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
using Box9.Leds.Core.LedLayouts;
using Box9.Leds.Core.Messages.UpdatePixels;
using Box9.Leds.Core.Multitasking;
using Box9.Leds.Core.Patterns;
using Box9.Leds.Core.UpdatePixels;
using Box9.Leds.DataStorage;
using Box9.Leds.FcClient;
using NAudio.Wave;

namespace Box9.Leds.Video
{
    public class VideoPlayer : IDisposable
    {
        private readonly IVideoTransformer videoReader;
        private readonly IChunkedStorageClient<int, FrameVideoData> videoStorageClient;
        private readonly IClientWrapper fcClient;
        private readonly int framesPerStorageKey;

        private ChunkedQueue<FrameVideoData> videoFrameQueue;
        private IMp3AudioPlayer mp3AudioPlayer;
        private VideoStatus currentStatus;
        private VideoData videoData;
        private VideoAudioData videoAudioData;

        private LedLayout ledLayout;
        private int totalPlayTime;
        private int loadedStorageKeys;

        public delegate void ChangeVideoStatus(VideoStatus status);
        public event ChangeVideoStatus VideoStatusChanged;

        public VideoPlayer(IChunkedStorageClient<int, FrameVideoData> videoStorageClient,
            IVideoTransformer videoReader,
            IClientWrapper fcClient,
            int framesPerStorageKey,
            int bufferInSeconds)
        {
            this.videoReader = videoReader;
            this.videoStorageClient = videoStorageClient;
            this.fcClient = fcClient;

            this.framesPerStorageKey = framesPerStorageKey;

            VideoStatusChanged += VideoStatusChangedHandle;
            VideoStatusChanged(VideoStatus.None);
        }

        public void Load(LedLayout ledLayout)
        {
            fcClient.ConnectAsync();

            this.videoFrameQueue = new ChunkedQueue<FrameVideoData>();

            if (currentStatus == VideoStatus.None)
            {
                videoAudioData = videoReader.ExtractAndSaveAudio();
                videoData = videoReader.ExtractAndSaveTransformedVideoInChunks(framesPerStorageKey, ledLayout);

                mp3AudioPlayer = new Mp3AudioPlayer(videoAudioData);
            }

            this.ledLayout = ledLayout;
            this.totalPlayTime = (int)Math.Round((double)((double)videoData.TotalNumberOfFrames / (double)videoData.Framerate) * 1000, 0);

            LoadBuffer(BufferReadyForStorageKeys());

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
            mp3AudioPlayer.Play();
            playStopwatch.Start();

            int loadedChunkStorageKey = 1;
            int currentFrame = 1;
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
                Color.Black, ledLayout, ledLayout.XNumberOfPixels, ledLayout.YNumberOfPixels, 0, 0);

            await fcClient.SendPixelUpdates(new UpdatePixelsRequest
            {
                PixelUpdates = allPixelsBlack
            });

            mp3AudioPlayer.Stop();
            await fcClient.CloseAsync();
            mp3AudioPlayer.Dispose();

            mp3AudioPlayer = new Mp3AudioPlayer(videoAudioData);
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
                if (videoStorageClient.LoadIfExists(storageKey, out chunk))
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
            this.mp3AudioPlayer.Dispose();
        }
    }
}
