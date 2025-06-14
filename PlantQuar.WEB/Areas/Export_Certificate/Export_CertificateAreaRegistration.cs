using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.Export_Certificate
{
    public class Export_CertificateAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Export_Certificate";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Export_Certificate_default",
                "Export_Certificate/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}