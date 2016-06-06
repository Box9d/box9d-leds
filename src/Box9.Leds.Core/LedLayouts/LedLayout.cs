namespace Box9.Leds.Core.LedLayouts
{
    public class LedLayout
    {
        public int XNumberOfPixels { get; }

        public int YNumberOfPixels { get; }

        protected LedLayout(int xNumberOfPixels, int yNumberOfPixels)
        {
            XNumberOfPixels = xNumberOfPixels;
            YNumberOfPixels = yNumberOfPixels;
        }
    }
}
