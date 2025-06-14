using PlantQuar.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PlantQuar.DTO.DTO.DataEntry.GovToVillage;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using System.Net.Http;
using System.Web.Script.Serialization;

namespace PlantQuar.WEB.Areas.DE_GovToVillage.Controllers
{
    public class CenterController : BaseController
    {
        // GET: DE_GovToVillage/Center
        string apiName = "Centers_API";
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AllCenterExcell()
        {
            var res = APIHandeling.getData(apiName + "?pageSize=-1&index=-1");

            var lst = res.Content.ReadAsAsync<List<CenterDTO>>().Result;//object

            //var StatusCode = lst.ElementAt(0).Value;
            //var obj = lst.ElementAt(1).Value;

            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        //LOAD SEARCH
        public JsonResult listCenters
        (string txt_AR_BTNSearch = "", string txt_EN_BTNSearch = "", int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = "")
        {
            try
            {

                var res = APIHandeling.getData(apiName + "?arName=" + txt_AR_BTNSearch + "&enName=" + txt_EN_BTNSearch + "&pageSize=" + jtPageSize.ToString() + "&index=" + jtStartIndex.ToString() + "&jtSorting=" + jtSorting.ToString());

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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "listCenter");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        //NEW
        [HttpPost]
        public JsonResult CreateCenters(CenterDTO model)
        {
            try
            {
                var allErrors = ModelState.Values.SelectMany(x => x.Errors).ToList();

                if (ModelState.IsValid)
                {

                    User_Session Current = User_Session.GetInstance;
                    model.User_Creation_Id =(short)Session["UserId"];
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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "CreateCenter");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        //UPDATE
        [HttpPost]
        public JsonResult UpdateCenters(CenterDTO model)
        {
            try
            {
                //var allErrors = ModelState.Values.SelectMany(x => x.Errors).ToList();
                if (ModelState.IsValid)
                {
                    User_Session Current = User_Session.GetInstance;

                    model.User_Updation_Id=(short)Session["UserId"];
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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "UpdateCenter");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        //DELETE
        [HttpPost]
        public JsonResult DeleteCenters(Int16 id)
        {
            try
            {
                DeleteParameters obj = new DeleteParameters();
                obj.id = id;
                User_Session Current = User_Session.GetInstance;
                obj.Userid = (short)Session["UserId"];
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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "DeleteCenter");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }


    }
}