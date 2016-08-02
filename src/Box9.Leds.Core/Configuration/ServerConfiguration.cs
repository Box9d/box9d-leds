using System;
using System.Collections.Generic;
using System.Net;
using Box9.Leds.Core.Coordination;
using Box9.Leds.Core.Servers;
using Newtonsoft.Json;

namespace Box9.Leds.Core.Configuration
{
    public class ServerConfiguration
    {
        public string IPAddress { get; set; }

        public ServerType ServerType { get; set; }

        public Guid Id { get; set; }

        public int XPixels { get; set; }

        public int YPixels { get; set; }

        public List<PixelMapping> PixelMappings { get; set; }

        public int Port { get; set; }

        public ServerVideoConfiguration VideoConfiguration { get; set; }

        public ServerConfiguration(Guid? id = null)
        {
            Id = id.HasValue ? id.Value : Guid.NewGuid();

            PixelMappings = new List<PixelMapping>();
        }

        public override string ToString()
        {
            return ServerType == ServerType.FadeCandy ? IPAddress.ToString() : "Display only server " + Id;
        }
    }
}
