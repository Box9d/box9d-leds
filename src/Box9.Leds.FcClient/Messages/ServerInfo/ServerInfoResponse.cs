using Newtonsoft.Json;

namespace Box9.Leds.FcClient.Messages.ServerInfo
{
    public class ServerInfoResponse
    {
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "version")]
        public string Version { get; set; }

        [JsonProperty(PropertyName = "config")]
        public ServerInfoConfigResponse Config { get; set; }
    }
}
