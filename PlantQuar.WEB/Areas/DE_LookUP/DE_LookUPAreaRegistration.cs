using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.DE_LookUP
{
    public class DE_LookUPAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "DE_LookUP";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "DE_LookUP_default",
                "DE_LookUP/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}