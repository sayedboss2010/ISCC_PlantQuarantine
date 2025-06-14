using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.CommonActions
{
    public class CommonActionsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "CommonActions";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "CommonActions_default",
                "CommonActions/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}