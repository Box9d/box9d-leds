using Autofac;
using Box9.Leds.FcClient;

namespace Box9.Leds.Video
{
    public class VideoAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<FcClientAutofacModule>();

            builder.RegisterType<VideoReader>().As<IVideoReader>();
            builder.RegisterType<VideoPlayer>().As<IVideoPlayer>();
        }
    }
}
