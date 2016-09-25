using AForge.Video.FFMPEG;
using Box9.Leds.Business.Dtos;

namespace Box9.Leds.Business.Services
{
    public class VideoMetadataService : IVideoMetadataService
    {
        public VideoMetadata GetMetadata(string videoFilePath)
        {
            using (var videoFileReader = new VideoFileReader())
            {
                videoFileReader.Open(videoFilePath);

                return new VideoMetadata(videoFilePath, videoFileReader.FrameRate, videoFileReader.FrameCount, videoFileReader.Height, videoFileReader.Width);
            }
        }
    }
}
