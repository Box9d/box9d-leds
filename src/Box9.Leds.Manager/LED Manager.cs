using System;
using System.Windows.Forms;
using Box9.Leds.Core.LedLayouts;
using Box9.Leds.Core.Servers;
using Box9.Leds.Manager.Forms;

namespace Box9.Leds.Manager
{
    public partial class LedManager : Form
    {
        private AddServerForm addServerForm;
        private int numberOfDisplayServers;

        public LedManager(int height, int width)
        {
            InitializeComponent();
        }

        private void addNewServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addServerForm = new AddServerForm();
            addServerForm.StartPosition = FormStartPosition.Manual;
            addServerForm.Location = new System.Drawing.Point(this.Location.X + 20, this.Location.X + 20);

            addServerForm.ServerAdded += ServerAddedHandle;

            addServerForm.Show();
        }

        private void ServerAddedHandle(ServerBase server, LedLayout ledLayout)
        {
            if (server is FadecandyServer)
            {
                var serverForm = new FadecandyServerForm((FadecandyServer)server, ledLayout);
                serverForm.StartPosition = FormStartPosition.Manual;
                serverForm.Location = new System.Drawing.Point(this.Location.X + 20, this.Location.X + 20);

                serverForm.Visible = true;
                serverForm.BringToFront();
                serverForm.Show();
            }

            if (server is DisplayOnlyServer)
            {
                var displayOnlyServer = (DisplayOnlyServer)server;

                numberOfDisplayServers++;

                var serverForm = new DisplayOnlyServerForm(displayOnlyServer, ledLayout, "Display only server " + numberOfDisplayServers);
                serverForm.StartPosition = FormStartPosition.Manual;
                serverForm.Location = new System.Drawing.Point(this.Location.X + 20, this.Location.X + 20);

                serverForm.Visible = true;
                serverForm.BringToFront();
                serverForm.Show();
            }
        }
    }
}
