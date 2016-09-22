using System;
using System.Net;
using System.Threading.Tasks;
using Box9.Leds.FcClient.Messages.ConnectedDevices;

namespace Box9.Leds.FcClient
{
    public class WsClientValidator
    {
        public async Task<bool> IsServerConnected(IPAddress ipAddress, int port )
        {
            try
            {
                using (IClientWrapper client = new WsClientWrapper(new Uri(string.Format("ws://{0}:{1}", ipAddress, port))))
                {
                    await client.ConnectAsync();
                    await client.SendMessage(new ConnectedDevicesRequest());
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
