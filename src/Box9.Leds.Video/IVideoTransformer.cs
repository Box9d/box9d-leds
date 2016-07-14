using System;
using Box9.Leds.Core.Configuration;

namespace Box9.Leds.Video
{
    public interface IVideoTransformer : IDisposable
    {
        VideoData ExtractAndSaveTransformedVideoInChunks(ServerConfiguration serverConfiguration);
    }
}
