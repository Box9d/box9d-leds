﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using Box9.Leds.Core.EventsArguments;

namespace Box9.Leds.FcClient.Search
{
    public class ClientSearch : IClientSearch
    {
        public event EventHandler<StringEventArgs> ServerFound;

        public event EventHandler<IntegerEventArgs> PercentageSearched;

        private int searched;
        private int total;
        private CancellationToken token;

        public ClientSearch()
        {
        }

        public void SearchForFadecandyServers(IEnumerable<string> ipAddresses, int pingTimeoutInMilliseconds, CancellationToken cancellationToken)
        {
            searched = 0;
            token = cancellationToken;
            total = ipAddresses.Count();
            var queue = new Queue<string>(ipAddresses);

            while (!cancellationToken.IsCancellationRequested && queue.Any())
            {
                var ipAddress = queue.Dequeue();
                var ping = new Ping();
                ping.PingCompleted += PingCompleted;
                ping.SendAsync(IPAddress.Parse(ipAddress), pingTimeoutInMilliseconds, ipAddress);
            }

            if (!cancellationToken.IsCancellationRequested)
            {
                PercentageSearched(null, new IntegerEventArgs(0));
            }
        }

        public DeviceDetails GetDeviceDetails(string ipAddress)
        {
            throw new NotImplementedException();
        }

        private async void PingCompleted(object sender, PingCompletedEventArgs args)
        {
            if (token.IsCancellationRequested)
            {
                return;
            }

            if (args.Reply.Status == IPStatus.Success)
            {
                var request = WebRequest.Create(string.Format("http://{0}:{1}", args.Reply.Address.ToString(), 7890));
                request.Method = "GET";

                try
                {
                    using (var response = (HttpWebResponse)(await request.GetResponseAsync()))
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            ServerFound(null, new StringEventArgs(args.Reply.Address.ToString()));
                        }

                        response.Close();
                    }
                }
                catch
                {
                }
            }

            searched++;
            var percentageSearchedValue = (searched * 100) / total;
            PercentageSearched(null, new IntegerEventArgs(percentageSearchedValue));
        }
    }
}
