using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.EX_User
{
    public class EX_UserAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "EX_User";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "EX_User_default",
                "EX_User/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}