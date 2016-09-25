using System;
using System.Collections.Generic;
using Glimr.Plugins.Sdk;

namespace Glimr.Plugins.Core
{
    public interface IPluginReader
    {
        IEnumerable<IPlugin> GetAvailablePlugins();
    }
}
