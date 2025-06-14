using PlantQuar.DTO.DTO.Common;
using PlantQuar.DTO.DTO.Import.CheckRequests;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using PlantQuar.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.Im_CheckRequests.Controllers
{
    public class List_ImCheckRequestController : BaseController
    {
        // GET: Im_CheckRequests/List_ImCheckRequest
        public ActionResult Index(long? outlet_ID)
        {
            try
            {
                var IsApproved = 1;
                var userId = (short)Session["UserId"];
                ViewBag.Outlet_ID = Session["Outlet_ID"].ToString();
              
                @ViewBag.DateTo=DateTime.Now;
                @ViewBag.DateFrom=DateTime.Now.AddDays(-7);
                if (Session["message"] != null)
                {
                    if (Session["message"].ToString() != String.Empty)
                    {
                        ViewBag.message = Session["message"].ToString();
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "List_ImRequestsController");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }           
        }
        [HttpGet]
        public ActionResult Im_CheckRequest_List(string DateFrom = "", string DateEnd = "",int selectApproveId=0
            ,int FinalResultListId=0, string CheckRequest_Number="", long Company_ID =0 )
        {
            var asd=new List<ImCheckRequestListDTO>();
            try
            {
                Session["message"] = "";
                var Outlet_ID =  Session["Outlet_ID"];
              
                // outlet_ID = 13;
                var userId = (short)Session["UserId"];
                var res = APIHandeling.getData("Im_CheckRequests_API?userId="+ userId + "&DateFrom=" + DateFrom +
                   "&DateEnd=" + DateEnd + "&selectApproveId=" + selectApproveId 
                   + "&FinalResultListId=" + FinalResultListId+ "&outlet="+ Outlet_ID + "&CheckRequest_Number=" + CheckRequest_Number+ "&Company_ID="+ Company_ID);
                var modelLst = res.Content.ReadAsAsync<List<ImCheckRequestListDTO>>().Result;
                asd= res.Content.ReadAsAsync<List<ImCheckRequestListDTO>>().Result;
                //  return Json(new { Result = "OK", Records = modelLst }, JsonRequestBehavior.AllowGet);
                ViewBag.selectApproveId = selectApproveId;
                if (modelLst.Count() > 0)
                {
                    return View(modelLst);
                }
                else
                {
                    Session["message"] = "لا توجد بيانات للبحث";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), asd +"sayedd ****"+ ex.Message+"//////"+ex.InnerException.Message, "List_ImRequestsController");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        [HttpPost]
        public JsonResult Im_CheckRequest_List2(string IsApproved = "1", string requestnumber = "")
        {
            try
            {
                var userId = (short)Session["UserId"];
                var res = APIHandeling.getData("Im_CheckRequests_API?ImCheckRequest_Number=" + requestnumber +
                   "&IsApproved=" + IsApproved +"&userId="+ userId );
                var modelLst = res.Content.ReadAsAsync<List<ImCheckRequestListDTO>>().Result;

                return Json(new { Result = "OK", Records = modelLst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "List_ImRequestsController");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        [HttpPost]
        public JsonResult CheckRequestStatusLst_AddEDIT()
        {
            try
            {
              
                var res = APIHandeling.getData("Im_CheckRequests_API?CheckRequestStatusLst=1");

                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "ItemType_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        [HttpPost]
        public JsonResult Company_List( long Outlet_ID)
        {
            try
            {
                var res = APIHandeling.getData("Company_National_API?Im_CheckRequest=1&Outlet_ID="+ Outlet_ID);
                var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Company_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
    }
}