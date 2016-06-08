using System;
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
            var frameTransforms = new Dictionary<int, IEnumerable<PixelInfo>>();
            int frameRate = 30; // Default

            using (var reader = new VideoFileReader())
            {
                reader.Open(videoFilePath);

                frameRate = reader.FrameRate;

                var xPixelMultiplier = reader.Width / ledLayout.XNumberOfPixels;
                var yPixelMultipler = reader.Height / ledLayout.YNumberOfPixels;

                for (int f = 0; f < reader.FrameCount; f++)
                {
                    var framePixels = new List<PixelInfo>();
                    var frame = reader.ReadVideoFrame();
                    for (int i = 0; i < ledLayout.XNumberOfPixels; i++)
                    {
                        for (int j = 0; j < ledLayout.YNumberOfPixels; j++)
                        {
                            framePixels.Add(new PixelInfo
                            {
                                X = i,
                                Y = j,
                                Color = frame.GetPixel(i * xPixelMultiplier, j * yPixelMultipler)
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
