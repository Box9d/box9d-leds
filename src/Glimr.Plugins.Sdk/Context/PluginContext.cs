using System;
using System.Collections.Generic;
using System.Linq;
using Glimr.Plugins.Sdk.Configuration;
using Glimr.Plugins.Sdk.Exceptions;

namespace Glimr.Plugins.Sdk.Context
{
    internal class PluginContext : IPluginContext
    {
        protected readonly Dictionary<PluginParameter, object> inputValues;
        protected readonly Dictionary<PluginParameter, object> outputValues;

        public event EventHandler<EventArgs> OutputSet;

        internal PluginContext(IPluginConfiguration pluginConfiguration)
        {
            inputValues = new Dictionary<PluginParameter, object>();
            outputValues = new Dictionary<PluginParameter, object>();

            foreach (var paramter in ((PluginConfiguration)pluginConfiguration).GetInputParameters())
            {
                inputValues.Add(paramter, null);
            }

            foreach (var paramter in ((PluginConfiguration)pluginConfiguration).GetOutputParameters())
            {
                outputValues.Add(paramter, null);
            }
        }

        public T GetInput<T>(string name)
        {
            var key = inputValues.Keys.SingleOrDefault(k => k.Name == name);
            if (key == null)
            {
                throw new PluginParameterException(name);
            }

            return (T)inputValues[key];
        }

        public T GetOutput<T>(string name)
        {
            var key = outputValues.Keys.SingleOrDefault(k => k.Name == name);
            if (key == null)
            {
                throw new PluginParameterException(name);
            }

            return (T)outputValues[key];
        }

        public void SetInput<T>(string name, T value)
        {
            var key = inputValues.Keys.SingleOrDefault(k => k.Name == name);
            if (key == null)
            {
                throw new PluginParameterException(name);
            }

            inputValues[key] = value;
        }

        public void SetOutput<T>(string name, T value)
        {
            var key = outputValues.Keys.SingleOrDefault(k => k.Name == name);
            if (key == null)
            {
                throw new PluginParameterException(name);
            }

            outputValues[key] = value;
        }

        public void SignalOutputChange()
        {
            OutputSet(null, EventArgs.Empty);
        }

        public IEnumerable<object> GetAllOutputs()
        {
            return outputValues.Values;
        }
    }
}
