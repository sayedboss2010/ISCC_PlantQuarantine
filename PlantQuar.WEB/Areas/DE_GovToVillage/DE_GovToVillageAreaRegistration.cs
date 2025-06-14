using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.DE_GovToVillage
{
    public class DE_GovToVillageAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "DE_GovToVillage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "DE_GovToVillage_default",
                "DE_GovToVillage/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}