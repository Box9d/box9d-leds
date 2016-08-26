using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Box9.Leds.Core;
using Box9.Leds.Core.Configuration;
using Box9.Leds.Core.Coordination;
using Box9.Leds.Core.UpdatePixels;
using Box9.Leds.Manager.Controls;

namespace Box9.Leds.Manager.Forms
{
    public partial class ConfigureLedMappingForm : Form
    {
        private readonly int xPixels;
        private readonly int yPixels;

        private Dictionary<int, PixelInfo> mappedPixels;
        private int scale;
        private bool enableDraw;

        public delegate void OnConfigurationConfirmed(ConfigureLedMappingForm form, Dictionary<int, PixelInfo> pixelMappings);
        public event OnConfigurationConfirmed ConfigurationConfirmed;

        public ConfigureLedMappingForm(int xPixels, int yPixels, List<PixelMapping> initialPixelMappings = null)
        {
            InitializeComponent();

            this.xPixels = xPixels;
            this.yPixels = yPixels;

            scale = 2;
            enableDraw = false;

            this.mappedPixels = new Dictionary<int, PixelInfo>();

            if (initialPixelMappings != null || initialPixelMappings.Any())
            {
                foreach (var pixelMapping in initialPixelMappings)
                {
                    mappedPixels.Add(pixelMapping.Order, new PixelInfo { X = pixelMapping.X, Y = pixelMapping.Y, Color = Color.White, Text = pixelMapping.Order.ToString() });
                }
            }

            ConfigurationConfirmed += (form, pixelMappings) => { };
        }

        private void ServerForm_Load(object sender, System.EventArgs e)
        {
            var clientWidth = xPixels * scale * (PixelDimensions.Width + PixelDimensions.Gap);
            var clientHeight = yPixels * scale * (PixelDimensions.Height + PixelDimensions.Gap);

            var clientSize = new Size(clientWidth, clientHeight + this.ClientSize.Height - this.displayPanel.Height);
            this.ClientSize = clientSize;

            this.ToggleDraw(false);

            this.Text = "LED mapping";

            this.displayPanel.Dock = DockStyle.Top;
            this.displayPanel.Left = this.ClientRectangle.Left;
            this.displayPanel.Top = this.ClientRectangle.Top;

            var pixels = GeneratePixels(xPixels, yPixels, Color.DarkSlateBlue, Color.White, mappedPixels.Select(mp => mp.Value));
            var panelBitmap = BitmapExtensions.CreateFromPixelInfo(pixels, new ServerConfiguration
            {
                XPixels = xPixels,
                YPixels = yPixels
            }, scale);

            this.displayPanel.SetAutoScrollMargin(800, 600);
            this.displayPanel.BackgroundImageLayout = ImageLayout.None;
            this.displayPanel.BackgroundImage = panelBitmap;
            this.displayPanel.Height = panelBitmap.Height;
            this.displayPanel.Width = panelBitmap.Width;

            this.displayPanel.MouseClick += (s, args) =>
            {
                ToggleDraw(!enableDraw);
            };
            this.displayPanel.MouseMove += (s, args) =>
            {
                this.displayPanel.Cursor = enableDraw
                    ? Cursors.Cross
                    : Cursors.Arrow;

                if (enableDraw)
                {
                    var pixelInfo = MouseOverLed(args);
                    if (pixelInfo != null && !mappedPixels.Any(mp => mp.Value.X == pixelInfo.X && mp.Value.Y == pixelInfo.Y))
                    {
                        MapPixel(pixelInfo, mappedPixels.Count + 1);
                    }
                }
            };
            this.displayPanel.PreviewKeyDown += (s, args) =>
            {
                if (args.Control && args.KeyCode == Keys.Z)
                {
                    buttonUndo_Click(null, null);
                }
            };
        }

        private void MapPixel(PixelInfo pixel, int order)
        {
            Invoke(new Action(() =>
            {
                mappedPixels.Add(order, pixel);

                displayPanel.BackgroundImage = BitmapExtensions.ModifyFromPixelInfo((Bitmap)displayPanel.BackgroundImage,
                    pixel,
                    new ServerConfiguration { XPixels = xPixels, YPixels = yPixels }, 
                    scale);

                displayPanel.Refresh();
                displayPanel.Focus();
            }));
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
            Invoke(new Action(() =>
            {
                if (mappedPixels.Any())
                {
                    var existingPixels = new List<PixelInfo>(mappedPixels.Select(mp => mp.Value));
                    var removedPixel = mappedPixels[mappedPixels.Count];
                    mappedPixels.Remove(mappedPixels.Count);

                    removedPixel.Color = Color.DarkSlateBlue;
                    removedPixel.Text = string.Empty;

                    displayPanel.BackgroundImage = BitmapExtensions.ModifyFromPixelInfo((Bitmap)displayPanel.BackgroundImage,
                        removedPixel,
                        new ServerConfiguration { XPixels = xPixels, YPixels = yPixels },
                        scale);

                    displayPanel.Refresh();
                    displayPanel.Focus();
                }
            }));
        }

        private void RefreshBackgroundImage(Bitmap image)
        {
            displayPanel.BackgroundImage = image;
            displayPanel.Refresh();
            displayPanel.Focus();
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

        private IEnumerable<PixelInfo> GeneratePixels(int xPixels, int yPixels, Color primaryColor, Color secondaryColor, IEnumerable<PixelInfo> secondaryColorPixels = null)
        {
            for (int i = 0; i < xPixels; i++)
            {
                for (int j = 0; j < yPixels; j++)
                {
                    var secondaryColorPixel = secondaryColorPixels != null
                        ? secondaryColorPixels.SingleOrDefault(t => t.X == i && t.Y == j)
                        : null;

                    var pixel = new PixelInfo
                    {
                        Color = secondaryColorPixel != null
                            ? secondaryColor
                            : primaryColor,
                        X = i,
                        Y = j,
                        Text = secondaryColorPixel != null 
                            ? secondaryColorPixel.Text
                            : string.Empty
                    };

                    yield return pixel;
                }
            }
        }

        private PixelInfo MouseOverLed(MouseEventArgs args)
        {
            var xLedWidth = PixelDimensions.Width * scale;
            var yLedWidth = PixelDimensions.Height * scale;
            var ledGap = PixelDimensions.Gap * scale;

            var closestXLed = args.Location.X / (xLedWidth + ledGap);
            var closestYLed = args.Location.Y / (yLedWidth + ledGap);

            if (args.Location.X >= closestXLed * (xLedWidth + ledGap) 
                && args.Location.X <= closestXLed * (xLedWidth + ledGap) + xLedWidth
                && args.Location.Y >= closestYLed * (yLedWidth + ledGap)
                && args.Location.Y <= closestYLed * (yLedWidth + ledGap) + yLedWidth)
            {
                return new PixelInfo
                {
                     X = closestXLed,
                     Y = closestYLed,
                     Color = Color.White,
                     Text = (mappedPixels.Count + 1).ToString()
                };
            }

            return null;
        }
    } 
}
