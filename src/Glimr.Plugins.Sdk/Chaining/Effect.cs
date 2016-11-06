using Glimr.Plugins.Sdk.Context;
using Glimr.Plugins.Sdk.Plugins;

namespace Glimr.Plugins.Sdk.Chaining
{
    internal class Effect : IEffect
    {
        public IEffectPlugin Plugin { get; }

        public IEffectPluginContext Context { get; }

        internal Effect(IEffectPlugin plugin, IEffectPluginContext context)
        {
            Plugin = plugin;
            Context = context;
        }
    }
}
