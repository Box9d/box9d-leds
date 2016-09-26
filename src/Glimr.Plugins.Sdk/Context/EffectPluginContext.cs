using System;
using Glimr.Plugins.Plugins.Configuration;
using Glimr.Plugins.Plugins.Context;
using Glimr.Plugins.Sdk.Plotting;

namespace Glimr.Plugins.Sdk.Context
{
    internal class EffectPluginContext : PluginContext, IEffectPluginContext
    {
        private PointsCollection pointsCollection;

        public int ElapsedMilliseconds { get; }

        public PointsCollection PointsCollection { get; set; }

        internal EffectPluginContext(IPluginConfiguration configuration, int elapsedMilliseconds) 
            : base(configuration)
        {
            ElapsedMilliseconds = elapsedMilliseconds;
        }
    }
}
