namespace Glimr.Plugins.Sdk.Context
{
    public interface IInputDevicePluginContext : IPluginContext
    {
        void SignalOutputChange();
    }
}
