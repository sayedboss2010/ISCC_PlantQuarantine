using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.Export_CheckRequest_New
{
    public class Export_CheckRequest_NewAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Export_CheckRequest_New";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Export_CheckRequest_New_default",
                "Export_CheckRequest_New/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}