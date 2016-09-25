using System.Collections.Generic;
using Newtonsoft.Json;

namespace Box9.Leds.FcClient.Messages.ServerInfo
{
    public class DeviceResponse
    {
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "map")]
        public List<int[]> Map { get; set; }

        public DeviceResponse()
        {
            Map = new List<int[]>();
        }
    }
}
