﻿using Autofac;
using Box9.Leds.FcClient;

namespace Box9.Leds.Video
{
    public class VideoAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<VideoReader>().As<IVideoReader>();
        }
    }
}
