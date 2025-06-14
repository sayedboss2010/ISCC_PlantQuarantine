using PlantQuar.DTO.DTO.Export_CheckRequest;
using PlantQuar.WEB.App_Start;
using PlantQuar.WEB.Controllers;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.Export_CheckRequest.Controllers
{
    public class EX_CheckRequestCertificateController : BaseController
    {
        // GET: Export_CheckRequest/EX_CheckRequestCertificate
        public ActionResult Index(string ExCheckRequest_Number)
        {
            var res = APIHandeling.getData("EX_CheckRequestCertificate_API?ExCheckRequest_Number=" + ExCheckRequest_Number);
        
            var Lst = res.Content.ReadAsAsync<List<EX_CheckRequestCertificatesDTO>>().Result;
            var Outlet_User_ID = Session["Outlet_ID"];
            ViewBag.Outlet_Name = Session["Outlet_Name"].ToString();
            ViewBag.FullName = Session["FullName"].ToString();
             // return (Lst, JsonRequestBehavior.AllowGet);
           return View(Lst);
            //return Json(lst, JsonRequestBehavior.AllowGet);
        }
    }
}