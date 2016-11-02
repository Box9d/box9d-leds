using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using AForge.Video.FFMPEG;
using Box9.Leds.Business.Configuration;
using Box9.Leds.Business.Service;
using Box9.Leds.FcClient.Messages.UpdatePixels;

namespace Box9.Leds.Video
{
    public class VideoPlayer
    {
        private readonly LedConfiguration configuration;
        private readonly IPatternCreationService patternCreationService;

        private IEnumerable<ClientConfigPair> clientConfigPairs;
        private VideoQueuer videoQueuer;
        private AudioData audioData;

        long totalFrames;
        int frameRate;
        int width;
        int height;
        int minutes;
        int seconds;

        public VideoPlayer(LedConfiguration configuration, IPatternCreationService patternCreationService)
        {
            this.configuration = configuration;
            this.patternCreationService = patternCreationService;
            videoQueuer = new VideoQueuer(configuration);
        }

        public async Task Load(IEnumerable<ClientConfigPair> clientConfigPairs, int minutes, int seconds)
        {
            await videoQueuer.QueueFrames(minutes, seconds);

            var audioTransformer = new VideoAudioTransformer(configuration.VideoConfig.SourceFilePath);
            audioData = audioTransformer.ExtractAndSaveAudio();

            this.clientConfigPairs = clientConfigPairs;

            using (var videoFileReader = new VideoFileReader())
            {
                videoFileReader.Open(configuration.VideoConfig.SourceFilePath);

                totalFrames = videoFileReader.FrameCount;
                frameRate = videoFileReader.FrameRate;
                width = videoFileReader.Width;
                height = videoFileReader.Height;
            }

            foreach (var clientConfigPair in clientConfigPairs)
            {
                await clientConfigPair.Client.ConnectAsync();
            }

            this.minutes = minutes;
            this.seconds = seconds;
        }

        public async Task Play(CancellationToken cancellationToken)
        {
            int totalPlayTimeMillseconds = (int)Math.Round((double)((double)totalFrames / (double)frameRate) * 1000, 0) - (minutes * 60 + seconds) * 1000;

            var playStopwatch = new Stopwatch();
            playStopwatch.Start();

            IMp3AudioPlayer audioPlayer = new Mp3AudioPlayer(audioData);
            audioPlayer.Play(minutes, seconds);

            var cancellationSource = new CancellationTokenSource();

            while (playStopwatch.ElapsedMilliseconds < totalPlayTimeMillseconds && !cancellationToken.IsCancellationRequested)
            {
                double secondsPassed = (double)playStopwatch.ElapsedMilliseconds / 1000 + minutes * 60 + seconds;
                var currentFrame = (int)Math.Round(secondsPassed * frameRate, 0) + 1;

                if (currentFrame <= totalFrames && videoQueuer.Frames.ContainsKey(currentFrame))
                {
                    var frame = videoQueuer.Frames[currentFrame];

                    foreach (var clientServer in clientConfigPairs)
                    {
                        cancellationSource.Cancel();
                        cancellationSource = new CancellationTokenSource();

                        var pixelUpdates = patternCreationService.FromBitmap(frame, clientServer.ServerConfiguration);
                        await clientServer.Client.SendPixelUpdates(new UpdatePixelsRequest(pixelUpdates), cancellationSource.Token);
                        clientServer.Client.SendBitmap(frame);
                    }
                }
            }

            audioPlayer.Stop();
            audioPlayer.Dispose();

            foreach (var clientConfigPair in clientConfigPairs)
            {
                var allPixelsOff = patternCreationService.AllPixelsOff(clientConfigPair.ServerConfiguration);

                var request = new UpdatePixelsRequest(allPixelsOff);

                await clientConfigPair.Client.SendPixelUpdates(request);
                await clientConfigPair.Client.SendPixelUpdates(request);
                await clientConfigPair.Client.CloseAsync();
                clientConfigPair.Client.Dispose();
            }

            this.videoQueuer.Dispose();
            this.videoQueuer = new VideoQueuer(configuration);
        }
    }
}

