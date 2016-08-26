using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Box9.Leds.Core.Configuration;
using Box9.Leds.Core.UpdatePixels;

namespace Box9.Leds.Core
{
    public static class BitmapExtensions
    {
        public static Bitmap CreateFromPixelInfo(IEnumerable<PixelInfo> pixelInfo, ServerConfiguration serverConfiguration, int scale = 1)
        {
            var width = (PixelDimensions.Width + PixelDimensions.Gap) * serverConfiguration.XPixels * scale;
            var height = (PixelDimensions.Height + PixelDimensions.Gap) * serverConfiguration.YPixels * scale;

            var bitmap = new Bitmap(width, height);

            using (var graphics = Graphics.FromImage(bitmap))
            {
                var brush = new SolidBrush(Color.Black);

                int currentX = 0;
                for (int i = 0; i < serverConfiguration.XPixels; i++)
                {
                    int currentY = 0;
                    for (int j = 0; j < serverConfiguration.YPixels; j++)
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

                        graphics.FillEllipse(brush, currentX, currentY, PixelDimensions.Width * scale, PixelDimensions.Height * scale);
                        graphics.DrawString(pixel.Text, 
                            new Font(FontFamily.GenericSansSerif, 7), 
                            new SolidBrush(Color.Black), 
                            new PointF(currentX + (PixelDimensions.Width * scale / 2) - 3 * pixel.Text.Length, 
                                currentY + (PixelDimensions.Height * scale) / 4));

                        currentY += (PixelDimensions.Height + PixelDimensions.Gap) * scale;
                    }

                    currentX += (PixelDimensions.Width + PixelDimensions.Gap) * scale;
                }
            }

            return bitmap;
        }

        public static Bitmap ModifyFromPixelInfo(Bitmap existingImage, PixelInfo pixelInfo, ServerConfiguration serverConfiguration, int scale = 1)
        {
            var width = (PixelDimensions.Width + PixelDimensions.Gap) * serverConfiguration.XPixels * scale;
            var height = (PixelDimensions.Height + PixelDimensions.Gap) * serverConfiguration.YPixels * scale;

            var bitmap = existingImage;
            using (var graphics = Graphics.FromImage(existingImage))
            { 
                var currentX = pixelInfo.X * (PixelDimensions.Width + PixelDimensions.Gap) * scale;
                var currentY = pixelInfo.Y * (PixelDimensions.Height + PixelDimensions.Gap) * scale;

                var brush = new SolidBrush(pixelInfo.Color);

                graphics.FillEllipse(brush, currentX, currentY, PixelDimensions.Width * scale, PixelDimensions.Height * scale);
                graphics.DrawString(pixelInfo.Text,
                    new Font(FontFamily.GenericSansSerif, 7),
                    new SolidBrush(Color.Black),
                    new PointF(currentX + (PixelDimensions.Width * scale / 2) - 3 * pixelInfo.Text.Length,
                        currentY + (PixelDimensions.Height * scale) / 4));
            }

            return bitmap;
        }

        public static byte[] Encode(Bitmap bitmap)
        {
            using (var stream = new MemoryStream())
            {
                bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);

                return stream.ToArray();
            }
        }

        public static Bitmap Decode(byte[] bytes)
        {
            using (var stream = new MemoryStream(bytes))
            {
                return new Bitmap(Image.FromStream(stream));
            }
        }
    }
}
