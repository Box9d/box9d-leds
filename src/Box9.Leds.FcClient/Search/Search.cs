using System.Net;

namespace Box9.Leds.FcClient.Search
{
    internal class Search
    {
        public IPAddress IPAddress { get; private set; }

        public int Port { get; private set; }

        internal Search(IPAddress ipAddress, int port)
        {
            Port = port;
            IPAddress = ipAddress;
        }
    }
}
