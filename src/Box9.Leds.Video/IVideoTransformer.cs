using System;
using Box9.Leds.Core.LedLayouts;

namespace Box9.Leds.Video
{
    public interface IVideoTransformer
    {
        VideoData ExtractAndSaveTransformedVideoInChunks(int framesPerStorageKey, LedLayout ledLayout);

        VideoAudioData ExtractAndSaveAudio();
    }
}
