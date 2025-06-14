using System.Web.Mvc;

namespace PlantQuar.Web.Areas.Im_Permissions
{
    public class Im_PermissionsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Im_Permissions";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Im_Permissions_default",
                "Im_Permissions/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}