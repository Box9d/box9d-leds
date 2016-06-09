namespace Box9.Leds.Core.Servers
{
    public abstract class ServerBase
    {
        public ServerType ServerType { get; }

        public ServerBase(ServerType serverType)
        {
            this.ServerType = serverType;
        }
    }
}
