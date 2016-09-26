using System.Windows.Forms;
using Glimr.Plugins.Plugins.Configuration;
using Glimr.Plugins.Plugins.Context;
using Glimr.Plugins.Plugins.InputDevice;
using Gma.System.MouseKeyHook;

namespace Glimr.Plugins.Custom.KeyboardInputDevice
{
    public class KeyboardInputDevice : IInputDevicePlugin
    {
        private IKeyboardMouseEvents hook;
        private IInputDevicePluginContext pluginContext;

        public KeyboardInputDevice()
        {
            hook = Hook.GlobalEvents();
        }

        public IPluginConfiguration Configure()
        {
            return PluginConfigurationFactory
                .NewConfiguration("Keyboard input device")
                .AddStringOutput("Key");
        }

        public void Run(IInputDevicePluginContext pluginContext)
        {
            this.pluginContext = pluginContext;
            hook.KeyPress += KeyPressHandler;
        }

        private void KeyPressHandler(object sender, KeyPressEventArgs args)
        {
            pluginContext.SetOutput("Key", args.KeyChar.ToString());
            pluginContext.SignalOutputChange();
        }

        public void Dispose()
        {
            hook.KeyPress -= KeyPressHandler;
            hook.Dispose();
        }
    }
}
