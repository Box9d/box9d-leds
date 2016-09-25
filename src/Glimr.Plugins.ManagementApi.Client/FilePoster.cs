using System;
using System.IO;
using System.Net;

namespace Glimr.Plugins.ManagementApi.Client
{
    public class FilePoster : IFilePoster
    {
        private readonly string pluginFilePostAddress;

        public FilePoster()
        {
            pluginFilePostAddress = "http://localhost:61525/api/plugins/{0}?filename={1}";
        }

        public PluginUploadResult PostFile(Guid pluginId, string filePath)
        {
            var fileName = filePath.Substring(filePath.LastIndexOf("\\") + 1);

            if (!fileName.EndsWith(".dll"))
            {
                return new PluginUploadResult("File not recognised as .dll");
            }

            var webRequest = WebRequest.CreateHttp(string.Format(pluginFilePostAddress, pluginId, fileName));
            webRequest.Method = "POST";

            try
            {
                using (var fileStream = File.Open(filePath, FileMode.Open))
                using (var requestStream = webRequest.GetRequestStream())
                {
                    fileStream.CopyTo(requestStream);
                }

                using (var response = (HttpWebResponse)webRequest.GetResponse())
                {
                    return new PluginUploadResult(response.StatusDescription, response.StatusCode);
                }
            }
            catch (WebException ex)
            {
                using (var response = (HttpWebResponse)ex.Response)
                {
                    return new PluginUploadResult(response.StatusDescription, response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                return new PluginUploadResult(ex.Message);
            }
        }
    }
}
