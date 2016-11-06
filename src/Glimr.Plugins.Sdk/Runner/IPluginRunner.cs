using System;
using System.Threading;
using Glimr.Plugins.Sdk.Chaining;

namespace Glimr.Plugins.Plugins.Runner
{
    public interface IPluginRunner : IDisposable
    {
        void StartProcessingPluginChain(IProcessingPluginChain processingPluginChain);

        void StopProcessingPluginChain();
    }
}
