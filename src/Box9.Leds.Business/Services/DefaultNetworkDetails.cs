using System.Collections.Generic;

namespace Box9.Leds.Business.Services
{
    public class DefaultNetworkDetails : INetworkDetails
    {
        public IEnumerable<INetworkDeviceDetails> Devices { get; private set; }

        internal DefaultNetworkDetails()
        {
        }
    }
}
