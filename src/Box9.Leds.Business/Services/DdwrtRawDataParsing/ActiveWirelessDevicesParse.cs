using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace Box9.Leds.Business.Services.DdwrtRawDataParsing
{
    public class ActiveWirelessDevicesParse
    {
        internal IEnumerable<ActiveWirelessDeviceParse> ActiveDevices { get; private set; }

        internal ActiveWirelessDevicesParse(string rawData)
        {
            var connectedDevicesRawData = rawData
                .Split(new string[] { "{active_wireless::" }, StringSplitOptions.None)[1];

            connectedDevicesRawData = connectedDevicesRawData
                .Remove(connectedDevicesRawData.IndexOf('}'));

            var devicesDataBlocks = connectedDevicesRawData
                .Split(new string[] { "," }, StringSplitOptions.None)
                .Select(s => s.Trim());

            var dataBlockCounter = 0;
            var dataBlocksPerDevice = 10;
            var activeDevices = new List<ActiveWirelessDeviceParse>();
            var deviceDataBlocks = new List<string>();

            foreach (var dataBlock in devicesDataBlocks)
            {
                dataBlockCounter++;
                deviceDataBlocks.Add(dataBlock);

                if ((dataBlockCounter != 0 && dataBlockCounter % dataBlocksPerDevice == 0) 
                    || dataBlockCounter == devicesDataBlocks.Count())
                {
                    activeDevices.Add(new ActiveWirelessDeviceParse(deviceDataBlocks));
                    deviceDataBlocks = new List<string>();
                }
            }

            ActiveDevices = activeDevices;
        }
    }
}
