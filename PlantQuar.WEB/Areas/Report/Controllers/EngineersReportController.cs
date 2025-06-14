using PlantQuar.DTO.DTO.Admin;
using PlantQuar.DTO.DTO.Reports.Engineers;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using PlantQuar.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.Report.Controllers
{
    public class EngineersReportController : BaseController
    {
        // GET: Report/EngineersReport


        public ActionResult Index()
        {
            var res = APIHandeling.getData("A_SystemCode_API?AddEdit=1&Sysnum=20");
            ViewBag.operationTypes = res.Content.ReadAsAsync<List<CustomOption>>().Result;
            //eman
            var resg = APIHandeling.getData("Governate_API?AddEdit=1");
            var lst = resg.Content.ReadAsAsync<List<CustomOption>>().Result;
            ViewBag.GovList = lst;
            //
            var resemp = APIHandeling.getData("Employee_API?all=1");
            var lstemp = resemp.Content.ReadAsAsync<List<User>>().Result;
            ViewBag.lstemp = lstemp;

            return View();
        }
        [HttpPost]
        public JsonResult GetEngineers(DateTime? from, DateTime? to, int? operationType, short? empId, short? govID , short? centerID,short ? villageId)
        {


            var res = APIHandeling.getData("EngineersReport?from=" + from + "&to=" + to + "&operationType=" + operationType+ "&empId=" + empId
                + "&govID="+ govID + "&centerID="+ centerID + "&villageId=" + villageId);
            var Lst = res.Content.ReadAsAsync<List<EngineersReportDTO>>().Result;
            //return View(Lst.ToPagedList(pageNumber, pageSize));
            return Json(Lst, JsonRequestBehavior.AllowGet);
        }

    }
}