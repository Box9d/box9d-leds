using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Box9.Leds.FcClient.Search
{
    public class DeviceDetails
    {
        public bool Connected { get; private set; }

        public string SSID { get; private set; }

        public int SignalStrengthPercentage { get; private set; }

        private string ipAddress;

        internal DeviceDetails(string ipAddress)
        {
            this.ipAddress = ipAddress;
        }
    }
}
