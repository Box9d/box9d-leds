using Box9.Leds.FcClient.PiSocketMessages.Video;
using Newtonsoft.Json;

namespace Box9.Leds.FcClient.PiSocketMessages.Playback
{
    public class PlayRequest : IPiSocketRequest<bool>
    {
        [JsonProperty("type")]
        public string Type { get { return PiSocketRequestType.Play;  } }

        [JsonProperty("video")]
        public VideoResponse Video { get; internal set; }

        [JsonProperty("startTimeMilliseconds")]
        public int StartTimeMilliseconds { get; internal set; }

        public PlayRequest(VideoResponse video, int startTimeMilliseconds)
        {
            Video = video;
            StartTimeMilliseconds = startTimeMilliseconds;
        }
    }
}
