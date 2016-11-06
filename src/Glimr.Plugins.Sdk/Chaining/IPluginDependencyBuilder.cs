using Glimr.Plugins.Plugins;

namespace Glimr.Plugins.Sdk.Chaining
{
    public interface IPluginDependencyBuilder<TPluginChain>
        where TPluginChain : IPluginChain
    {
        void AddPluginDependency(IPlugin sourcePlugin, string sourcePluginOutputName, string inputName);

        TPluginChain FinishAddingDependencies();
    }
}
