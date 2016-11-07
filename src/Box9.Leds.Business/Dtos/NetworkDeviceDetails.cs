using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Box9.Leds.Business.Services;

namespace Box9.Leds.Business.Dtos
{
    public class NetworkDeviceDetails
    {
        public string DeviceName { get; set; }

        public int? SignalStrengthPercentage { get; set; }

        public string IPAddress { get; set; }

        public string MacAddress { get; set; }

        internal NetworkDeviceDetails(INetworkDeviceDetails details)
        {
            DeviceName = details.DeviceName;
            SignalStrengthPercentage = details.SignalStrengthPercentage;
            IPAddress = details.IPAddress;
            MacAddress = details.MacAddress;
        }

        public NetworkDeviceDetails()
        {
        }
    }
}
