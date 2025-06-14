using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.DE_Fees
{
    public class DE_FeesAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "DE_Fees";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "DE_Fees_default",
                "DE_Fees/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}