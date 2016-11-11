using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Box9.Leds.FcClient
{
    public class ClientWrapper : IClientWrapper
    {
        public WebSocketState State
        {
            get
            {
                return socket.State;
            }
        }

        public string Host { get { return serverAddress.Host; } }

        protected ClientWebSocket socket;

        protected readonly Uri serverAddress;
        protected readonly int sendMaxBufferLength;
        protected readonly int receiveMaxBufferLength;

        internal ClientWrapper(Uri serverAddress)
        {
            socket = new ClientWebSocket();
            socket.Options.KeepAliveInterval = TimeSpan.FromMilliseconds(100);
            this.serverAddress = serverAddress;
            this.sendMaxBufferLength = 4096;
            this.receiveMaxBufferLength = 4096;
        }

        internal ClientWrapper(string ipAddress, int port)
            : this(new Uri(string.Format("ws://{0}:{1}", ipAddress, port)))
        {
        }

        public void Connect(CancellationToken? cancellationToken = null)
        {
            while (!(cancellationToken.HasValue && cancellationToken.Value.IsCancellationRequested) && socket.State != WebSocketState.Open)
            {
                var newSocket = new ClientWebSocket();

                try
                {
                    newSocket.ConnectAsync(serverAddress, !cancellationToken.HasValue ? CancellationToken.None : cancellationToken.Value).Wait();
                    socket = newSocket;
                }
                catch (Exception)
                {
                    newSocket.Dispose();
                }
            }
        }

        public void Close()
        {
            lock (socket)
            {
                if (socket.State == WebSocketState.Open)
                {
                    socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Finished sending data", CancellationToken.None).Wait();
                }
            }
        }

        public void Dispose()
        {
            lock (socket)
            {
                Close();
                socket.Dispose();
            }
        }
    }
}
