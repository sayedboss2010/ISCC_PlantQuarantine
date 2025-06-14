using PlantQuar.DTO.DTO.Admin;
using PlantQuar.WEB.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.Admin
{
    public class LogsController : Controller
    {
        // GET: Admin/Logs
        public ActionResult Index()
        {

            var res = APIHandeling.getData("Table_Action_Log_API");
            var modelLst = res.Content.ReadAsAsync<List<LogTableNamecsDTO>>().Result;

             


            ViewBag.currentTables = modelLst;

            return View();
        }



        public JsonResult getLogs(decimal number, int tableid)
        {                         

            var res1 = APIHandeling.getData("Table_Action_Log_API?id="+number+"&tableName="+tableid+"");
            var modelLst1 = res1.Content.ReadAsAsync<List<logDataDTO>>().Result;

            return Json(modelLst1, JsonRequestBehavior.AllowGet);

        }

    }
}