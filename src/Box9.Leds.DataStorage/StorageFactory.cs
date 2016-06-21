using System;
using System.IO;
using DBreeze;

namespace Box9.Leds.DataStorage
{
    public static class StorageFactory
    {
        public static DBreezeEngine GetDBreezeEngine()
        {
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var dataFolder = Path.Combine(appDataPath, "Box9Leds");

            if (!Directory.Exists(dataFolder))
            {
                Directory.CreateDirectory(dataFolder);
            }

            return new DBreezeEngine(dataFolder);
        }
    }
}
