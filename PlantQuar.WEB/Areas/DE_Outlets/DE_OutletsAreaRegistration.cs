using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.DE_Outlets
{
    public class DE_OutletsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "DE_Outlets";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "DE_Outlets_default",
                "DE_Outlets/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}