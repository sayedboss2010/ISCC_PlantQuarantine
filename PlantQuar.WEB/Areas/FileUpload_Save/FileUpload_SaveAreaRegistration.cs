using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.FileUpload_Save
{
    public class FileUpload_SaveAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "FileUpload_Save";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "FileUpload_Save_default",
                "FileUpload_Save/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}