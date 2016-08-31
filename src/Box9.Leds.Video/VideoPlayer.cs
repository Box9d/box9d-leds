using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AForge.Video.FFMPEG;
using Box9.Leds.Core;
using Box9.Leds.Core.Configuration;
using Box9.Leds.Core.Messages.UpdatePixels;
using Box9.Leds.Core.Patterns;
using Box9.Leds.FcClient;
using Box9.Leds.Video;

namespace Box9.Leds.Video
{
    public class VideoPlayer
    {
        private readonly LedConfiguration configuration;
        private VideoQueuer videoQueuer;
        private AudioData audioData;

        public VideoPlayer(LedConfiguration configuration)
        {
            this.configuration = configuration;
            this.videoQueuer = new VideoQueuer(configuration);
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

            IMp3AudioPlayer audioPlayer = new Mp3AudioPlayer(audioData);
            audioPlayer.Play(minutes, seconds);

            var playStopwatch = new Stopwatch();
            playStopwatch.Start();

            while (playStopwatch.ElapsedMilliseconds < totalPlayTimeMillseconds && !cancellationToken.IsCancellationRequested)
            {
                var frameGroup = new Dictionary<Guid, Tuple<IClientWrapper, UpdatePixelsRequest>>();

                double secondsPassed = (double)playStopwatch.ElapsedMilliseconds / 1000 + minutes * 60 + seconds;
                var currentFrame = (int)Math.Round(secondsPassed * frameRate, 0) + 1;

                if (currentFrame <= totalFrames && videoQueuer.Frames.ContainsKey(currentFrame))
                {
                    var frame = videoQueuer.Frames[currentFrame];

                    foreach (var clientServer in clientServers)
                    {
                        if (clientServer.ServerConfiguration.ServerType == Core.Servers.ServerType.FadeCandy)
                        {
                            var pixelUpdates = BitmapExtensions.CreatePixelInfo(frame, clientServer.ServerConfiguration);

                            clientServer.Client.SendPixelUpdates(new UpdatePixelsRequest(pixelUpdates));
                        }
                        else
                        {
                            clientServer.Client.SendBitmap(frame);
                        }
                    }
                }
            }

            audioPlayer.Stop();
            audioPlayer.Dispose();

            foreach (var clientServer in clientServers.Where(cs => cs.ServerConfiguration.ServerType == Core.Servers.ServerType.FadeCandy))
            {
                var allPixelsBlack = Block.GeneratePattern(
                Color.Black, clientServer.ServerConfiguration, clientServer.ServerConfiguration.XPixels, clientServer.ServerConfiguration.YPixels, 0, 0);

                var request = new UpdatePixelsRequest(allPixelsBlack);

                clientServer.Client.SendPixelUpdates(request);
                clientServer.Client.SendPixelUpdates(request);

                if (clientServer.ServerConfiguration.ServerType == Core.Servers.ServerType.FadeCandy)
                {
                    ((WsClientWrapper)clientServer.Client).FinishedUpdates += async() =>
                    {
                        await clientServer.Client.CloseAsync();
                    };
                }
            }

            this.videoQueuer.Dispose();
            this.videoQueuer = new VideoQueuer(configuration);
        }
    }
}

