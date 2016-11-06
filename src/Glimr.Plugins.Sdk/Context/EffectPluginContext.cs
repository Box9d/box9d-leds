using System;
using Glimr.Plugins.Plugins.Configuration;
using Glimr.Plugins.Plugins.Context;
using Glimr.Plugins.Sdk.Plotting;

namespace Glimr.Plugins.Sdk.Context
{
    internal class EffectPluginContext : PluginContext, IEffectPluginContext
    {
        private PointsCollection initialPointsCollection;

        public long ElapsedMilliseconds { get; internal set; }

        public PointsCollection PointsCollection { get; set; }

        internal EffectPluginContext(IPluginConfiguration configuration, int elapsedMilliseconds, int xCount = 10, int yHeight = 10) 
            : base(configuration)
        {
            ElapsedMilliseconds = elapsedMilliseconds;

            PointsCollection = new PointsCollection(xCount, yHeight);
        }

        internal void FixInitialPointsCollection()
        {
            initialPointsCollection = PointsCollection;
        }

        internal void ResetToInitialPointsCollection()
        {
            PointsCollection = initialPointsCollection;
        }
    }
}
