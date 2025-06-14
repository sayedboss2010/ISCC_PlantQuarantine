using PlantQuar.WEB.API;
using PlantQuar.WEB.Controllers;
using System.Web;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.Admin.Controllers
{
    public class UploadAPKController :  Controller
    {
        // GET: Admin/UploadAPK
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            try
            {
                FileUpload_SaveDataController fileUpload = new FileUpload_SaveDataController();
                fileUpload.Upload_File_Data(file, "APK");
                ViewBag.Message = "File Uploaded Successfully!!";
                return View();
            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                return View();
            }
        }
    }
}