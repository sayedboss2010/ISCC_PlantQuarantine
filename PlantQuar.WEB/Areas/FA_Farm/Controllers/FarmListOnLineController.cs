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
    public class FarmListOnLineController : BaseController
    {
        // GET: FA_Farm/FarmListOnLine
        [HttpGet]
        public ActionResult Index()
        {
            @ViewBag.DateTo = DateTime.Now;
            @ViewBag.DateFrom = DateTime.Now.AddDays(+7);
            var Item = APIHandeling.getData("Farm_Constrain_API?Item=1");
            ViewBag.Item = Item.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
            var resg = APIHandeling.getData("Governate_API?AddEdit=1");
            var lst = resg.Content.ReadAsAsync<List<CustomOption>>().Result;
            ViewBag.GovList = lst;
            //var res = APIHandeling.getData("FarmRequest_API?List=1");
            //if (res.Content.ReadAsAsync<FarmsListDTO>().Result != null)
            //{
            //var Lst = res.Content.ReadAsAsync<List<FarmsListDTO>>().Result;
            return View();
            //}
            //else
            //{
            //    return View();
            //}
            // return View(Lst);
        }
        [HttpGet]
        public JsonResult getFarmListByItemId(long? itemId)
        {
            try
            {
                var res = APIHandeling.getData("FarmRequest_API?itemId=" + itemId);
                var modelLst = res.Content.ReadAsAsync<List<FarmsListDTO>>().Result;
                return Json(modelLst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "FarmListOnLine");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        //eman request status change
        //[HttpGet]
        //public JsonResult getFarmListByStatus(int status)
        //{
        //    try
        //    {
        //       bool? isStatus =null;

        //        if(status  ==2)
        //        {
        //            isStatus = true;

        //        }else if(status == 3)
        //        {
        //            isStatus = false;
        //        }
        //        var res = APIHandeling.getData("FarmRequest_API?Is_Status=" + isStatus);

        //        var modelLst = res.Content.ReadAsAsync<List<FarmsListDTO>>().Result;

        //        return Json(modelLst, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "FarmListOnLine");
        //        return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
        //    }
        //}
        [HttpGet]
        public JsonResult getFarmListByAnyChange(int status, long? itemId, int? govId, int? centerId, int? villageId, string Date_From = "", string Date_End = "")
        {
            try
            {
                //bool? isStatus = null;

                //if (status == 2)
                //{
                //    isStatus = true;

                //}
                //else if (status == 3)
                //{
                //    isStatus = false;
                //}
                var res = APIHandeling.getData("FarmRequest_API?Is_Status=" + status + "&itemId=" + itemId + "&govId=" + govId + "&centerId=" + centerId + "&villageId=" + villageId
                    + "&Date_From=" + Date_From + "&Date_End=" + Date_End);

                var modelLst = res.Content.ReadAsAsync<List<FarmsListDTO>>().Result;

                return Json(modelLst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "FarmListOnLine");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        [HttpPost]
        public JsonResult getFarmListByReqId(List<Farm_Requst_ListDTO> F_List)
        {
            try
            {
                Session["Farm_Requst_List"] = new List<Farm_Requst_ListDTO>();
                Session["Farm_Requst_List"] = F_List;
                //var res = APIHandeling.getData("FarmRequest_API?itemId=" + itemId);
                //var modelLst = res.Content.ReadAsAsync<List<FarmsListDTO>>().Result;
                return null;
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "FarmListOnLine");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
    }
}