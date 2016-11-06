using Glimr.Plugins.Plugins.Context;
using Glimr.Plugins.Plugins.InputDevice;

namespace Glimr.Plugins.Sdk.Chaining
{
    internal class InputDevice : IInputDevice
    {
        public IInputDevicePlugin Plugin { get; }

        public IInputDevicePluginContext Context { get; }

        internal InputDevice(IInputDevicePlugin plugin, IInputDevicePluginContext context)
        {
            Plugin = plugin;
            Context = context;
        }
    }
}
