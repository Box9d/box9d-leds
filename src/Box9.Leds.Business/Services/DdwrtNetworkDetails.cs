using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Box9.Leds.Business.Services.DdwrtRawDataParsing;

namespace Box9.Leds.Business.Services
{
    public class DdwrtNetworkDetails : INetworkDetails
    {
        private List<DdwrtNetworkDeviceDetails> devices;

        public IEnumerable<INetworkDeviceDetails> Devices
        {
            get
            {
                return devices;
            }
        }

        internal DdwrtNetworkDetails(string routerIpAddress)
        {
            devices = new List<DdwrtNetworkDeviceDetails>();

            var request = WebRequest.CreateHttp(string.Format("http://{0}/{1}", routerIpAddress, "Info.live.htm"));
            request.Method = "GET";

            string rawData = string.Empty;

            try
            {
                using (var response = request.GetResponse())
                using (var stream = response.GetResponseStream())
                using (var streamReader = new StreamReader(stream))
                {
                    rawData = streamReader.ReadToEnd();
                }

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
            catch (Exception)
            {
                // TODO: Get default network details          
            }
        }
    }
}
