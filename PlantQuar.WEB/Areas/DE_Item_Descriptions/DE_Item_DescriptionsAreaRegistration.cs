using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.DE_Item_Descriptions
{
    public class DE_Item_DescriptionsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "DE_Item_Descriptions";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "DE_Item_Descriptions_default",
                "DE_Item_Descriptions/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}