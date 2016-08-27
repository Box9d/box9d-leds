using System.Collections.Concurrent;
using System.Drawing;
using AForge.Video.FFMPEG;
using Box9.Leds.Core.Configuration;

namespace Box9.Leds.RealtimeVideo
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
            using (var reader = new VideoFileReader())
            {
                reader.Open(configuration.VideoConfig.SourceFilePath);
                var framerate = reader.FrameRate;
                var frameCount = reader.FrameCount;
                
                while (currentFrame <= reader.FrameCount)
                {
                    var frame = reader.ReadVideoFrame();

                    if (currentFrame / framerate > minutes * 60 + seconds)
                    {
                        Frames.TryAdd(currentFrame, frame);
                    }
                    currentFrame++;
                }
            }
        }
    }
}
