using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Box9.Leds.Core.Messages;
using Box9.Leds.Core.Messages.UpdatePixels;
using Newtonsoft.Json;

namespace Box9.Leds.FcClient
{
    public class WsClientWrapper : IClientWrapper
    {
        public WebSocketState State { get; private set; }

        private ClientWebSocket socket;
        private readonly Uri serverAddress;
        private readonly int sendMaxBufferLength;
        private readonly int receiveMaxBufferLength;
        private readonly Queue<byte[]> updatePixelsQueue;
        private bool cancelQueueRead;
        private bool dequeueingStarted;

        public delegate void FinishedUpdatesHandler();
        public event FinishedUpdatesHandler FinishedUpdates;

        public WsClientWrapper(Uri serverAddress)
        {
            updatePixelsQueue = new Queue<byte[]>();

            socket = new ClientWebSocket();
            this.serverAddress = serverAddress;
            this.sendMaxBufferLength = 4096;
            this.receiveMaxBufferLength = 4096;

            State = WebSocketState.None;

            dequeueingStarted = false;

            FinishedUpdates += () =>
            {
            };
        }

        public async Task ConnectAsync()
        {
            if (socket.State != WebSocketState.Open)
            {
                socket.Dispose();
                socket = new ClientWebSocket();
                await socket.ConnectAsync(serverAddress, CancellationToken.None);
            }

            State = socket.State;
        }

        public async Task<TResponse> SendMessage<TResponse>(IJsonRequest<TResponse> request)
            where TResponse : new()
        {
            var jsonString = JsonConvert.SerializeObject(request);
            var requestData = Encoding.UTF8.GetBytes(jsonString);

            if (requestData.Length > sendMaxBufferLength)
            {
                throw new ArgumentException(string.Format("Request exceeds the max buffer length of {0}", sendMaxBufferLength));
            }

            await socket.SendAsync(new ArraySegment<byte>(requestData, 0, requestData.Length), WebSocketMessageType.Text, true, new CancellationToken(false));

            var resultArray = new ArraySegment<byte>(new byte[receiveMaxBufferLength]);
            WebSocketReceiveResult result = null;

            result = await socket.ReceiveAsync(resultArray, CancellationToken.None);
            var message = Encoding.UTF8.GetString(resultArray.Array.Take(result.Count).ToArray());

            return JsonConvert.DeserializeObject<TResponse>(message);
        }

        public async Task CloseAsync()
        {
            cancelQueueRead = true;

            await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
        }

        public void Dispose()
        {
            cancelQueueRead = true;

            socket.Dispose();
        }

        public void SendPixelUpdates(UpdatePixelsRequest request)
        {
            var data = new List<byte>
            {
                0,0,0,0
            };

            foreach (var pixel in request.PixelUpdates)
            {
                data.Add(pixel.Color.R);
                data.Add(pixel.Color.G);
                data.Add(pixel.Color.B);
            }

            if (!dequeueingStarted)
            {
                Task.Run(async () => await DequeuePixelUpdates());
            }

            lock(updatePixelsQueue)
            {
                updatePixelsQueue.Enqueue(data.ToArray());
            }
        }

        private async Task DequeuePixelUpdates()
        {
            dequeueingStarted = true;

            while (!cancelQueueRead)
            {
                byte[] update = null;
                lock (updatePixelsQueue)
                {
                    if (updatePixelsQueue.Count > 0)
                    {
                        update = updatePixelsQueue.Dequeue();
                    }
                }

                if (update != null)
                {
                    await socket.SendAsync(new ArraySegment<byte>(update), WebSocketMessageType.Binary, true, CancellationToken.None);
                }
            }

            lock(updatePixelsQueue)
            {
                updatePixelsQueue.Clear();
            }

            dequeueingStarted = false;
        }

        public void SendBitmap(Bitmap bitmap)
        {
            throw new InvalidOperationException("Cannot send a bitmap to a FadeCandy server");
        }
    }
}
