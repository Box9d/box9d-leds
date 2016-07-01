using System.IO;
using Box9.Leds.Core.Configuration;
using Newtonsoft.Json;

namespace Box9.Leds.DataStorage
{
    public class ConfigurationStorageClient : IConfigurationStorageClient
    {
        public LedConfiguration Get(string filePath)
        {
            using (var stream = new FileStream(filePath, FileMode.Open))
            using (var streamReader = new StreamReader(stream))
            {
                var raw = streamReader.ReadToEnd();
                return JsonConvert.DeserializeObject<LedConfiguration>(raw);
            }
        }

        public void Save(LedConfiguration config, string filePath)
        {
            var raw = JsonConvert.SerializeObject(config);

            using (var stream = File.Create(filePath))
            using (var streamWriter = new StreamWriter(stream))
            {
                streamWriter.WriteLine(raw);
            }
        }
    }
}
