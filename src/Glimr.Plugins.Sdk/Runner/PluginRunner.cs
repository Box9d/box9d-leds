using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Glimr.Plugins.Plugins.Context;
using Glimr.Plugins.Plugins.InputDevice;

namespace Glimr.Plugins.Plugins.Runner
{
    public class PluginRunner : IPluginRunner
    {
        private readonly List<IPlugin> plugins;

        public PluginRunner()
        {
            plugins = new List<IPlugin>();
        }

        public void Dispose()
        {
            foreach (var plugin in plugins)
            {
                plugin.Dispose();
            }
        }

        public IInputDevicePluginContext CreateInputDivicePluginContext(IPlugin plugin)
        {
            return new InputDevicePluginContext(plugin.Configure());
        }

        public void RunInputDevicePlugin(IInputDevicePlugin plugin, IInputDevicePluginContext context, Action<IInputDevicePluginContext> contextChangeHandler, Action<Exception> exceptionHandler, CancellationToken cancellationToken)
        {
            ((InputDevicePluginContext)context).OutputSet += (sender, args) =>
            {
                contextChangeHandler(context);
            };

            plugins.Add(plugin);

            try
            {
                plugin.Run(context);
            }
            catch (Exception ex)
            {
                exceptionHandler(ex);
            }
        }
    }
}
