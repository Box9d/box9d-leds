using Glimr.Plugins.Plugins.Configuration;
using Glimr.Plugins.Plugins.Context;
using Glimr.Plugins.Sdk.Plotting;

namespace Glimr.Plugins.Sdk.Context
{
    internal class OutputDevicePluginContext : PluginContext, IOutputDevicePluginContext
    {
        public PointsCollection PointsCollection { get; private set; }

        internal OutputDevicePluginContext(IPluginConfiguration configuration)
            : base(configuration)
        {
        }

        internal void SetPointsCollection(PointsCollection pointsCollection)
        {
            PointsCollection = pointsCollection;
        }
    }
}
