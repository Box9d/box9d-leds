﻿namespace Box9.Leds.Core.Messages.ConnectedDevices
{
    public class ConnectedDevicesRequest : IJsonRequest<ConnectedDevicesResponse>
    {
        public string type { get; set; }

        public ConnectedDevicesRequest()
        {
            type = "list_connected_devices";
        }
    }
}
