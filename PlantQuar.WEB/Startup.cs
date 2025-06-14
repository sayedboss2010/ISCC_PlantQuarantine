using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using PlantQuar.WEB.App_Start;
using System;
using System.Web.Services.Description;

[assembly: OwinStartup(typeof(PlantQuar.WEB.Startup))]

namespace PlantQuar.WEB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
{
ConfigureAuth(app);
        }



         




    }
}