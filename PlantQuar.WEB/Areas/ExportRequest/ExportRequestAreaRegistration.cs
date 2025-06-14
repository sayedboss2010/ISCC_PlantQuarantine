using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.ExportRequest
{
    public class ExportRequestAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ExportRequest";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ExportRequest_default",
                "ExportRequest/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}