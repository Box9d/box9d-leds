using Box9.Leds.FcClient.PiSocketMessages.Video;
using Newtonsoft.Json;

namespace Box9.Leds.FcClient.PiSocketMessages.Playback
{
    public class StopRequest : IPiSocketRequest<bool>
    {
        [JsonProperty("type")]
        public string Type { get { return PiSocketRequestType.Stop; } }

        [JsonProperty("video")]
        public VideoResponse Video { get; internal set; }

        public StopRequest(VideoResponse video)
        {
            Video = video;
        }
    }
}
