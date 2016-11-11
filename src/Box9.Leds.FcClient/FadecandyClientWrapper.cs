using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using Box9.Leds.FcClient.FadecandyMessages;
using Box9.Leds.FcClient.FadecandyMessages.UpdatePixels;
using Newtonsoft.Json;

namespace Box9.Leds.FcClient
{
    public class FadecandyClientWrapper : ClientWrapper, IFadecandyClientWrapper
    {       
        public FadecandyClientWrapper(Uri serverAddress)
            : base(serverAddress)
        {
        }

        public FadecandyClientWrapper(string ipAddress, int port)
            : this(new Uri(string.Format("ws://{0}:{1}", ipAddress, port)))
        {
        }

        public TResponse SendMessage<TResponse>(IJsonRequest<TResponse> request)
            where TResponse : new()
        {
            var jsonString = JsonConvert.SerializeObject(request);
            var requestData = Encoding.UTF8.GetBytes(jsonString);

            if (requestData.Length > sendMaxBufferLength)
            {
                throw new ArgumentException(string.Format("Request exceeds the max buffer length of {0}", sendMaxBufferLength));
            }

            lock(socket)
            {
                socket.SendAsync(new ArraySegment<byte>(requestData, 0, requestData.Length), WebSocketMessageType.Text, true, new CancellationToken(false)).Wait();
            }

            var resultArray = new ArraySegment<byte>(new byte[receiveMaxBufferLength]);
            WebSocketReceiveResult result = null;

            lock(socket)
            {
                result = socket.ReceiveAsync(resultArray, CancellationToken.None).Result;
            }

            var message = Encoding.UTF8.GetString(resultArray.Array.Take(result.Count).ToArray());

            return JsonConvert.DeserializeObject<TResponse>(message);
        }

        public void SendPixelUpdates(UpdatePixelsRequest request, CancellationToken token)
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

            lock (socket)
            {
                try
                {
                    socket.SendAsync(new ArraySegment<byte>(data.ToArray()), WebSocketMessageType.Binary, true, token).Wait();
                }
                catch (Exception)
                {
                    // Swallow exceptions and handle re-connection elsewhere
                }               
            }
        }

        public void SendBitmap(Bitmap bitmap)
        {
            // Surpress - it's just too slow and should never be done :)
        }
    }
}
