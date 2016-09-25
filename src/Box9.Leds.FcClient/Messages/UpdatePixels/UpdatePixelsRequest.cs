using System.Collections.Generic;
using Box9.Leds.Core.UpdatePixels;

namespace Box9.Leds.FcClient.Messages.UpdatePixels
{
    public class UpdatePixelsRequest
    {
        private object p;

        public IEnumerable<PixelInfo> PixelUpdates { get; set; }

        public UpdatePixelsRequest(IEnumerable<PixelInfo> pixelUpdates)
        {
            PixelUpdates = pixelUpdates;
        }

        public UpdatePixelsRequest(object p)
        {
            this.p = p;
        }
    }
}
