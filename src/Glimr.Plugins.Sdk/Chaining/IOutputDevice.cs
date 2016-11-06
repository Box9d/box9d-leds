using Glimr.Plugins.Sdk.Context;
using Glimr.Plugins.Sdk.Plugins;

namespace Glimr.Plugins.Sdk.Chaining
{
    public interface IOutputDevice
    {
        IOutputDevicePlugin Plugin { get; }

        IOutputDevicePluginContext Context { get; }
    }
}
