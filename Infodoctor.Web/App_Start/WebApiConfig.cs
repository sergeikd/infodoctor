using System.Web.Http;
using Microsoft.Owin.Security.OAuth;

namespace Infodoctor.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            //prevent Internal Server Error 500 "Message":"An error has occurred.","ExceptionMessage":
            //"The 'ObjectContent`1' type failed to serialize the response body for content type 'application/json; charset=utf-8'"
            //when performs GET request of complex types like Clinic 
            //http://stackoverflow.com/questions/23098191/failed-to-serialize-the-response-in-web-api-with-json
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings
                .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            GlobalConfiguration.Configuration.Formatters
                .Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
