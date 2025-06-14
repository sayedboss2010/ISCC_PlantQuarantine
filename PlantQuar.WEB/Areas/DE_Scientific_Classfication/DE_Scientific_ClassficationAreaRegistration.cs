using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.DE_Scientific_Classfication
{
    public class DE_Scientific_ClassficationAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "DE_Scientific_Classfication";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "DE_Scientific_Classfication_default",
                "DE_Scientific_Classfication/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}