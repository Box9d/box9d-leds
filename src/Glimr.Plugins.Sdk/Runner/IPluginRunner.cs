using System;
using System.Threading;
using System.Threading.Tasks;
using Glimr.Plugins.Sdk.Context;
using Glimr.Plugins.Sdk.InputDevice;

namespace Glimr.Plugins.Sdk.Runner
{
    public interface IPluginRunner : IDisposable
    {
        IInputDevicePluginContext CreateInputDivicePluginContext(IPlugin plugin);

        Task RunInputDevicePlugin(IInputDevicePlugin plugin, IInputDevicePluginContext context, Action<IInputDevicePluginContext> contextChangeHandler, Action<Exception> exceptionHandler, CancellationToken cancellationToken);
    }
}
