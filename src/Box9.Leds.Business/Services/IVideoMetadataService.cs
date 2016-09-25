using Box9.Leds.Business.Dtos;

namespace Box9.Leds.Business.Services
{
    public interface IVideoMetadataService
    {
        VideoMetadata GetMetadata(string videoFilePath);
    }
}
