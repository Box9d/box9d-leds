using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Box9.Leds.Core;
using Box9.Leds.Core.LedLayouts;
using Box9.Leds.Core.Servers;
using Box9.Leds.DataStorage;
using Box9.Leds.FcClient;
using Box9.Leds.Video;

namespace Box9.Leds.Manager.Forms
{
    public partial class DisplayOnlyServerForm : Form
    {
        private readonly LedLayout ledLayout;
        private readonly string identifier;

        private IClientWrapper displayClientWrapper;
        private ChunkedStorageClient<int, FrameVideoData> videoStorageClient;
        private VideoPlayer displayVideoPlayer;
        private Task displayVideoPlayerTask;
        private CancellationTokenSource cts;

        public DisplayOnlyServerForm(DisplayOnlyServer server, LedLayout ledLayout, string identifier)
        {
            InitializeComponent();

            this.ledLayout = ledLayout;
            this.identifier = identifier;

            this.cts = new CancellationTokenSource();

            this.videoBrowserDialog.FileOk += VideoSelected;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void ServerForm_Load(object sender, System.EventArgs e)
        {
            var clientWidth = ledLayout.XNumberOfPixels * (PixelDimensions.Width + PixelDimensions.Gap);
            var clientHeight = ledLayout.YNumberOfPixels * (PixelDimensions.Height + PixelDimensions.Gap);

            this.ClientSize = new Size(clientWidth, clientHeight + videoControlPanel.Height);

            this.Text = identifier;

            this.videoControlPanel.Height = 130;
            this.videoControlPanel.Width = this.ClientRectangle.Width;
            this.videoControlPanel.Left = this.ClientRectangle.Left;
            this.videoControlPanel.Top = this.ClientRectangle.Bottom - this.videoControlPanel.Height; 
            this.videoControlPanel.Anchor = AnchorStyles.Bottom;

            this.displayPanel.Height = this.ClientRectangle.Height - this.videoControlPanel.Height;
            this.displayPanel.Width = this.ClientRectangle.Width;
            this.displayPanel.Left = this.ClientRectangle.Left;
            this.displayPanel.Top = this.ClientRectangle.Top;
            this.displayPanel.Anchor = AnchorStyles.Top;

            this.importVideoButton.Anchor = AnchorStyles.Bottom;
            this.importVideoButton.Left = this.ClientRectangle.Left + 10;
            this.importVideoButton.Top = importVideoButton.Top - 10;

            this.playButton.Left = this.ClientRectangle.Left + 10;
            this.stopButton.Left = this.playButton.Right + 10;

            this.fileNameLabel.Anchor = AnchorStyles.Bottom;
            this.fileNameLabel.Left = this.importVideoButton.Right + 10;
            this.fileNameLabel.Top = this.fileNameLabel.Top - 10;
            this.fileNameLabel.Width = this.ClientRectangle.Right - this.fileNameLabel.Left - 10;
            this.fileNameLabel.AutoSize = false;
            this.fileNameLabel.AutoEllipsis = true;

            this.toolStripProgressBar.MarqueeAnimationSpeed = 40;
            this.toolStripProgressBar.Style = ProgressBarStyle.Blocks;
            this.toolStripProgressBar.Anchor = AnchorStyles.Right;

            this.toolStripStatusLabel.AutoSize = false;
            this.toolStripStatusLabel.Width = 7 * this.ClientRectangle.Width / 10;
            this.toolStripStatusLabel.Anchor = AnchorStyles.Left;
            this.toolStripStatusLabel.TextAlign = ContentAlignment.MiddleLeft;

            this.FormClosing += OnClose;
        }

        private void OnClose(object sender, FormClosingEventArgs e)
        {
            this.displayVideoPlayer.Dispose();
        }

        private void importVideoButton_Click(object sender, System.EventArgs e)
        {
            videoBrowserDialog.ShowDialog();
        }

        private void VideoSelected(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.displayClientWrapper = new DisplayClientWrapper(this.displayPanel, this.ledLayout);

            var dbEngine = StorageFactory.GetDBreezeEngine();

            videoStorageClient = new ChunkedStorageClient<int, FrameVideoData>(dbEngine, "video" + Guid.NewGuid().ToString());

            var videoTransformer = new VideoTransformer(videoStorageClient, this.videoBrowserDialog.FileName);

            displayVideoPlayer = new VideoPlayer(videoStorageClient, videoTransformer, this.displayClientWrapper, VideoSettings.FramesPerStorageKey, VideoSettings.FramesPerStorageKey);

            this.displayVideoPlayer.VideoStatusChanged += VideoStatusChanged;

            Task.Run(() =>
            {
                displayVideoPlayer.Load(this.ledLayout);
            });

            this.Enabled = false;
            this.toolStripStatusLabel.Text = "Loading video...";
            this.toolStripProgressBar.Style = ProgressBarStyle.Marquee;
        }

        private void VideoStatusChanged(VideoStatus status)
        {
            switch (status)
            {
                case VideoStatus.ReadyToPlay:

                    this.Invoke(new Action(() =>
                    {
                        this.fileNameLabel.Text = new FileInfo(this.videoBrowserDialog.FileName).Name;
                        this.playButton.Enabled = true;

                        this.toolStripProgressBar.Style = ProgressBarStyle.Blocks;
                        this.toolStripProgressBar.Value = 0;

                        this.toolStripStatusLabel.Text = string.Empty;

                        this.Enabled = true;
                    }));
                    break;
            }        
        }

        private void playButton_Click(object sender, System.EventArgs e)
        {
            displayVideoPlayerTask = new Task(async () => await displayVideoPlayer.Play(cts.Token), cts.Token);
            displayVideoPlayerTask.Start();

            this.playButton.Enabled = false;
            this.stopButton.Enabled = true;
        }

        private void stopButton_Click(object sender, System.EventArgs e)
        {
            cts.Cancel();
            cts = new CancellationTokenSource();

            this.playButton.Enabled = true;
            this.stopButton.Enabled = false;
        }
    }
}
