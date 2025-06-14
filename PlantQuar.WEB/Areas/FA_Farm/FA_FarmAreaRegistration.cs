using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.FA_Farm
{
    public class FA_FarmAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "FA_Farm";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "FA_Farm_default",
                "FA_Farm/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}