using System;
using System.IO;

namespace Glimr.Plugins.Core
{
    public static class PluginFolder
    {
        public static string GetRootFolder()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "GlimrPlugins"); 
        }

        public static string Get(Guid pluginId)
        {
            return Path.Combine(GetRootFolder(), pluginId.ToString());
        }
    }
}
