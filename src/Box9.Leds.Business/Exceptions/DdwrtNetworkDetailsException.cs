using System;

namespace Box9.Leds.Business.Exceptions
{
    public class DdwrtNetworkDetailsException : Exception
    {
        public DdwrtNetworkDetailsException(Exception innerException)
            : base("Unable to parse DD-WRT connected devices", innerException)
        {
        }
    }
}
