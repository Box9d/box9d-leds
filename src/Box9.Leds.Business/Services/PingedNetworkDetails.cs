using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;

namespace Box9.Leds.Business.Services
{
    public class PingedNetworkDetails : INetworkDetails
    {
        private List<PingedNetworkDevice> pingedNetworkDevices;
        private int pingedDevices;

        public IEnumerable<INetworkDeviceDetails> Devices
        {
            get
            {
                return pingedNetworkDevices;
            }
        }

        internal PingedNetworkDetails(IEnumerable<string> ipAddresses, CancellationToken token)
        {
            pingedNetworkDevices = new List<PingedNetworkDevice>();
            pingedDevices = 0;

            var total = ipAddresses.Count();
            var pingTimeoutInMilliseconds = 250;
            var searchTimeoutSeconds = 5;
            var queue = new Queue<string>(ipAddresses);

            var searchTimeoutStopwatch = new Stopwatch();
            searchTimeoutStopwatch.Start();
            while (!token.IsCancellationRequested && queue.Any())
            {
                var ipAddress = queue.Dequeue();
                var ping = new Ping();
                ping.PingCompleted += (sender, args) => 
                {
                    if (args.Reply.Status == IPStatus.Success)
                    {
                        string host = null;

                        try
                        {
                            IPHostEntry entry = Dns.GetHostEntry(args.Reply.Address);
                            host = entry.HostName;
                        }
                        catch (Exception)
                        {
                        }

                        pingedNetworkDevices.Add(new PingedNetworkDevice(args.Reply.Address.ToString(), host));
                    }

                    pingedDevices++;
                };
                ping.SendAsync(IPAddress.Parse(ipAddress), pingTimeoutInMilliseconds, ipAddress);
            }

            while (pingedDevices < ipAddresses.Count() && searchTimeoutStopwatch.ElapsedMilliseconds < searchTimeoutSeconds * 1000)
            {
                Thread.Sleep(100);
            }
        }
    }
}
