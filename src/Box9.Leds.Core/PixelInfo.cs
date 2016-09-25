using System.Drawing;

namespace Box9.Leds.Core.UpdatePixels
{
    public class PixelInfo
    {
        public int X { get; set; }

        public int Y { get; set; }

        public int Order { get; set; }

        public Color Color { get; set; }

        public PixelInfo()
        {
            Color = Color.Black;
            Order = 0;
            X = 0;
            Y = 0;
        }

        public override bool Equals(object obj)
        {
            if (obj is PixelInfo)
            {
                var compare = (PixelInfo)obj;

                return compare.Color == Color
                    && compare.X == X
                    && compare.Y == Y
                    && compare.Order == Order;
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Color.R * 3 + Color.G * 5 + Color.B * 17 + X * 7 + Y * 11 + Order * 23;
        }
    }
}
