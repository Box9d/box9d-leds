using System;
using System.Net;

namespace Glimr.Plugins.ManagementApi.Client
{
    public interface IFilePoster
    {
        PluginUploadResult PostFile(Guid pluginId, string filePath);
    }
}
