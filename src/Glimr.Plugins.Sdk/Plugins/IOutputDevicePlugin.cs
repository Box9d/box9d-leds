using Glimr.Plugins.Plugins;
using Glimr.Plugins.Sdk.Context;

namespace Glimr.Plugins.Sdk.Plugins
{
    public interface IOutputDevicePlugin : IPlugin
    {
        void Run(IOutputDevicePluginContext context);
    }
}
