using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Box9.Leds.Core.LedLayouts;
using Box9.Leds.Core.Messages.UpdatePixels;
using Box9.Leds.Core.Patterns;
using Box9.Leds.FcClient;
using NAudio.Wave;

namespace Box9.Leds.Video
{
    public class VideoPlayer : IDisposable
    {
        private readonly IVideoReader videoReader;

        private IClientWrapper client;
        private VideoData videoData;
        private string audioFilePath;
        private LedLayout ledLayout;
        private int lastFrame;
        private bool videoIsLoaded;
        private int totalPlayTime;

        public delegate void ChangeVideoStatus(VideoStatus status);
        public event ChangeVideoStatus VideoStatusChanged;

        public VideoPlayer(IVideoReader videoReader)
        {
            this.videoReader = videoReader;
            this.videoIsLoaded = false;

            VideoStatusChanged += VideoStatusChangedHandle;

            VideoStatusChanged(VideoStatus.None);
        }

        public void Load(IClientWrapper client, string videoFilePath, LedLayout ledLayout)
        {
            videoData = videoReader.TransformVideo(videoFilePath, ledLayout);

            if (videoData.Frames.Any())
            {
                this.lastFrame = videoData.Frames.Max(f => f.Key);
            }

            this.ledLayout = ledLayout;
            this.client = client;
            this.audioFilePath = videoReader.ExtractAudioToFile(videoFilePath);
            this.totalPlayTime = (int)Math.Round((double)((double)videoData.Frames.Count / (double)videoData.Framerate) * 1000, 0);

            videoIsLoaded = true;
            VideoStatusChanged(VideoStatus.ReadyToPlay);
        }

        public async Task Play(CancellationToken cancellationToken)
        {
            if (!videoIsLoaded)
            {
                throw new InvalidOperationException("Load a video before trying to play");
            }

            await client.ConnectAsync();

            VideoStatusChanged(VideoStatus.Playing);

            using (var ms = File.OpenRead(audioFilePath))
            using (var rdr = new Mp3FileReader(ms))
            using (var wavStream = WaveFormatConversionStream.CreatePcmStream(rdr))
            using (var baStream = new BlockAlignReductionStream(wavStream))
            using (var waveOut = new WaveOut(WaveCallbackInfo.FunctionCallback()))
            {
                waveOut.Init(baStream);

                var playStopwatch = new Stopwatch();
                waveOut.Play();
                playStopwatch.Start();
                while (playStopwatch.ElapsedMilliseconds < totalPlayTime && !cancellationToken.IsCancellationRequested)
                {
                    double secondsPassed = (double)playStopwatch.ElapsedMilliseconds / 1000;

                    var currentFrame = (int)Math.Round(secondsPassed * videoData.Framerate, 0);
                    if (currentFrame > lastFrame)
                    {
                        break;
                    }

                    await client.SendPixelUpdates(new UpdatePixelsRequest
                    {
                        PixelUpdates = videoData.Frames[currentFrame]
                    });
                }

                var allPixelsBlack = Block.GeneratePattern(
                    Color.Black, ledLayout, ledLayout.XNumberOfPixels, ledLayout.YNumberOfPixels, 0, 0);

                await client.SendPixelUpdates(new UpdatePixelsRequest
                {
                    PixelUpdates = allPixelsBlack
                });

                if (waveOut.PlaybackState == PlaybackState.Playing)
                {
                    waveOut.Stop();
                }

                await client.CloseAsync();

                VideoStatusChanged(VideoStatus.ReadyToPlay);
            }           
        }

        private void VideoStatusChangedHandle(VideoStatus status)
        {
        }

        public void Dispose()
        {
            this.videoReader.Dispose();
        }
    }
}
