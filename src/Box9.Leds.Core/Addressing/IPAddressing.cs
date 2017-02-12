using System.Collections.Generic;

namespace Box9.Leds.Core.Addressing
{
    public class IPAddressing
    {
        public static IEnumerable<string> DefaultIPAddressRange
        {
            get
            {
                var ipAddresses = new List<string>();
                ipAddresses.Add("127.0.0.1");

                for (int i = 1; i < 256; i++)
                {
                    ipAddresses.Add(string.Format("192.168.1.{0}", i));
                }

                return ipAddresses;
            }
        }
    }
}
