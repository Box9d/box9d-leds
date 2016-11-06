using Glimr.Plugins.Plugins.Context;
using Glimr.Plugins.Plugins.InputDevice;
using Glimr.Plugins.Sdk.Plugins;

namespace Glimr.Plugins.Sdk.Context
{
    public static class PluginContextFactory
    {
        public static IEffectPluginContext GenerateInitialEffectPluginContext(IEffectPlugin effectPlugin)
        {
            return new EffectPluginContext(effectPlugin.Configure(), 0);
        }

        public static IInputDevicePluginContext GenerateInitialInputDevicePluginContext(IInputDevicePlugin inputDevicePlugin)
        {
            return new InputDevicePluginContext(inputDevicePlugin.Configure());
        }
        
        public static IOutputDevicePluginContext GenerateInitialOutputDevicePluginContext(IOutputDevicePlugin outputDevicePlugin)
        {
            return new OutputDevicePluginContext(outputDevicePlugin.Configure());
        }
    }
}
