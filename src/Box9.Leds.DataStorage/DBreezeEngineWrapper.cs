using System;
using System.IO;
using DBreeze;

namespace Box9.Leds.DataStorage
{
    public class DBreezeEngineWrapper : IDisposable
    {
        private DBreezeEngine engine;

        public void Dispose()
        {
            if (engine != null)
            {
                engine.Dispose();
            }
        }

        public DBreezeEngine GetDBreezeEngine()
        {
            if (engine == null)
            {
                var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                var dataFolder = Path.Combine(appDataPath, "Box9Leds");

                if (!Directory.Exists(dataFolder))
                {
                    Directory.CreateDirectory(dataFolder);
                }

                this.engine = new DBreezeEngine(dataFolder);
            }

            return engine;
        }
    }
}
