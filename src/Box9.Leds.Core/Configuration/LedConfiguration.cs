using System.Collections.Generic;

namespace Box9.Leds.Core.Configuration
{
    public class LedConfiguration
    {
        public List<ServerConfiguration> Servers { get; set; }

        public VideoConfiguration VideoConfig { get; set; }

        public LedConfiguration()
        {
            Servers = new List<ServerConfiguration>();
        }
    }
}
