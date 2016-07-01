using System.Net;

namespace Box9.Leds.Core.Configuration
{
    public class ServerConfiguration
    {
        public int XPixels { get; set; }

        public int YPixels { get; set; }

        public IPAddress IPAddress { get; set; }

        public int Port { get; set; }

        public string VideoChunkedStorageTable { get; set; }

        public string VideoChunkedStorageKey { get; set; }
    }
}
