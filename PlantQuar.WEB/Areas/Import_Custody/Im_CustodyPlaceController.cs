using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using PlantQuar.DTO.HelperClasses;
using System.Net.Http;
using PlantQuar.WEB.Controllers;
using PlantQuar.WEB.App_Start;
using PlantQuar.DTO.DTO.Import_Custody;

namespace PlantQuar.WEB.Areas.Import_Custody.Controllers
{
    public class Im_CustodyPlaceController : BaseController
    {

        string apiName = "Im_CustodyPlace_API";

        // GET: Import_Custody/Im_CustodyPlace
        public ActionResult Index()
        {
            return View();
        }

        //LOAD SEARCH 
        [HttpPost]
        public JsonResult listIm_CustodyPlace
        (string permissionId = null, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                var res = APIHandeling.getData(apiName + "?permissionId=" + permissionId + "&pageSize=" + jtPageSize.ToString() + "&index=" + jtStartIndex.ToString());
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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "listTBLRows");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        //Insert into Im_CustodyPlace
        [HttpPost]
        public JsonResult CreateIm_CustodyPlace(Im_CustodyPlace_DTO model, string txt_AR_BTNSearch)
        {
            try
            {
                var allErrors = ModelState.Values.SelectMany(x => x.Errors).ToList();

                if (ModelState.IsValid)
                {

                    model.User_Creation_Id = (short)Session["UserId"];
                    model.User_Creation_Date = DateTime.Now;
                    model.Im_CheckRequest_ID = long.Parse(txt_AR_BTNSearch == "" ? "0" : txt_AR_BTNSearch);//long.Parse( Session["permissionNum"].ToString());
                    //check Repeated Data
                    var res = APIHandeling.Post(apiName, model);
                    return Json(new { Result = "OK", Record = model });
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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "CreateTBLRows");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        //Update  Im_CustodyPlace
        [HttpPost]
        public JsonResult UpdateIm_CustodyPlace(Im_CustodyPlace_DTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    model.User_Updation_Id = (short)Session["UserId"];
                    model.User_Updation_Date = DateTime.Now;
                    model.Im_CheckRequest_ID = 5;
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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "CreateTBLRows");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        //Delete Im_CustodyPlace 
        [HttpPost]
        public JsonResult DeleteIm_CustodyPlace(byte ID)
        {
            try
            {
                DeleteParameters obj = new DeleteParameters();
                obj.id = ID;

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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "DeleteTBLRows");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }
    }
}