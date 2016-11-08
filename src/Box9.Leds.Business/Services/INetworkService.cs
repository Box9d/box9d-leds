using System.Threading;
using Box9.Leds.Business.Dtos;

namespace Box9.Leds.Business.Services
{
    public interface INetworkService
    {
        NetworkDetails GetDdwrtNetworkDetails(string routerIpAddress);

        NetworkDetails GetPingedNetworkDetails(CancellationToken token);

        NetworkDetails GetDdwrtTestNetworkDetails();

        bool IsFadecandyDevice(NetworkDeviceDetails networkDevice);
    }
}
