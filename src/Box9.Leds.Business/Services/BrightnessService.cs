﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Box9.Leds.Business.Configuration;
using Box9.Leds.FcClient;
using Box9.Leds.FcClient.FadecandyMessages.ColorCorrection;
using Box9.Leds.FcClient.FadecandyMessages.ConnectedDevices;

namespace Box9.Leds.Business.Services
{
    public class BrightnessService : IBrightnessService
    {
        public void AdjustBrightness(int brightnessPercentage, IEnumerable<ServerConfiguration> serverConfigurations)
        {
            foreach (var server in serverConfigurations)
            {
                using (var client = new FadecandyClientWrapper(new Uri(string.Format("ws://{0}:{1}", server.NetworkDeviceDetails.IPAddress, server.Port))))
                {
                    client.Connect();

                    var serverInfo = client.SendMessage(new ConnectedDevicesRequest());
                    var fadecandySerials = serverInfo.Devices.Select(d => d.Serial);

                    foreach (var serial in fadecandySerials)
                    {
                        client.SendMessage(new ColorCorrectionRequest(brightnessPercentage, serial));
                    }

                    client.Close();
                }   
            }
        }
    }
}
