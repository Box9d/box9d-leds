using Glimr.Plugins.Plugins;

namespace Glimr.Plugins.Sdk.Chaining
{
    internal class PluginDependencyBuilder<TPluginChain> : IPluginDependencyBuilder<TPluginChain> 
        where TPluginChain : IPluginChain
    {
        private readonly TPluginChain pluginChain;
        private readonly IPlugin plugin;

        internal PluginDependencyBuilder(TPluginChain pluginChain, IPlugin plugin)
        {
            this.pluginChain = pluginChain;
            this.plugin = plugin;
        }

        public void AddPluginDependency(IPlugin sourcePlugin, string sourcePluginOutputName, string inputName)
        {
            pluginChain.AddDependency(PluginDependency.Create(sourcePluginOutputName, inputName, plugin, sourcePlugin));
        }

        public TPluginChain FinishAddingDependencies()
        {
            return pluginChain;
        }
    }
}
