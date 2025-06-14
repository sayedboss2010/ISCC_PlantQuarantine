using PlantQuar.DTO.DTO.Farm.FarmRequest;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using PlantQuar.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.FA_Farm.Controllers
{
    public class FarmRequestController : BaseController
    {
        // GET: FA_Farm/FarmRequest
        string apiName = "FarmRequest_API"; // GET: ExportRequest/List_ExportRequests
        public ActionResult Index(long? farmId)
        {
            //var res = APIHandeling.getData(apiName + "?all=1");

            //var modelLst = res.Content.ReadAsAsync<List<Farm_Committee_GetData_DTO>>().Result;

            //int pageSize = 15;
            //int pageNumber = (page ?? 1);
            if(farmId != null)
            {
                var farmCode = "";
                var res = APIHandeling.getData(apiName + "?farmcode=" + farmCode + "&Status=-1&farmId=" + farmId);
                var modelLst = res.Content.ReadAsAsync<List<Farm_Committee_GetData_DTO>>().Result;

                ViewBag.farmId = farmId;

                return View(modelLst);
            }
            else
            {
                return View();
            }

            //return View(modelLst.ToPagedList(pageNumber, pageSize));
        }

        //public ActionResult getRequestData(long reqId = 0)
        //{
        //    return RedirectToAction("Index", "ExRequest_Details", new { requestId = reqId, area = "ExportRequest" });
        //}

        //LOAD SEARCH
        [HttpGet]
        public JsonResult getFarmCommitteeData(string farmcode, int? Status,long? farmId)
        {
            try
            {
                var res = APIHandeling.getData(apiName + "?farmcode=" + farmcode +
                    "&Status=" + Status + "&farmId=" + farmId);
                var modelLst = res.Content.ReadAsAsync<List<Farm_Committee_GetData_DTO>>().Result;

                return Json(modelLst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "List_ExportRequestsController");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
    }
}