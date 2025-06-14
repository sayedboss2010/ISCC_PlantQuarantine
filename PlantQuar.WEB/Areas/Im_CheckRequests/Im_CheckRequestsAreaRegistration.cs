using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.Im_CheckRequests
{
    public class Im_CheckRequestsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Im_CheckRequests";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Im_CheckRequests_default",
                "Im_CheckRequests/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}