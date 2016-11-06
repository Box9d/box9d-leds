using System;
using System.Collections.Generic;
using System.Linq;
using Glimr.Plugins.Plugins.Configuration;
using Glimr.Plugins.Plugins.Exceptions;

namespace Glimr.Plugins.Plugins.Context
{
    internal class PluginContext : IPluginContext
    {
        protected readonly Dictionary<PluginParameter, object> inputValues;
        protected readonly Dictionary<PluginParameter, object> outputValues;

        internal List<string> log;

        internal PluginContext(IPluginConfiguration pluginConfiguration)
        {
            inputValues = new Dictionary<PluginParameter, object>();
            outputValues = new Dictionary<PluginParameter, object>();

            log = new List<string>();

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
                throw new PluginParameterNotFoundException(name);
            }

            return (T)inputValues[key];
        }

        public T GetOutput<T>(string name)
        {
            return (T)GetOutput(name);
        }

        public object GetOutput(string name)
        {
            var key = outputValues.Keys.SingleOrDefault(k => k.Name == name);
            if (key == null)
            {
                throw new PluginParameterNotFoundException(name);
            }

            return outputValues[key];
        }

        public void SetInput<T>(string name, T value)
        {
            SetInput(name, (object)value);
        }

        public void SetInput(string name, object value)
        {
            var key = inputValues.Keys.SingleOrDefault(k => k.Name == name);
            if (key == null)
            {
                throw new PluginParameterNotFoundException(name);
            }

            inputValues[key] = value;
        }

        public void SetOutput<T>(string name, T value)
        {
            var key = outputValues.Keys.SingleOrDefault(k => k.Name == name);
            if (key == null)
            {
                throw new PluginParameterNotFoundException(name);
            }

            outputValues[key] = value;
        }

        public Dictionary<string, Type> GetPluginInputs()
        {
            var pluginInputs = new Dictionary<string, Type>();

            foreach (var inputKey in inputValues.Keys)
            {
                pluginInputs.Add(inputKey.Name, inputKey.Type);
            }

            return pluginInputs;
        }


        public Dictionary<string, Type> GetPluginOutputs()
        {
            var pluginOutputs = new Dictionary<string, Type>();

            foreach (var outputKey in outputValues.Keys)
            {
                pluginOutputs.Add(outputKey.Name, outputKey.Type);
            }

            return pluginOutputs;
        }

        public void WriteToLog(string message)
        {
            log.Add(message);
        }
    }
}
