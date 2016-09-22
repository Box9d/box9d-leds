using System.Collections.Generic;

namespace Glimr.Plugins.Sdk.Configuration
{
    internal class PluginConfiguration : IPluginConfiguration
    {
        private readonly List<PluginParameter> inputParameters;
        private readonly List<PluginParameter> outputParameters;

        public string PluginDisplayName { get; }

        internal PluginConfiguration(string pluginDisplayName)
        {
            PluginDisplayName = pluginDisplayName;

            inputParameters = new List<PluginParameter>();
            outputParameters = new List<PluginParameter>();
        }

        public IPluginConfiguration AddStringInput(string name)
        {
            inputParameters.Add(new PluginParameter(name, typeof(string)));

            return this;
        }

        public IPluginConfiguration AddIntegerInput(string name)
        {
            inputParameters.Add(new PluginParameter(name, typeof(int)));

            return this;
        }

        public IPluginConfiguration AddStringOutput(string name)
        {
            outputParameters.Add(new PluginParameter(name, typeof(string)));

            return this;
        }

        public IPluginConfiguration AddIntegerOutput(string name)
        {
            outputParameters.Add(new PluginParameter(name, typeof(int)));

            return this;
        }

        internal IEnumerable<PluginParameter> GetInputParameters()
        {
            return inputParameters;
        }

        internal IEnumerable<PluginParameter> GetOutputParameters()
        {
            return outputParameters;
        }
    }
}
