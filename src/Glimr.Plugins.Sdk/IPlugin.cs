using Glimr.Plugins.Sdk.Configuration;

namespace Glimr.Plugins.Sdk
{
    public interface IPlugin
    {
        IPluginConfiguration Configure();
    }
}
