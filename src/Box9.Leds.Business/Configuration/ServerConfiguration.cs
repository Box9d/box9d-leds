using System.Collections.Generic;
using Box9.Leds.Business.Dtos;
using Box9.Leds.Core.UpdatePixels;

namespace Box9.Leds.Business.Configuration
{
    public class ServerConfiguration
    {
        public NetworkDeviceDetails NetworkDeviceDetails { get; set; }

        public int XPixels { get; set; }

        public int YPixels { get; set; }

        public List<PixelInfo> PixelMappings { get; set; }

        public ServerVideoConfiguration VideoConfiguration { get; set; }

        public int Port { get; set; }

        public ServerConfiguration()
        {
            NetworkDeviceDetails = new NetworkDeviceDetails();
            PixelMappings = new List<PixelInfo>();
            VideoConfiguration = new ServerVideoConfiguration();
        }

        public override string ToString()
        {
            return NetworkDeviceDetails.ToString();
        }
    }
}
