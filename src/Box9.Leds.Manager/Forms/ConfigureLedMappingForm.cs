using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Box9.Leds.Core;
using Box9.Leds.Core.Coordination;
using Box9.Leds.Manager.Controls;

namespace Box9.Leds.Manager.Forms
{
    public partial class ConfigureLedMappingForm : Form
    {
        private readonly int xPixels;
        private readonly int yPixels;

        private List<PixelMapping> initialPixelMappings;
        private Dictionary<int, PixelPanel> mappedPixels;
        private int scale;
        private bool enableDraw;

        public delegate void OnConfigurationConfirmed(ConfigureLedMappingForm form, Dictionary<int, PixelPanel> pixelMappings);
        public event OnConfigurationConfirmed ConfigurationConfirmed;

        public ConfigureLedMappingForm(int xPixels, int yPixels, List<PixelMapping> initialPixelMappings = null)
        {
            InitializeComponent();

            this.xPixels = xPixels;
            this.yPixels = yPixels;

            scale = 2;
            enableDraw = false;

            this.mappedPixels = new Dictionary<int, PixelPanel>();
            this.initialPixelMappings = new List<PixelMapping>();

            if (initialPixelMappings != null || initialPixelMappings.Any())
            {
                this.initialPixelMappings = initialPixelMappings;
            }

            ConfigurationConfirmed += (form, pixelMappings) => { };
        }

        private void ServerForm_Load(object sender, System.EventArgs e)
        {
            var clientWidth = xPixels * scale * (PixelDimensions.Width + PixelDimensions.Gap);
            var clientHeight = yPixels * scale * (PixelDimensions.Height + PixelDimensions.Gap);

            this.ClientSize = new Size(clientWidth, clientHeight + this.ClientSize.Height - this.displayPanel.Height);

            this.ToggleDraw(false);

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

                    var pixelMapping = initialPixelMappings.SingleOrDefault(p => p.X == i && p.Y == j);
                    if (pixelMapping != null)
                    {
                        MapPixel(pixel, pixelMapping.Order);
                    }

                    pixel.MouseEnter += (s, args) =>
                    {
                        if (enableDraw && !mappedPixels.ContainsValue(pixel))
                        {
                            MapPixel(pixel, mappedPixels.Count + 1);
                        }
                    };

                    pixel.MouseMove += (s, arg) =>
                    {
                        pixel.Cursor = enableDraw
                            ? Cursors.Cross
                            : Cursors.Arrow;
                    };

                    pixel.MouseClick += (s, args) =>
                    {
                        ToggleDraw(!enableDraw);
                    };

                    this.displayPanel.Controls.Add(pixel);
                }
            }

            this.displayPanel.MouseClick += (s, args) =>
            {
                ToggleDraw(!enableDraw);
            };

            this.displayPanel.MouseMove += (s, args) =>
            {
                this.displayPanel.Cursor = enableDraw
                    ? Cursors.Cross
                    : Cursors.Arrow;
            };

            this.displayPanel.PreviewKeyDown += (s, args) =>
            {
                if (args.Control && args.KeyCode == Keys.Z)
                {
                    buttonUndo_Click(null, null);
                }
            };
        }

        private void MapPixel(PixelPanel pixel, int order)
        {
            Invoke(new Action(() =>
            {
                mappedPixels.Add(order, pixel);
            }));

            pixel.UpdateColor(Color.White);
            pixel.UpdateText(Color.Black, mappedPixels.Count.ToString());
            pixel.Refresh();

            displayPanel.Focus();
        }

        private void ToggleDraw(bool enabled)
        {
            enableDraw = enabled;

            if (enabled)
            {
                this.toolStripStatusLabel.Text = "Draw mode enabled...";
            }
            else
            {
                this.toolStripStatusLabel.Text = "Click to start mapping pixels";
            }
        }

        private void buttonUndo_Click(object sender, System.EventArgs e)
        {
            if (mappedPixels.Any())
            {
                var removedPixel = mappedPixels[mappedPixels.Count];
                removedPixel.UpdateColor(Color.DarkSlateBlue);
                removedPixel.UpdateText(Color.Black, string.Empty);
                removedPixel.Refresh();

                mappedPixels.Remove(mappedPixels.Count);
            }
        }

        private void buttonClear_Click(object sender, System.EventArgs e)
        {
            while (mappedPixels.Any())
            {
                buttonUndo_Click(null, null);
            }
        }

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            ConfigurationConfirmed(this, mappedPixels);
        }
    }
}
