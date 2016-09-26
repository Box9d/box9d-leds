using System;
using Glimr.Plugins.Plugins.Configuration;

namespace Glimr.Plugins.Plugins
{
    public interface IPlugin : IDisposable
    {
        IPluginConfiguration Configure();
    }
}
