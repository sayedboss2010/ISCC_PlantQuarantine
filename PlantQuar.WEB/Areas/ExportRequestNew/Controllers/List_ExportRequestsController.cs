using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using PlantQuar.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using PlantQuar.DTO.DTO.ExportRequestNew;
 

namespace PlantQuar.WEB.Areas.ExportRequestNew.Controllers
{
    public class List_ExportRequestsController : BaseController
    {
        // GET: ExportRequestNew/List_ExportRequests
        string apiName = "List_ExportRequests_API"; 
        public ActionResult Index()
        {
            var res_CommitteeType = APIHandeling.getData("CommitteeType_API?Lst=1");
            var lst_CommitteeType = res_CommitteeType.Content.ReadAsAsync<List<CustomOptionShortId>>().Result;
            ViewBag.CommitteeType_ID = new SelectList(lst_CommitteeType.ToList(), "Value", "DisplayText");
            return View();
        }
        //عرض جميع الطلبات
        [HttpPost]
        public JsonResult Export_CheckRequest_IsApproved(int CommitteeType_ID, string IsApproved = "-1", string requestnumber = "")
        {
            try
            {
                var res = APIHandeling.getData(apiName + "?CommitteeType_ID=" + CommitteeType_ID +
                    "&IsApproved=" + IsApproved + "&requestnumber=" + requestnumber);
                var modelLst = res.Content.ReadAsAsync<List<List_ExportRequestsDTO>>().Result;

                return Json(new { Result = "OK", Records = modelLst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "List_ExportRequestsController");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        // التفاصيل
        public ActionResult getRequestData(long reqId = 0)
        {
            return RedirectToAction("Index", "ExRequest_Details", new { requestId = reqId, area = "ExportRequest" });
        }


        [HttpPost]
        public JsonResult Export_CheckRequest_UpdateStatus(long CommitteID)
        {
            try
            {
                DeleteParameters obj = new DeleteParameters();
                obj.id = CommitteID;
                // User_Session Current = User_Session.GetInstance;
                obj.Userid = (short)Session["UserId"]; ;
                obj._DateNow = DateTime.Now;
                APIHandeling.Put("Committee?update=1", obj);

                var res = APIHandeling.Put(apiName, obj);
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "List_ExportRequestsController");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        //createCommittee
        public ActionResult createCommittee(long reqId = 0, long committeeId = 0, byte? Committe_Type_Id = null)
        {
            //CommitteeType_ID
            //requestId
            return RedirectToAction("Index", "EX_Committee", new { committeeId = committeeId, requestId = reqId, CommitteeType_ID = Committe_Type_Id, area = "ExportRequest" });
        }


        //generateCertificate
        public ActionResult generateCertificate(long reqId = 0)
        {
            return RedirectToAction("Index", "Certificate", new { requestId = reqId, area = "ExportRequest" });
        }

        //payFees
        public ActionResult payFees(long reqId = 0)
        {
            return RedirectToAction("Index", "ExportPayment", new { requestId = reqId, area = "Payment" });
        }
    }
}