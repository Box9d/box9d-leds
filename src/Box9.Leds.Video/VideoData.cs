using System.Collections.Generic;
using Box9.Leds.Core.UpdatePixels;

namespace Box9.Leds.Video
{
    public class VideoData
    {
        public Dictionary<int, IEnumerable<PixelInfo>> Frames { get; set; }

        public int Framerate { get; set; }

        public VideoData()
        {
            Frames = new Dictionary<int, IEnumerable<PixelInfo>>();
        }
    }
}
