using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Box9.Leds.Business.Service;
using Box9.Leds.Business.Services;
using Box9.Leds.Core;
using Box9.Leds.Core.UpdatePixels;
using Box9.Leds.Manager.Views;

namespace Box9.Leds.Manager.Presenters
{
    public class ConfigureLedMappingPresenter : PresenterBase<IEnumerable<PixelInfo>>
    {
        private readonly IConfigureLedMappingView view;
        private readonly IPatternCreationService patternService;
        private readonly int xPixels;
        private readonly int yPixels;

        private List<PixelInfo> pixelMappings;
        private bool canDraw;

        public ConfigureLedMappingPresenter(IConfigureLedMappingView view, int xPixels, int yPixels, List<PixelInfo> initialPixelMappings = null)
        {
            this.view = view;
            this.xPixels = xPixels;
            this.yPixels = yPixels;
            patternService = new PatternCreationService();

            canDraw = false;

            if (initialPixelMappings != null)
            {
                pixelMappings = initialPixelMappings;
            }
            else
            {
                InitializeMappings(null, null);
            }

            this.view.ReadyToDraw += EnableDraw;
            this.view.StopDrawing += DisableDraw;
            this.view.MousePositionChanged += DetermineMapping;
            this.view.Undo += Undo;
            this.view.Clear += InitializeMappings;
            this.view.Confirm += (s, a) =>
            {
                FinishPresenting(pixelMappings);
            };
        }

        public void Load()
        {
            RedrawBitmap();
        }

        public void Undo(object sender, EventArgs e)
        {
            var mapping = pixelMappings
                .OrderByDescending(pi => pi.Order)
                .FirstOrDefault();

            if (mapping != null)
            {
                var index = pixelMappings.IndexOf(mapping);
                pixelMappings.Remove(mapping);

                mapping.Color = Color.Black;
                mapping.Order = 0;
                pixelMappings.Insert(index, mapping);
            }

            RedrawBitmap();
        }

        public void EnableDraw(object sender, MouseEventArgs e)
        {
            canDraw = true;
        }

        public void DisableDraw(object sender, MouseEventArgs e)
        {
            canDraw = false;
        }

        public void DetermineMapping(object sender, MouseEventArgs e)
        {
            if (!canDraw)
            {
                return;
            }

            var xLedWidth = PixelDimensions.Width;
            var yLedWidth = PixelDimensions.Height;
            var ledGap = PixelDimensions.Gap;

            var closestXLed = e.Location.X / (xLedWidth + ledGap);
            var closestYLed = e.Location.Y / (yLedWidth + ledGap);
            var mapping = pixelMappings.SingleOrDefault(pm => pm.Order <= 0
                    && pm.X == closestXLed
                    && pm.Y == closestYLed);

            if (e.Location.X >= closestXLed * (xLedWidth + ledGap)
                && e.Location.X <= closestXLed * (xLedWidth + ledGap) + xLedWidth
                && e.Location.Y >= closestYLed * (yLedWidth + ledGap)
                && e.Location.Y <= closestYLed * (yLedWidth + ledGap) + yLedWidth
                && mapping != null)
            {
                var index = pixelMappings.IndexOf(mapping);
                pixelMappings.Remove(mapping);

                mapping.Order = pixelMappings.Max(pm => pm.Order) + 1;
                mapping.Color = Color.White;
                pixelMappings.Insert(index, mapping);

                RedrawBitmap();
            }
        }

        private void RedrawBitmap()
        {
            view.Image = patternService.CreateFromPixelInfo(pixelMappings);

            MarkAsDirty();
        }

        private void InitializeMappings(object sender, EventArgs args)
        {
            pixelMappings = new List<PixelInfo>();

            for (int i = 0; i < xPixels; i++)
            {
                for (int j = 0; j < yPixels; j++)
                {
                    pixelMappings.Add(new PixelInfo
                    {
                        X = i,
                        Y = j,
                    });
                }
            }

            RedrawBitmap();
        }
    }
}
