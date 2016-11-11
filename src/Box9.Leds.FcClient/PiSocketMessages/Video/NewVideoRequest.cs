using Box9.Leds.Core.Validation;
using Newtonsoft.Json;

namespace Box9.Leds.FcClient.PiSocketMessages.Video
{
    public class NewVideoRequest : IPiSocketRequest<NewVideoResponse>
    {
        [JsonProperty(PropertyName = "type")]
        public string Type
        {
            get
            {
                return PiSocketRequestType.NewVideo;
            }
        }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; private set; }

        public NewVideoRequest(string name)
        {
            Guard.NotNullOrEmpty(name);

            Name = name;
        }
    }
}
