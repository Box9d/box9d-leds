using Autofac;
using Box9.Leds.Video;

namespace Box9.Leds.TestHarness
{
    public static class AutofacBootstrapper
    {
        public static IContainer Initialize()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<VideoAutofacModule>();

            return builder.Build();
        }
    }
}
