using AForge.Video.FFMPEG;
using Box9.Leds.Business.Dtos;
using MediaInfoDotNet;

namespace Box9.Leds.Business.Services
{
    public class VideoMetadataService : IVideoMetadataService
    {
        public VideoMetadata GetMetadata(string videoFilePath)
        {
            var frameRate = (double)new MediaFile(videoFilePath).Video[0].FrameRate;

            using (var videoFileReader = new VideoFileReader())
            {
                videoFileReader.Open(videoFilePath);

                return new VideoMetadata(videoFilePath, frameRate, videoFileReader.FrameCount, videoFileReader.Height, videoFileReader.Width);
            }
        }
    }
}
