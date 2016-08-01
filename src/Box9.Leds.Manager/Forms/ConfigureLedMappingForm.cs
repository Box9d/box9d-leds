using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Box9.Leds.Core;
using Box9.Leds.Core.Configuration;
using Box9.Leds.FcClient;
using Box9.Leds.Manager.Controls;

namespace Box9.Leds.Manager.Forms
{
    public partial class ConfigureLedMappingForm : Form
    {
        private readonly int xPixels;
        private readonly int yPixels;

        private Dictionary<int, PixelPanel> mappedPixels;
        private int scale;
        private bool enableDraw;

        public ConfigureLedMappingForm(int xPixels, int yPixels)
        {
            InitializeComponent();

            this.xPixels = xPixels;
            this.yPixels = yPixels;

            scale = 2;
            enableDraw = false;

            this.mappedPixels = new Dictionary<int, PixelPanel>();
        }

        private void ServerForm_Load(object sender, System.EventArgs e)
        {
            var clientWidth = xPixels * scale * (PixelDimensions.Width + PixelDimensions.Gap);
            var clientHeight = yPixels * scale * (PixelDimensions.Height + PixelDimensions.Gap);

            this.ClientSize = new Size(clientWidth, clientHeight + this.ClientSize.Height - this.displayPanel.Height);

            this.Text = "LED mapping";

            this.displayPanel.Height = this.ClientRectangle.Height + this.displayPanel.Height;
            this.displayPanel.Width = this.ClientRectangle.Width;
            this.displayPanel.Left = this.ClientRectangle.Left;
            this.displayPanel.Top = this.ClientRectangle.Top;
            this.displayPanel.Anchor = AnchorStyles.Top;

            for (int i = 0; i < xPixels; i++)
            {
                for (int j = 0; j < yPixels; j++)
                {
                    var pixel = new PixelPanel(Color.DarkSlateBlue, i, j)
                    {
                        Width = PixelDimensions.Width * scale,
                        Height = PixelDimensions.Height * scale,
                        Left = displayPanel.ClientRectangle.Left + i * scale * (PixelDimensions.Width + PixelDimensions.Gap),
                        Top = displayPanel.ClientRectangle.Top + j * scale * (PixelDimensions.Height + PixelDimensions.Gap),
                    };

                    pixel.MouseEnter += (s, args) =>
                    {
                        if (enableDraw)
                        {
                            mappedPixels.Add(mappedPixels.Count + 1, pixel);
                            pixel.UpdateColor(Color.White);
                        }
                    };

                    pixel.MouseClick += (s, args) =>
                    {
                        enableDraw = !enableDraw;
                    };

                    this.displayPanel.Controls.Add(pixel);
                }
            }

            this.displayPanel.MouseClick += (s, args) =>
            {
                enableDraw = !enableDraw;
            };
        }
    }
}
