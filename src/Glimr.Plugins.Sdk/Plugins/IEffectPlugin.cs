using Glimr.Plugins.Plugins;
using Glimr.Plugins.Sdk.Context;

namespace Glimr.Plugins.Sdk.Plugins
{
    public interface IEffectPlugin : IPlugin
    {
        void Run(IEffectPluginContext context);
    }
}
