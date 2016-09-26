using System;

namespace Glimr.Plugins.Core.Exceptions
{
    public class PluginActivationException : Exception
    {
        internal PluginActivationException(Type pluginType, Exception ex)
            : base(string.Format("Could not activate plugin type '{0}'. Ensure that plugin has an empty constructor", pluginType), ex)
        {
        }
    }
}
