using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Box9.Leds.Core.Messages.ConnectedDevices;
using Box9.Leds.Core.Messages.ServerInfo;

namespace Box9.Leds.FcClient.Search
{
    public class ClientSearch
    {
        public int TotalIPSearches { get; private set; }

        public delegate void ServerFoundHandler(IPAddress client, IEnumerable<ConnectedDeviceResponse> devices);
        public event ServerFoundHandler ServerFound;

        public delegate void SearchStatusChangedHandler(SearchStatus status);
        public event SearchStatusChangedHandler SearchStatusChanged;

        public delegate void IPAddressSearchedHandler();
        public event IPAddressSearchedHandler IPAddressSearched;

        public ClientSearch()
        {
            ServerFound += ServerFoundHandle;
            IPAddressSearched += IPAddressSearchedHandle;
            SearchStatusChanged += SearchStatusChangedHandle;

            TotalIPSearches = 255; // Assume this is the normal subnet range 1-254 and 1 extra for the loopbackIP
        }

        public void SearchForFadecandyServers(int port, CancellationToken token)
        {
            SearchStatusChanged(SearchStatus.Searching);

            var localIp = GetLocalIPAddress().MapToIPv4().ToString();
            var localIpComponents = localIp.Split('.');

            var subnet = string.Format("{0}.{1}.{2}.", localIpComponents[0], localIpComponents[1], localIpComponents[2]);

            var searches = new List<Search>();

            // Search on loopback IP
            searches.Add(new Search(IPAddress.Loopback, port));

            // Also search on all subnet addresses
            for (int i = 1; i < TotalIPSearches; i++)
            {
                searches.Add(new Search(IPAddress.Parse(subnet + i), port));
            }

            var searchTasks = searches.Select(s => Task.Run((async () => await SearchIP(s.IPAddress, s.Port))));

            try
            {
                Task.WaitAll(searchTasks.ToArray(), token);
                SearchStatusChanged(SearchStatus.Finished);
            }
            catch (OperationCanceledException)
            {
                SearchStatusChanged(SearchStatus.Cancelled);
            }
        }

        private static IPAddress GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList.OrderBy(a => a.ToString()))
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip;
                }
            }

            throw new Exception("Local IP Address Not Found!");
        }

        private async Task SearchIP(IPAddress ipAddress, int port)
        {
            try
            {
                IClientWrapper client = new WsClientWrapper(new Uri(string.Format("ws://{0}:{1}", ipAddress, port)));
                await client.ConnectAsync();
                var devices = await client.SendMessage(new ConnectedDevicesRequest());
                ServerFound(ipAddress, devices.Devices);
            }
            catch
            {
            }

            IPAddressSearched();
        }

        private void ServerFoundHandle(IPAddress client, IEnumerable<ConnectedDeviceResponse> devices)
        {
        }

        private void IPAddressSearchedHandle()
        {
        }

        private void SearchStatusChangedHandle(SearchStatus status)
        {
        }
    }
}
