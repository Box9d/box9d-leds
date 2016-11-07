namespace Box9.Leds.Business.Services
{
    public class PingedNetworkDevice : INetworkDeviceDetails
    {
        public string DeviceName { get; private set; }

        public string IPAddress { get; private set; }

        public string MacAddress { get; private set; }

        public int? SignalStrengthPercentage { get; private set; }

        internal PingedNetworkDevice(string ipAddress)
        {
            IPAddress = ipAddress;

            SignalStrengthPercentage = null;
            MacAddress = null;
            DeviceName = null;
        }
    }
}
