using System.Threading.Tasks;
using Box9.Leds.FcClient;
using Box9.Leds.Core.Messages.UpdatePixels;
using System.Threading;
using Box9.Leds.Video;
using Box9.Leds.Core.LedLayouts;

namespace Box9.Leds.TestHarness
{
    class Program
    { 
        static void Main(string[] args)
        {
            MainAsync(args).Wait();
        }

        static async Task MainAsync(string[] args)
        {

            IClientWrapper client = new HttpClientWrapper(new System.Uri("http://localhost:7891/"));
            // IClientWrapper client = new WsClientWrapper(new System.Uri("ws://localhost:7890"), 4096, 4096);

            await client.ConnectAsync();

            IVideoReader reader = new VideoReader();
            var video = reader.Transform("C:\\temp\\video background.mp4", new SnareDrumLedLayout());

            foreach (var frame in video.Frames)
            {
                var approximateGapBetweenSend = 1000 / video.Framerate;

                await client.SendPixelUpdates(new UpdatePixelsRequest
                {
                    PixelUpdates = frame.Value
                });

                Thread.Sleep(approximateGapBetweenSend);
            }

            client.Dispose();
        }
    }
}
