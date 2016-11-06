using System;
using Glimr.Plugins.Plugins;

namespace Glimr.Plugins.Sdk.Chaining
{
    internal class PluginDependency : IPluginDependency
    {
        public string SourcePluginOutputName { get; }

        public IPlugin DependentPlugin { get; }

        public IPlugin SourcePlugin { get; }

        public string DependentPluginInputName { get; }

        private PluginDependency(string sourcePluginOutputName, string dependentPluginInputName, IPlugin dependentPlugin, IPlugin sourcePlugin)
        {
            SourcePluginOutputName = sourcePluginOutputName;
            DependentPlugin = dependentPlugin;
            SourcePlugin = sourcePlugin;
            DependentPluginInputName = dependentPluginInputName;
        }

        internal static PluginDependency Create(string sourcePluginOutputName, string dependentPluginInputName, IPlugin dependentPlugin, IPlugin sourcePlugin)
        {
            return new PluginDependency(sourcePluginOutputName, dependentPluginInputName, dependentPlugin, sourcePlugin);
        }
    }
}
