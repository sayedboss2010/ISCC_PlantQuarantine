using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.DE_Import
{
    public class DE_ImportAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "DE_Import";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "DE_Import_default",
                "DE_Import/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}