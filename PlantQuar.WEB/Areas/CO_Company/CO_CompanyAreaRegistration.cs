using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.CO_Company
{
    public class CO_CompanyAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "CO_Company";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "CO_Company_default",
                "CO_Company/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}