using System;
using System.Drawing;
using System.Linq;
using System.Threading;
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
        private Timer timer;
        private LedLayout ledLayout;
        private int frameTime;
        private int currentFrame;
        private int lastFrame;
        private bool videoIsLoaded;

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
            this.frameTime = 1000 / videoData.Framerate;
            this.fcClient = fcClient;

            videoIsLoaded = true;
        }

        public void Pause()
        {
            throw new NotImplementedException();
        }

        public void Play(int startFrame = 0)
        {
            if (!videoIsLoaded)
            {
                throw new InvalidOperationException("Use Load() to load a video before trying to play");
            }

            fcClient.ConnectAsync().Wait();

            timer = new Timer(PlayFrameCallback, videoData, 0, frameTime);
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        private void PlayFrameCallback(object videoData)
        {
            fcClient.SendPixelUpdates(new UpdatePixelsRequest
            {
                PixelUpdates = ((VideoData)videoData).Frames[currentFrame]
            });

            Console.WriteLine("Pixel updates sent..."); // Temp

            if (currentFrame == lastFrame)
            {
                var allPixelsBlack = Block.GeneratePattern(
                    Color.Black, ledLayout, ledLayout.XNumberOfPixels, ledLayout.YNumberOfPixels, 0, 0);

                fcClient.SendPixelUpdates(new UpdatePixelsRequest
                {
                    PixelUpdates = allPixelsBlack
                });

                timer.Change(Timeout.Infinite, Timeout.Infinite);
            }
            else
            {
                currentFrame++;
            }
        }
    }
}
