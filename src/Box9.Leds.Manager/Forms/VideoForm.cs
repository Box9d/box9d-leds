using System.Drawing;
using System.Windows.Forms;

namespace Box9.Leds.Manager.Forms
{
    public partial class VideoForm : Form
    {
        public Panel DisplayPanel { get { return displayPanel; } }

        public VideoForm()
        {
            InitializeComponent();
        }

        private void ServerForm_Load(object sender, System.EventArgs e)
        {
            var clientWidth = 720;
            var clientHeight = 480;

            this.ClientSize = new Size(clientWidth, clientHeight);

            this.Text = "Video display";

            this.displayPanel.Height = this.ClientRectangle.Height;
            this.displayPanel.Width = this.ClientRectangle.Width;
            this.displayPanel.Left = this.ClientRectangle.Left;
            this.displayPanel.Top = this.ClientRectangle.Top;
            this.displayPanel.Anchor = AnchorStyles.Top;
        }
    }
}
