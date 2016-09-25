using Box9.Leds.Core.EventArgs;

namespace Box9.Leds.Core.EventsArguments
{
    public class IntegerEventArgs : CustomTypeEventArgs<int>
    {
        public IntegerEventArgs(int value) : base(value)
        {
        }
    }
}
