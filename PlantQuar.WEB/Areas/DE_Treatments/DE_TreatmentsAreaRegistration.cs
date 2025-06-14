using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.DE_Treatments
{
    public class DE_TreatmentsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "DE_Treatments";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "DE_Treatments_default",
                "DE_Treatments/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}