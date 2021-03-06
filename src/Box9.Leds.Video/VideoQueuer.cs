﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using AForge.Video.FFMPEG;
using Box9.Leds.Business.Configuration;
using Box9.Leds.Business.Services;

namespace Box9.Leds.Video
{
    public class VideoQueuer : IDisposable
    {
        public Dictionary<int, Bitmap> Frames { get; }
        private readonly LedConfiguration configuration;
        private readonly IVideoMetadataService videoMetadataService;

        public VideoQueuer(LedConfiguration configuration, IVideoMetadataService videoMetadataService)
        {
            this.configuration = configuration;
            this.videoMetadataService = videoMetadataService;
            Frames = new Dictionary<int, Bitmap>();
        }

        public async Task QueueFrames(int minutes, int seconds)
        {
            await Task.Run(() =>
            {
                var videoMetadata = videoMetadataService.GetMetadata(configuration.VideoConfig.SourceFilePath);
                var currentFrame = 0;

                using (var videoFileReader = new VideoFileReader())
                {
                    videoFileReader.Open(configuration.VideoConfig.SourceFilePath);

                    while (currentFrame < videoMetadata.TotalFrames)
                    {
                        var frame = videoFileReader.ReadVideoFrame();

                        try
                        {
                            if (frame == null)
                            {
                                break;
                            }

                            if (currentFrame / videoMetadata.FrameRate > minutes * 60 + seconds)
                            {
                                Frames.Add(currentFrame, (Bitmap)frame.GetThumbnailImage(0, 0, null, IntPtr.Zero));
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(string.Format("Couldn't queue one of more frames. Width & Height of frame was {0} & {1}", frame.Width, frame.Height), ex);
                        }

                        frame.Dispose();
                        currentFrame++;
                    }
                }
            });
        }

        public void Dispose()
        {
            foreach (var frame in Frames.Values)
            {
                if (frame != null)
                {
                    frame.Dispose();
                }
            }
        }
    }
}
