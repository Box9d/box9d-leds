using System.Collections.Concurrent;
using System.Drawing;
using System.Threading;
using AForge.Video.FFMPEG;
using Box9.Leds.Core.Configuration;

namespace Box9.Leds.Video
{
    public class VideoQueuer
    {
        public ConcurrentDictionary<int, Bitmap> Frames { get; }

        private readonly LedConfiguration configuration;

        public VideoQueuer(LedConfiguration configuration)
        {
            this.configuration = configuration;

            Frames = new ConcurrentDictionary<int, Bitmap>();
        }

        public void QueueFrames(int minutes, int seconds)
        {
            int currentFrame = 1;

            using (var videoFileReader = new VideoFileReader())
            {
                videoFileReader.Open(configuration.VideoConfig.SourceFilePath);
                var framerate = videoFileReader.FrameRate;
                var frameCount = videoFileReader.FrameCount;

                var frame = videoFileReader.ReadVideoFrame();
                while (frame != null)
                {
                    if (currentFrame / framerate + 1 > minutes * 60 + seconds)
                    {
                        Frames.TryAdd(currentFrame, frame);
                    }

                    frame = videoFileReader.ReadVideoFrame();
                    currentFrame++;
                }
            }
        }
    }
}
