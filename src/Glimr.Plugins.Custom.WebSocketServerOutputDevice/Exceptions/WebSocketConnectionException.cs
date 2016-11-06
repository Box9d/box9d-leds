using System;
using System.Net;

namespace Glimr.Plugins.Custom.WebSocketServerOutputDevice.Exceptions
{
    public class WebSocketConnectionException : Exception
    {
        public WebSocketConnectionException(IPAddress address, int port)
            : base (string.Format("Web socket at 'ws://{0}:{1}' was expected to be listening but it was not. Did you open the connection first?", address, port))
        {
        }
    }
}
