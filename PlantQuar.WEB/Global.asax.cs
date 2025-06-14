using PlantQuar.WEB.App_Start;
using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PlantQuar.WEB
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //GlobalConfiguration.Configure(WebApiConfig.Register);

            HttpContext context = HttpContext.Current;
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Ssl3
                     | System.Net.SecurityProtocolType.Tls
                     | System.Net.SecurityProtocolType.Tls11
                     | System.Net.SecurityProtocolType.Tls12;
            if (context != null && context.Session != null)
            {
                //11-5-2020 fz session time out
                Session.Timeout = 150;//
            }
        }
         


        //protected void Application_End()
        //{
        //}
        //void Application_End(object sender, EventArgs e)
        //{

        //    User_Session current = User_Session.GetInstance;
        //    current.LogOut();
        //}
    }
}
