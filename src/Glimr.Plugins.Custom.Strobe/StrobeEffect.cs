using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glimr.Plugins.Plugins.Configuration;
using Glimr.Plugins.Sdk.Context;
using Glimr.Plugins.Sdk.Plugins;

namespace Glimr.Plugins.Custom.Strobe
{
    public class StrobeEffect : IEffectPlugin
    {
        public IPluginConfiguration Configure()
        {
            return PluginConfigurationFactory
                .NewConfiguration("Strobe")
                .AddIntegerInput("RGB Value - R")
                .AddIntegerInput("RGB Value - G")
                .AddIntegerInput("RGB Value - B")
                .AddIntegerInput("Frequency (Hz)");
        }

        public void Dispose()
        {
            return;
        }

        public void Run(IEffectPluginContext context)
        {
            var frequency = context.GetInput<int>("Frequency (Hz)");

            if (frequency <= 0)
            {
                return;
            }

            var color = Color.FromArgb(
                context.GetInput<int>("RGB Value - R"),
                context.GetInput<int>("RGB Value - G"),
                context.GetInput<int>("RGB Value - B"));

            var period = 1000 / frequency;

            var numberOfHalfPeriodsPassed = Math.Round((double)context.ElapsedMilliseconds / (double)2 / (double)period, 0);

            var on = numberOfHalfPeriodsPassed % 2 == 0;

            foreach (var point in context.PointsCollection)
            {
                context.PointsCollection.ChangeColorOfPoint(point, on ? color : Color.Transparent);
            }
        }
    }
}
