using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using Owin;
using System;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using WebAppFullFrameworkApi.Models;
using WebAppFullFrameworkApi.Providers;

namespace WebAppFullFrameworkApi
{
    public static class Extensions
    {
        public static HttpConfiguration JsonFormatterCamelCasePropertyNamesContractResolver(this HttpConfiguration config)
        {
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().FirstOrDefault();
            if (jsonFormatter != null)
            {
                jsonFormatter.SerializerSettings.ContractResolver =
                    new CamelCasePropertyNamesContractResolver();
            }
            return config;
        }

        public static HttpConfiguration XmlFormatterRemove(this HttpConfiguration config)
        {
            config.Formatters.Remove(new XmlMediaTypeFormatter());
            return config;
        }

        public static IAppBuilder UseOAuthBearerAuthorizationServerCustom(this IAppBuilder app)
        {
            OAuthAuthorizationServerOptions oAuthAuthorizationServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new SimpleAuthorizationServerProvider()
            };
            //app.CreatePerOwinContext(() => (AuthContext)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(AuthContext)));
            //app.CreatePerOwinContext(() => (UserManager<IdentityUser>)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(UserManager<IdentityUser>)));            
            app.UseOAuthAuthorizationServer(oAuthAuthorizationServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
            return app;
        }

        public static IAppBuilder UseCorsAllowAll(this IAppBuilder app)
        {
            return app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);            
        }
    }
}