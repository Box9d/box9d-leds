using System;
using System.IO;

namespace Box9.Leds.DataStorage
{
    public class PathManager : IPathManager
    {
        public string TempDataFolder
        {
            get
            {
                return Path.GetTempPath();
            }
        }
    }
}
