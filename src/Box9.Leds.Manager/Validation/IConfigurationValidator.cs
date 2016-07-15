using System.Threading.Tasks;
using Box9.Leds.Core.Configuration;

namespace Box9.Leds.Manager.Validation
{
    public interface IConfigurationValidator
    {
        Task<ValidationResult> Validate(LedConfiguration configuration);
    }
}
