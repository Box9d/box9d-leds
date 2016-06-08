using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Box9.Leds.Core.LedLayouts;
using Box9.Leds.Core.Messages.UpdatePixels;
using Box9.Leds.Core.Patterns;
using Box9.Leds.FcClient;

namespace Box9.Leds.Video
{
    public class VideoPlayer : IVideoPlayer
    {
        private readonly IVideoReader videoReader;

        private IClientWrapper fcClient;
        private VideoData videoData;
        private LedLayout ledLayout;
        private int lastFrame;
        private bool videoIsLoaded;
        private int totalPlayTime;

        public VideoPlayer(IVideoReader videoReader)
        {
            this.videoReader = videoReader;
            this.videoIsLoaded = false;
        }

        public void Load(IClientWrapper fcClient, string videoFilePath, LedLayout ledLayout)
        {
            videoData = videoReader.Transform(videoFilePath, ledLayout);

            if (videoData.Frames.Any())
            {
                this.lastFrame = videoData.Frames.Max(f => f.Key);
            }

            this.ledLayout = ledLayout;
            this.fcClient = fcClient;
            this.totalPlayTime = (int)Math.Round((double)((double)videoData.Frames.Count / (double)videoData.Framerate) * 1000, 0);

            videoIsLoaded = true;
        }

        public void Pause()
        {
            throw new NotImplementedException();
        }

        public async Task Play(int startFrame = 0)
        {
            if (!videoIsLoaded)
            {
                throw new InvalidOperationException("Load a video before trying to play");
            }

            await fcClient.ConnectAsync();

            var playStopwatch = new Stopwatch();
            playStopwatch.Start();
            while (playStopwatch.ElapsedMilliseconds < totalPlayTime)
            {
                double secondsPassed = (double)playStopwatch.ElapsedMilliseconds / 1000;

                var currentFrame = (int)Math.Round(secondsPassed * videoData.Framerate, 0);
                if (currentFrame > lastFrame)
                {
                    break;
                }

                await fcClient.SendPixelUpdates(new UpdatePixelsRequest
                {
                    PixelUpdates = videoData.Frames[currentFrame]
                });

                Console.WriteLine("Sent frame {0} at {1}", currentFrame, playStopwatch.ElapsedMilliseconds);
            }

            var allPixelsBlack = Block.GeneratePattern(
                Color.Black, ledLayout, ledLayout.XNumberOfPixels, ledLayout.YNumberOfPixels, 0, 0);

            await fcClient.SendPixelUpdates(new UpdatePixelsRequest
            {
                PixelUpdates = allPixelsBlack
            });
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
