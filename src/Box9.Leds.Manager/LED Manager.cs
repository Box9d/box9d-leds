using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Box9.Leds.Core.Configuration;
using Box9.Leds.Core.Messages.ColorCorrection;
using Box9.Leds.Core.Messages.ConnectedDevices;
using Box9.Leds.Core.Messages.ServerInfo;
using Box9.Leds.DataStorage;
using Box9.Leds.FcClient;
using Box9.Leds.Manager.Events;
using Box9.Leds.Manager.Extensions;
using Box9.Leds.Manager.Forms;
using Box9.Leds.Manager.Playback;
using Box9.Leds.Manager.Validation;

namespace Box9.Leds.Manager
{
    public partial class LedManager : Form
    {
        private AddServerForm addServerForm;
        private string loadedConfigFilePath;
        private string videoSourceFilePath;
        private readonly IConfigurationStorageClient configurationStorage;
        private DisposablePlayback disposablePlayback;
        private bool displayOutput;

        public List<ServerForm> ServerForms { get; private set; }

        public LedManager()
        {
            InitializeComponent();
            configurationStorage = new ConfigurationStorageClient();
            ServerForms = new List<ServerForm>();
        }

        private async void LedManager_Load(object sender, EventArgs e)
        {
            this.trackBarBrightness.ValueChanged += (s, args) =>
            {
                this.labelBrightness.Text = string.Format("Brightness {0}%", trackBarBrightness.Value);
            };

            this.trackBarBrightness.MouseUp += (s, args) =>
            {
                foreach (var server in this.listBoxServers.Items.Cast<ServerConfiguration>().Where(sc => sc.ServerType == Core.Servers.ServerType.FadeCandy))
                {
                    Task.Run(async () =>
                    {
                        var client = new WsClientWrapper(new Uri(string.Format("ws://{0}:{1}", server.IPAddress, server.Port)));

                        var serverInfo = await client.SendMessage(new ConnectedDevicesRequest());
                        var fadecandySerials = serverInfo.Devices.Select(d => d.Serial);

                        foreach (var serial in fadecandySerials)
                        {
                            await client.SendMessage(new ColorCorrectionRequest(trackBarBrightness.Value, serial));
                        }
                    });
                }
            };
        }

        private void ServerAddedHandle(ServerConfiguration server)
        {
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
            this.listBoxServers.RemoveAllItems();

            foreach (var server in config.Servers)
            {
                this.listBoxServers.Items.Add(server);
            }

            videoSourceFilePath = config.VideoConfig != null
                ? config.VideoConfig.SourceFilePath
                : string.Empty;

            if (!string.IsNullOrEmpty(videoSourceFilePath))
            {
                labelVideoFilePath.Text = videoSourceFilePath;
            }
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
                    SourceFilePath = this.videoSourceFilePath
                },
                AudioConfig = new AudioConfiguration
                {
                    SourceFilePath = this.videoSourceFilePath
                }
            }, this.loadedConfigFilePath);
        }

        private DialogResult SaveConfigAs()
        {
            var result = this.saveConfigurationDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                loadedConfigFilePath = saveConfigurationDialog.FileName;
                SaveConfig();
            }

            return result;
        }

        private void importVideoButton_Click(object sender, EventArgs e)
        {
            var result = videoBrowserDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                labelVideoFilePath.Text = videoBrowserDialog.FileName;
                videoSourceFilePath = videoBrowserDialog.FileName;
            }
        }

        private async void buttonInitializePlayback_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(loadedConfigFilePath))
            {
                var dialogResult = SaveConfigAs();
                if (dialogResult != DialogResult.OK)
                {
                    return; // Don't validate if the file isn't saved
                }
            }
            else
            {
                SaveConfig();
            }

            listIssues.RemoveAllItems();

            IConfigurationValidator validator = new ConfigurationValidator();
            var config = configurationStorage.Get(this.loadedConfigFilePath);
            var result = await validator.Validate(config);

            if (result.OK)
            {
                Task.Run(() => InitializePlayback(config));
            }
            else
            {
                buttonPlay.Enabled = false;
                foreach (var issue in result.Errors)
                {
                    listIssues.Items.Add(issue);
                }
            }
        }

        private async Task InitializePlayback(LedConfiguration config)
        {
            var disposablePlaybackFactory = new DisposablePlaybackFactory();
            disposablePlaybackFactory.StatusUpdate += DisposablePlaybackInitializeUpdate;

            this.Invoke(new Action(() =>
            {
                this.ToggleControlAvailabilites(false);
            }));

            try
            {
                this.disposablePlayback = disposablePlaybackFactory.InitializeFromConfig(this, config);

                this.Invoke(new Action(() =>
                {
                    this.ToggleControlAvailabilites(false, buttonPlay, trackBarStartTime, labelStartTime, checkBoxDisplayOutputOnScreen);

                    this.trackBarStartTime.Maximum = this.disposablePlayback.DurationInSeconds;
                    this.trackBarStartTime.Minimum = 0;
                    this.trackBarStartTime.TickFrequency = 1;

                    this.trackBarStartTime.ValueChanged += (sender, e) =>
                    {
                        var startTime = new TimeSpan(0, 0, trackBarStartTime.Value);

                        labelStartTime.Text = string.Format("Start playback at {0}", startTime.ToString(@"mm\:ss"));
                    };
                }));
            }
            catch (Exception ex)
            {
                this.Invoke(new Action(() =>
                {
                    this.ToggleControlAvailabilites(true, this.buttonPlay, this.buttonStop, this.trackBarStartTime);
                    MessageBox.Show(ex.Message);
                }));
            }

            await Task.Yield();
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            var startTime = new TimeSpan(0, 0, this.trackBarStartTime.Value);

            Task.Run(() => this.disposablePlayback.Play(startTime.Minutes, startTime.Hours, displayOutput));

            this.ToggleControlAvailabilites(false, buttonStop);

            this.disposablePlayback.Finished += () =>
            {
                this.Invoke(new Action(() =>
                {
                    this.ToggleControlAvailabilites(true, buttonPlay, buttonStop);
                    this.disposablePlayback.Dispose();

                    foreach (var server in ServerForms)
                    {
                        server.Close();
                        server.Dispose();
                    }

                    ServerForms = new List<ServerForm>();
                }));
            };
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                this.trackBarStartTime.Value = 0;
                this.ToggleControlAvailabilites(true, buttonPlay, buttonStop, trackBarStartTime);
                this.disposablePlayback.Stop();
                this.disposablePlayback.Dispose();

                foreach (var server in ServerForms)
                {
                    server.Close();
                    server.Dispose();
                }

                ServerForms = new List<ServerForm>();
            }));
        }

        private void newConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.listBoxServers.RemoveAllItems();
            this.videoSourceFilePath = null;
            this.labelVideoFilePath.Text = "No video selected";

            this.loadedConfigFilePath = null;
        }

        private void DisposablePlaybackInitializeUpdate(EventMessage message)
        {
            this.Invoke(new Action(() =>
            {
                this.stripStatusLabel.Text = message.Status + ": " + message.Message;
            }));
        }

        private void checkBoxDisplayOutputOnScreen_CheckedChanged(object sender, EventArgs e)
        {
            this.displayOutput = checkBoxDisplayOutputOnScreen.Checked;
        }
    }
}
