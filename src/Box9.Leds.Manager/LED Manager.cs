using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Box9.Leds.Business.Configuration;
using Box9.Leds.Business.Dtos;
using Box9.Leds.Business.Services;
using Box9.Leds.Core.EventsArguments;
using Box9.Leds.Manager.Presenters;
using Box9.Leds.Manager.Views;

namespace Box9.Leds.Manager
{
    public partial class LedManager : Form, ILedManagerView
    {
        private readonly LedManagerPresenter presenter;

        public List<ServerConfiguration> Servers { get; set; }

        public VideoMetadata VideoMetadata { get; set; }

        public string ConfigurationFilePath { get; set; }

        public TimeSpan? TotalVideoLength { get; set; }

        public bool DisplayVideo { get; set; }

        public int BrightnessPercentage { get; set; }

        public event EventHandler<StringEventArgs> SaveConfiguration;
        public event EventHandler<StringEventArgs> OpenConfiguration;
        public event EventHandler<EventArgs> NewConfiguration;
        public event EventHandler<EventArgs> AddNewServer;
        public event EventHandler<StringEventArgs> EditServer;
        public event EventHandler<StringEventArgs> RemoveServer;
        public event EventHandler<StringEventArgs> ImportVideo;
        public event EventHandler<BooleanEventArgs> DisplayVideoToggle;
        public event EventHandler<IntegerEventArgs> PreviewBrightnessChanged;
        public event EventHandler<IntegerEventArgs> BrightnessChanged;
        public event EventHandler<IntegerEventArgs> StartTimeChanged;
        public event EventHandler<EventArgs> InitializePlayback;
        public event EventHandler<EventArgs> Play;
        public event EventHandler<EventArgs> Stop;

        public LedManager()
        {
            InitializeComponent();

            presenter = new LedManagerPresenter(this, new ConfigurationStorageService(), new BrightnessService(), new VideoMetadataService());
        }

        private void LedManager_Load(object sender, EventArgs e)
        {
            presenter.IsDirty += ReloadDirtyForm;
            presenter.ExceptionRaised += ShowException;

            saveToolStripMenuItem.Click += (s, args) =>
            {
                SaveConfiguration(s, new StringEventArgs(ConfigurationFilePath));
            };

            saveAsToolStripMenuItem.Click += (s, args) =>
            {
                saveConfigurationDialog.ShowDialog();
            };

            saveConfigurationDialog.FileOk += (s, args) =>
            {
                SaveConfiguration(s, new StringEventArgs(saveConfigurationDialog.FileName));
            };

            loadConfigurationToolStripMenuItem.Click += (s, args) =>
            {
                loadConfigurationDialog.ShowDialog();
            };

            loadConfigurationDialog.FileOk += (s, args) =>
            {
                OpenConfiguration(s, new StringEventArgs(loadConfigurationDialog.FileName));
            };

            newConfigurationToolStripMenuItem.Click += (s, args) =>
            {
                NewConfiguration(s, args);
            };

            buttonAddServer.Click += (s, args) =>
            {
                AddNewServer(s, e);
            };

            buttonEditServer.Click += (s, args) =>
            {
                if (listBoxServers.SelectedIndex > -1)
                {
                    EditServer(s, new StringEventArgs(listBoxServers.SelectedItem.ToString()));
                }
            };

            buttonRemoveServer.Click += (s, args) =>
            {
                if (listBoxServers.SelectedIndex > -1)
                {
                    RemoveServer(s, new StringEventArgs(listBoxServers.SelectedItem.ToString()));
                }
            };

            importVideoButton.Click += (s, args) =>
            {
                videoBrowserDialog.ShowDialog();
            };

            videoBrowserDialog.FileOk += (s, args) =>
            {
                ImportVideo(s, new StringEventArgs(videoBrowserDialog.FileName));
            };

            checkBoxDisplayOutputOnScreen.Click += (s, args) =>
            {
                DisplayVideoToggle(s, new BooleanEventArgs(checkBoxDisplayOutputOnScreen.Checked));
            };

            trackBarBrightness.ValueChanged += (s, args) =>
            {
                PreviewBrightnessChanged(s, new IntegerEventArgs(trackBarBrightness.Value));
            };

            trackBarBrightness.MouseUp += (s, args) =>
            {
                BrightnessChanged(s, new IntegerEventArgs(trackBarBrightness.Value));
            };

            trackBarStartTime.ValueChanged += (s, args) =>
            {
                StartTimeChanged(s, new IntegerEventArgs(trackBarStartTime.Value));
            };

            buttonInitializePlayback.Click += (s, args) =>
            {
                InitializePlayback(s, args);
            };

            buttonPlay.Click += (s, args) =>
            {
                Play(s, args);
            };

            buttonStop.Click += (s, args) =>
            {
                Stop(s, args);
            };
        }

        private void ReloadDirtyForm(object sender, EventArgs args)
        {
            Invoke(new Action(() =>
            {
                listBoxServers.Items.Clear();
                foreach (var server in Servers)
                {
                    listBoxServers.Items.Add(server);
                }

                labelBrightness.Text = string.Format("Brightness {0}%", BrightnessPercentage);
                trackBarStartTime.Minimum = 0;

                if (VideoMetadata != null)
                {
                    labelStartTime.Text = string.Format("Start playback at {0}", VideoMetadata.StartTime.ToString(@"mm\:ss"));
                    labelVideoFilePath.Text = VideoMetadata.FilePath;

                    trackBarStartTime.Maximum = (int)VideoMetadata.TotalTime.TotalSeconds;
                    trackBarStartTime.Value = (int)(VideoMetadata.StartTime.TotalSeconds);
                }
                else
                {
                    labelStartTime.Text = "Start playback at 0:00";

                    labelVideoFilePath.Text = "No video selected";

                    trackBarStartTime.Maximum = 0;
                    trackBarStartTime.Value = 0;
                }

                Text = string.IsNullOrEmpty(ConfigurationFilePath)
                    ? "LED Manager"
                    : string.Format("LED Manager - {0}", ConfigurationFilePath);
            }));
        }

        private void ShowException(object sender, ExceptionArgs args)
        {
            var ex = args.Value;

            Invoke(new Action(() =>
            {
                stripStatusLabel.Text = string.Format("An error occurred: {0}", ex.Message);
            }));
        }

        //private void ServerAddedHandle(ServerConfiguration server)
        //{
        //    this.listBoxServers.Items.Add(server);
        //}

        //private void ServerEditedHandle(ServerConfiguration server)
        //{
        //    ServerConfiguration editedServer = null;
        //    int editedServerIndex = 0;
        //    foreach (var config in listBoxServers.Items.Cast<ServerConfiguration>())
        //    {
        //        if (config.Id == server.Id)
        //        {
        //            editedServer = config;
        //            break;
        //        }

        //        editedServerIndex++;
        //    }

        //    if (editedServer != null)
        //    {
        //        this.listBoxServers.Items.Remove(editedServer);
        //        this.listBoxServers.Items.Insert(editedServerIndex, server);
        //    }
        //}

        //private void buttonAddServer_Click(object sender, EventArgs e)
        //{
        //    AddOrEditServer();
        //}

        //private void AddOrEditServer(ServerConfiguration serverConfig = null)
        //{
        //    var serverForm = new AddServerForm();

        //    if (serverConfig != null)
        //    {
        //        serverForm = new AddServerForm(serverConfig);
        //        serverForm.ServerEdited += ServerEditedHandle;
        //    }
        //    else
        //    {
        //        serverForm.ServerAdded += ServerAddedHandle;
        //    }

        //    serverForm.StartPosition = FormStartPosition.Manual;
        //    serverForm.Location = new System.Drawing.Point(this.Location.X + 20, this.Location.X + 20);

        //    serverForm.Show();
        //}

        //private void buttonRemoveServer_Click(object sender, EventArgs e)
        //{
        //    var toRemove = this.listBoxServers.SelectedIndices;

        //    foreach (var index in toRemove)
        //    {
        //        this.listBoxServers.Items.RemoveAt((int)index);
        //    }

        //    this.listBoxServers.ClearSelected();
        //}

        //private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    if (string.IsNullOrEmpty(loadedConfigFilePath))
        //    {
        //        SaveConfigAs();
        //    }
        //    else
        //    {
        //        SaveConfig();
        //    }
        //}

        //private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    SaveConfigAs();
        //}

        //private void loadConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    var result = loadConfigurationDialog.ShowDialog();
        //    if (result == DialogResult.OK)
        //    {
        //        this.loadedConfigFilePath = loadConfigurationDialog.FileName;

        //        var config = configurationStorage.Get(this.loadedConfigFilePath);
        //        this.LoadConfig(config);
        //    }
        //}

        //private LedConfiguration GetConfig()
        //{
        //    var servers = new List<ServerConfiguration>();
        //    foreach (var server in this.listBoxServers.Items)
        //    {
        //        servers.Add((ServerConfiguration)server);
        //    }

        //    return new LedConfiguration
        //    {
        //        Servers = servers,
        //        VideoConfig = new VideoConfiguration
        //        {
        //            SourceFilePath = this.videoSourceFilePath
        //        },
        //        AudioConfig = new AudioConfiguration
        //        {
        //            SourceFilePath = this.videoSourceFilePath
        //        }
        //    };
        //}

        //private void LoadConfig(LedConfiguration config)
        //{
        //    this.listBoxServers.RemoveAllItems();

        //    foreach (var server in config.Servers)
        //    {
        //        this.listBoxServers.Items.Add(server);
        //    }

        //    videoSourceFilePath = config.VideoConfig != null
        //        ? config.VideoConfig.SourceFilePath
        //        : string.Empty;

        //    if (!string.IsNullOrEmpty(videoSourceFilePath))
        //    {
        //        labelVideoFilePath.Text = videoSourceFilePath;
        //    }
        //}

        //private void SaveConfig()
        //{
        //    var servers = new List<ServerConfiguration>();
        //    foreach (var server in this.listBoxServers.Items)
        //    {
        //        servers.Add((ServerConfiguration)server);
        //    }

        //    configurationStorage.Save(new LedConfiguration
        //    {
        //        Servers = servers,
        //        VideoConfig = new VideoConfiguration
        //        {
        //            SourceFilePath = this.videoSourceFilePath
        //        },
        //        AudioConfig = new AudioConfiguration
        //        {
        //            SourceFilePath = this.videoSourceFilePath
        //        }
        //    }, this.loadedConfigFilePath);
        //}

        //private DialogResult SaveConfigAs()
        //{
        //    var result = this.saveConfigurationDialog.ShowDialog();
        //    if (result == DialogResult.OK)
        //    {
        //        loadedConfigFilePath = saveConfigurationDialog.FileName;
        //        SaveConfig();
        //    }

        //    return result;
        //}

        //private void importVideoButton_Click(object sender, EventArgs e)
        //{
        //    var result = videoBrowserDialog.ShowDialog();

        //    if (result == DialogResult.OK)
        //    {
        //        labelVideoFilePath.Text = videoBrowserDialog.FileName;
        //        videoSourceFilePath = videoBrowserDialog.FileName;
        //    }
        //}

        //private async Task ValidateForm()
        //{
        //    IConfigurationValidator validator = new ConfigurationValidator();
        //    var config = GetConfig();
        //    var result = await validator.Validate(config);

        //    if (result.OK)
        //    {
        //         InitializePlayback(config);
        //    }
        //    else
        //    {
        //        MessageBox.Show(result.Errors.Aggregate((prev, curr) => prev + Environment.NewLine + curr));
        //    }

        //    await Task.Yield();
        //}

        //private void InitializePlayback(LedConfiguration config)
        //{
        //    this.ToggleControlAvailabilites(false);

        //    clientServers = new List<ClientServer>();
        //    foreach (var serverConfig in config.Servers.Where(s => checkBoxDisplayOutputOnScreen.Checked || s.ServerType == Core.Servers.ServerType.FadeCandy))
        //    {
        //        if (serverConfig.ServerType == Core.Servers.ServerType.FadeCandy)
        //        {
        //            var client = new WsClientWrapper(new Uri(string.Format("ws://{0}:{1}", serverConfig.IPAddress, serverConfig.Port)));
        //            clientServers.Add(new ClientServer(client, serverConfig));
        //        }
        //    }

        //    if (checkBoxDisplayOutputOnScreen.Checked)
        //    {
        //        this.videoForm = new VideoForm();
        //        videoForm.BringToFront();
        //        videoForm.Show();

        //        var client = new DisplayClientWrapper(videoForm);

        //        clientServers.Add(new ClientServer(client, new ServerConfiguration { ServerType = Core.Servers.ServerType.DisplayOnly }));
        //    }

        //    this.videoPlayback = new VideoPlayer(configurationStorage.Get(loadedConfigFilePath));
        //    this.cancellationTokenSource = new CancellationTokenSource();

        //    this.ToggleControlAvailabilites(false, buttonPlay, trackBarStartTime, labelStartTime, checkBoxDisplayOutputOnScreen);

        //    this.trackBarStartTime.Maximum = videoPlayback.GetDurationInSeconds();
        //    this.trackBarStartTime.Minimum = 0;
        //    this.trackBarStartTime.TickFrequency = 1;

        //    this.trackBarStartTime.ValueChanged += (sender, e) =>
        //    {
        //        var startTime = new TimeSpan(0, 0, trackBarStartTime.Value);

        //        labelStartTime.Text = string.Format("Start playback at {0}", startTime.ToString(@"mm\:ss"));
        //    };
        //}

        //private void buttonPlay_Click(object sender, EventArgs e)
        //{
        //    var startTime = new TimeSpan(0, 0, this.trackBarStartTime.Value);

        //    Task.Run(async() =>
        //    {
        //        try
        //        {
        //            videoPlayback.Load(startTime.Minutes, startTime.Seconds);
        //            await videoPlayback.Play(this.clientServers, startTime.Minutes, startTime.Seconds, cancellationTokenSource.Token);
        //        }
        //        catch(Exception ex)
        //        {
        //            this.Invoke(new Action(() =>
        //            {
        //                while (true)
        //                {
        //                    MessageBox.Show(string.Format("Playback failed: {0}\r\n\r\n{1}", ex.Message, ex.StackTrace));

        //                    if (ex.InnerException == null)
        //                    {
        //                        break;
        //                    }

        //                    ex = ex.InnerException;                   
        //                }
        //            }));
        //        }
        //        finally
        //        {
        //            Stop();
        //        }
        //    });

        //    this.ToggleControlAvailabilites(false, buttonStop, trackBarBrightness);
        //}

        //private void buttonStop_Click(object sender, EventArgs e)
        //{
        //    Stop();
        //}

        //private void Stop()
        //{
        //    this.Invoke(new Action(() =>
        //    {
        //        this.cancellationTokenSource.Cancel();

        //        this.trackBarStartTime.Value = 0;
        //        this.ToggleControlAvailabilites(true, buttonPlay, buttonStop, trackBarStartTime);

        //        if (this.videoForm != null)
        //        {
        //            this.videoForm.Close();
        //        }
        //    }));
        //}

        //private void newConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    this.listBoxServers.RemoveAllItems();
        //    this.videoSourceFilePath = null;
        //    this.labelVideoFilePath.Text = "No video selected";

        //    this.loadedConfigFilePath = null;
        //}

        //private void checkBoxDisplayOutputOnScreen_CheckedChanged(object sender, EventArgs e)
        //{
        //    this.displayOutput = checkBoxDisplayOutputOnScreen.Checked;
        //}

        //private async void buttonValidatePlayback_Click(object sender, EventArgs e)
        //{
        //    await ValidateForm();
        //}

        //private async Task AdjustBrightnessOfConnectedServers(int brightnessPercentage)
        //{
        //    foreach (var server in this.listBoxServers.Items.Cast<ServerConfiguration>().Where(sc => sc.ServerType == Core.Servers.ServerType.FadeCandy))
        //    {
        //        var client = new WsClientWrapper(new Uri(string.Format("ws://{0}:{1}", server.IPAddress, server.Port)));
        //        await client.ConnectAsync();

        //        var serverInfo = await client.SendMessage(new ConnectedDevicesRequest());
        //        var fadecandySerials = serverInfo.Devices.Select(d => d.Serial);

        //        foreach (var serial in fadecandySerials)
        //        {
        //            await client.SendMessage(new ColorCorrectionRequest(trackBarBrightness.Value, serial));
        //        }

        //        await client.CloseAsync();
        //    }
        //}

        //private void buttonEditServer_Click(object sender, EventArgs e)
        //{
        //    if (this.listBoxServers.SelectedIndex > -1)
        //    {
        //        AddOrEditServer((ServerConfiguration)this.listBoxServers.SelectedItem);
        //    }
        //}
    }
}
