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
            var videoPath = "C:\\Users\\rzp\\Desktop\\Test Videos\\Rocket League® - Neo Tokyo Trailer.mp4";

            var videoQueuer = new VideoQueuer(new Core.Configuration.LedConfiguration
            {
                VideoConfig = new Core.Configuration.VideoConfiguration
                {
                    SourceFilePath = videoPath
                },
                AudioConfig = new Core.Configuration.AudioConfiguration
                {
                    SourceFilePath = videoPath
                }
            });

            videoQueuer.QueueFrames(0, 0);
        }
    }
}
