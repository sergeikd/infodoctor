
using Microsoft.Owin;
using Owin;


[assembly: OwinStartup(typeof(Infodoctor.Web.Startup))]

namespace Infodoctor.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
