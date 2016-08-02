using System;
using System.Drawing;
using System.Windows.Forms;

namespace Box9.Leds.Manager.Controls
{
    public class PixelPanel : Panel
    {
        private Color fillColor;
        private Color textColor;
        private string text;
        public int XCoordinate { get; private set; }
        public int YCoordinate { get; private set; }

        public PixelPanel(Color fillColor, int xCoordinate, int yCoordinate)
        {
            this.fillColor = fillColor;
            text = string.Empty;
            textColor = Color.Black;

            XCoordinate = xCoordinate;
            YCoordinate = yCoordinate;
       }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.FillEllipse(new SolidBrush(fillColor), 0, 0, this.Width - 1, this.Height - 1);
            g.DrawString(text, new Font(FontFamily.GenericSansSerif, 7), new SolidBrush(textColor), new PointF((this.Width / 2) - 3 * text.Length, this.Height / 4));
        }

        protected override void OnResize(EventArgs e)
        {
            this.Width = this.Height;
            base.OnResize(e);
        }

        public void UpdateColor(Color color)
        {
            fillColor = color;
        }

        public void UpdateText(Color textColor, string text)
        {
            this.text = text;
        }
    }
}
