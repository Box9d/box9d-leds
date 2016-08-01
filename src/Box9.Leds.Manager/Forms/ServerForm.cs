using System.Drawing;
using System.Windows.Forms;
using Box9.Leds.Core;
using Box9.Leds.Core.Configuration;
using Box9.Leds.FcClient;

namespace Box9.Leds.Manager.Forms
{
    public partial class ServerForm : Form
    {
        private readonly ServerConfiguration serverConfiguration;

        public Panel DisplayPanel { get { return displayPanel; } }

        public ServerForm(ServerConfiguration serverConfiguration)
        {
            InitializeComponent();

            this.serverConfiguration = serverConfiguration;
        }

        private void ServerForm_Load(object sender, System.EventArgs e)
        {
            var clientWidth = serverConfiguration.XPixels * (PixelDimensions.Width + PixelDimensions.Gap);
            var clientHeight = serverConfiguration.YPixels * (PixelDimensions.Height + PixelDimensions.Gap);

            this.ClientSize = new Size(clientWidth, clientHeight);

            this.Text = serverConfiguration.ToString();

            this.displayPanel.Height = this.ClientRectangle.Height;
            this.displayPanel.Width = this.ClientRectangle.Width;
            this.displayPanel.Left = this.ClientRectangle.Left;
            this.displayPanel.Top = this.ClientRectangle.Top;
            this.displayPanel.Anchor = AnchorStyles.Top;
        }
    }
}
