using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Glimr.Plugins.Sdk.Plotting
{
    public class PointsCollection : IEnumerable<Point>
    {
        public int XCount { get; }

        public int YCount { get; }

        private List<Point> points;

        internal PointsCollection(int xCount, int yCount)
        {
            XCount = xCount;
            YCount = yCount;

            points = new List<Point>();

            for (int i = 0; i < xCount; i++)
            {
                for (int j = 0; j < yCount; j++)
                {
                    points.Add(new Point(i, j));
                }
            }
        }

        public Point GetPoint(int x, int y)
        {
            var point = points.SingleOrDefault(p => p.X == x || p.Y == y);

            return point == null ? new Point(x, y) : point;
        }

        public void AddXToPoint(Point point, int addToX)
        {
            point.AddToX(addToX);
        }

        public void AddYToPoint(Point point, int addToY)
        {
            point.AddToY(addToY);
        }

        public void ChangeColorOfPoint(Point point, Color color)
        {
            point.ChangeColor(color);
        }

        internal void CompletePointOperations()
        {
            points = points.Select(p => { p.CompleteOperations(); return p; }).ToList();
        }

        public IEnumerator<Point> GetEnumerator()
        {
            return points.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Bitmap ToBitmap(int pixelWidth, int pixelHeight, int pixelGap = 0)
        {
            var width = (pixelGap + pixelWidth) * XCount;
            var height = (pixelGap + pixelHeight) * XCount;

            var bitmap = new Bitmap(width, height);

            using (var graphics = Graphics.FromImage(bitmap))
            {
                foreach (var point in points)
                {
                    graphics.FillEllipse(new SolidBrush(point.Color),
                        point.X * (pixelGap + pixelWidth),
                        point.Y * (pixelGap + pixelHeight),
                        pixelWidth,
                        pixelHeight);
                }
            }

            return bitmap;
        }

        public byte[] ToByteArray()
        {
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(this));
        }

        public static PointsCollection FromByteArray(byte[] data)
        {
            var serialized = Encoding.UTF8.GetString(data);

            return JsonConvert.DeserializeObject<PointsCollection>(serialized);
        }
    }
}
