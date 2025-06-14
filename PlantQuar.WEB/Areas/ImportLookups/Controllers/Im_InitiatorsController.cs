using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PlantQuar.Web.Controllers;
using System.Net.Http;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.DTO.DTO;
using PlantQuar.Web.App_Start;
using System.Web.Script.Serialization;
using System.Web;
using PlantQuar.Web.API;

namespace PlantQuar.Web.Areas.ImportLookups.Controllers
{
    public class Im_InitiatorsController : BaseController
    {
        string apiName = "Im_Initiators";
        string action = "PutDeleteIm_Initiator";
        FileUpload_SaveDataController fileUpload = new FileUpload_SaveDataController();
        // GET: ImportLookups/Im_Initiators
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult ListInitiator
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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "listAnalysisType");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }
        [HttpPost]
        public JsonResult CreateRow(Im_InitiatorDTO model, HttpPostedFileBase Picture)
        {
            try
            {
                var allErrors = ModelState.Values.SelectMany(x => x.Errors).ToList();
                if (ModelState.IsValid)
                {
                    User_Session Current = User_Session.GetInstance;
                    model.User_Creation_Id = Current.UserId;
                    model.User_Creation_Date = DateTime.Now;

                    if (Picture != null)
                    {
                        model.Picture = fileUpload.Upload_File_Data(Picture, "Im_Initiators");
                    }
                    var res = APIHandeling.Post(apiName, model);
                    return ((int)res.StatusCode != 409) ? Json(new { Result = "OK", Record = model }): Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });
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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "CreateAnalysisType");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }
        //UPDATE
        [HttpPost]
        public JsonResult UpdateRow(Im_InitiatorDTO model, HttpPostedFileBase Picture)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var FindByID = APIHandeling.getData("Im_Initiators", "GetBy_ID?ID=" + model.ID + "");
                    var obj = FindByID.Content.ReadAsAsync<Im_InitiatorDTO>().Result;
                    User_Session Current = User_Session.GetInstance;
                    model.User_Updation_Id = Current.UserId;
                    model.User_Updation_Date = DateTime.Now;
                    model.User_Creation_Id = obj.User_Creation_Id;
                    model.User_Creation_Date = obj.User_Creation_Date;
                    model.Picture = Picture.FileName;
                    if (Picture != null)
                    {
                        string fPath = Server.MapPath("~/" + obj.Picture);
                        fileUpload.IfExisit_DeleteFile(fPath);
                        model.Picture = fileUpload.Upload_File_Data(Picture, "Im_Initiators");
                    }
                    var res = APIHandeling.Put(apiName, model);
                    return ((int)res.StatusCode != 409) ? Json(new { Result = "OK" }): Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });
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
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }
        //DELETE
        [HttpPost]
        public JsonResult DeleteRow(Im_InitiatorDTO obj)
        {
            try
            {
                var FindByID = APIHandeling.getData("Im_Initiators", "GetBy_ID?ID=" + obj.ID + "");
                var objID = FindByID.Content.ReadAsAsync<Im_InitiatorDTO>().Result;
                User_Session Current = User_Session.GetInstance;
                objID.User_Deletion_Id = Current.UserId;
                objID.User_Deletion_Date = DateTime.Now;
                var res = APIHandeling.DeletePut(apiName, action, objID);
                if (objID.Picture != null)
                {
                    string fPath = Server.MapPath("~/" + objID.Picture);
                    fileUpload.IfExisit_DeleteFile(fPath);
                }
                return ((int)res.StatusCode != 409) ? Json(new { Result = "OK" }): Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });
            }
            catch (Exception ex)
            {
                if (ex.HResult == -2146233087)
                {
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RelatedData) });
                }
                else
                {
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }

        }
        [HttpPost]
        public JsonResult Initiator_Status()
        {
            try
            {
                var res = APIHandeling.getData("A_SystemCode?Syscode=" + 16);
                var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "PlantPart_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        public JsonResult GetPlant_list()
        {
            try
            {
                var res = APIHandeling.getData("PlantShortName", "GetPlantShortName_List?List=1");
                var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Im_Initiators");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

    }
}