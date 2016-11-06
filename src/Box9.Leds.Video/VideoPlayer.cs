using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using AForge.Video.FFMPEG;
using Box9.Leds.Business.Configuration;
using Box9.Leds.Business.Service;
using Box9.Leds.Business.Services;
using Box9.Leds.Core.Multitasking;
using Box9.Leds.FcClient;
using Box9.Leds.FcClient.Messages.UpdatePixels;

namespace Box9.Leds.Video
{
    public class VideoPlayer
    {
        private readonly LedConfiguration configuration;
        private readonly IPatternCreationService patternCreationService;
        private readonly IVideoMetadataService videoMetadataService;

        private IEnumerable<ClientConfigPair> clientConfigPairs;
        private VideoQueuer videoQueuer;
        private AudioData audioData;

        long totalFrames;
        double frameRate;
        int width;
        int height;
        int minutes;
        int seconds;

        public VideoPlayer(LedConfiguration configuration, IPatternCreationService patternCreationService, IVideoMetadataService videoMetadataService)
        {
            this.configuration = configuration;
            this.patternCreationService = patternCreationService;
            this.videoMetadataService = videoMetadataService;
            videoQueuer = new VideoQueuer(configuration, videoMetadataService);
        }

        public async Task Load(IEnumerable<ClientConfigPair> clientConfigPairs, int minutes, int seconds)
        {
            await videoQueuer.QueueFrames(minutes, seconds);

            var audioTransformer = new VideoAudioTransformer(configuration.VideoConfig.SourceFilePath);
            audioData = audioTransformer.ExtractAndSaveAudio();

            this.clientConfigPairs = clientConfigPairs;

            var videoMetadata = videoMetadataService.GetMetadata(configuration.VideoConfig.SourceFilePath);
            totalFrames = videoMetadata.TotalFrames;
            frameRate = videoMetadata.FrameRate;
            width = videoMetadata.Width;
            height = videoMetadata.Height;

            foreach (var clientConfigPair in clientConfigPairs)
            {
                clientConfigPair.Client.Connect();
            }

            this.minutes = minutes;
            this.seconds = seconds;
        }

        public void Play(CancellationToken cancellationToken)
        {
            int totalPlayTimeMillseconds = (int)Math.Round((double)((double)totalFrames / frameRate) * 1000, 0) - (minutes * 60 + seconds) * 1000;

            var disconnectedClients = new List<IClientWrapper>();

            var playStopwatch = new Stopwatch();
            playStopwatch.Start();

            IMp3AudioPlayer audioPlayer = new Mp3AudioPlayer(audioData);
            audioPlayer.Play(minutes, seconds);

            var cancellationSource = new CancellationTokenSource();

            while (playStopwatch.ElapsedMilliseconds < totalPlayTimeMillseconds && !cancellationToken.IsCancellationRequested)
            {
                double millisecondsPassed = playStopwatch.ElapsedMilliseconds + (((minutes * 60) + seconds) * 1000);
                var currentFrame = (int)((double)(millisecondsPassed * frameRate) / (double)1000);

                if (currentFrame <= totalFrames && videoQueuer.Frames.ContainsKey(currentFrame))
                {
                    var frame = videoQueuer.Frames[currentFrame];

                    foreach (var clientServer in clientConfigPairs)
                    {
                        cancellationSource.Cancel();
                        cancellationSource = new CancellationTokenSource();

                        if (clientServer.Client.State == System.Net.WebSockets.WebSocketState.Open)
                        {
                            var pixelUpdates = patternCreationService.FromBitmap(frame, clientServer.ServerConfiguration);
                            clientServer.Client.SendPixelUpdates(new UpdatePixelsRequest(pixelUpdates), cancellationSource.Token);
                            clientServer.Client.SendBitmap(frame);
                        }
                        else if (!disconnectedClients.Contains(clientServer.Client))
                        {
                            Task.Run(() =>
                            {
                                clientServer.Client.Connect(cancellationToken);
                                disconnectedClients.Remove(clientServer.Client);
                            }).Forget();

                            disconnectedClients.Add(clientServer.Client);
                        }
                    }
                }
            }

            audioPlayer.Stop();
            audioPlayer.Dispose();

            foreach (var clientConfigPair in clientConfigPairs)
            {
                var allPixelsOff = patternCreationService.AllPixelsOff(clientConfigPair.ServerConfiguration);

                var request = new UpdatePixelsRequest(allPixelsOff);

                clientConfigPair.Client.SendPixelUpdates(request);
                clientConfigPair.Client.SendPixelUpdates(request);
                clientConfigPair.Client.CloseAsync();
                clientConfigPair.Client.Dispose();
            }

            this.videoQueuer.Dispose();
            this.videoQueuer = new VideoQueuer(configuration, videoMetadataService);
        }
    }
}

