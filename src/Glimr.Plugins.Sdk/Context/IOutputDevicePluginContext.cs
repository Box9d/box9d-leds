using Glimr.Plugins.Plugins.Context;
using Glimr.Plugins.Sdk.Plotting;

namespace Glimr.Plugins.Sdk.Context
{
    public interface IOutputDevicePluginContext : IPluginContext
    {
        PointsCollection PointsCollection { get; }
    }
}
