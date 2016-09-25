using System.Diagnostics;
using System.Threading;
using Glimr.Plugins.Custom.OscMidiInputDevice;
using Glimr.Plugins.Sdk.Configuration;
using Glimr.Plugins.Sdk.Context;
using Glimr.Plugins.Sdk.InputDevice;
using Rug.Osc;

namespace Glimr.Plugins.TestConsole
{
    public class OscMidiInputDevicePlugin : IInputDevicePlugin
    {

        public OscMidiInputDevicePlugin()
        {
        }

        public IPluginConfiguration Configure()
        {
            var configuration = PluginConfigurationFactory
                .NewConfiguration("OSC Wirless Midi Input")
                .AddIntegerInput("Listening port")
                .AddIntegerInput("Incoming Midi Throttle (milliseconds)")
                .AddStringOutput("Midi note")
                .AddIntegerOutput("Midi octave")
                .AddIntegerOutput("Midi velocity");

            return configuration;
        }

        public void Run(IPluginContext pluginContext)
        {
            var port = pluginContext.GetInput<int>("Listening port");
            var millisecondThrottle = pluginContext.GetInput<int>("Incoming Midi Throttle (milliseconds)");

            using (var listener = new OscReceiver(port))
            {
                var stopWatch = new Stopwatch();

                while (listener.State == OscSocketState.Connected)
                {
                    stopWatch.Stop();
                    stopWatch.Reset();
                    stopWatch.Start();

                    var packet = listener.Receive();
                    var midiMessage = MidiMessage.FromOscPacket(packet);

                    pluginContext.SetOutput("Midi note", midiMessage.MidiNote.Note);
                    pluginContext.SetOutput("Midi octave", midiMessage.MidiNote.Octave);
                    pluginContext.SetOutput("Midi velocity", midiMessage.Velocity);

                    pluginContext.SignalOutputChange();

                    while (stopWatch.ElapsedMilliseconds < millisecondThrottle)
                    {
                        listener.Receive();
                    }
                }
            }
        }
    }
}
