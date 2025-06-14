using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.Station_Pages
{
    public class Station_PagesAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Station_Pages";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Station_Pages_default",
                "Station_Pages/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}