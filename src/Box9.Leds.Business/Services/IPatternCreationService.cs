using System.Collections.Generic;
using System.Drawing;
using Box9.Leds.Business.Configuration;
using Box9.Leds.Core.UpdatePixels;

namespace Box9.Leds.Business.Service
{
    public interface IPatternCreationService
    {
        IEnumerable<PixelInfo> AllPixelsOff(ServerConfiguration serverConfiguration);

        IEnumerable<PixelInfo> FromBitmap(Bitmap image, ServerConfiguration serverConfiguration);

        Bitmap CreateFromPixelInfo(IEnumerable<PixelInfo> pixelInfo);

        Bitmap ModifyFromPixelInfo(Bitmap existingImage, PixelInfo pixelInfo, ServerConfiguration serverConfiguration);
    }
}
