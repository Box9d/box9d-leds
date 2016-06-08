using System.Threading.Tasks;
using Box9.Leds.Video;
using Box9.Leds.Core.LedLayouts;
using Autofac;
using Box9.Leds.FcClient;
using System;

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
            using (var container = AutofacBootstrapper.Initialize())
            using (var scope = container.BeginLifetimeScope())
            {
                IClientWrapper fcClient = new WsClientWrapper(new Uri("ws://localhost:7890"));

                var videoPlayer = scope.Resolve<IVideoPlayer>();
                videoPlayer.Load(
                    fcClient, 
                    "C:\\Users\\rzp\\Desktop\\Test Videos\\TEST VIDEO.avi",
                    new SnareDrumLedLayout());
                videoPlayer.Play();
            }

            await Task.Yield();
        }
    }
}
