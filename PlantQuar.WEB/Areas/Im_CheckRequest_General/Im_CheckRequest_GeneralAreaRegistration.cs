using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.Im_CheckRequest_General
{
    public class Im_CheckRequest_GeneralAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Im_CheckRequest_General";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Im_CheckRequest_General_default",
                "Im_CheckRequest_General/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}