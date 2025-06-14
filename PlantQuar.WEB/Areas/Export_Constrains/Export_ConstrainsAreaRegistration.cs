using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.Export_Constrains
{
    public class Export_ConstrainsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Export_Constrains";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Export_Constrains_default",
                "Export_Constrains/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}