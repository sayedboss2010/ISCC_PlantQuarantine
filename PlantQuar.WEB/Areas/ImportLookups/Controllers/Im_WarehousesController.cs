using PlantQuar.DTO.DTO.Import.DataEntry;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using PlantQuar.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace PlantQuar.Web.Areas.ImportLookups.Controllers
{
    public class Im_WarehousesController : BaseController
    {
        string apiName = "Im_Warehouses_API";

        // GET: ImportLookups/Im_Warehouses
        public ActionResult Index()
        {
            return View();
        }

        //LOAD SEARCH
        [HttpPost]
        public JsonResult listIm_Warehouses
        (string txt_AR_BTNSearch = null, string txt_EN_BTNSearch = null, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                var res = APIHandeling.getData(apiName + "?arName=" + txt_AR_BTNSearch + "&enName=" + txt_EN_BTNSearch + "&pageSize=" + jtPageSize.ToString() + "&index=" + jtStartIndex.ToString());

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
                    APIHandeling.Insert_Error( Request.Url.AbsoluteUri.ToString(), ex.Message, "listIm_Warehouses");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        //Insert
        [HttpPost]
        public JsonResult CreateIm_Warehouses(Im_WarehousesDTO model)
        {
            try
            {
                var allErrors = ModelState.Values.SelectMany(x => x.Errors).ToList();

                if (ModelState.IsValid)
                {

                 
                    model.User_Creation_Id = (short)Session["UserId"];
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
                    APIHandeling.Insert_Error( Request.Url.AbsoluteUri.ToString(), ex.Message, "CreateIm_Warehouses");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        [HttpPost]
        public JsonResult UpdateIm_Warehouses(Im_WarehousesDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    User_Session Current = User_Session.GetInstance;

                    model.User_Updation_Id = (short)Session["UserId"];
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
                    APIHandeling.Insert_Error( Request.Url.AbsoluteUri.ToString(), ex.Message, "CreateIm_Warehouses");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        [HttpPost]
        public JsonResult DeleteIm_Warehouses(int ID)
        {
            try
            {
                DeleteParameters obj = new DeleteParameters();
                obj.id = ID;

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
                    APIHandeling.Insert_Error( Request.Url.AbsoluteUri.ToString(), ex.Message, "DeletelistIm_Warehouses");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        #region syscode

        [HttpPost]
        public JsonResult syscode_List()
        {
            try
            {
                var res = APIHandeling.getData(apiName+"?List=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "syscode_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        [HttpPost]
        public JsonResult syscode_AddEDIT()
        {
            try
            {
                var res = APIHandeling.getData(apiName+"?AddEdit=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error( Request.Url.AbsoluteUri.ToString(), ex.Message, "syscode_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        #endregion


    }
}