using System.Collections.Generic;

namespace Glimr.Plugins.Sdk.Chaining
{
    public interface IPluginChain
    {
        IEnumerable<IPluginDependency> PluginDependencies { get; }

        void AddDependency(IPluginDependency dependency);
    }
}
