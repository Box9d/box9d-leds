using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Glimr.Plugins.ManagementApi.Controllers
{
    public class PluginsController : ApiController
    {
        // GET api/plugins
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return request.CreateResponse(HttpStatusCode.NotImplemented, "Not implemented");
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post(HttpPostedFile file)
        {

        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
