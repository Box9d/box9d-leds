using System;
using System.Collections.Generic;

namespace Glimr.Plugins.Plugins.Context
{
    public interface IPluginContext
    {
        void SetInput<T>(string name, T value);

        void SetInput(string name, object value);

        T GetInput<T>(string name);

        T GetOutput<T>(string name);

        object GetOutput(string name);

        Dictionary<string, Type> GetPluginInputs();

        Dictionary<string, Type> GetPluginOutputs();

        void SetOutput<T>(string name, T value);

        void WriteToLog(string message);
    }
}
