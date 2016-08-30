using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Box9.Leds.Core;
using Box9.Leds.Core.LedLayouts;
using Box9.Leds.Core.Messages;
using Box9.Leds.Core.Messages.UpdatePixels;
using Newtonsoft.Json;

namespace Box9.Leds.FcClient
{
    public class HttpClientWrapper : IClientWrapper
    {
        private Uri uri;
        private HttpClient httpClient;

        public HttpClientWrapper(Uri uri)
        {
            this.uri = uri;
            this.httpClient = new HttpClient();
        }

        public async Task ConnectAsync()
        {
            // Nothing to do here...
            await Task.Yield();

            return;
        }

        public void Dispose()
        {
            httpClient.Dispose();
        }

        public async Task<TResponse> SendMessage<TResponse>(IJsonRequest<TResponse> request)
            where TResponse : new()
        {
            var jsonRequest = JsonConvert.SerializeObject(request);

            var response = await httpClient.PostAsync(uri.AbsoluteUri, new StringContent(jsonRequest));

            return JsonConvert.DeserializeObject<TResponse>(await response.Content.ReadAsStringAsync());
        }

        public void SendPixelUpdates(UpdatePixelsRequest request)
        {
            var jsonRequest = JsonConvert.SerializeObject(request);

            try
            {
                // Do all the hard work here by sending an image, rather than letting the server draw the output.
                var bitmap = BitmapExtensions.CreateFromPixelInfo(request.PixelUpdates, new Core.Configuration.ServerConfiguration { XPixels = request.PixelUpdates.Max(p => p.X), YPixels = request.PixelUpdates.Max(p => p.Y) });
                var encodedBitmap = BitmapExtensions.Encode(bitmap);

                httpClient.PostAsync(uri.AbsoluteUri, new ByteArrayContent(encodedBitmap)).Wait();

                // await httpClient.PostAsync(uri.AbsoluteUri, new StringContent(jsonRequest));
            }
            catch (Exception)
            {
                // Surpress pixel update problems
            }

            return;
        }

        public async Task CloseAsync()
        {
            await Task.Yield();
        }
    }
}
