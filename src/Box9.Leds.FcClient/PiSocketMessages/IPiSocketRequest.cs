namespace Box9.Leds.FcClient.PiSocketMessages
{
    public interface IPiSocketRequest<TResponse> : IJsonRequest<TResponse> where TResponse: new()
    {
        string Type { get; }
    }
}
