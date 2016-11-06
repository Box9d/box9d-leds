using System;
using System.Net;

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
                    client.Connect();
                    return client.State == System.Net.WebSockets.WebSocketState.Open;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
