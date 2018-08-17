﻿using System.Web.Http;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(WebAppFullFrameworkApi.Startup))]
namespace WebAppFullFrameworkApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
            app.UseCorsAllowAll();
            app.UseOAuthBearerAuthorizationServerCustom();            
            app.UseWebApi(config);
        }
    }
}
