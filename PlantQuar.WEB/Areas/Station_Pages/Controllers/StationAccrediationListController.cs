using PlantQuar.DTO.DTO.Station;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using PlantQuar.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.Station_Pages.Controllers
{
    public class StationAccrediationListController : BaseController
    {
        string apiName = "StationAccrediationCommittee_API";
        // GET: Station_Pages/StationAccrediationList
        public ActionResult Index(long? outlet_ID)
        {
            try
            {
                var IsApproved = 1;
                var userId = (short)Session["UserId"];
                ViewBag.Outlet_ID = Session["Outlet_ID"].ToString();

                @ViewBag.DateTo = DateTime.Now;
                @ViewBag.DateFrom = DateTime.Now.AddDays(-7);
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
        //Fill Station_Accreditation_Request_Type List
        [HttpPost]
        public JsonResult Station_Accreditation_Request_Type_AddEDIT()
        {
            try
            {
                var res = APIHandeling.getData(apiName+"?Station_Accreditation_Request_Type_AddEDIT=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Station_Accreditation_Request_Type_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        //Fill Company Name
        [HttpPost]
        public JsonResult CompanyNameLst_AddEDIT()
        {
            try
            {
                var res = APIHandeling.getData(apiName+"?CompanyNameLst_AddEDIT=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "CompanyNameLst_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        //Fill StationActivityLst_AddEDIT
        [HttpPost]
        public JsonResult StationActivityLst_AddEDIT()
        {
            try
            {
                var res = APIHandeling.getData(apiName + "?StationActivityLst_AddEDIT=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "StationActivityLst_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        public ActionResult Get_Station_List(string DateFrom = "", string DateEnd = "", string stationCode = ""
, int Status = 0, short stationActivityType = 0,long? outlet_ID=0, int? stationAccrTypeLstId=0,int? CompanyNameLst_Id=0, int? StationActivityType_ID=0)
        {
            var asd = new List<Station_Accrediation_Committee_GetData_DTO>();
            try
            {
                Session["message"] = "";
                var Outlet_ID = Session["Outlet_ID"];

                // outlet_ID = 13;
                var userId = (short)Session["UserId"];
                var res = APIHandeling.getData(apiName + "?stationCode=" +stationCode +
                   "&Status=" + Status + "&stationActivityType=" + stationActivityType
                   + "&DateFrom=" + DateFrom + "&DateEnd=" + DateEnd+ "&Outlet_ID="+ outlet_ID+ "&stationAccrTypeLstId="+ stationAccrTypeLstId+ "&CompanyNameLst_Id="+CompanyNameLst_Id+ "&StationActivityType_ID="+ StationActivityType_ID);
                var modelLst = res.Content.ReadAsAsync<List<Station_Accrediation_Committee_GetData_DTO>>().Result;
                asd = res.Content.ReadAsAsync<List<Station_Accrediation_Committee_GetData_DTO>>().Result;
                //  return Json(new { Result = "OK", Records = modelLst }, JsonRequestBehavior.AllowGet);
                ViewBag.Status = Status;
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
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), asd + "Eslam ****" + ex.Message + "//////" + ex.InnerException.Message, "Get_Station_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
    }
}