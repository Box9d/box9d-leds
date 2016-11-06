namespace Glimr.Plugins.Plugins.Configuration
{
    public interface IPluginConfiguration
    {
        string PluginDisplayName { get; }

        IPluginConfiguration AddStringInput(string name);

        IPluginConfiguration AddIntegerInput(string name);

        IPluginConfiguration AddStringOutput(string name);

        IPluginConfiguration AddIntegerOutput(string name);

        IPluginConfiguration AddBooleanInput(string name);

        IPluginConfiguration AddBooleanOutput(string name);
    }
}
