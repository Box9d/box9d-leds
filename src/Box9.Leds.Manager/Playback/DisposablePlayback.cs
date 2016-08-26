using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Box9.Leds.Core.Configuration;
using Box9.Leds.Core.Servers;
using Box9.Leds.DataStorage;
using Box9.Leds.FcClient;
using Box9.Leds.Manager.Forms;
using Box9.Leds.Video;
using Box9.Leds.Video.Storage;
using DBreeze;

namespace Box9.Leds.Manager.Playback
{
    public class DisposablePlayback : IDisposable
    {
        public delegate void PlaybackFinished();
        public event PlaybackFinished Finished;

        private readonly List<VideoPlayer> videoPlayers;
        private readonly Mp3AudioPlayer audioPlayer;

        private readonly DBreezeEngine engine;
        private CancellationTokenSource cancelTokenSource;

        public DisposablePlayback(LedManager ledManager, LedConfiguration ledConfiguration, IEnumerable<DisposableVideoPlayback> videoPlaybacks, AudioData audioPlayback)
        {
            engine = DBreezeEngineFactory.GetDBreezeEngine();
            videoPlayers = new List<VideoPlayer>();

            Finished += PlaybackFinishedHandler;

            foreach (var videoPlayback in videoPlaybacks)
            {
                ServerForm currentServerForm = null;

                ledManager.Invoke(new Action(() =>
                {
                    var serverForm = new ServerForm(videoPlayback.ServerConfig);
                    serverForm.StartPosition = FormStartPosition.Manual;
                    serverForm.Visible = true;
                    serverForm.BringToFront();
                    serverForm.Show();

                    currentServerForm = serverForm;
                    ledManager.ServerForms.Add(serverForm);
                }));

                IClientWrapper server;
                if (videoPlayback.ServerConfig.ServerType == ServerType.DisplayOnly)
                {
                    server = new DisplayClientWrapper(currentServerForm.DisplayPanel, videoPlayback.ServerConfig);
                }
                else
                {
                    server = new WsClientWrapper(new Uri(string.Format("ws://{0}:{1}", videoPlayback.ServerConfig.IPAddress, videoPlayback.ServerConfig.Port)));
                }

                var videoPlayer = new VideoPlayer(server, VideoStorageFactory.GetVideoStorageClient(engine, 
                    ledConfiguration.GetServerVideoStorageKey(videoPlayback.ServerConfig)), 
                    videoPlayback.VideoData, 
                    videoPlayback.ServerConfig);

                videoPlayers.Add(videoPlayer);
            }

            audioPlayer = new Mp3AudioPlayer(audioPlayback);
            cancelTokenSource = new CancellationTokenSource();
        }

        public async Task Play(int minutes = 0, int seconds = 0)
        {
            this.audioPlayer.Stopped += () =>
            {
                Finished();
            };

            foreach (var videoPlayer in videoPlayers)
            {
                videoPlayer.PreBuffer(minutes, seconds);
            }

            this.audioPlayer.Play(minutes, seconds);
            foreach (var videoPlayer in videoPlayers)
            {
                videoPlayer.Play(cancelTokenSource.Token, minutes, seconds);
            }

            await Task.Yield();
        }

        public async Task Stop()
        {
            this.audioPlayer.Stop();
            cancelTokenSource.Cancel();

            await Task.Yield();
        }

        private void PlaybackFinishedHandler()
        {
        }

        public void Dispose()
        {
            foreach (var videoPlayer in videoPlayers)
            {
                videoPlayer.Dispose();
            }

            engine.Dispose();
        }
    }
}
