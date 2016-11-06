using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Glimr.Plugins.Plugins.Context;
using Glimr.Plugins.Plugins.InputDevice;
using Glimr.Plugins.Sdk.Chaining;
using Glimr.Plugins.Sdk.Context;
using Glimr.Plugins.Sdk.Plotting;

namespace Glimr.Plugins.Plugins.Runner
{
    public class PluginRunner : IPluginRunner
    {
        private CancellationTokenSource cancellationTokenSource;
        private readonly List<IPlugin> plugins;

        public PluginRunner()
        {
            plugins = new List<IPlugin>();
        }

        public void Dispose()
        {
            if (cancellationTokenSource != null)
            {
                cancellationTokenSource.Cancel();
            }

            foreach (var plugin in plugins)
            {
                plugin.Dispose();
            }
        }

        public void StartProcessingPluginChain(IProcessingPluginChain processingPluginChain)
        {
            cancellationTokenSource = new CancellationTokenSource();

            Task.Run(() =>
            {
                RunProcessingPluginChain(processingPluginChain, cancellationTokenSource.Token);
            });
        }

        public void StopProcessingPluginChain()
        {
            if (cancellationTokenSource != null)
            {
                cancellationTokenSource.Cancel();
            }
        }

        private void RunProcessingPluginChain(IProcessingPluginChain processingPluginChain, CancellationToken cancellationToken)
        {
            foreach (var inputDevice in processingPluginChain.InputDevices)
            {
                inputDevice.Plugin.Run(inputDevice.Context);
            }

            foreach (var effect in processingPluginChain.EffectPluginChain.Effects)
            {
                ((EffectPluginContext)effect.Context).FixInitialPointsCollection();
            }

            var processingStopwatch = new Stopwatch();
            processingStopwatch.Start();

            var throttleMilliseconds = 100;
            long timeOfLastUpdateMilliseconds = 0;

            while (!cancellationToken.IsCancellationRequested)
            {
                PointsCollection effectChange = null;

                var chainedOutputContexts = new List<Tuple<IPluginDependency, IPluginContext>>();

                foreach (var effect in processingPluginChain.EffectPluginChain.Effects)
                {
                    var context = (EffectPluginContext)effect.Context;
                    context.ResetToInitialPointsCollection();

                    foreach (var chainedOutputContext in chainedOutputContexts.Where(coc => coc.Item1.DependentPlugin == effect.Plugin))
                    {
                        var output = chainedOutputContext.Item2.GetOutput(chainedOutputContext.Item1.SourcePluginOutputName);
                        effect.Context.SetInput(chainedOutputContext.Item1.DependentPluginInputName, output);
                    }

                    if (effectChange != null)
                    {
                        context.PointsCollection = effectChange;
                    }

                    context.ElapsedMilliseconds = processingStopwatch.ElapsedMilliseconds;
                    effect.Plugin.Run(context);

                    foreach (var chainedOutput in processingPluginChain.PluginDependencies.Where(pd => pd.DependentPlugin == effect.Plugin))
                    {
                        chainedOutputContexts.Add(new Tuple<IPluginDependency, IPluginContext>(chainedOutput, context));
                    }

                    effectChange = context.PointsCollection;
                }

                effectChange.CompletePointOperations();

                foreach (var outputDevice in processingPluginChain.OutputDevices)
                {
                    var context = (OutputDevicePluginContext)outputDevice.Context;
                    context.SetPointsCollection(effectChange);

                    outputDevice.Plugin.Run(context);
                }

                while (processingStopwatch.ElapsedMilliseconds < timeOfLastUpdateMilliseconds + throttleMilliseconds)
                {
                    continue;
                }

                timeOfLastUpdateMilliseconds = processingStopwatch.ElapsedMilliseconds;
            }
        }
    }
}
