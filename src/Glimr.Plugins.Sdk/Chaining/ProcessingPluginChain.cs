using System;
using System.Collections.Generic;
using System.Linq;
using Glimr.Plugins.Plugins.Context;
using Glimr.Plugins.Plugins.InputDevice;
using Glimr.Plugins.Sdk.Context;
using Glimr.Plugins.Sdk.Plugins;

namespace Glimr.Plugins.Sdk.Chaining
{
    internal class ProcessingPluginChain : PluginChain, IProcessingPluginChain
    {
        public IEnumerable<IInputDevice> InputDevices { get; private set; }

        public IEffectPluginChain EffectPluginChain { get; private set; }

        public IEnumerable<IOutputDevice> OutputDevices { get; private set; }

        internal ProcessingPluginChain()
        {
            InputDevices = new List<InputDevice>();
            OutputDevices = new List<OutputDevice>();
        }

        public IProcessingPluginChain SetEffectPluginChain(IEffectPluginChain effectPluginChain)
        {
            EffectPluginChain = effectPluginChain;

            return this;
        }

        public IProcessingPluginChain AddInputDevicePlugin(IInputDevicePlugin plugin, IInputDevicePluginContext startingContext)
        {
            var inputDevices = InputDevices.ToList();
            inputDevices.Add(new InputDevice(plugin, startingContext));
            InputDevices = inputDevices;

            return this;
        }

        public IProcessingPluginChain AddOutputDevicePlugin(IOutputDevicePlugin plugin, IOutputDevicePluginContext startingContext)
        {
            var outputDevices = OutputDevices.ToList();
            outputDevices.Add(new OutputDevice(plugin, startingContext));
            OutputDevices = outputDevices;

            return this;
        }
    }
}
