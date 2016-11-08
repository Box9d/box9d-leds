using System.Collections.Generic;
using System.Linq;
using Box9.Leds.Business.Services.DdwrtRawDataParsing;

namespace Box9.Leds.Business.Services
{
    public class DdwrtNetworkDetails : INetworkDetails
    {
        protected List<DdwrtNetworkDeviceDetails> devices;

        public IEnumerable<INetworkDeviceDetails> Devices
        {
            get
            {
                return devices;
            }
        }

        internal DdwrtNetworkDetails()
        {
            devices = new List<DdwrtNetworkDeviceDetails>();
        }

        internal DdwrtNetworkDetails(string rawData)
        {
            devices = new List<DdwrtNetworkDeviceDetails>();           

            var activeWirelessDevices = new ActiveWirelessDevicesParse(rawData);
            var dhcpLeases = new DhcpLeasesParse(rawData);

            foreach (var activeWirlessDevice in activeWirelessDevices.ActiveDevices)
            {
                var dhcpLease = dhcpLeases.DhcpLeases.Single(l => l.MacAddress == activeWirlessDevice.MacAddress);

                var deviceDetails = new DdwrtNetworkDeviceDetails
                {
                    DeviceName = dhcpLease.DeviceName,
                    IPAddress = dhcpLease.IpAddress,
                    MacAddress = activeWirlessDevice.MacAddress,
                    SignalStrengthPercentage = activeWirlessDevice.SignalStrengthPercentage
                };

                devices.Add(deviceDetails);
            }
        }
    }
}
