namespace Glimr.Plugins.Sdk.Chaining
{
    public static class PluginChainBuilder
    {
        public static IProcessingPluginChain CreateProcessingPluginChain()
        {
            return new ProcessingPluginChain();
        }

        public static IEffectPluginChain CreateEffectPluginChain()
        {
            return new EffectPluginChain();
        }
    }
}
