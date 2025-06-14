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

namespace PlantQuar.WEB.Areas.ST_Station.Controllers
{
    public class StationAccrediationCommitteeController : BaseController
    {
        // GET: ST_Station/StationAccrediationCommittee
        string apiName = "StationAccrediationCommittee_API";
        // GET: Stations/StationAccrediationCommittee
        public ActionResult Index()
        {
            @ViewBag.DateTo = DateTime.Now;
            @ViewBag.DateFrom = DateTime.Now.AddDays(-7);
            var res = APIHandeling.getData("StationActivityType_API?List=1");
            var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
            ViewBag.StationActivityTypes = lst;

            return View();
        }
        [HttpGet]
        public JsonResult getFarmCommitteeData(string stationCode, int? Status, short? stationActivityType)
        {
            try
            {
                var Outlet_ID = Session["Outlet_ID"].ToString();
                var res = APIHandeling.getData(apiName + "?stationCode=" + stationCode +
                    "&Status=" + Status + "&stationActivityType=" + stationActivityType+ "&Outlit_ID="+ Outlet_ID);
                var modelLst = res.Content.ReadAsAsync<List<Station_Accrediation_Committee_GetData_DTO>>().Result;
                //Session["Accreditation_Type_ID"]
              //  return Json(new { Result = "OK", Records = modelLst }, JsonRequestBehavior.AllowGet);
                return Json(modelLst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "getFarmCommitteeData");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
    }
}