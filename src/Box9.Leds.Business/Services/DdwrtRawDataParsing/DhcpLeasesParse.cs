using System;
using System.Collections.Generic;
using System.Linq;

namespace Box9.Leds.Business.Services.DdwrtRawDataParsing
{
    public class DhcpLeasesParse
    {
        internal IEnumerable<DhcpLeaseParse> DhcpLeases { get; private set; }

        internal DhcpLeasesParse(string rawData)
        {
            var connectedDevicesRawData = rawData
                .Split(new string[] { "{dhcp_leases::" }, StringSplitOptions.None)[1];

            connectedDevicesRawData = connectedDevicesRawData
                .Remove(connectedDevicesRawData.IndexOf('}'));

            var devicesDataBlocks = connectedDevicesRawData
                .Split(new string[] { "," }, StringSplitOptions.None)
                .Select(s => s.Trim());

            var dataBlockCounter = 0;
            var dataBlocksPerDevice = 5;
            var dhcpLeases = new List<DhcpLeaseParse>();
            var dhcpLeaseDataBlocks = new List<string>();

            foreach (var dataBlock in devicesDataBlocks)
            {
                dataBlockCounter++;
                dhcpLeaseDataBlocks.Add(dataBlock);

                if ((dataBlockCounter != 0 && dataBlockCounter % dataBlocksPerDevice == 0)
                    || dataBlockCounter == devicesDataBlocks.Count())
                {
                    dhcpLeases.Add(new DhcpLeaseParse(dhcpLeaseDataBlocks));
                    dhcpLeaseDataBlocks = new List<string>();
                }
            }

            DhcpLeases = dhcpLeases;
        }
    }
}
