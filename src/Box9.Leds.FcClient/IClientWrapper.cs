﻿using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using Box9.Leds.FcClient.Messages;
using Box9.Leds.FcClient.Messages.UpdatePixels;

namespace Box9.Leds.FcClient
{
    public interface IClientWrapper : IDisposable
    {
        Task ConnectAsync();

        Task CloseAsync();

        Task<TResponse> SendMessage<TResponse>(IJsonRequest<TResponse> request)
            where TResponse : new();

        Task SendPixelUpdates(UpdatePixelsRequest request, CancellationToken token = default(CancellationToken));

        void SendBitmap(Bitmap bitmap);
    }
}
