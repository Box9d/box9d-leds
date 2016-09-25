using System;
using Box9.Leds.Core.EventArgs;

namespace Box9.Leds.Core.EventsArguments
{
    public class ExceptionArgs : CustomTypeEventArgs<Exception>
    {
        public ExceptionArgs(Exception ex) : base(ex)
        {
        }
    }
}
