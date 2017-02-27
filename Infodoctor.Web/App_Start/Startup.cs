
using Microsoft.Owin;
using Owin;


[assembly: OwinStartup(typeof(Infodoctor.Web.Startup))]

namespace Infodoctor.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //enable CORS
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            ConfigureAuth(app);
        }
    }
}
