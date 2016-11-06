using System;
using System.Drawing;
using Glimr.Plugins.Plugins.Configuration;
using Glimr.Plugins.Sdk.Context;
using Glimr.Plugins.Sdk.Plugins;

namespace Glimr.Plugins.Custom.MoveEffectPlugin
{
    public class MoveEffect : IEffectPlugin
    {
        public IPluginConfiguration Configure()
        {
            return PluginConfigurationFactory
                .NewConfiguration("Move effect")
                .AddIntegerInput("Duration in seconds")
                .AddIntegerInput("Angle in degrees");
        }

        public void Run(IEffectPluginContext context)
        {
            var durationInMilliseconds = context.GetInput<int>("Duration in seconds") * 1000;

            var angle = context.GetInput<int>("Angle in degrees");

            var sin = Math.Sin(angle * (Math.PI / 180));
            var cos = Math.Cos(angle * (Math.PI / 180));

            var fractionalMove = (double)((double)context.ElapsedMilliseconds / (double)durationInMilliseconds);

            var xMove = (int)Math.Round(context.PointsCollection.XCount * fractionalMove * sin, 0);
            var yMove = (int)Math.Round(context.PointsCollection.YCount * fractionalMove * cos, 0);

            foreach (var point in context.PointsCollection)
            {
                context.PointsCollection.AddXToPoint(point, xMove);
                context.PointsCollection.AddYToPoint(point, yMove);
            }

            //var loopingX = context.GetInput<bool>("Looping horizontal boundary");
            //var loopingY = context.GetInput<bool>("Looping vertical boundary");

            //foreach (var point in context.PointsCollection)
            //{
            //    if (loopingX && point.X + xMove > context.PointsCollection.XCount)
            //    {
            //        xMove = xMove - context.PointsCollection.XCount;
            //    }

            //    if (loopingY && point.Y + yMove > context.PointsCollection.YCount)
            //    {
            //        yMove = yMove - context.PointsCollection.YCount;
            //    }

            //    context.PointsCollection.AddXToPoint(point, (int)xMove);
            //    context.PointsCollection.AddYToPoint(point, (int)yMove);

            //    if (point.Color == Color.White)
            //    {
            //        context.WriteToLog((double)(context.ElapsedMilliseconds / 1000) + ", " + xMove + ", " + yMove);
            //    }
            //}
        }

        public void Dispose()
        {
            return;
        }

    }
}
