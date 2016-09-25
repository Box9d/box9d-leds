using System;

namespace Box9.Leds.Business.Dtos
{
    public class VideoMetadata
    {
        public string FilePath { get; }

        public int FrameRate { get; }

        public long TotalFrames { get; }

        public int Height { get; }

        public int Width { get; }

        public TimeSpan StartTime { get; private set; }

        public TimeSpan TotalTime
        {
            get
            {
                var seconds = (int)Math.Ceiling((double)TotalFrames / (double)FrameRate);

                return new TimeSpan(0, 0, seconds);
            }
        }

        internal VideoMetadata(string filePath, int frameRate, long totalFrames, int height, int width)
        {
            FilePath = filePath;
            FrameRate = frameRate;
            TotalFrames = totalFrames;
            Height = height;
            Width = width;
            StartTime = new TimeSpan(0, 0, 0);
        }

        public void SetStartTime(int seconds)
        {
            if (seconds > TotalTime.TotalSeconds)
            {
                throw new ArgumentException("Cannot set the start time greater than the total time");
            }

            StartTime = new TimeSpan(0, 0, seconds);
        }
    }
}
