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

namespace PlantQuar.WEB.Areas.Stations.Controllers
{
    public class StationAccrediationCommitteeController : BaseController
    {
        // GET: Stations/StationAccrediationCommittee
        string apiName = "StationAccrediationCommittee";
        public ActionResult Index()
        {

            var res = APIHandeling.getData("StationActivityType?List=1");
            var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
            ViewBag.StationActivityTypes = lst;

            return View();
        }
        [HttpGet]
        public JsonResult getStationCommitteeData(string stationCode, int? Status, short? stationActivityType)
        {
            try
            {
                var res = APIHandeling.getData(apiName + "?stationCode=" + stationCode +
                    "&Status=" + Status + "&stationActivityType=" + stationActivityType);
                var modelLst = res.Content.ReadAsAsync<List<Station_Accrediation_Committee_GetData_DTO>>().Result;

                return Json(modelLst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "StationAccrediationCommittee");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
    }
}