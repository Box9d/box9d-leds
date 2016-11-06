using System.Drawing;

namespace Glimr.Plugins.Sdk
{
    public class Point
    {
        private int cummulativeXOperations;

        private int cummulativeYOperations;

        private Color colorOperation;

        public int X { get; private set; }

        public int Y { get; private set; }

        public Color Color { get; private set; }

        internal Point(int x, int y, Color? color = null)
        {
            X = x;
            Y = y;
            Color = color.HasValue ? color.Value : Color.Transparent;

            cummulativeXOperations = 0;
            cummulativeYOperations = 0;
        }

        internal void AddToX(int addToX)
        {
            cummulativeXOperations += addToX;
        }

        internal void AddToY(int addToY)
        {
            cummulativeYOperations += addToY;
        }

        internal void ChangeColor(Color color)
        {
            colorOperation = color;
        }

        internal void CompleteOperations()
        {
            X += cummulativeXOperations;
            Y += cummulativeYOperations;
            Color = colorOperation;

            cummulativeXOperations = 0;
            cummulativeYOperations = 0;
        }
    }
}
