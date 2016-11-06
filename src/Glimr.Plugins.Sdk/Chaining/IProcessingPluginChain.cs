using System.Collections.Generic;
using Glimr.Plugins.Plugins.Context;
using Glimr.Plugins.Plugins.InputDevice;
using Glimr.Plugins.Sdk.Context;
using Glimr.Plugins.Sdk.Plugins;

namespace Glimr.Plugins.Sdk.Chaining
{
    public interface IProcessingPluginChain : IPluginChain
    {
        IEnumerable<IInputDevice> InputDevices { get; }

        IEnumerable<IOutputDevice> OutputDevices { get; }

        IEffectPluginChain EffectPluginChain { get; }

        IProcessingPluginChain AddInputDevicePlugin(IInputDevicePlugin plugin, IInputDevicePluginContext startingContext);

        IProcessingPluginChain AddOutputDevicePlugin(IOutputDevicePlugin plugin, IOutputDevicePluginContext startingContext);

        IProcessingPluginChain SetEffectPluginChain(IEffectPluginChain effectPluginChain);
    }
}
