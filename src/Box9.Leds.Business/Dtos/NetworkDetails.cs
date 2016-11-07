using System.Collections.Generic;
using System.Linq;
using Box9.Leds.Business.Services;

namespace Box9.Leds.Business.Dtos
{
    public class NetworkDetails
    {
        public IEnumerable<NetworkDeviceDetails> Devices { get; set; }

        internal NetworkDetails(INetworkDetails details)
        {
            Devices = details.Devices.Select(d => new NetworkDeviceDetails(d));
        }

        public NetworkDetails()
        {
            Devices = new List<NetworkDeviceDetails>();
        }
    }
}
