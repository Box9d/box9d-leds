using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Glimr.Plugins.Sdk.Context;
using Glimr.Plugins.Sdk.InputDevice;

namespace Glimr.Plugins.Sdk.Runner
{
    public class PluginRunner : IPluginRunner
    {
        private readonly List<Task> runningTasks;

        public PluginRunner()
        {
            runningTasks = new List<Task>();
        }

        public void Dispose()
        {
            foreach (var task in runningTasks)
            {
                task.Dispose();
            }
        }

        public IInputDevicePluginContext CreateInputDivicePluginContext(IPlugin plugin)
        {
            return new InputDevicePluginContext(plugin.Configure());
        }

        public async Task RunInputDevicePlugin(IInputDevicePlugin plugin, IInputDevicePluginContext context, Action<IInputDevicePluginContext> contextChangeHandler, Action<Exception> exceptionHandler, CancellationToken cancellationToken)
        {
            ((InputDevicePluginContext)context).OutputSet += (sender, args) =>
            {
                contextChangeHandler(context);
            };

            var runTask = Task.Run(() =>
            {
                try
                {
                    plugin.Run(context);

                    while (cancellationToken.IsCancellationRequested)
                    {
                        continue;
                    }
                }
                catch (Exception ex)
                {
                    exceptionHandler(ex);
                }
            });

            runningTasks.Add(runTask);

            await runTask;
        }
    }
}
