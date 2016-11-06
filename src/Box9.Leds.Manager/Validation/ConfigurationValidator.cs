using System.Collections.Generic;
using Box9.Leds.Business.Configuration;

namespace Box9.Leds.Manager.Validation
{
    public class ConfigurationValidator : IConfigurationValidator
    {
        public ValidationResult Validate(LedConfiguration configuration)
        {
            var errors = new List<string>();
            errors.AddRange(ValidateVideoSelected(configuration));

            return new ValidationResult(errors); 
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
    }
}
