using System.Collections.Generic;
using System.Windows.Forms;
using Box9.Leds.Core;
using Box9.Leds.Core.LedLayouts;
using Box9.Leds.Core.UpdatePixels;

namespace Box9.Leds.Manager.Controls
{
    public class LedPanel : Panel
    {
        private LedLayout layout;

        public LedPanel(LedLayout layout)
        {
            this.layout = layout;

            this.Controls.Add(new Button
            {
                Text = "test",
                Dock = DockStyle.Bottom,
                BackColor = System.Drawing.Color.White,
                Size = new System.Drawing.Size(20, 20)
            });
        }

        public void Update(IEnumerable<PixelInfo> pixels)
        {
            this.BackgroundImage = BitmapExtensions.CreateFromPixelInfo(pixels, layout);
        }
    }
}
