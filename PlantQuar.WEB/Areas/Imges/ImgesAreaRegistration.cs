using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.Imges
{
    public class ImgesAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Imges";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Imges_default",
                "Imges/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}