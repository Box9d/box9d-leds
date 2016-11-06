using System;

namespace Box9.Leds.Business.Services
{
    public class DdwrtNetworkService : INetworkService
    {
        public INetworkDetails GetNetworkDetails()
        {
            return new DdwrtNetworkDetails("192.168.0.1");
        }
    }
}
