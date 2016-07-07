using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Box9.Leds.Core.Messages.ConnectedDevices;

namespace Box9.Leds.FcClient
{
    public class WsClientValidator
    {
        public bool IsServerConnected(IPAddress ipAddress, int port )
        {
            try
            {
                using (IClientWrapper client = new WsClientWrapper(new Uri(string.Format("ws://{0}:{1}", ipAddress, port))))
                {
                    client.ConnectAsync().Wait();
                    client.SendMessage(new ConnectedDevicesRequest()).Wait();
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
