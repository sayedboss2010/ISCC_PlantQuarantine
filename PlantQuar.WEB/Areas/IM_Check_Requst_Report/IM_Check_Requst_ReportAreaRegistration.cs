using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.IM_Check_Requst_Report
{
    public class IM_Check_Requst_ReportAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "IM_Check_Requst_Report";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "IM_Check_Requst_Report_default",
                "IM_Check_Requst_Report/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}