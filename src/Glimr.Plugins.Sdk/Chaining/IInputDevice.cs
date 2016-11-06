using Glimr.Plugins.Plugins.Context;
using Glimr.Plugins.Plugins.InputDevice;

namespace Glimr.Plugins.Sdk.Chaining
{
    public interface IInputDevice
    {
        IInputDevicePlugin Plugin { get; }

        IInputDevicePluginContext Context { get; }
    }
}
