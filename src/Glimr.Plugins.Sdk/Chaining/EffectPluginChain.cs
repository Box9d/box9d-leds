using System;
using System.Collections.Generic;
using System.Linq;
using Glimr.Plugins.Sdk.Context;
using Glimr.Plugins.Sdk.Plugins;

namespace Glimr.Plugins.Sdk.Chaining
{
    internal class EffectPluginChain : PluginChain, IEffectPluginChain
    {
        public IEnumerable<IEffect> Effects { get { return effects; } }

        private readonly List<IEffect> effects;

        public EffectPluginChain()
        {
            effects = new List<IEffect>();
        }

        public IPluginDependencyBuilder<IEffectPluginChain> AddEffectPlugin(IEffectPlugin plugin, IEffectPluginContext context)
        {
            effects.Add(new Effect(plugin, context));

            return new PluginDependencyBuilder<IEffectPluginChain>(this, plugin);
        }
    }
}
