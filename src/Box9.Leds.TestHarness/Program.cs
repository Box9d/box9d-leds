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
            IClientWrapper client = new WsClientWrapper(new Uri(string.Format("ws://{0}:{1}", "192.168.0.10", "7890")));
            await client.ConnectAsync();

            for (int i = 0; i < 200; i++)
            {
                var listOfPixelInfo = new List<PixelInfo>();

                for (int j = 0; j < i; j++)
                {
                    listOfPixelInfo.Add(new PixelInfo
                    {
                        X = i,
                        Y = i,
                        Color = Color.Black
                    });
                }

                listOfPixelInfo.Add(new PixelInfo
                {
                    X = i,
                    Y = i,
                    Color = Color.Blue
                });

                await client.SendPixelUpdates(new Core.Messages.UpdatePixels.UpdatePixelsRequest(listOfPixelInfo));
            }

            await client.CloseAsync();
        }
    }
}
