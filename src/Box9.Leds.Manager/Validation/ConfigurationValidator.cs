using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Box9.Leds.Business.Configuration;
using Box9.Leds.FcClient;

namespace Box9.Leds.Manager.Validation
{
    public class ConfigurationValidator : IConfigurationValidator
    {
        public async Task<ValidationResult> Validate(LedConfiguration configuration)
        {
            var errors = new List<string>();
            errors.AddRange(ValidateAtLeastOneServer(configuration));
            errors.AddRange(ValidateVideoSelected(configuration));
            errors.AddRange(await ValidateServerConnections(configuration));

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

        private async Task<IEnumerable<string>> ValidateServerConnections(LedConfiguration configuration)
        {
            var errors = new List<string>();

            var clientValidator = new WsClientValidator();
            foreach (var server in configuration.Servers)
            {
                bool isConnected = await clientValidator.IsServerConnected(IPAddress.Parse(server.IPAddress), server.Port);

                if (!isConnected)
                {
                    errors.Add(string.Format("Could not find a Fadecandy server on IP Address {0}, port {1}", server.IPAddress, server.Port));
                }
            }

            return errors;
        }
    }
}
