using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {//EditFromEslam_2398
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}