using System;
using System.Drawing;
using System.Threading;
using Box9.Leds.FcClient.FadecandyMessages.UpdatePixels;

namespace Box9.Leds.FcClient
{
    public interface IFadecandyClientWrapper : IClientWrapper, IDisposable
    {
        TResponse SendMessage<TResponse>(IJsonRequest<TResponse> request)
            where TResponse : new();

        void SendPixelUpdates(UpdatePixelsRequest request, CancellationToken token = default(CancellationToken));

        void SendBitmap(Bitmap bitmap);
    }
}
