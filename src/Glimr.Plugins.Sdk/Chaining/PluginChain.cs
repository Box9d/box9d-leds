using System.Collections.Generic;
using System.Linq;

namespace Glimr.Plugins.Sdk.Chaining
{
    internal class PluginChain
    {
        public IEnumerable<IPluginDependency> PluginDependencies { get; private set; }

        public PluginChain()
        {
            PluginDependencies = new List<IPluginDependency>();
        }

        public void AddDependency(IPluginDependency dependency)
        {
            var pluginDependencies = PluginDependencies.ToList();
            pluginDependencies.Add(dependency);

            PluginDependencies = pluginDependencies;
        }
    }
}
