namespace Glimr.Plugins.ManagementApi.Client
{
    public static class PluginInstanceFactory
    {
        public static IPluginInstance NewPluginInstance()
        {
            return new PluginInstance(new FilePoster());
        }
    }
}
