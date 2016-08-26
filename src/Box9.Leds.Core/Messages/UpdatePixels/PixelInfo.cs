using System.Drawing;

namespace Box9.Leds.Core.UpdatePixels
{
    public class PixelInfo
    {
        private string text;

        public int X { get; set; }

        public int Y { get; set; }

        public Color Color { get; set; }

        public string Text
        {
            get
            {
                return text == null
                    ? string.Empty
                    : text;
            }
            set
            {
                text = value;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is PixelInfo)
            {
                var compare = (PixelInfo)obj;

                return compare.Color == this.Color
                    && compare.Text == this.Text
                    && compare.X == this.X
                    && compare.Y == this.Y;
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Color.R * 3 + Color.G * 5 + Color.B * 17 + Text.GetHashCode() + X * 7 + Y * 11;
        }
    }
}
