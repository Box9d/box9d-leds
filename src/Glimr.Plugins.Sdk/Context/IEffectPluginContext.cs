using Glimr.Plugins.Plugins.Context;
using Glimr.Plugins.Sdk.Plotting;

namespace Glimr.Plugins.Sdk.Context
{
    public interface IEffectPluginContext : IPluginContext
    {
        long ElapsedMilliseconds { get; }

        PointsCollection PointsCollection { get; set; }
    }
}
