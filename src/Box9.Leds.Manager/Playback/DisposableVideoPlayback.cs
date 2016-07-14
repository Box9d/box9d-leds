using Box9.Leds.Core.Configuration;
using Box9.Leds.Video;

namespace Box9.Leds.Manager.Playback
{
    public class DisposableVideoPlayback
    {
        public VideoData VideoData { get; private set; }

        public ServerConfiguration ServerConfig { get; private set; }

        public DisposableVideoPlayback(VideoData videoData, ServerConfiguration serverConfiguration)
        {
            VideoData = videoData;
            ServerConfig = serverConfiguration;
        }
    }
}
