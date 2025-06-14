using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.Shipping
{
    public class ShippingAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Shipping";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Shipping_default",
                "Shipping/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}