using System.Collections.Generic;
using System.Drawing;
using AForge.Video.FFMPEG;
using Box9.Leds.Core.Configuration;
using Box9.Leds.Core.UpdatePixels;
using Box9.Leds.DataStorage;

namespace Box9.Leds.Video
{
    public class VideoTransformer : IVideoTransformer
    {
        private string videoFilePath;
        private IChunkedStorageClient<int, FrameVideoData> videoStorageClient;
        private int framesPerStorageKey;

        public VideoTransformer(IChunkedStorageClient<int, FrameVideoData> videoStorageClient, string videoFilePath)
        {
            this.videoFilePath = videoFilePath;
            this.videoStorageClient = videoStorageClient;
            this.framesPerStorageKey = 100;
        }

        public VideoData ExtractAndSaveTransformedVideoInChunks(ServerConfiguration serverConfiguration)
        {
            int chunkCounter = 0;

            var storageKeys = new List<int>();
            var chunkedVideoData = new List<FrameVideoData>();

            using (var reader = new VideoFileReader())
            {
                reader.Open(videoFilePath);

                var startX = reader.Width * serverConfiguration.VideoConfiguration.StartAtXPercent / 100;
                var startY = reader.Height * serverConfiguration.VideoConfiguration.StartAtYPercent / 100;
                var finishX = startX + (reader.Width * serverConfiguration.VideoConfiguration.XPercent / 100);
                var finishY = startY + (reader.Height * serverConfiguration.VideoConfiguration.YPercent / 100);

                var xPixelGap = (finishX - startX) / serverConfiguration.XPixels;
                var yPixelGap = (finishY - startY) / serverConfiguration.YPixels;

                for (int f = 0; f < reader.FrameCount; f++)
                {
                    var frame = reader.ReadVideoFrame();
                    var frameVideoData = new FrameVideoData();

                    for (int i = 0; i < serverConfiguration.XPixels; i++)
                    {
                        for (int j = 0; j < serverConfiguration.YPixels; j++)
                        {
                            frameVideoData.PixelInfo.Add(new PixelInfo
                            {
                                X = i,
                                Y = j,
                                Color = frame.GetPixel(startX + (i * xPixelGap), startY + (j * yPixelGap))
                            });
                        }
                    }

                    if (chunkCounter == framesPerStorageKey)
                    {
                        storageKeys.Add(storageKeys.Count + 1);
                        videoStorageClient.Save(storageKeys.Count, chunkedVideoData);

                        chunkedVideoData = new List<FrameVideoData>();
                        chunkCounter = 0;
                    }
                    else
                    {
                        chunkedVideoData.Add(frameVideoData);
                        chunkCounter++;
                    }
                }

                if (chunkCounter != 0)
                {
                    storageKeys.Add(storageKeys.Count + 1);
                    videoStorageClient.Save(storageKeys.Count, chunkedVideoData); 
                }

                return new VideoData
                {
                    Framerate = reader.FrameRate,
                    FramesPerStorageKey = framesPerStorageKey,
                    FrameStorageKeys = storageKeys,
                    TotalNumberOfFrames = reader.FrameCount
                };
            }
        }

        public void Dispose()
        {
            if (videoStorageClient != null)
            {
                videoStorageClient.Dispose();
            }
        }
    }
}
