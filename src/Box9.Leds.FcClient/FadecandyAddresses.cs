using System;
using System.Collections.Generic;

namespace Box9.Leds.FcClient
{
    public static class FadecandyAddresses
    {
        /// <summary>
        /// Returns a list of IP Addresses on the local default subnet 192.168.0/24, and the loopback IP (127.0.0.1) all on port 7890
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public static IEnumerable<string> DefaultAddressRange()
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
