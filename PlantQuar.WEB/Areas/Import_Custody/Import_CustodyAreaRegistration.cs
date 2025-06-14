using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.Import_Custody
{
    public class Import_CustodyAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Import_Custody";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Import_Custody_default",
                "Import_Custody/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}