using Autofac;

namespace Box9.Leds.TestHarness
{
    public static class AutofacBootstrapper
    {
        public static IContainer Initialize()
        {
            var builder = new ContainerBuilder();

            return builder.Build();
        }
    }
}
