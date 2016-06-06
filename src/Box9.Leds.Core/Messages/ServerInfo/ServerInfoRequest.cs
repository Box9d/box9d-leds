﻿using Newtonsoft.Json;

namespace Box9.Leds.Core.Messages.ServerInfo
{
    public class ServerInfoRequest : IJsonRequest<ServerInfoResponse>
    {
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        public ServerInfoRequest()
        {
            Type = "server_info";
        }
    }
}
