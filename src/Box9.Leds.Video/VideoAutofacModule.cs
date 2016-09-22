using Autofac;

namespace Box9.Leds.Video
{
    public class VideoAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<VideoTransformer>().As<IVideoTransformer>();
        }
    }
}
