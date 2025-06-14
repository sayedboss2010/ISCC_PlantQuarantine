using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.Tam
{
    public class TamAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Tam";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Tam_default",
                "Tam/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}