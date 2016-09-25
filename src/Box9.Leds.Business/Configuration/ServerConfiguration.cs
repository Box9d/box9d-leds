using System.Collections.Generic;
using Box9.Leds.Core.UpdatePixels;

namespace Box9.Leds.Business.Configuration
{
    public class ServerConfiguration
    {
        public string IPAddress { get; set; }

        public int XPixels { get; set; }

        public int YPixels { get; set; }

        public List<PixelInfo> PixelMappings { get; set; }

        public ServerVideoConfiguration VideoConfiguration { get; set; }

        public int Port { get; set; }

        public ServerConfiguration()
        {
            IPAddress = string.Empty;
            PixelMappings = new List<PixelInfo>();
            VideoConfiguration = new ServerVideoConfiguration();
        }

        public override string ToString()
        {
            return IPAddress.ToString();
        }
    }
}
