using System;
using Newtonsoft.Json;

namespace Box9.Leds.FcClient.PiSocketMessages.Video
{
    public class VideoResponse
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "createdon")]
        public DateTime CreatedOn { get; set; }
    }
}
