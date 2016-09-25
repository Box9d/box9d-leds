using Box9.Leds.Business.Configuration;

namespace Box9.Leds.Business.Services
{
    public interface IConfigurationStorageService
    {
        LedConfiguration Get(string filePath);

        void Save(LedConfiguration config, string filePath);
    }
}
