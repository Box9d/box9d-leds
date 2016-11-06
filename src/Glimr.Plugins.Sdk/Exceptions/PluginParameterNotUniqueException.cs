using System;

namespace Glimr.Plugins.Plugins.Exceptions
{
    public sealed class PluginParameterNotUniqueException : ArgumentException
    {
        public string ParameterName { get; }

        internal PluginParameterNotUniqueException(string parameterName)
            : base(string.Format("A plugin parameter already exists with the name '{0}'", parameterName))
        {
            ParameterName = parameterName;
        }
    }
}
