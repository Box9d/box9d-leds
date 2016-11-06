using System.Collections.Generic;
using Glimr.Plugins.Sdk.Context;
using Glimr.Plugins.Sdk.Plugins;

namespace Glimr.Plugins.Sdk.Chaining
{
    public interface IEffectPluginChain : IPluginChain
    {
        IEnumerable<IEffect> Effects { get; }

        IPluginDependencyBuilder<IEffectPluginChain> AddEffectPlugin(IEffectPlugin plugin, IEffectPluginContext context);
    }
}
