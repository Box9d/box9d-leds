using System;
using System.Collections.Generic;
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

        public IPluginContext CreateContext(IPlugin plugin)
        {
            return new PluginContext(plugin.Configure());
        }

        public async Task RunInputDevicePlugin(IInputDevicePlugin plugin, IPluginContext context, Action<IPluginContext> contextChangeHandler)
        {
            var runTask = Task.Run(() =>
            {
                plugin.Run(context);
                ((PluginContext)context).OutputSet += (sender, args) =>
                {
                    contextChangeHandler(context);
                };
            });

            runningTasks.Add(runTask);
            await runTask;
        }
    }
}
