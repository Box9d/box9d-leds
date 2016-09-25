namespace Box9.Leds.Core.EventArgs
{
    public abstract class CustomTypeEventArgs<T> : System.EventArgs
    {
        public T Value { get; }

        public CustomTypeEventArgs(T value)
        {
            Value = value;
        }
    }
}
