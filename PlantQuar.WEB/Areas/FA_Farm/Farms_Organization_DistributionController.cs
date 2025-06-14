using PlantQuar.DTO.DTO.Farm.FarmsDistribution;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using PlantQuar.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.FA_Farm
{
    public class Farms_Organization_DistributionController : BaseController
    {
        private readonly string apiName = "Farms_Organization_Distribution_API";
        // GET: FA_Farm/Farms_Organization_Distribution
        public ActionResult Index()
        {
           // var Farms = APIHandeling.getData(apiName+"?Farm=1");
           // ViewBag.Farms = Farms.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
            return View();
        }
        public JsonResult GetFarms()
        {
            try
            {
                var res = APIHandeling.getData(apiName + "?Farm=1");
                var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "PlantCategoryList");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        [HttpGet]
        public JsonResult getFarmList(int Farm_ID)
        {
            try
            {
                var res = APIHandeling.getData(apiName+ "?Farm_ID=" + Farm_ID);

                var modelLst = res.Content.ReadAsAsync<List<FarmsDistributionListDTO>>().Result;

                return Json(modelLst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "FarmListOnLine");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

    }
}