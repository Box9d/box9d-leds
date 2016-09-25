using System;
using System.Collections.Generic;

namespace Box9.Leds.FcClient
{
    public static class FadecandyUris
    {
        /// <summary>
        /// Returns a list of IP Addresses on the local default subnet 192.168.0/24, and the loopback IP (127.0.0.1) all on port 7890
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public static Uri[] DefaultHttp()
        {
            var uris = new List<Uri>();
            uris.Add(new Uri("http://127.0.0.1:7890"));

            for (int i = 1; i < 256; i++)
            {
                uris.Add(new Uri(string.Format("http://192.168.0.{0}:7890", i)));
            }

            return uris.ToArray();
        }
    }
}
