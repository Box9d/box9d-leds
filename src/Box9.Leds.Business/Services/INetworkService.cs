using System.Threading;
using Box9.Leds.Business.Dtos;

namespace Box9.Leds.Business.Services
{
    public interface INetworkService
    {
        NetworkDetails GetNetworkDetails(string routerIpAddress, CancellationToken token);

        bool IsFadecandyDevice(NetworkDeviceDetails networkDevice);
    }
}
