using System;
using System.Drawing;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Box9.Leds.FcClient.Messages;
using Box9.Leds.FcClient.Messages.UpdatePixels;

namespace Box9.Leds.FcClient
{
    public interface IClientWrapper : IDisposable
    {
        WebSocketState State { get; }

        void Connect(CancellationToken? cancellationToken = null);

        void CloseAsync();

        TResponse SendMessage<TResponse>(IJsonRequest<TResponse> request)
            where TResponse : new();

        void SendPixelUpdates(UpdatePixelsRequest request, CancellationToken token = default(CancellationToken));

        void SendBitmap(Bitmap bitmap);
    }
}
