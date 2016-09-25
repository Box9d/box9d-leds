using Box9.Leds.Core.EventArgs;

namespace Box9.Leds.Core.EventsArguments
{
    public class StringEventArgs : CustomTypeEventArgs<string>
    {
        public StringEventArgs(string value) : base(value)
        {
        }
    }
}
