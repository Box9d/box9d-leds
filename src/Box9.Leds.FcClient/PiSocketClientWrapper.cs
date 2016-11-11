using System;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using Box9.Leds.FcClient.PiSocketMessages;
using Newtonsoft.Json;

namespace Box9.Leds.FcClient
{
    public class PiSocketClientWrapper : ClientWrapper, IPiSocketClientWrapper
    {
        public PiSocketClientWrapper(Uri serverAddress)
            : base(serverAddress)
        {
        }

        public PiSocketClientWrapper(string ipAddress, int port)
            : this(new Uri(string.Format("ws://{0}:{1}", ipAddress, port)))
        {
        }

        public TResponse SendMessage<TResponse>(IPiSocketRequest<TResponse> request) where TResponse : new()
        {
            var jsonString = JsonConvert.SerializeObject(request);
            var requestData = Encoding.UTF8.GetBytes(jsonString);

            if (requestData.Length > sendMaxBufferLength)
            {
                throw new ArgumentException(string.Format("Request exceeds the max buffer length of {0}", sendMaxBufferLength));
            }

            lock (socket)
            {
                socket.SendAsync(new ArraySegment<byte>(requestData, 0, requestData.Length), WebSocketMessageType.Text, true, new CancellationToken(false)).Wait();
            }

            var resultArray = new ArraySegment<byte>(new byte[receiveMaxBufferLength]);
            WebSocketReceiveResult result = null;

            lock (socket)
            {
                result = socket.ReceiveAsync(resultArray, CancellationToken.None).Result;
            }

            var message = Encoding.UTF8.GetString(resultArray.Array.Take(result.Count).ToArray());

            return JsonConvert.DeserializeObject<TResponse>(message);
        }
    }
}
