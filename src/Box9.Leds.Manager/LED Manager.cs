using System;
using System.Windows.Forms;
using Box9.Leds.Manager.Controls;

namespace Box9.Leds.Manager
{
    public partial class LedManager : Form
    {
        private ServerPanel serverPanel;
        private InputPanel inputPanel;

        public LedManager()
        {
            InitializeComponent();

            this.serverPanel = new ServerPanel((2 * Width) / 3, Height);
            this.inputPanel = new InputPanel(Width / 3, Height);
        }

        private void LedManager_Load(object sender, EventArgs e)
        {
            this.Controls.Add(serverPanel);
            this.Controls.Add(inputPanel);
        }
    }
}
