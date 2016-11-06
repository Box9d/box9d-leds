using System;
using System.Collections.Generic;

namespace Glimr.Plugins.ManagementApi.Client
{
    public interface IPluginInstance
    {
        Guid PluginInstanceId { get; }

        IEnumerable<PluginUploadResult> SubmitFiles(string directoryPath);
    }
}
