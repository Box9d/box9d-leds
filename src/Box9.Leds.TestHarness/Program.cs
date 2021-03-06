﻿using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Box9.Leds.FcClient;
using Box9.Leds.FcClient.PiSocketMessages.Frames;
using Box9.Leds.FcClient.PiSocketMessages.Playback;
using Box9.Leds.FcClient.PiSocketMessages.Video;

namespace Box9.Leds.TestHarness
{
    class Program
    {
        static void Main(string[] args)
        {
            using (IPiSocketClientWrapper piClient = new PiSocketClientWrapper("localhost", 7891))
            {
                piClient.Connect();
                var video = piClient.SendMessage(new NewVideoRequest("mynewvideo"));

                piClient.SendMessage(new AddFrameRequest(video, 0000, new System.Collections.Generic.List<Pixel>
                {
                    new Pixel(255, 255, 255),
                    new Pixel(0, 0, 0)
                }));

                piClient.SendMessage(new AddFrameRequest(video, 2500, new System.Collections.Generic.List<Pixel>
                {
                    new Pixel(0,0,0),
                    new Pixel(0,0,0)
                }));

                piClient.SendMessage(new AddFrameRequest(video, 5000, new System.Collections.Generic.List<Pixel>
                {
                    new Pixel(0,0,0),
                    new Pixel(255, 255, 255)
                }));

                Task.Run(() =>
                {
                    piClient.SendMessage(new PlayRequest(video, 2500));
                });

                piClient.SendMessage(new StopRequest(video));
            }
        }
    }
}
