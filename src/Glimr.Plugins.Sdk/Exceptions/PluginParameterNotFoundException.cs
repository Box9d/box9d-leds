using System;

namespace Glimr.Plugins.Plugins.Exceptions
{
    public sealed class PluginParameterNotFoundException : ArgumentException
    {
        public string ParameterName { get; }

        internal PluginParameterNotFoundException(string parameterName)
            : base(string.Format("Plugin parameter was not found", parameterName))
        {
            ParameterName = parameterName;
        }
    }
}
