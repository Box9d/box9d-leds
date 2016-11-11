using System.Collections.Generic;
using Box9.Leds.FcClient.PiSocketMessages.Video;
using Newtonsoft.Json;

namespace Box9.Leds.FcClient.PiSocketMessages.Frames
{
    public class AddFrameRequest : IPiSocketRequest<bool>
    {
        [JsonProperty(PropertyName = "milliseconds")]
        public int Milliseconds { get; internal set; }

        [JsonProperty(PropertyName = "pixels")]
        public List<Pixel> Pixels { get; internal set; }

        [JsonProperty(PropertyName = "video")]
        public NewVideoResponse Video { get; internal set; }

        [JsonProperty("type")]
        public string Type
        {
            get
            {
                return PiSocketRequestType.AddFrame;
            }
        }

        public AddFrameRequest(NewVideoResponse video, int milliseconds, List<Pixel> pixels)
        {
            Video = video;
            Milliseconds = milliseconds;
            Pixels = pixels;
        }
    }
}
