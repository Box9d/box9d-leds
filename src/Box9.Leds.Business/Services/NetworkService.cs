using System;
using System.IO;
using System.Net;
using System.Threading;
using Box9.Leds.Business.Dtos;
using Box9.Leds.Core.Addressing;

namespace Box9.Leds.Business.Services
{
    public class NetworkService : INetworkService
    {
        public NetworkDetails GetNetworkDetails(string routerIpAddress, CancellationToken token)
        {
            try
            {
                var request = WebRequest.CreateHttp(string.Format("http://{0}/{1}", routerIpAddress, "Info.live.htm"));
                request.Method = "GET";

                using (var response = request.GetResponse())
                using (var stream = response.GetResponseStream())
                using (var streamReader = new StreamReader(stream))
                {
                    return new NetworkDetails(new DdwrtNetworkDetails(streamReader.ReadToEnd()));
                }     
            }
            catch (Exception)
            {
                // Default to using ping to find network devices
                return new NetworkDetails(new PingedNetworkDetails(IPAddressing.DefaultIPAddressRange, token));
            }
        }

        public bool IsFadecandyDevice(NetworkDeviceDetails networkDevice)
        {
            var request = WebRequest.Create(string.Format("http://{0}:{1}", networkDevice.IPAddress, 7890));
            request.Method = "GET";

            try
            {
                using (var response = (HttpWebResponse)(request.GetResponse()))
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return true;
                    }

                    response.Close();
                }
            }
            catch
            {
            }

            return false; 
        }
    }
}
