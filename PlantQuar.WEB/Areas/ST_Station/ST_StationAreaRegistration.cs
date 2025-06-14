using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.ST_Station
{
    public class ST_StationAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ST_Station";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ST_Station_default",
                "ST_Station/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}