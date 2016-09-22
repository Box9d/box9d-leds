using System.Collections.Generic;

namespace Glimr.Plugins.Sdk.Context
{
    public interface IPluginContext
    {
        void SetInput<T>(string name, T value);

        T GetOutput<T>(string name);

        IEnumerable<object> GetAllOutputs();

        void SetOutput<T>(string name, T value);

        T GetInput<T>(string name);

        void SignalOutputChange();
    }
}
