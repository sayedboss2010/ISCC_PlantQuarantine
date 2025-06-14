using PlantQuar.DTO.DTO.ExportRequest;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using PlantQuar.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.ExportRequest.Controllers
{
    public class CheckRequestStoppingController : BaseController
    {
        // GET: ExportRequest/CheckRequestStopping
        string Api = "CheckRequestStopping_API";
        public ActionResult Index()
        {
            var reasons = APIHandeling.getData(Api+"?List=1&refuse=2");
            var reasonsList = reasons.Content.ReadAsAsync<List<CustomOption>>().Result;
            ViewBag.ListReasons = reasonsList;
            return View();
        }
        public ActionResult saveReasons(List<short> listIDs, string checkReqNum)
        {

            CheckRequestStoppingDTO dto = new CheckRequestStoppingDTO();

            dto.checkRequestNumber = checkReqNum;
            dto.refuseReasonsIds = listIDs;
          
            dto.User_Creation_Id = (short)Session["UserId"];
            dto.User_Creation_Date = DateTime.Now;

            var res = APIHandeling.Post(Api+"?liststop=1", dto);
            var data = res.Content.ReadAsAsync<string>().Result;
            return Json(data);
        }
    }
}