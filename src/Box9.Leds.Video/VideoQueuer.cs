﻿using System.Collections.Concurrent;
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

        public void QueueFrames(int minutes, int seconds, VideoFileReader videoFileReader)
        {
            int currentFrame = 1;

            videoFileReader.Open(configuration.VideoConfig.SourceFilePath);
            var framerate = videoFileReader.FrameRate;
            var frameCount = videoFileReader.FrameCount;

            while (currentFrame <= frameCount)
            {
                var frame = videoFileReader.ReadVideoFrame();
                if (currentFrame / framerate + 1 > minutes * 60 + seconds)
                {
                    Frames.TryAdd(currentFrame, frame.Clone(new Rectangle(0, 0, frame.Width, frame.Height), System.Drawing.Imaging.PixelFormat.DontCare));
                }

                frame.Dispose();

                currentFrame++;
            }
        }
    }
}
