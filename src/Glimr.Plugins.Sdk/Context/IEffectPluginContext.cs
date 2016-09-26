using Glimr.Plugins.Plugins.Context;
using Glimr.Plugins.Sdk.Plotting;

namespace Glimr.Plugins.Sdk.Context
{
    public interface IEffectPluginContext : IPluginContext
    {
        int ElapsedMilliseconds { get; }

        PointsCollection PointsCollection { get; set; }
    }
}
