using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Box9.Leds.Core.LedLayouts;
using Box9.Leds.Core.UpdatePixels;

namespace Box9.Leds.Core
{
    public static class BitmapExtensions
    {
        public static Bitmap CreateFromPixelInfo(IEnumerable<PixelInfo> pixelInfo, LedLayout layout)
        {
            var width = (PixelDimensions.Width + PixelDimensions.Gap) * layout.XNumberOfPixels;
            var height = (PixelDimensions.Height + PixelDimensions.Gap) * layout.YNumberOfPixels;

            var bitmap = new Bitmap(width, height);

            using (var graphics = Graphics.FromImage(bitmap))
            {
                var brush = new SolidBrush(Color.Black);

                int currentX = 0;
                for (int i = 0; i < layout.XNumberOfPixels; i++)
                {
                    int currentY = 0;
                    for (int j = 0; j < layout.YNumberOfPixels; j++)
                    {
                        var pixel = pixelInfo.SingleOrDefault(p => p.X == i && p.Y == j);

                        if (pixel != null)
                        {
                            brush = new SolidBrush(pixel.Color);
                        }

                        graphics.FillEllipse(brush, currentX, currentY, PixelDimensions.Width, PixelDimensions.Height);

                        currentY += (PixelDimensions.Height + PixelDimensions.Gap);
                    }

                    currentX += PixelDimensions.Width + PixelDimensions.Gap;
                }
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
