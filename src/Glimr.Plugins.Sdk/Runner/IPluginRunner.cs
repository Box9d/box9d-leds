using System;
using System.Threading;
using System.Threading.Tasks;
using Glimr.Plugins.Plugins.Context;
using Glimr.Plugins.Plugins.InputDevice;

namespace Glimr.Plugins.Plugins.Runner
{
    public interface IPluginRunner : IDisposable
    {
        IInputDevicePluginContext CreateInputDivicePluginContext(IPlugin plugin);

        void RunInputDevicePlugin(IInputDevicePlugin plugin, IInputDevicePluginContext context, Action<IInputDevicePluginContext> contextChangeHandler, Action<Exception> exceptionHandler, CancellationToken cancellationToken);
    }
}
