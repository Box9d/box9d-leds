using System;
using System.Net.WebSockets;
using System.Threading;

namespace Box9.Leds.FcClient
{
    public interface IClientWrapper : IDisposable
    {
        WebSocketState State { get; }

        string Host { get; }

        void Connect(CancellationToken? cancellationToken = null);

        void Close();
    }
}
