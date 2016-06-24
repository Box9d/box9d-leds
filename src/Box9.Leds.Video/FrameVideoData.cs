using System.Collections.Generic;
using Box9.Leds.Core.UpdatePixels;

namespace Box9.Leds.Video
{
    public class FrameVideoData
    {
        public List<PixelInfo> PixelInfo { get; set; }

        public FrameVideoData()
        {
            PixelInfo = new List<PixelInfo>();
        }
    }
}
