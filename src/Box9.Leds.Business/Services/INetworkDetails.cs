using System.Collections.Generic;

namespace Box9.Leds.Business.Services
{
    public interface INetworkDetails
    { 
        IEnumerable<INetworkDeviceDetails> Devices { get; }
    }
}
