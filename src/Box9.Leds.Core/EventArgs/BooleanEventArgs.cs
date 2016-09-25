using Box9.Leds.Core.EventArgs;

namespace Box9.Leds.Core.EventsArguments
{
    public class BooleanEventArgs : CustomTypeEventArgs<bool>
    {
        public BooleanEventArgs(bool value) : base(value)
        {
        }
    }
}
