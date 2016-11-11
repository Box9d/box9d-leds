using System.Collections.Generic;
using Newtonsoft.Json;

namespace Box9.Leds.FcClient.FadecandyMessages.ConnectedDevices
{
    public class ConnectedDevicesResponse
    {
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "devices")]
        public List<ConnectedDeviceResponse> Devices { get; set; }

        public ConnectedDevicesResponse()
        {
            Devices = new List<ConnectedDeviceResponse>();
        }
    }
}
