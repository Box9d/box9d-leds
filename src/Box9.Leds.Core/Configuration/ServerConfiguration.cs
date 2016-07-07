using System;
using System.Net;
using Box9.Leds.Core.Servers;

namespace Box9.Leds.Core.Configuration
{
    public class ServerConfiguration
    {
        public ServerType ServerType { get; set; }

        public Guid Id { get; set; }

        public int XPixels { get; set; }

        public int YPixels { get; set; }

        public IPAddress IPAddress { get; set; }

        public int Port { get; set; }

        public ServerVideoConfiguration VideoConfiguration { get; set; }

        public string VideoChunkedStorageTable { get; set; }

        public string VideoChunkedStorageKey { get; set; }

        public ServerConfiguration(Guid? id = null)
        {
            Id = id.HasValue ? id.Value : Guid.NewGuid();
        }

        public override string ToString()
        {
            return ServerType == ServerType.FadeCandy ? IPAddress.ToString() : "Display only server " + Id;
        }
    }
}
