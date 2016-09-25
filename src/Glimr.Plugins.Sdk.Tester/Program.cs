using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Glimr.Plugins.Core;
using Glimr.Plugins.Sdk.InputDevice;
using Glimr.Plugins.Sdk.Runner;
using Newtonsoft.Json;

namespace Glimr.Plugins.Sdk.Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- Plugin test console ---");
            Console.WriteLine();

            int pluginCount = 0;
            IPluginReader reader = new PluginReader();
            var availablePlugins = new Dictionary<int, IPlugin>();
            var pluginOptions = new List<string>();
            var plugins = reader.GetAvailablePlugins();

            foreach (var plugin in plugins)
            {
                availablePlugins.Add(pluginCount++, plugin);
                pluginOptions.Add(string.Format("[{0}] {1}", pluginCount, plugin.Configure().PluginDisplayName));
            }

            var pluginSelection = RequestForValidSelection<int>("Select from the following available plugins:", pluginOptions, availablePlugins.Keys.Select(k => (k + 1).ToString()));
            Console.Clear();

            IPluginRunner runner = new PluginRunner();
            var context = runner.CreateContext(availablePlugins[pluginSelection - 1]);
            
            foreach (var input in context.GetPluginInputs())
            {
                var param = RequestForValidInput(string.Format("Enter value for parameter '{0}':", input.Key), input.Value);
                context.SetInput(input.Key, param);
            }

            Console.Clear();
            Console.WriteLine("Running plugin...");
            Console.WriteLine("Press ESC to stop");
            Console.WriteLine();

            var cts = new CancellationTokenSource();

            runner.RunInputDevicePlugin((IInputDevicePlugin)availablePlugins[pluginSelection - 1], context, 
                (c) =>
            {
                Console.WriteLine();
                Console.WriteLine("Output changed: ");
                var outputValues = new List<object>();
                foreach (var output in context.GetPluginOutputs())
                {
                    outputValues.Add(context.GetOutput(output.Key));
                }

                Console.WriteLine(JsonConvert.SerializeObject(outputValues));
            },
                (ex) =>
            {
                Console.WriteLine("An error occurred in the plugin: " + ex.Message);
            }, 
                cts.Token);

            while (Console.ReadKey().Key != ConsoleKey.Escape)
            {
                cts.Cancel();
                continue;
            }
        }

        public static object RequestForValidInput(string instruction, Type input)
        {
            var pluginSelection = string.Empty;
            bool invalidInput = false;
            object result = null;

            while (!TryCast(pluginSelection, input, out result) || string.IsNullOrEmpty(pluginSelection))
            {
                if (invalidInput)
                {
                    Console.WriteLine("Invalid input");
                }

                pluginSelection = Console.ReadLine();
                invalidInput = true;
            }

            return result;
        }

        public static TSelection RequestForValidSelection<TSelection>(string instruction, IEnumerable<string> options, IEnumerable<string> validSelections)
        {
            return (TSelection)RequestForValidSelection(instruction, options, validSelections, typeof(TSelection));
        }

        public static object RequestForValidSelection(string instruction, IEnumerable<string> options, IEnumerable<string> validSelections, Type type)
        {
            Console.WriteLine(instruction);
            foreach (var option in options)
            {
                Console.WriteLine(option);
            }

            var pluginSelection = string.Empty;
            bool invalidSelection = false;
            object pluginSelected = null;

            while (validSelections.All(vs => vs != pluginSelection) || !TryCast(pluginSelection, type, out pluginSelected))
            {
                if (invalidSelection)
                {
                    Console.WriteLine("Invalid selection");
                }

                pluginSelection = Console.ReadLine();
                invalidSelection = true;
            }

            return pluginSelected;
        }

        public static bool TryCast(string input, Type type, out object result)
        {
            result = null;

            try
            {
                result = Convert.ChangeType(input, type);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool TryCast<T>(string input,  out T result)
        {
            object output;
            if (TryCast(input, typeof(T), out output))
            {
                result = (T)output;
                return true;
            }

            result = default(T);
            return false;
        }
    }
}
