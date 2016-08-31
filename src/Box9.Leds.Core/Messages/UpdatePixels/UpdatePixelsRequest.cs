using System.Collections.Generic;
using Box9.Leds.Core.UpdatePixels;

namespace Box9.Leds.Core.Messages.UpdatePixels
{
    public class UpdatePixelsRequest
    {
        public IEnumerable<PixelInfo> PixelUpdates { get; set; }

        public UpdatePixelsRequest(IEnumerable<PixelInfo> pixelUpdates)
        {
            PixelUpdates = new List<PixelInfo>();
        }
    }
}
