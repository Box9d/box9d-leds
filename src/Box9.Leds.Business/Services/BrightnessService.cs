using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Box9.Leds.Business.Configuration;
using Box9.Leds.FcClient;
using Box9.Leds.FcClient.Messages.ColorCorrection;
using Box9.Leds.FcClient.Messages.ConnectedDevices;

namespace Box9.Leds.Business.Services
{
    public class BrightnessService : IBrightnessService
    {
        public async Task AdjustBrightness(int brightnessPercentage, IEnumerable<ServerConfiguration> serverConfigurations)
        {
            foreach (var server in serverConfigurations)
            {
                using (var client = new WsClientWrapper(new Uri(string.Format("ws://{0}:{1}", server.IPAddress, server.Port))))
                {
                    await client.ConnectAsync();

                    var serverInfo = await client.SendMessage(new ConnectedDevicesRequest());
                    var fadecandySerials = serverInfo.Devices.Select(d => d.Serial);

                    foreach (var serial in fadecandySerials)
                    {
                        await client.SendMessage(new ColorCorrectionRequest(brightnessPercentage, serial));
                    }

                    await client.CloseAsync();
                }   
            }
        }
    }
}
