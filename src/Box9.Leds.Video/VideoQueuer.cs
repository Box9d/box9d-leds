using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Threading;
using AForge.Video.FFMPEG;
using Box9.Leds.Core;
using Box9.Leds.Core.Configuration;
using NReco.VideoConverter;

namespace Box9.Leds.Video
{
    public class VideoQueuer : IDisposable
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
            var thumbnailReader = new FFMpegConverter();
            var imageConverter = new ImageConverter();

            using (var videoFileReader = new VideoFileReader())
            {
                videoFileReader.Open(configuration.VideoConfig.SourceFilePath);
                var framerate = videoFileReader.FrameRate;
                var frameCount = videoFileReader.FrameCount;

                var currentFrame = 1 + (minutes * 60 + seconds) * framerate;

                while (currentFrame < frameCount)
                {
                    var frame = videoFileReader.ReadVideoFrame();
                    
                    if (frame == null)
                    {
                        break;
                    }

                    if (currentFrame / framerate + 1 > minutes * 60 + seconds)
                    {
                        Frames.TryAdd(currentFrame, frame);
                    }

                    currentFrame++;
                }
            }
        }

        public void Dispose()
        {
            foreach (var frame in Frames.Where(f => f.Value != null).Select(f => f.Value))
            {
                frame.Dispose();
            }
        }
    }
}
