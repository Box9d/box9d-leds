using System.Collections.Generic;
using System.Linq;
using Glimr.Plugins.Plugins.Exceptions;

namespace Glimr.Plugins.Plugins.Configuration
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
            ValidateParameterNameUniqueness(name);

            inputParameters.Add(new PluginParameter(name, typeof(string)));

            return this;
        }

        public IPluginConfiguration AddIntegerInput(string name)
        {
            ValidateParameterNameUniqueness(name);

            inputParameters.Add(new PluginParameter(name, typeof(int)));

            return this;
        }

        public IPluginConfiguration AddStringOutput(string name)
        {
            ValidateParameterNameUniqueness(name);

            outputParameters.Add(new PluginParameter(name, typeof(string)));

            return this;
        }

        public IPluginConfiguration AddIntegerOutput(string name)
        {
            ValidateParameterNameUniqueness(name);

            outputParameters.Add(new PluginParameter(name, typeof(int)));

            return this;
        }

        public IPluginConfiguration AddBooleanInput(string name)
        {
            ValidateParameterNameUniqueness(name);

            inputParameters.Add(new PluginParameter(name, typeof(bool)));

            return this;
        }

        public IPluginConfiguration AddBooleanOutput(string name)
        {
            ValidateParameterNameUniqueness(name);

            outputParameters.Add(new PluginParameter(name, typeof(bool)));

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

        private void ValidateParameterNameUniqueness(string name)
        {
            if (inputParameters.Any(ip => ip.Name == name)
                || outputParameters.Any(ip => ip.Name == name))
            {
                throw new PluginParameterNotUniqueException(name); 
            }
        }
    }
}
