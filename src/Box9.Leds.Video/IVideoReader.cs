using Box9.Leds.Core.LedLayouts;

namespace Box9.Leds.Video
{
    public interface IVideoReader
    {
        VideoData Transform(string videoFilePath, LedLayout ledLayout);
    }
}
