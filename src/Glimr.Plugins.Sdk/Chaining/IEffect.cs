using Glimr.Plugins.Sdk.Context;
using Glimr.Plugins.Sdk.Plugins;

namespace Glimr.Plugins.Sdk.Chaining
{
    public interface IEffect
    {
        IEffectPlugin Plugin { get; }

        IEffectPluginContext Context { get; }
    }
}
