using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.DE_Packages
{
    public class DE_PackagesAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "DE_Packages";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "DE_Packages_default",
                "DE_Packages/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}