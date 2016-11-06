namespace Box9.Leds.Business.Services
{
    public class DdwrtNetworkDeviceDetails : INetworkDeviceDetails
    {
        public string DeviceName { get; internal set; }

        public string IPAddress { get; internal set; }

        public string MacAddress { get; internal set; }

        public int? SignalStrengthPercentage { get; internal set; }

        internal DdwrtNetworkDeviceDetails()
        {
        }
    }
}
