using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.ExportRequestNew
{
    public class ExportRequestNewAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ExportRequestNew";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ExportRequestNew_default",
                "ExportRequestNew/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}