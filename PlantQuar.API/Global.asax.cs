//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Http;
//using System.Web.Mvc;
//using System.Web.Optimization;
//using System.Web.Routing;

//namespace PlantQuar.API
//{
//    public class WebApiApplication : System.Web.HttpApplication
//    {
//        protected void Application_Start()
//        {
//            AreaRegistration.RegisterAllAreas();
//            GlobalConfiguration.Configure(WebApiConfig.Register);
//            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
//            RouteConfig.RegisterRoutes(RouteTable.Routes);
//            BundleConfig.RegisterBundles(BundleTable.Bundles);
//        }
//    }
//}

using AutoMapper;
using PlantQuar.BLL.AppStart;
using System.Web.Http;

namespace PlantQuar.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            Mapper.Initialize(c =>
            {
                c.AddProfile<ApplicationProfile>();
            });

        }
    }
}

