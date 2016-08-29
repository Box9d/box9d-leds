using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using AForge.Video.FFMPEG;
using Box9.Leds.Core;
using Box9.Leds.Core.Configuration;
using Box9.Leds.Core.Messages.UpdatePixels;
using Box9.Leds.Core.Patterns;
using Box9.Leds.Video;

namespace Box9.Leds.Video
{
    public class VideoPlayer : IDisposable
    {
        private readonly LedConfiguration configuration;
        private readonly VideoQueuer videoQueuer;
        private readonly VideoFileReader videoFileReader;
        private AudioData audioData;

        public VideoPlayer(LedConfiguration configuration)
        {
            this.configuration = configuration;
            this.videoQueuer = new VideoQueuer(configuration);
            this.videoFileReader = new VideoFileReader();
        }

        public int GetDurationInSeconds()
        {
            videoFileReader.Open(configuration.VideoConfig.SourceFilePath);

            return (int)videoFileReader.FrameCount / videoFileReader.FrameRate;
        }

        public void Load(int minutes, int seconds)
        {
            videoQueuer.QueueFrames(minutes, seconds, videoFileReader);

            var audioTransformer = new VideoAudioTransformer(configuration.VideoConfig.SourceFilePath);
            audioData = audioTransformer.ExtractAndSaveAudio();
        }

        public async Task Play(IEnumerable<ClientServer> clientServers, int minutes, int seconds, CancellationToken cancellationToken)
        {
            long totalFrames;
            int frameRate;

            videoFileReader.Open(configuration.VideoConfig.SourceFilePath);

            totalFrames = videoFileReader.FrameCount;
            frameRate = videoFileReader.FrameRate;

            foreach (var clientServer in clientServers)
            {
                await clientServer.Client.ConnectAsync();
            }

            int totalPlayTimeMillseconds = (int)Math.Round((double)((double)totalFrames / (double)frameRate) * 1000, 0) - (minutes * 60 + seconds) * 1000;

            IMp3AudioPlayer audioPlayer = new Mp3AudioPlayer(audioData);
            audioPlayer.Play(minutes, seconds);

            var playStopwatch = new Stopwatch();
            playStopwatch.Start();

            while (playStopwatch.ElapsedMilliseconds < totalPlayTimeMillseconds && !cancellationToken.IsCancellationRequested)
            {
                double secondsPassed = (double)playStopwatch.ElapsedMilliseconds / 1000 + minutes * 60 + seconds;
                var currentFrame = (int)Math.Round(secondsPassed * frameRate, 0) + 1;

                if (currentFrame <= totalFrames && videoQueuer.Frames.ContainsKey(currentFrame))
                {
                    var frame = videoQueuer.Frames[currentFrame];

                    foreach (var clientServer in clientServers)
                    {
                        var pixelUpdates = BitmapExtensions.CreatePixelInfo(frame, clientServer.ServerConfiguration);

                        clientServer.Client.SendPixelUpdates(new UpdatePixelsRequest
                        {
                            PixelUpdates = pixelUpdates
                        });                 
                    }
                }
            }

            audioPlayer.Stop();
            audioPlayer.Dispose();

            foreach (var clientServer in clientServers)
            {
                var allPixelsBlack = Block.GeneratePattern(
                Color.Black, clientServer.ServerConfiguration, clientServer.ServerConfiguration.XPixels, clientServer.ServerConfiguration.YPixels, 0, 0);

                var request = new UpdatePixelsRequest
                {
                    PixelUpdates = allPixelsBlack
                };

                await clientServer.Client.SendPixelUpdates(request);
                await clientServer.Client.SendPixelUpdates(request);

                await clientServer.Client.CloseAsync();
            }
        }

        public void Dispose()
        {
            videoFileReader.Dispose();
        }
    }
}

