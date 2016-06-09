using System.Threading.Tasks;
using Box9.Leds.Video;
using Box9.Leds.Core.LedLayouts;
using Autofac;
using Box9.Leds.FcClient;
using System;
using Box9.Leds.FcClient.Search;
using System.Net;
using System.Collections.Generic;
using Box9.Leds.Core.Messages.ConnectedDevices;
using System.Threading;

namespace Box9.Leds.TestHarness
{
    class Program
    {
        private static int searchProgressCounter;
        private static ClientSearch clientSearch;

        static void Main(string[] args)
        {
            MainAsync(args).Wait();
        }

        static async Task MainAsync(string[] args)
        {
            clientSearch = new ClientSearch();
            clientSearch.IPAddressSearched += UpdateProgress;
            clientSearch.ServerFound += ServerFound;

            clientSearch.SearchForFadecandyServers(7890, CancellationToken.None);

            using (var container = AutofacBootstrapper.Initialize())
            using (var scope = container.BeginLifetimeScope())
            {
                IClientWrapper fcClient = new WsClientWrapper(new Uri("ws://localhost:7890"));

                var videoPlayer = scope.Resolve<IVideoPlayer>();
                
                videoPlayer.Load(
                    fcClient,
                    "C:\\Users\\rzp\\Desktop\\Test Videos\\Bubble Sound - Pre-rendered 3D Music Visualizer (1).mp4",
                    new SnareDrumLedLayout());

                await videoPlayer.Play(CancellationToken.None);
            }
        }

        private static void UpdateProgress()
        {
            searchProgressCounter++;

            var percentageProgress = Math.Round((double)searchProgressCounter / (double)clientSearch.TotalIPSearches * 100, 0);

            Console.WriteLine(percentageProgress + "% completed");
        }

        private static void ServerFound(IPAddress client, IEnumerable<ConnectedDeviceResponse> connectedDevices)
        {
            Console.WriteLine("server found at {0}", client);
        }
    }
}
