using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.ASP
{
    public class ASPAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ASP";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ASP_default",
                "ASP/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}