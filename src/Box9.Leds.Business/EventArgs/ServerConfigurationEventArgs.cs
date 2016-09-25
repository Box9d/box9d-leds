using Box9.Leds.Business.Configuration;
using Box9.Leds.Core.EventArgs;

namespace Box9.Leds.Business.EventsArguments
{
    public class ServerConfigurationEventArgs : CustomTypeEventArgs<ServerConfiguration>
    {
        public ServerConfigurationEventArgs(ServerConfiguration value) : base(value)
        {
        }
    }
}
