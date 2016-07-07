using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Box9.Leds.Core.Configuration;
using Box9.Leds.DataStorage;
using Box9.Leds.Manager.Forms;

namespace Box9.Leds.Manager
{
    public partial class LedManager : Form
    {
        private AddServerForm addServerForm;
        private int numberOfDisplayServers;
        private string loadedConfigFilePath;
        private readonly IConfigurationStorageClient configurationStorage;

        public LedManager()
        {
            InitializeComponent();
            configurationStorage = new ConfigurationStorageClient();
        }

        private void ServerAddedHandle(ServerConfiguration server)
        {
            //if (server is FadecandyServer)
            //{
            //    var serverForm = new FadecandyServerForm((FadecandyServer)server, ledLayout);
            //    serverForm.StartPosition = FormStartPosition.Manual;
            //    serverForm.Location = new System.Drawing.Point(this.Location.X + 20, this.Location.X + 20);

            //    serverForm.Visible = true;
            //    serverForm.BringToFront();
            //    serverForm.Show();
            //}

            //if (server is DisplayOnlyServer)
            //{
            //    var displayOnlyServer = (DisplayOnlyServer)server;

            //    numberOfDisplayServers++;

            //    var serverForm = new DisplayOnlyServerForm(displayOnlyServer, ledLayout, "Display only server " + numberOfDisplayServers);
            //    serverForm.StartPosition = FormStartPosition.Manual;
            //    serverForm.Location = new System.Drawing.Point(this.Location.X + 20, this.Location.X + 20);

            //    serverForm.Visible = true;
            //    serverForm.BringToFront();
            //    serverForm.Show();
            //}

            this.listBoxServers.Items.Add(server);
        }

        private void buttonAddServer_Click(object sender, EventArgs e)
        {
            addServerForm = new AddServerForm();
            addServerForm.StartPosition = FormStartPosition.Manual;
            addServerForm.Location = new System.Drawing.Point(this.Location.X + 20, this.Location.X + 20);

            addServerForm.ServerAdded += ServerAddedHandle;

            addServerForm.Show();
        }

        private void buttonRemoveServer_Click(object sender, EventArgs e)
        {
            var toRemove = this.listBoxServers.SelectedIndices;

            foreach (var index in toRemove)
            {
                this.listBoxServers.Items.RemoveAt((int)index);
            }

            this.listBoxServers.ClearSelected();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(loadedConfigFilePath))
            {
                SaveConfigAs();
            }
            else
            {
                SaveConfig();
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveConfigAs();
        }

        private void loadConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = loadConfigurationDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.loadedConfigFilePath = loadConfigurationDialog.FileName;

                var config = configurationStorage.Get(this.loadedConfigFilePath);
                this.LoadConfig(config);
            }
        }

        private void LoadConfig(LedConfiguration config)
        {
            for (int i = 0; i < this.listBoxServers.Items.Count; i++)
            {
                this.listBoxServers.Items.RemoveAt(i);
            }

            foreach (var server in config.Servers)
            {
                this.listBoxServers.Items.Add(server);
            }

            this.labelVideoFilePath.Text = config.VideoConfig != null
                ? config.VideoConfig.SourceFilePath
                : string.Empty;
        }

        private void SaveConfig()
        {
            var servers = new List<ServerConfiguration>();
            foreach (var server in this.listBoxServers.Items)
            {
                servers.Add((ServerConfiguration)server);
            }

            configurationStorage.Save(new LedConfiguration
            {
                Servers = servers,
                VideoConfig = new VideoConfiguration
                {
                    SourceFilePath = this.labelVideoFilePath.Text
                }
            }, this.loadedConfigFilePath);
        }

        private void SaveConfigAs()
        {
            var result = this.saveConfigurationDialog.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }

            loadedConfigFilePath = saveConfigurationDialog.FileName;
            SaveConfig();
        }

        private void importVideoButton_Click(object sender, EventArgs e)
        {
            var result = videoBrowserDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                labelVideoFilePath.Text = videoBrowserDialog.FileName;
            }
        }
    }
}
