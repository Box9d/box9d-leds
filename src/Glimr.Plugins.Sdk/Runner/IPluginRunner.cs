using System;
using System.Threading.Tasks;
using Glimr.Plugins.Sdk.Context;
using Glimr.Plugins.Sdk.InputDevice;

namespace Glimr.Plugins.Sdk.Runner
{
    public interface IPluginRunner : IDisposable
    {
        IPluginContext CreateContext(IPlugin plugin);

        Task RunInputDevicePlugin(IInputDevicePlugin plugin, IPluginContext context, Action<IPluginContext> contextChangeHandler);
    }
}
