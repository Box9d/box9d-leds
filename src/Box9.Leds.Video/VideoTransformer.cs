using System;
using System.Collections.Generic;
using System.IO;
using AForge.Video.FFMPEG;
using Box9.Leds.Core.LedLayouts;
using Box9.Leds.Core.UpdatePixels;
using Box9.Leds.DataStorage;

namespace Box9.Leds.Video
{
    public class VideoTransformer : IVideoTransformer
    {
        private string videoFilePath;
        private IChunkedStorageClient<int, FrameVideoData> videoStorageClient;

        public VideoTransformer(IChunkedStorageClient<int, FrameVideoData> videoStorageClient, string videoFilePath)
        {
            this.videoFilePath = videoFilePath;
            this.videoStorageClient = videoStorageClient;
        }

        public VideoAudioData ExtractAndSaveAudio()
        {
            var converter = new NReco.VideoConverter.FFMpegConverter();
            var audioFilePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".mp3");

            string storageKey = Guid.NewGuid().ToString();
            converter.ConvertMedia(videoFilePath, audioFilePath, "mp3");

            return new VideoAudioData
            {
                FilePath = audioFilePath,
                AudioFormat = "mp3"
            };
        }

        public VideoData ExtractAndSaveTransformedVideoInChunks(int framesPerStorageKey, LedLayout ledLayout)
        {
            int chunkCounter = 0;
            var xPixelMultiplier = 0;
            var yPixelMultiplier = 0;

            var storageKeys = new List<int>();
            var chunkedVideoData = new List<FrameVideoData>();

            using (var reader = new VideoFileReader())
            {
                reader.Open(videoFilePath);

                xPixelMultiplier = reader.Width / ledLayout.XNumberOfPixels;
                yPixelMultiplier = reader.Height / ledLayout.YNumberOfPixels;

                for (int f = 0; f < reader.FrameCount; f++)
                {
                    var frame = reader.ReadVideoFrame();
                    var frameVideoData = new FrameVideoData();

                    for (int i = 0; i < ledLayout.XNumberOfPixels; i++)
                    {
                        for (int j = 0; j < ledLayout.YNumberOfPixels; j++)
                        {
                            frameVideoData.PixelInfo.Add(new PixelInfo
                            {
                                X = i,
                                Y = j,
                                Color = frame.GetPixel(i * xPixelMultiplier, j * yPixelMultiplier)
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
    }
}
