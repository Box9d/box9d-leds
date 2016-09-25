using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Box9.Leds.Business.Configuration;
using Box9.Leds.Business.Service;
using Box9.Leds.Core;
using Box9.Leds.Core.UpdatePixels;

namespace Box9.Leds.Business.Services
{
    public class PatternCreationService : IPatternCreationService
    {
        public IEnumerable<PixelInfo> AllPixelsOff(ServerConfiguration serverConfiguration)
        {
            var pixelUpdates = new List<PixelInfo>();
            for (int i = 0; i < serverConfiguration.XPixels * serverConfiguration.YPixels; i++)
            {
                yield return new PixelInfo
                {
                    Color = Color.Black
                };
            }
        }

        public IEnumerable<PixelInfo> FromBitmap(Bitmap image, ServerConfiguration serverConfiguration)
        {
            foreach (var pixelMapping in serverConfiguration.PixelMappings.OrderBy(pm => pm.Order))
            {
                var xPercent = serverConfiguration.VideoConfiguration.StartAtXPercent + serverConfiguration.VideoConfiguration.XPercent * (pixelMapping.X + 1) / serverConfiguration.XPixels;
                var yPercent = serverConfiguration.VideoConfiguration.StartAtYPercent + serverConfiguration.VideoConfiguration.YPercent * (pixelMapping.Y + 1) / serverConfiguration.YPixels;

                var x = (xPercent * image.Width) / 100;
                var y = (yPercent * image.Height) / 100;

                x = x >= image.Width ? image.Width - 1 : x;
                y = y >= image.Height ? image.Height - 1 : y;

                yield return new PixelInfo
                {
                    X = pixelMapping.X,
                    Y = pixelMapping.Y,
                    Color = image.GetPixel(x, y)
                };
            }
        }

        public Bitmap CreateFromPixelInfo(IEnumerable<PixelInfo> pixelInfo)
        {
            var xPixels = pixelInfo.Max(pi => pi.X);
            var yPixels = pixelInfo.Max(pi => pi.Y);
            var width = (PixelDimensions.Width + PixelDimensions.Gap) * xPixels;
            var height = (PixelDimensions.Height + PixelDimensions.Gap) * yPixels;

            var bitmap = new Bitmap(width, height);

            using (var graphics = Graphics.FromImage(bitmap))
            {
                var brush = new SolidBrush(Color.Black);

                int currentX = 0;
                for (int i = 0; i < xPixels; i++)
                {
                    int currentY = 0;
                    for (int j = 0; j < yPixels; j++)
                    {
                        var pixel = pixelInfo.SingleOrDefault(p => p.X == i && p.Y == j);

                        if (pixel != null)
                        {
                            brush = new SolidBrush(pixel.Color);
                        }
                        else
                        {
                            brush = new SolidBrush(Color.Black);
                        }

                        graphics.FillEllipse(brush, currentX, currentY, PixelDimensions.Width, PixelDimensions.Height);

                        if (pixel.Order > 0)
                        {
                            graphics.DrawString(pixel.Order.ToString(),
                            new Font(FontFamily.GenericSansSerif, 7),
                            new SolidBrush(Color.Black),
                            new PointF(currentX + (PixelDimensions.Width / 2) - 3 * pixel.Order.ToString().Length,
                                currentY + (PixelDimensions.Height) / 4));
                        }

                        currentY += (PixelDimensions.Height + PixelDimensions.Gap);
                    }

                    currentX += (PixelDimensions.Width + PixelDimensions.Gap);
                }
            }

            return bitmap;
        }

        public Bitmap ModifyFromPixelInfo(Bitmap existingImage, PixelInfo pixelInfo, ServerConfiguration serverConfiguration)
        {
            var width = (PixelDimensions.Width + PixelDimensions.Gap) * serverConfiguration.XPixels;
            var height = (PixelDimensions.Height + PixelDimensions.Gap) * serverConfiguration.YPixels;

            var bitmap = existingImage;
            using (var graphics = Graphics.FromImage(existingImage))
            {
                var currentX = pixelInfo.X * (PixelDimensions.Width + PixelDimensions.Gap);
                var currentY = pixelInfo.Y * (PixelDimensions.Height + PixelDimensions.Gap);

                var brush = new SolidBrush(pixelInfo.Color);

                graphics.FillEllipse(brush, currentX, currentY, PixelDimensions.Width, PixelDimensions.Height);
            }

            return bitmap;
        }
    }
}
