using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.DE_Countries
{
    public class DE_CountriesAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "DE_Countries";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "DE_Countries_default",
                "DE_Countries/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}