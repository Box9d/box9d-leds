using System.Collections.Generic;
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
            var pingTimeoutInMilliseconds = 50;
            var queue = new Queue<string>(ipAddresses);

            while (!token.IsCancellationRequested && queue.Any())
            {
                var ipAddress = queue.Dequeue();
                var ping = new Ping();
                ping.PingCompleted += (sender, args) => 
                {
                    if (args.Reply.Status == IPStatus.Success)
                    {
                        pingedNetworkDevices.Add(new PingedNetworkDevice(args.Reply.Address.ToString()));
                    }

                    pingedDevices++;
                };
                ping.SendAsync(IPAddress.Parse(ipAddress), pingTimeoutInMilliseconds, ipAddress);
            }

            while (pingedDevices != ipAddresses.Count())
            {
                Thread.Sleep(100);
            }
        }
    }
}
