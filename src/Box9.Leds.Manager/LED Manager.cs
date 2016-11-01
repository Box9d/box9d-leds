using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Box9.Leds.Business.Configuration;
using Box9.Leds.Business.Dtos;
using Box9.Leds.Business.Services;
using Box9.Leds.Core.EventsArguments;
using Box9.Leds.Manager.Presenters;
using Box9.Leds.Manager.Views;
using Box9.Leds.Video;

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

        public PlaybackStatus PlaybackStatus { get; set; }

        public event EventHandler<StringEventArgs> SaveConfiguration;
        public event EventHandler<StringEventArgs> OpenConfiguration;
        public event EventHandler<EventArgs> NewConfiguration;
        public event EventHandler<EventArgs> AddNewServer;
        public event EventHandler<StringEventArgs> EditServer;
        public event EventHandler<StringEventArgs> RemoveServer;
        public event EventHandler<StringEventArgs> ImportVideo;
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

            trackBarBrightness.ValueChanged += (s, args) =>
            {
                PreviewBrightnessChanged(s, new IntegerEventArgs(trackBarBrightness.Value));
            };

            trackBarBrightness.MouseUp += (s, args) =>
            {
                BrightnessChanged(s, new IntegerEventArgs(trackBarBrightness.Value));
            };

            checkBoxDisplayOutputOnScreen.CheckedChanged += (s, args) =>
            {
                DisplayVideo = checkBoxDisplayOutputOnScreen.Checked;
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

                buttonPlay.Enabled = PlaybackStatus == PlaybackStatus.ReadyToPlay;
                buttonStop.Enabled = PlaybackStatus == PlaybackStatus.Playing;
                buttonInitializePlayback.Enabled = PlaybackStatus != PlaybackStatus.Playing;
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
    }
}
