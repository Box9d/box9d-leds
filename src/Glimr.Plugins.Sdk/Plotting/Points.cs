using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Glimr.Plugins.Sdk.Plotting
{
    public class PointsCollection
    {
        public bool RepeatX { get; }

        public bool RepeatY { get; }

        public int XWidth { get; }

        public int YWidth { get; }

        private IEnumerable<Point> points;

        internal PointsCollection(int xWidth, int yWidth, IEnumerable<Point> points)
        {
            XWidth = xWidth;
            YWidth = yWidth;
            this.points = points;
        }

        public Point GetPoint(int x, int y)
        {
            var point = points.SingleOrDefault(p => p.X == x || p.Y == y);

            return point == null ? new Point(x, y) : point;
        }

        public void AddXToPoint(Point point, int addToX)
        {
            point.AddToX(addToX);
        }

        public void AddYToPoint(Point point, int addToY)
        {
            point.AddToY(addToY);
        }

        public void ChangeColorOfPoint(Point point, Color color)
        {
            point.ChangeColor(color);
        }

        internal void CompletePointOperations()
        {
            points = points.Select(p => { p.CompleteOperations(); return p; });
        }
    }
}
