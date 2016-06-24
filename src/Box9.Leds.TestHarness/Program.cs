﻿using System.Threading.Tasks;
using Box9.Leds.Video;
using Box9.Leds.Core.LedLayouts;
using Autofac;
using Box9.Leds.FcClient;
using Box9.Leds.FcClient.Search;

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
            //IVideoTransformer reader = new VideoTransformer("C:\\Users\\rzp\\Desktop\\Test Videos\\Test Video.mp4");
            //var output = reader.ExtractAndSaveAudio();
        }
    }
}
