using System.Collections.Generic;
using AForge.Video.FFMPEG;
using Box9.Leds.Core.LedLayouts;
using Box9.Leds.Core.UpdatePixels;

namespace Box9.Leds.Video
{
    public class VideoReader : IVideoReader
    {
        public VideoData Transform(string videoFilePath, LedLayout ledLayout)
        {
            int frameRate = 25; // Default
            long frameCount = 0;
            var xPixelMultiplier = 0;
            var yPixelMultiplier = 0;

            var frameTransforms = new Dictionary<int, IEnumerable<PixelInfo>>();

            using (var reader = new VideoFileReader())
            {
                reader.Open(videoFilePath);

                frameRate = reader.FrameRate;
                frameCount = reader.FrameCount;

                xPixelMultiplier = reader.Width / ledLayout.XNumberOfPixels;
                yPixelMultiplier = reader.Height / ledLayout.YNumberOfPixels;

                for (int f = 0; f < frameCount; f++)
                {
                    var frame = reader.ReadVideoFrame();
                    var framePixels = new List<PixelInfo>();

                    for (int i = 0; i < ledLayout.XNumberOfPixels; i++)
                    {
                        for (int j = 0; j < ledLayout.YNumberOfPixels; j++)
                        {
                            framePixels.Add(new PixelInfo
                            {
                                X = i,
                                Y = j,
                                Color = frame.GetPixel(i * xPixelMultiplier, j * yPixelMultiplier)
                            });
                        }
                    }

                    frameTransforms.Add(f, framePixels);
                }

            }

            return new VideoData
            {
                Framerate = frameRate,
                Frames = frameTransforms
            };    
        }
    }
}
