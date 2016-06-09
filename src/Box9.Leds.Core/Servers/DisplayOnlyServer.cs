using System;

namespace Box9.Leds.Core.Servers
{
    public class DisplayOnlyServer : ServerBase
    {
        public DisplayOnlyServer()
            : base(ServerType.DisplayOnly)
        {
        }

        public override string ToString()
        {
            return "Display only server";
        }
    }
}
