using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.DE_Port
{
    public class DE_PortAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "DE_Port";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "DE_Port_default",
                "DE_Port/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}