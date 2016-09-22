using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Glimr.Plugins.ManagementApi.Startup))]

namespace Glimr.Plugins.ManagementApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
