using System.Net;

namespace Box9.Leds.Core.Servers
{
    public class FadecandyServer : ServerBase
    {
        public IPAddress IPAddress { get; }

        public int Port { get; }

        public FadecandyServer(IPAddress ipAddress, int port)
            : base(ServerType.FadeCandy)
        {
            this.IPAddress = ipAddress;
            this.Port = port;
        }

        public override string ToString()
        {
            return IPAddress.ToString();
        }
    }
}
