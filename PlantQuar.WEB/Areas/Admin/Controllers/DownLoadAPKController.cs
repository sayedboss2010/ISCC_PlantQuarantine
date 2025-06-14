using PlantQuar.WEB.Controllers;
using System.Web.Mvc;
using System;
using System.IO;
using Newtonsoft.Json.Linq;
using PlantQuar.WEB.API;

namespace PlantQuar.WEB.Areas.Admin.Controllers
{
    public class DownLoadAPKController : BaseController
    {
        // GET: Admin/DownLoadAPK
        public ActionResult Index()
        {
            //sayed
            //Eslam
            //get latest file to download
            string filepath = FileUpload_SaveDataController.GetLatestFile("Upload/APK");
            ViewBag.filepath =$"../Upload/APK/{filepath}";
            return View();
        }
    }
}