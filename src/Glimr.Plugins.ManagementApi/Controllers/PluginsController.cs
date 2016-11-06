using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Glimr.Plugins.Core;

namespace Glimr.Plugins.ManagementApi.Controllers
{
    public class PluginsController : ApiController
    {
        // GET api/plugins
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return request.CreateResponse(HttpStatusCode.NotImplemented, "Not implemented");
        }

        // POST api/plugins/{guid}
        [HttpPost]
        public async Task<HttpResponseMessage> Post(Guid id, string fileName)
        {
            try
            {
                var folder = PluginFolder.Get(id);
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                using (var requestStream = await Request.Content.ReadAsStreamAsync())
                using (var fileStream = File.Create(Path.Combine(folder, fileName)))
                {
                    await requestStream.CopyToAsync(fileStream);
                }

                return new HttpResponseMessage(HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                var response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                response.ReasonPhrase = ex.Message;
                return response;
            }
        }

        // PUT api/plugins/{guid}
        [HttpPut]
        public async Task<HttpResponseMessage> Put(Guid id, string fileName)
        {
            return await Post(id, fileName);
        }

        [HttpDelete]
        // DELETE api/plugins/{Guid}
        public HttpResponseMessage Delete(Guid id)
        {
            try
            {
                var appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                var pluginFolder = Path.Combine(appDataFolder, id.ToString());

                foreach (var file in Directory.EnumerateFiles(pluginFolder))
                {
                    File.Delete(file);
                }

                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
    }
}
