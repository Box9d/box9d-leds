using System;
using System.Collections.Generic;
using System.Linq;

namespace Box9.Leds.Core.Configuration
{
    public class LedConfiguration
    {
        public List<ServerConfiguration> Servers { get; set; }

        public VideoConfiguration VideoConfig { get; set; }

        public AudioConfiguration AudioConfig { get; set; }

        public LedConfiguration()
        {
            Servers = new List<ServerConfiguration>();
            AudioConfig = new AudioConfiguration();
        }

        public string GetServerVideoStorageKey(ServerConfiguration serverConfig)
        {
            if (!Servers.Contains(serverConfig))
            {
                throw new InvalidOperationException("Server configuration is not part of this LED configuration");
            }

            string storageKey = string.Empty;
            storageKey += serverConfig.XPixels;
            storageKey += serverConfig.YPixels;
            storageKey += serverConfig.VideoConfiguration.StartAtXPercent;
            storageKey += serverConfig.VideoConfiguration.StartAtYPercent;
            storageKey += serverConfig.VideoConfiguration.XPercent;
            storageKey += serverConfig.VideoConfiguration.YPercent;
            storageKey += VideoConfig.SourceFilePath
                .Replace("\\", string.Empty)
                .Replace(":", string.Empty);
            storageKey += serverConfig.PixelMappings
                .Select(pm => pm.Order.ToString() + pm.X.ToString() + pm.Y.ToString())
                .Aggregate((prev, curr) => prev += curr);

            return storageKey.GetHashCode().ToString();
        }

        public string GetAudioStorageKey()
        {
            if (string.IsNullOrEmpty(AudioConfig.SourceFilePath))
            {
                throw new InvalidOperationException("Cannot get storage key for unspecified audio source file path");
            }

            return AudioConfig.SourceFilePath.GetHashCode().ToString();
        }
    }
}
