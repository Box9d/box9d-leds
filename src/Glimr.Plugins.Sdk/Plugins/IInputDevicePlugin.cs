using Glimr.Plugins.Plugins.Context;

namespace Glimr.Plugins.Plugins.InputDevice
{
    public interface IInputDevicePlugin : IPlugin
    {
        void Run(IInputDevicePluginContext pluginContext);
    }
}
