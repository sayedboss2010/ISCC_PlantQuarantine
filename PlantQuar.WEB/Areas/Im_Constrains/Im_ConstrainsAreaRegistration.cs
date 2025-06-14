using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.Im_Constrains
{
    public class Im_ConstrainsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Im_Constrains";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Im_Constrains_default",
                "Im_Constrains/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}