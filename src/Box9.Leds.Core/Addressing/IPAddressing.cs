using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    ipAddresses.Add(string.Format("192.168.0.{0}", i));
                }

                return ipAddresses;
            }
        }
    }
}
