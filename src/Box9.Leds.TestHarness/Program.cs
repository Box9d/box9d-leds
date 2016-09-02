using System.Threading.Tasks;
using Box9.Leds.Video;
using Box9.Leds.Core.LedLayouts;
using Autofac;
using Box9.Leds.FcClient;
using Box9.Leds.FcClient.Search;
using AForge.Video.FFMPEG;
using System.Drawing;
using System.Drawing.Imaging;
using System;
using System.Runtime.InteropServices;
using System.IO;
using Box9.Leds.Core;
using Box9.Leds.Core.UpdatePixels;
using System.Collections.Generic;
using System.Net;
using System.Threading;

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
            Console.Write("Enter IP address of server: ");
            var input = Console.ReadLine();

            var ip = IPAddress.Parse(input);
            var cancellationTokenSource = new CancellationTokenSource();
            var cancelToken = cancellationTokenSource.Token;

            var patternTask = SendPixelPattern(ip, cancellationTokenSource.Token);

            Console.WriteLine("Press any key to exit");
            Console.Read();
            cancellationTokenSource.Cancel();

            await patternTask;
        }

        static async Task SendPixelPattern(IPAddress ip, CancellationToken token)
        {
            IClientWrapper client = new WsClientWrapper(new Uri(string.Format("ws://{0}:{1}", ip, "7890")));
            await client.ConnectAsync();

            int iterations = 1;
            while (!token.IsCancellationRequested)
            {
                var listOfPixelInfo = new List<PixelInfo>();

                for (int i = 0; i < iterations; i++)
                {
                    listOfPixelInfo.Add(new PixelInfo
                    {
                        Color = Color.Black
                    });
                }

                listOfPixelInfo.Add(new PixelInfo
                {
                    Color = Color.Blue
                });

                await client.SendPixelUpdates(new Core.Messages.UpdatePixels.UpdatePixelsRequest(listOfPixelInfo));

                Thread.Sleep(500);
                iterations++;
            }

            await client.CloseAsync();
            client.Dispose();
        }
    }
}
