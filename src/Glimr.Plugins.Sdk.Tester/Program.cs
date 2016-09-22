using System;
using Glimr.Plugins.Custom.MidiInputDevice;
using Glimr.Plugins.Sdk.Runner;

namespace Glimr.Plugins.Sdk.Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            IPluginRunner runner = new PluginRunner();
            var plugin = new MidiInputDevicePlugin();

            var context = runner.CreateContext(plugin);

            runner.RunInputDevicePlugin(plugin, context, (c) =>
            {
                Console.WriteLine(string.Format("Output: {0}, {1}, {2}",
                    context.GetOutput<string>("Note"),
                    context.GetOutput<int>("Octave"),
                    context.GetOutput<int>("Velocity")));
            }).Wait();

            Console.Read();
        }
    }
}
