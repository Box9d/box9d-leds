using System.Collections.Generic;
using System.Linq;
using AForge.Video.FFMPEG;
using Box9.Leds.Business.Configuration;
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

                    foreach (var pixelMapping in serverConfiguration.PixelMappings.OrderBy(pm => pm.Order))
                    {
                        var x = startX + (pixelMapping.X * xPixelGap);
                        var y = startY + (pixelMapping.Y * yPixelGap);

                        x = x >= finishX ? finishX -1 : x;
                        y = y >= finishY ? finishY -1 : y;

                        frameVideoData.PixelInfo.Add(new PixelInfo
                        {
                            X = pixelMapping.X,
                            Y = pixelMapping.Y,
                            Color = frame.GetPixel(x, y)
                        });
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
