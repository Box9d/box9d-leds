using System;
using System.Collections.Generic;

namespace Box9.Leds.Business.Services
{
    public class TestDdwrtNetworkDetails : DdwrtNetworkDetails
    {
        internal TestDdwrtNetworkDetails()
        {
            devices = new List<DdwrtNetworkDeviceDetails>();
            devices.Add(new DdwrtNetworkDeviceDetails
            {
                DeviceName = "RickPC",
                IPAddress = "127.0.0.1",
                SignalStrengthPercentage = new Random().Next(0, 100)
            });
            devices.Add(new DdwrtNetworkDeviceDetails
            {
                DeviceName = "TestDevice",
                IPAddress = "127.0.0.1",
                SignalStrengthPercentage = new Random().Next(0, 100)
            });
        }
    }
}
