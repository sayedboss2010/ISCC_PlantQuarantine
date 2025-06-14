using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.General_Permission
{
    public class General_PermissionAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "General_Permission";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "General_Permission_default",
                "General_Permission/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}