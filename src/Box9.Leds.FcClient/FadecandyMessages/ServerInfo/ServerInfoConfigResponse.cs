using System.Collections.Generic;
using Newtonsoft.Json;

namespace Box9.Leds.FcClient.FadecandyMessages.ServerInfo
{
    public class ServerInfoConfigResponse
    {
        [JsonProperty(PropertyName = "listen")]
        public string[] Listen { get; set; }

        [JsonProperty(PropertyName = "verbose")]
        public bool Verbose { get; set; }

        [JsonProperty(PropertyName = "color")]
        public ColorResponse Color { get; set; }

        [JsonProperty(PropertyName = "devices")]
        public List<DeviceResponse> ConnectedDevices { get; set; }

        // Custom properties
        [JsonIgnore]
        public string IP
        {
            get
            {
                return Listen != null && Listen.Length > 0 
                        ? Listen[0]
                        : null;
            }
        }

        [JsonIgnore]
        public string Port
        {
            get
            {
                return Listen != null && Listen.Length > 1
                    ? Listen[1]
                    : null;
            }
        }

        public ServerInfoConfigResponse()
        {
            ConnectedDevices = new List<DeviceResponse>();
        }
    }
}
