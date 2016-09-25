using System.Collections.Generic;
using Box9.Leds.Core.EventArgs;
using Box9.Leds.Core.UpdatePixels;

namespace Box9.Leds.Business.EventArgs
{
    public class LedMappingEventArgs : CustomTypeEventArgs<IEnumerable<PixelInfo>>
    {
        public LedMappingEventArgs(IEnumerable<PixelInfo> pixelMappings) : base(pixelMappings)
        {
        }
    }
}
