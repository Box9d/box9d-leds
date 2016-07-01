using Box9.Leds.Core.Configuration;

namespace Box9.Leds.DataStorage
{
    public interface IConfigurationStorageClient
    {
        LedConfiguration Get(string filePath);

        void Save(LedConfiguration config, string filePath);
    }
}
