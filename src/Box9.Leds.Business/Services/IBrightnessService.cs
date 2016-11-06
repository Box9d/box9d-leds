using System.Collections.Generic;
using System.Threading.Tasks;
using Box9.Leds.Business.Configuration;

namespace Box9.Leds.Business.Services
{
    public interface IBrightnessService
    {
        void AdjustBrightness(int brightnessPercentage, IEnumerable<ServerConfiguration> serverConfigurations);
    }
}
