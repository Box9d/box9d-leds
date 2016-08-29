using System;
using Box9.Leds.Core.Configuration;
using Box9.Leds.FcClient;

namespace Box9.Leds.Video
{
    public class ClientServer
    {
        public IClientWrapper Client { get; }

        public ServerConfiguration ServerConfiguration { get; }

        public Guid Id { get; }

        public ClientServer(IClientWrapper clientWrapper, ServerConfiguration serverConfiguration)
        {
            this.Client = clientWrapper;
            this.ServerConfiguration = serverConfiguration;
            Id = Guid.NewGuid();
        }
    }
}
