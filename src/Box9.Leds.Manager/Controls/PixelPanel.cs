using System;
using System.Drawing;
using System.Windows.Forms;

namespace Box9.Leds.Manager.Controls
{
    public class PixelPanel : Panel
    {
        private Color fillColor;
        public int XCoordinate { get; private set; }
        public int YCoordinate { get; private set; }

        public PixelPanel(Color fillColor, int xCoordinate, int yCoordinate)
        {
            this.fillColor = fillColor;

            XCoordinate = xCoordinate;
            YCoordinate = yCoordinate;
       }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.FillEllipse(new SolidBrush(fillColor), 0, 0, this.Width - 1, this.Height - 1);
        }

        protected override void OnResize(EventArgs e)
        {
            this.Width = this.Height;
            base.OnResize(e);
        }

        public void UpdateColor(Color color)
        {
            this.fillColor = color;

            Refresh();
        }
    }
}
