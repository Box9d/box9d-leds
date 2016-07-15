using System;
using System.Net;
using Box9.Leds.Core.Servers;
using Newtonsoft.Json;

namespace Box9.Leds.Core.Configuration
{
    public class ServerConfiguration
    {
        private string ipAddress;

        public ServerType ServerType { get; set; }

        public Guid Id { get; set; }

        public int XPixels { get; set; }

        public int YPixels { get; set; }

        [JsonIgnore]
        public IPAddress IPAddress
        {
            get
            {
                return !string.IsNullOrEmpty(ipAddress)
                    ? IPAddress.Parse(ipAddress)
                    : null;
            }
            set
            {
                ipAddress = value.ToString();
            }
        }

        public int Port { get; set; }

        public ServerVideoConfiguration VideoConfiguration { get; set; }

        public ServerConfiguration(Guid? id = null)
        {
            Id = id.HasValue ? id.Value : Guid.NewGuid();
        }

        public override string ToString()
        {
            return ServerType == ServerType.FadeCandy ? IPAddress.ToString() : "Display only server " + Id;
        }
    }
}
