using System;
using Box9.Leds.Business.Configuration;
using Box9.Leds.FcClient;

namespace Box9.Leds.Video
{
    public class ClientConfigPair
    {
        public IClientWrapper Client { get; }

        public ServerConfiguration ServerConfiguration { get; }

        public ClientConfigPair(IClientWrapper clientWrapper, ServerConfiguration serverConfiguration)
        {
            Client = clientWrapper;
            ServerConfiguration = serverConfiguration;
        }
    }
}
