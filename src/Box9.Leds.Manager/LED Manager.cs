using System;
using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;
using Box9.Leds.Core.LedLayouts;
using Box9.Leds.Core.Servers;
using Box9.Leds.Manager.Forms;

namespace Box9.Leds.Manager
{
    public partial class LedManager : Form
    {
        private AddServerForm addServerForm;
        private Dictionary<string, FadecandyServerForm> fadecandyServers;
        private List<DisplayOnlyServerForm> displayOnlyServers;

        public LedManager(int height, int width)
        {
            InitializeComponent();

            fadecandyServers = new Dictionary<string, FadecandyServerForm>();
            displayOnlyServers = new List<DisplayOnlyServerForm>();
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
                var fadecandyServer = (FadecandyServer)server;

                FadecandyServerForm serverForm = null;
                if (!fadecandyServers.ContainsKey(fadecandyServer.IPAddress.ToString()))
                {
                    serverForm = new FadecandyServerForm(fadecandyServer, ledLayout);
                    serverForm.StartPosition = FormStartPosition.Manual;
                    serverForm.Location = new System.Drawing.Point(this.Location.X + 20, this.Location.X + 20);

                    fadecandyServers.Add(fadecandyServer.IPAddress.ToString(), serverForm);
                }
                else
                {
                    serverForm = fadecandyServers[fadecandyServer.IPAddress.ToString()];
                }

                serverForm.Visible = true;
                serverForm.BringToFront();
                serverForm.Show();
            }

            if (server is DisplayOnlyServer)
            {
                var displayOnlyServer = (DisplayOnlyServer)server;

                var serverForm = new DisplayOnlyServerForm(displayOnlyServer, ledLayout, "Display only server " + (displayOnlyServers.Count + 1));
                serverForm.StartPosition = FormStartPosition.Manual;
                serverForm.Location = new System.Drawing.Point(this.Location.X + 20, this.Location.X + 20);

                displayOnlyServers.Add(serverForm);

                serverForm.Visible = true;
                serverForm.BringToFront();
                serverForm.Show();
            }
        }
    }
}
