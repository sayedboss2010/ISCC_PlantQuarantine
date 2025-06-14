using System.Web.Mvc;

namespace PlantQuar.Web.Areas.LaboratoryResult
{
    public class LaboratoryResultAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "LaboratoryResult";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "LaboratoryResult_default",
                "LaboratoryResult/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}