using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.Pallet_Export_CheckRequest
{
    public class Pallet_Export_CheckRequestAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Pallet_Export_CheckRequest";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Pallet_Export_CheckRequest_default",
                "Pallet_Export_CheckRequest/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}