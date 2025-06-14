using PlantQuar.DTO.DTO;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.Web.App_Start;
using PlantQuar.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace PlantQuar.Web.Areas.ImportLookups.Controllers
{
    public class Im_ManafestController : BaseController
    {
        string apiName = "Im_Manafest";

        // GET: Liable/Im_Manafest
        public ActionResult Index()
        {


            var outletres = APIHandeling.getData("Outlet?AddEdit=1");
            var outletLst = outletres.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
            ViewBag.outLST = new SelectList(outletLst, "Value", "DisplayText");

            var GAres = APIHandeling.getData("GeneralAdmin?AddEdit=1");
            var GAlst = GAres.Content.ReadAsAsync<List<CustomOption>>().Result;
            ViewBag.GADDLlst =new SelectList(GAlst, "Value", "DisplayText");

            return View();
        }

        //LOAD SEARCH
        [HttpPost]
        public JsonResult listIm_Manafest
        (string txt_AR_BTNSearch = null,  int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                

                var res = APIHandeling.getData(apiName + "?Manafest_Num=" + txt_AR_BTNSearch +  "&pageSize=" + jtPageSize.ToString() + "&index=" + jtStartIndex.ToString());

                var lst = res.Content.ReadAsAsync<Dictionary<string, object>>().Result;//object

                var StatusCode = lst.ElementAt(0).Value;
                var obj = lst.ElementAt(1).Value;

                JavaScriptSerializer ser = new JavaScriptSerializer();
                var myObj = ser.Deserialize<Dictionary<string, object>>(obj.ToString());

                var count = myObj.ElementAt(0).Value;
                var Lobj = myObj.ElementAt(1).Value;

                return Json(new { Result = "OK", Records = Lobj, TotalRecordCount = count });
            }
            catch (Exception ex)
            {
                if (ex.HResult == -2146233087)
                {
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RelatedData) });
                }
                else
                {
                    APIHandeling.Insert_Error( Request.Url.AbsoluteUri.ToString(), ex.Message, "listIm_Manafest");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        //Insert
        [HttpPost]
        public JsonResult CreateIm_Manafest(Im_ManafestDTO model)
        {
            try
            {
                var allErrors = ModelState.Values.SelectMany(x => x.Errors).ToList();

                if (ModelState.IsValid)
                {
                    User_Session Current = User_Session.GetInstance;
                    model.User_Creation_Id = Current.UserId;
                    model.User_Creation_Date = DateTime.Now;

                    //check Repeated Data
                    var res = APIHandeling.Post(apiName, model);
                    return ((int)res.StatusCode != 409) ? Json(new { Result = "OK", Record = model })
                      : Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });
                }
                else
                {
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
            catch (Exception ex)
            {
                if (ex.HResult == -2146233087)
                {
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RelatedData) });
                }
                else
                {
                    APIHandeling.Insert_Error( Request.Url.AbsoluteUri.ToString(), ex.Message, "CreateIm_Manafest");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        [HttpPost]
        public JsonResult UpdateIm_Manafest(Im_ManafestDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    User_Session Current = User_Session.GetInstance;
                    model.User_Updation_Id = Current.UserId;
                    model.User_Updation_Date = DateTime.Now;

                    //check Repeated Data
                    var res = APIHandeling.Put(apiName, model);
                    return ((int)res.StatusCode != 409) ? Json(new { Result = "OK" })
                      : Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });
                }
                else
                {
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
            catch (Exception ex)
            {
                if (ex.HResult == -2146233087)
                {
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RelatedData) });
                }
                else
                {
                    APIHandeling.Insert_Error( Request.Url.AbsoluteUri.ToString(), ex.Message, "CreateIm_Manafest");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        [HttpPost]
        public JsonResult DeleteIm_Manafest(long ID)
        {
            try
            {
                DeleteParameters obj = new DeleteParameters();
                obj.id = ID;
                User_Session Current = User_Session.GetInstance;
                obj.Userid = Current.UserId;
                obj._DateNow = DateTime.Now;
                APIHandeling.Delete(apiName, obj);

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                if (ex.HResult == -2146233087)
                {
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RelatedData) });
                }
                else
                {
                    APIHandeling.Insert_Error( Request.Url.AbsoluteUri.ToString(), ex.Message, "DeletelistIm_Manafest");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

       

    }
}