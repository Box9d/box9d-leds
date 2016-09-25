using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;

namespace Glimr.Plugins.ManagementApi.Client
{
    internal class PluginInstance : IPluginInstance
    {
        public Guid PluginInstanceId { get; }

        private readonly IFilePoster filePoster;

        internal PluginInstance(IFilePoster filePoster)
        {
            this.filePoster = filePoster;

            PluginInstanceId = Guid.NewGuid();
        }

        public IEnumerable<PluginUploadResult> SubmitFiles(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                throw new ArgumentException("Directory does not exist");
            }

            foreach (var file in Directory.EnumerateFiles(directoryPath))
            {
                yield return filePoster.PostFile(PluginInstanceId, file);              
            }
        }
    }
}
