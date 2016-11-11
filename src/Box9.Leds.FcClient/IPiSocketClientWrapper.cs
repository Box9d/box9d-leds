using Box9.Leds.FcClient.PiSocketMessages;

namespace Box9.Leds.FcClient
{
    public interface IPiSocketClientWrapper : IClientWrapper
    {
        TResponse SendMessage<TResponse>(IPiSocketRequest<TResponse> request)
            where TResponse : new();
    }
}
