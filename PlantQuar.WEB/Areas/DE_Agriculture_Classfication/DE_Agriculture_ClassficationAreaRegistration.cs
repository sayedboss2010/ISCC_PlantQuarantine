using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.DE_Agriculture_Classfication
{
    public class DE_Agriculture_ClassficationAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "DE_Agriculture_Classfication";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "DE_Agriculture_Classfication_default",
                "DE_Agriculture_Classfication/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}