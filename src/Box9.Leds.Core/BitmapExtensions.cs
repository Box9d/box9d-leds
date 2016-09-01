using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Box9.Leds.Core.Configuration;
using Box9.Leds.Core.UpdatePixels;
using PixelMapSharp;

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

                        if (pixel != null && !string.IsNullOrEmpty(pixel.Text))
                        {
                            graphics.DrawString(pixel.Text,
                            new Font(FontFamily.GenericSansSerif, 7),
                            new SolidBrush(Color.Black),
                            new PointF(currentX + (PixelDimensions.Width * scale / 2) - 3 * pixel.Text.Length,
                                currentY + (PixelDimensions.Height * scale) / 4));
                        }

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

        public static IEnumerable<PixelInfo> CreatePixelInfo(Bitmap image, ServerConfiguration serverConfiguration)
        {
            var startX = image.Width * serverConfiguration.VideoConfiguration.StartAtXPercent / 100;
            var startY = image.Height * serverConfiguration.VideoConfiguration.StartAtYPercent / 100;
            var finishX = startX + (image.Width * serverConfiguration.VideoConfiguration.XPercent / 100);
            var finishY = startY + (image.Height * serverConfiguration.VideoConfiguration.YPercent / 100);

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
                    Color = image.GetPixel(x, y)
                };
            }
        }

        public static Bitmap Resize(Bitmap bitmap, int width, int height)
        {
            Bitmap resizedImg = new Bitmap(width, height);

            double ratioX = (double)resizedImg.Width / (double)bitmap.Width;
            double ratioY = (double)resizedImg.Height / (double)bitmap.Height;
            double ratio = ratioX < ratioY ? ratioX : ratioY;

            int newHeight = Convert.ToInt32(bitmap.Height * ratio);
            int newWidth = Convert.ToInt32(bitmap.Width * ratio);

            using (Graphics g = Graphics.FromImage(resizedImg))
            {
                g.DrawImage(bitmap, 0, 0, newWidth, newHeight);
            }

            return resizedImg;
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

        public static byte[] ToBytes(Bitmap bitmap)
        {
            var converter = new ImageConverter();
            return (byte[])converter.ConvertTo(bitmap, typeof(byte[]));
        }

        public static Bitmap FromBytes(byte[] bytes)
        {
            var converter = new ImageConverter();
            return (Bitmap)converter.ConvertTo(bytes, typeof(Bitmap));
        }
    }
}
