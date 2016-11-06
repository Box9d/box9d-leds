using Glimr.Plugins.Sdk.Context;
using Glimr.Plugins.Sdk.Plugins;

namespace Glimr.Plugins.Sdk.Chaining
{
    internal class OutputDevice : IOutputDevice
    {
        public IOutputDevicePlugin Plugin { get; }

        public IOutputDevicePluginContext Context { get; }

        internal OutputDevice(IOutputDevicePlugin plugin, IOutputDevicePluginContext context)
        {
            Plugin = plugin;
            Context = context;
        }
    }
}
