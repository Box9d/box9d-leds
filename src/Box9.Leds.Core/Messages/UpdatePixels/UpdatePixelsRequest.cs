using System.Collections.Generic;
using System.Drawing;
using Box9.Leds.Core.UpdatePixels;

namespace Box9.Leds.Core.Messages.UpdatePixels
{
    public class UpdatePixelsRequest
    {
        public IEnumerable<PixelInfo> PixelUpdates { get; set; }

        public UpdatePixelsRequest()
        {
            PixelUpdates = new List<PixelInfo>();
        }
    }
}
