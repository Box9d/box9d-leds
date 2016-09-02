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
using PixelMapSharp;

namespace Box9.Leds.Video
{
    public class VideoQueuer : IDisposable
    {
        public Dictionary<int, Bitmap> Frames { get; }
        private readonly LedConfiguration configuration;

        public VideoQueuer(LedConfiguration configuration)
        {
            this.configuration = configuration;
            Frames = new Dictionary<int, Bitmap>();
        }

        public void QueueFrames(int minutes, int seconds)
        {
            using (var videoFileReader = new VideoFileReader())
            {
                videoFileReader.Open(configuration.VideoConfig.SourceFilePath);
                var framerate = videoFileReader.FrameRate;
                var frameCount = videoFileReader.FrameCount;

                var currentFrame = 1 + (minutes * 60 + seconds) * framerate;

                while (currentFrame < frameCount)
                {
                    var frame = videoFileReader.ReadVideoFrame();

                    try
                    {
                        if (frame == null)
                        {
                            break;
                        }

                        if (currentFrame / framerate + 1 > minutes * 60 + seconds)
                        {
                            Frames.Add(currentFrame, (Bitmap)frame.GetThumbnailImage(0,0,null, IntPtr.Zero));
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(string.Format("Couldn't queue one of more frames. Width & Height of frame was {0} & {1}", frame.Width, frame.Height), ex);
                    }

                    frame.Dispose();
                    currentFrame++;
                }
            }
        }

        public void Dispose()
        {
            foreach (var frame in Frames.Values)
            {
                if (frame != null)
                {
                    frame.Dispose();
                }
            }
        }
    }
}
