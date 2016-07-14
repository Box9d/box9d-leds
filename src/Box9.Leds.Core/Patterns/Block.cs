using System.Collections.Generic;
using System.Drawing;
using Box9.Leds.Core.Configuration;
using Box9.Leds.Core.UpdatePixels;

namespace Box9.Leds.Core.Patterns
{
    public class Block
    {
        public static IEnumerable<PixelInfo> GeneratePattern(Color blockColor, ServerConfiguration serverConfig, int width, int height, int startX, int startY)
        {
            for (int i = 0; i < serverConfig.XPixels; i++)
            {
                for (int j = 0; j < serverConfig.YPixels; j++)
                {
                    var pixelInfo = new PixelInfo
                    {
                        X = i,
                        Y = j,
                    };

                    if (startX < i  
                        && i < startX + width
                        && startY < j
                        && j < startY + height)
                    {
                        pixelInfo.Color = blockColor;
                    }
                    else
                    {
                        pixelInfo.Color = Color.Black;
                    }

                    yield return pixelInfo;
                }
            }
        }
    }
}
