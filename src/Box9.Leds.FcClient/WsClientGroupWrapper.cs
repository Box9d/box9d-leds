using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Box9.Leds.Core.Messages.UpdatePixels;

namespace Box9.Leds.FcClient
{
    public class WsClientGroupWrapper : IDisposable
    {
        private readonly Dictionary<Uri, ClientWebSocket> connectionGroup;
        private readonly Queue<Tuple<Uri, byte[]>> updatePixelsQueue;

        public delegate void FinishedUpdatesHandler();
        public event FinishedUpdatesHandler FinishedUpdates;
        public bool dequeueingStarted;
        public bool cancelQueueRead;

        public WsClientGroupWrapper(params Uri[] serverAddresses)
        {
            connectionGroup = new Dictionary<Uri, ClientWebSocket>();
            updatePixelsQueue = new Queue<Tuple<Uri, byte[]>>();

            foreach (var serverAddress in serverAddresses)
            {
                connectionGroup.Add(serverAddress, new ClientWebSocket());
            }

            dequeueingStarted = false;

            FinishedUpdates += () =>
            {
            };
        }

        public async Task ConnectAsync()
        {
            var changedSockets = new Dictionary<Uri, ClientWebSocket>();

            foreach (var connection in connectionGroup)
            {
                if (connection.Value.State != WebSocketState.Open)
                {
                    var webSocket = connectionGroup[connection.Key];
                    webSocket.Dispose();

                    webSocket = new ClientWebSocket();
                    await webSocket.ConnectAsync(connection.Key, CancellationToken.None);

                    changedSockets.Add(connection.Key, webSocket);
                }
            }

            foreach (var changedSocket in changedSockets)
            {
                connectionGroup[changedSocket.Key] = changedSocket.Value;
            }
        }

        public async Task CloseAsync()
        {
            cancelQueueRead = true;

            foreach (var connection in connectionGroup)
            {
                await connection.Value.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
            }
        }

        public void Dispose()
        {
            cancelQueueRead = true;

            foreach (var connection in connectionGroup)
            {
                connection.Value.Dispose();
            }
        }

        public void SendPixelUpdates(UpdatePixelsRequest request, Uri client)
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
                dequeueingStarted = true;
                Task.Run(async () => await DequeuePixelUpdates());
            }

            lock (updatePixelsQueue)
            {
                updatePixelsQueue.Enqueue(new Tuple<Uri, byte[]>(client, data.ToArray()));
            }
        }
        private async Task DequeuePixelUpdates()
        {
            while (!cancelQueueRead)
            {
                Tuple<Uri, byte[]> update = null;
                lock (updatePixelsQueue)
                {
                    if (updatePixelsQueue.Count > 0)
                    {
                        update = updatePixelsQueue.Dequeue();
                    }
                }

                if (update != null)
                {
                    await connectionGroup[update.Item1].SendAsync(new ArraySegment<byte>(update.Item2), WebSocketMessageType.Binary, true, CancellationToken.None);
                }
            }

            lock (updatePixelsQueue)
            {
                updatePixelsQueue.Clear();
            }

            dequeueingStarted = false;
        }
    }
}
