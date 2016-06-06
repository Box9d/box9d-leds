using System;
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

        private readonly ClientWebSocket socket;
        private readonly Uri serverAddress;
        private readonly int sendMaxBufferLength;
        private readonly int receiveMaxBufferLength;

        public WsClientWrapper(Uri serverAddress, int sendMaxBufferLength, int receiveMaxBufferLength)
        {
            socket = new ClientWebSocket();
            this.serverAddress = serverAddress;
            this.sendMaxBufferLength = sendMaxBufferLength;
            this.receiveMaxBufferLength = receiveMaxBufferLength;

            State = WebSocketState.None;
        }

        public async Task ConnectAsync()
        {
            await socket.ConnectAsync(serverAddress, new CancellationToken(false));

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

        public void Dispose()
        {
            socket.Dispose();
        }

        public Task SendPixelUpdates(UpdatePixelsRequest request)
        {
            // TODO:
            throw new NotImplementedException();
        }
    }
}
