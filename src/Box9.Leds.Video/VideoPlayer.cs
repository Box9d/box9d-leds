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
        private VideoQueuer videoQueuer;
        private AudioData audioData;

        public VideoPlayer(LedConfiguration configuration, IPatternCreationService patternCreationService)
        {
            this.configuration = configuration;
            this.patternCreationService = patternCreationService;
            videoQueuer = new VideoQueuer(configuration);
        }

        public int GetDurationInSeconds()
        {
            using (var videoFileReader = new VideoFileReader())
            {
                videoFileReader.Open(configuration.VideoConfig.SourceFilePath);

                return (int)videoFileReader.FrameCount / videoFileReader.FrameRate;
            }  
        }

        public void Load(int minutes, int seconds)
        {
            videoQueuer.QueueFrames(minutes, seconds);

            var audioTransformer = new VideoAudioTransformer(configuration.VideoConfig.SourceFilePath);
            this.audioData = audioTransformer.ExtractAndSaveAudio();
        }

        public async Task Play(IEnumerable<ClientServer> clientServers, int minutes, int seconds, CancellationToken cancellationToken)
        {
            long totalFrames;
            int frameRate;
            int width;
            int height;

            using (var videoFileReader = new VideoFileReader())
            {
                videoFileReader.Open(configuration.VideoConfig.SourceFilePath);

                totalFrames = videoFileReader.FrameCount;
                frameRate = videoFileReader.FrameRate;
                width = videoFileReader.Width;
                height = videoFileReader.Height;
            }  

            foreach (var clientServer in clientServers)
            {
                await clientServer.Client.ConnectAsync();
            }

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

                    foreach (var clientServer in clientServers)
                    {
                        cancellationSource.Cancel();
                        cancellationSource = new CancellationTokenSource();

                        var pixelUpdates = patternCreationService.FromBitmap(frame, clientServer.ServerConfiguration);
                        await clientServer.Client.SendPixelUpdates(new UpdatePixelsRequest(pixelUpdates), cancellationSource.Token);
                    }
                }
            }

            audioPlayer.Stop();
            audioPlayer.Dispose();

            foreach (var clientServer in clientServers)
            {
                var allPixelsBlack = patternCreationService.AllPixelsOff(clientServer.ServerConfiguration);

                var request = new UpdatePixelsRequest(allPixelsBlack);

                await clientServer.Client.SendPixelUpdates(request);
                await clientServer.Client.SendPixelUpdates(request);
                await clientServer.Client.CloseAsync();
            }

            this.videoQueuer.Dispose();
            this.videoQueuer = new VideoQueuer(configuration);
        }
    }
}

