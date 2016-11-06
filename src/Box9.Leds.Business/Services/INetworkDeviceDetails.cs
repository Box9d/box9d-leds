namespace Box9.Leds.Business.Services
{
    public interface INetworkDeviceDetails
    {
        string DeviceName { get; }

        int? SignalStrengthPercentage { get; }

        string IPAddress { get; }

        string MacAddress { get; }
    }
}
