using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.DE_Analysis
{
    public class DE_AnalysisAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "DE_Analysis";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "DE_Analysis_default",
                "DE_Analysis/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}