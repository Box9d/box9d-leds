using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Glimr.Plugins.ManagementApi.Client
{
    public interface IPluginInstance
    {
        Guid PluginInstanceId { get; }

        IEnumerable<PluginUploadResult> SubmitFiles(string directoryPath);
    }
}
