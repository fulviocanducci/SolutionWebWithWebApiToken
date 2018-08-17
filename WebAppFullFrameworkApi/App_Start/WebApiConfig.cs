using System.Web.Http;
using WebApiContrib.IoC.Ninject;

namespace WebAppFullFrameworkApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.DependencyResolver = new NinjectResolver(NinjectResolverKernel.Load());

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.JsonFormatterCamelCasePropertyNamesContractResolver();

            config.XmlFormatterRemove();
            
        }
    }
}
