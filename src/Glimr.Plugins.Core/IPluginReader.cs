using System.Collections.Generic;
using Glimr.Plugins.Plugins;

namespace Glimr.Plugins.Core
{
    public interface IPluginReader
    {
        IEnumerable<T> GetAvailablePlugins<T>() where T : IPlugin;
    }
}
