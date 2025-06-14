using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.Export_CheckRequest
{
    public class Export_CheckRequestAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Export_CheckRequest";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Export_CheckRequest_default",
                "Export_CheckRequest/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}