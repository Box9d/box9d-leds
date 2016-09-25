using System.Threading;
using Glimr.Plugins.Sdk.Configuration;
using Glimr.Plugins.Sdk.Context;
using Glimr.Plugins.Sdk.InputDevice;
using Midi;

namespace Glimr.Plugins.Custom.MidiInputDevice
{
    public class MidiInputDevicePlugin : IInputDevicePlugin
    {
        public IPluginConfiguration Configure()
        {
            return PluginConfigurationFactory
                .NewConfiguration("Midi Input Device")
                .AddIntegerOutput("Velocity")
                .AddIntegerOutput("Octave")
                .AddStringOutput("Note");
        }

        public void Run(IPluginContext pluginContext)
        {
            foreach (var inputDevice in InputDevice.InstalledDevices)
            {
                inputDevice.Open();
                inputDevice.StartReceiving(null);

                inputDevice.NoteOn += (input) =>
                {
                    pluginContext.SetOutput("Velocity", input.Velocity);
                    pluginContext.SetOutput("Octave", input.Pitch.Octave());
                    pluginContext.SetOutput("Note", input.Pitch.NotePreferringFlats().Letter.ToString() + input.Pitch.NotePreferringFlats().Accidental);

                    pluginContext.SignalOutputChange();
                };
            }
        }
    }
}
