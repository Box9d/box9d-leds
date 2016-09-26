﻿namespace Glimr.Plugins.Plugins.Configuration
{
    public static class PluginConfigurationFactory
    {
        public static IPluginConfiguration NewConfiguration(string pluginDisplayName)
        {
            return new PluginConfiguration(pluginDisplayName);
        }
    }
}
