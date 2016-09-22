using System;
using Box9.Leds.Business.Configuration;

namespace Box9.Leds.Video
{
    public interface IVideoTransformer : IDisposable
    {
        VideoData ExtractAndSaveTransformedVideoInChunks(ServerConfiguration serverConfiguration);
    }
}
