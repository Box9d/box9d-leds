using System.Drawing;
using System.Windows.Forms;
using Box9.Leds.Core.LedLayouts;

namespace Box9.Leds.Manager.Controls
{
    public class ServerPanel : Panel
    {
        public ServerPanel(int width, int height)
        {
            this.Height = height;
            this.Width = width;

            this.BackColor = Color.Gray;
            this.Dock = DockStyle.Right;
        }
    }
}
