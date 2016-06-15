using System;
using Box9.Leds.Core.LedLayouts;

namespace Box9.Leds.Video
{
    public interface IVideoReader : IDisposable
    {
        VideoData TransformVideo(string videoFilePath, LedLayout ledLayout);

        string ExtractAudioToFile(string videoFilePath);
    }
}
