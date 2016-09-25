using System;
using Box9.Leds.Core.UpdatePixels;

namespace Box9.Leds.Core.EventArgs
{
    public class PixelMappingEventArgs : CustomTypeEventArgs<Tuple<int, PixelInfo>>
    {
        public PixelMappingEventArgs(Tuple<int, PixelInfo> values) : base(values)
        {
        }
    }
}
