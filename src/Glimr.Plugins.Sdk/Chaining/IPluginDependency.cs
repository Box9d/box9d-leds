using System;
using Glimr.Plugins.Plugins;

namespace Glimr.Plugins.Sdk.Chaining
{
    public interface IPluginDependency
    {
        string SourcePluginOutputName { get; }

        string DependentPluginInputName { get; }

        IPlugin DependentPlugin { get; }

        IPlugin SourcePlugin { get; }
    }
}
