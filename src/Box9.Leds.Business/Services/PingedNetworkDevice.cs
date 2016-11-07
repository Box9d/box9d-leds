namespace Box9.Leds.Business.Services
{
    public class PingedNetworkDevice : INetworkDeviceDetails
    {
        public string DeviceName { get; private set; }

        public string IPAddress { get; private set; }

        public string MacAddress { get; private set; }

        public int? SignalStrengthPercentage { get; private set; }

        internal PingedNetworkDevice(string ipAddress, string deviceName = null)
        {
            IPAddress = ipAddress;
            DeviceName = deviceName;

            SignalStrengthPercentage = null;
            MacAddress = null;
        }
    }
}
