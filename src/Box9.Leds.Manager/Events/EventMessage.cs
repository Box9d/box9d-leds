namespace Box9.Leds.Manager.Events
{
    public class EventMessage
    {
        public EventStatus Status { get; private set; }

        public string Message { get; private set; }

        public EventMessage(EventStatus status, string message)
        {
            Status = status;
            Message = message;
        }
    }
}
