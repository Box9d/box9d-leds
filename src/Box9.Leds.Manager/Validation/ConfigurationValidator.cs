﻿using System.Collections.Generic;
using System.Linq;
using Box9.Leds.Core.Configuration;
using Box9.Leds.FcClient;

namespace Box9.Leds.Manager.Validation
{
    public class ConfigurationValidator : IConfigurationValidator
    {
        public ValidationResult Validate(LedConfiguration configuration)
        {
            var errors = new List<string>();
            errors.AddRange(ValidateAtLeastOneServer(configuration));
            errors.AddRange(ValidateVideoSelected(configuration));
            errors.AddRange(ValidateServerConnections(configuration));

            return new ValidationResult(errors); 
        }

        private List<string> ValidateAtLeastOneServer(LedConfiguration configuration)
        {
            if (configuration.Servers == null || !configuration.Servers.Any())
            {
                return new List<string>
                {
                    "Must have at least one server selected in order to playback video"
                };
            }

            return new List<string>();
        }

        private List<string> ValidateVideoSelected(LedConfiguration configuration)
        {
            if (configuration.VideoConfig == null || string.IsNullOrEmpty(configuration.VideoConfig.SourceFilePath))
            {
                return new List<string>
                {
                    "No video selected for playback"
                };
            }

            return new List<string>();
        }

        private IEnumerable<string> ValidateServerConnections(LedConfiguration configuration)
        {
            var clientValidator = new WsClientValidator();
            foreach (var server in configuration.Servers.Where(s => s.ServerType == Core.Servers.ServerType.FadeCandy))
            {
                if (!clientValidator.IsServerConnected(server.IPAddress, server.Port))
                {
                    yield return
                        string.Format("Could not find a Fadecandy server on IP Address {0}, port {1}", server.IPAddress, server.Port);
                }
            }
        }
    }
}