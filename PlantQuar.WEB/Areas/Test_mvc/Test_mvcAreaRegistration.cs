using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.Test_mvc
{
    public class Test_mvcAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Test_mvc";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Test_mvc_default",
                "Test_mvc/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}