using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.DE_Item_Data
{
    public class DE_Item_DataAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "DE_Item_Data";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "DE_Item_Data_default",
                "DE_Item_Data/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}