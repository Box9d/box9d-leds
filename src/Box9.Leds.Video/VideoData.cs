using System.Collections.Generic;

namespace Box9.Leds.Video
{
    public class VideoData
    {
        public IEnumerable<int> FrameStorageKeys { get; set; }

        public long TotalNumberOfFrames { get; set; }

        public int FramesPerStorageKey { get; set; }

        public int Framerate { get; set; }

        public VideoData()
        {
            FrameStorageKeys = new List<int>();
        }
    }
}
