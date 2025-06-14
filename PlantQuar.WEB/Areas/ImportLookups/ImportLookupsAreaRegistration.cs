using System.Web.Mvc;

namespace PlantQuar.Web.Areas.ImportLookups
{
    public class ImportLookupsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ImportLookups";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ImportLookups_default",
                "ImportLookups/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}