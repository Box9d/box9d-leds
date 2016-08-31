using System;
using System.Drawing;
using System.Threading.Tasks;
using Box9.Leds.Core.Messages;
using Box9.Leds.Core.Messages.UpdatePixels;

namespace Box9.Leds.FcClient
{
    public interface IClientWrapper : IDisposable
    {
        Task ConnectAsync();

        Task CloseAsync();

        Task<TResponse> SendMessage<TResponse>(IJsonRequest<TResponse> request)
            where TResponse : new();

        void SendPixelUpdates(UpdatePixelsRequest request);

        void SendBitmap(Bitmap bitmap);
    }
}
