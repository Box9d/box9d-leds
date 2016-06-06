using Newtonsoft.Json;

namespace Box9.Leds.Core.Messages.ConnectedDevices
{
    public class ConnectedDeviceResponse
    {
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "serial")]
        public string Serial { get; set; }

        [JsonProperty(PropertyName = "timestamp")]
        public string Timestamp { get; set; }

        [JsonProperty(PropertyName = "version")]
        public string Version { get; set; }

        [JsonProperty(PropertyName = "bcd_version")]
        public string BcdVersion { get; set; }
    }
}
