using Box9.Leds.Core.Configuration;

namespace Box9.Leds.Manager.Validation
{
    public interface IConfigurationValidator
    {
        ValidationResult Validate(LedConfiguration configuration);
    }
}
