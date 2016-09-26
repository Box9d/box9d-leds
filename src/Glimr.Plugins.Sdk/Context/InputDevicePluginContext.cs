using System;
using Glimr.Plugins.Plugins.Configuration;

namespace Glimr.Plugins.Plugins.Context
{
    internal class InputDevicePluginContext : PluginContext, IInputDevicePluginContext
    {
        public event EventHandler<EventArgs> OutputSet;

        public InputDevicePluginContext(IPluginConfiguration configuration) 
            : base(configuration)
        {
            OutputSet += (s, args) =>
            {
            };
        }

        public void SignalOutputChange()
        {
            OutputSet(null, EventArgs.Empty);
        }
    }
}
