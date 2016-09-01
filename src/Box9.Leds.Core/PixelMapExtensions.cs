using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Box9.Leds.Core.Configuration;
using Box9.Leds.Core.UpdatePixels;
using PixelMapSharp;

namespace Box9.Leds.Core
{
    public static class PixelMapExtensions
    {
        public static IEnumerable<PixelInfo> CreatePixelInfo(this PixelMap map, ServerConfiguration serverConfiguration)
        {
            var startX = map.Width * serverConfiguration.VideoConfiguration.StartAtXPercent / 100;
            var startY = map.Height * serverConfiguration.VideoConfiguration.StartAtYPercent / 100;
            var finishX = startX + (map.Width * serverConfiguration.VideoConfiguration.XPercent / 100);
            var finishY = startY + (map.Height * serverConfiguration.VideoConfiguration.YPercent / 100);

            var xPixelGap = (finishX - startX) / serverConfiguration.XPixels;
            var yPixelGap = (finishY - startY) / serverConfiguration.YPixels;

            foreach (var pixelMapping in serverConfiguration.PixelMappings.OrderBy(pm => pm.Order))
            {
                var x = startX + (pixelMapping.X * xPixelGap);
                var y = startY + (pixelMapping.Y * yPixelGap);

                x = x >= finishX ? finishX - 1 : x;
                y = y >= finishY ? finishY - 1 : y;

                yield return new PixelInfo
                {
                    X = pixelMapping.X,
                    Y = pixelMapping.Y,
                    Color = map[x, y].Color
                };
            }
        }
    }
}
