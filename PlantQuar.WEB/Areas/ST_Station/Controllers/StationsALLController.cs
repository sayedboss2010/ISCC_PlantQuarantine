using PlantQuar.DTO.DTO.Station;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.API;
using PlantQuar.WEB.App_Start;
using PlantQuar.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace PlantQuar.WEB.Areas.ST_Station.Controllers
{
    public class StationsALLController : BaseController
    {
        // GET: ST_Station/StationsALL
        string apiName = "Station_API";
        string apiName_StationPlantProduct = "StationPlantProduct_API";

        public ActionResult Index()
        {
            var model = new StationDTO();
            var res = APIHandeling.getData("ContactType_API?List=1");
            var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
            ViewBag.ContactTypelst = lst.OrderBy(a => a.DisplayText);

            return View(model);
        }

        //LOAD SEARCH
        [HttpPost]
        public JsonResult listStation
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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "listTBLRows");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        //Insert
        [HttpPost]
        public JsonResult CreateStation(StationDTO model, HttpPostedFileBase Picture1)
        {
            try
            {
                var allErrors = ModelState.Values.SelectMany(x => x.Errors).ToList();

                if (ModelState.IsValid)
                {
                    User_Session Current = User_Session.GetInstance;

                    short user_id = (short)Session["UserId"];
                    DateTime Date_Now = DateTime.Now;

                    //if (file != "")
                    //{
                    //    string fName = "Stationfile" + DateTime.Now.Year + DateTime.Now.DayOfYear + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second;
                    //    var ext = Path.GetExtension(file.FileName);

                    //    string fPath = Server.MapPath("~/Upload/Files/");

                    //    if (!Directory.Exists(fPath))
                    //    {
                    //        Directory.CreateDirectory(fPath);
                    //    }
                    //    string fPathName = Path.Combine(fPath, fName + ext);
                    //    file.SaveAs(fPathName);
                    //    model.FileUpload = fName + ext;
                    //}


                    model.User_Creation_Id = user_id;
                    model.User_Creation_Date = Date_Now;
                    if (Picture1 != null)
                    {
                        FileUpload_SaveDataController fileUpload = new FileUpload_SaveDataController();
                        model.FileUpload = fileUpload.Upload_File_Data(Picture1, "Station");
                    }
                    var data = APIHandeling.Post(apiName, model);

                    if ((int)data.StatusCode != 409)
                    {
                        var dto = data.Content.ReadAsAsync<StationDTO>().Result;

                        long StationID = dto.ID;
                        //add in Station contact table
                        var Contacts = model.Contacts.Where(a => a.DeleteCheck != 1 & a.ContactType_ID != 0);
                        if (Contacts != null)
                            Contacts.ToList().ForEach(u => u.StationID = StationID);

                        APIHandeling.Post("StationContact_API?user_id=" + user_id + "&Date_Now=" + Date_Now.ToString("yyyy-MM-dd hh:mm:ss") + "&StationID=" + StationID, Contacts);
                        return Json(new { Result = "OK", Record = model });
                    }
                    else
                    {
                        return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });
                    }

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

        [HttpPost]
        public JsonResult UpdateStation(StationDTO model, HttpPostedFileBase Picture1)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    User_Session Current = User_Session.GetInstance;
                    short user_id = (short)Session["UserId"];
                    DateTime Date_Now = DateTime.Now;

                    /* if (file !="")
                     {
                         string fName = "Stationfile" + DateTime.Now.Year + DateTime.Now.DayOfYear + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second;
                         var ext = Path.GetExtension(file.FileName);

                         //delete old one
                         string fPath_f = Server.MapPath("~/Upload/Files/" + model.FileUpload);

                         var exisit = System.IO.File.Exists(fPath_f);
                         if (exisit == true)
                         {
                             System.IO.File.Delete(fPath_f);
                         }

                         //create new one
                         string fPath = Server.MapPath("~/Upload/Files/");

                         if (!Directory.Exists(fPath))
                         {
                             Directory.CreateDirectory(fPath);
                         }
                         string fPathName = Path.Combine(fPath, fName + ext);
                         file.SaveAs(fPathName);
                         model.FileUpload = fName + ext;
                     }
                     */
                    model.User_Updation_Id = user_id;
                    model.User_Updation_Date = Date_Now;
                    if (Picture1 != null)
                    {
                        FileUpload_SaveDataController fileUpload = new FileUpload_SaveDataController();
                        model.FileUpload = fileUpload.Upload_File_Data(Picture1, "Station");
                    }
                    APIHandeling.Put(apiName, model);
                    long StationID = model.ID;
                    APIHandeling.Put("StationContact_API?user_id=" + user_id + "&Date_Now=" + Date_Now.ToString("yyyy-MM-dd hh:mm:ss") + "&StationID=" + StationID, model.Contacts);
                    // var mycontact = mytest.Content.ReadAsAsync<StationContactDTO>().Result;


                    return Json(new { Result = "OK" });
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

        [HttpPost]
        public JsonResult DeleteStation(long ID)
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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "DeletelistTBLRows");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }
        public JsonResult GetContactList(int StationId)
        {
            try
            {
                Dictionary<string, string> dicData = new Dictionary<string, string>();
                var res = APIHandeling.getData("StationContact_API?StationID=" + StationId);
                var data = res.Content.ReadAsAsync<List<StationContactDTO>>().Result;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        #region Plant_Product


        [HttpPost]
        public JsonResult listStationPlantProduct(int Station_ID, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = "")
        {
            try
            {
                var res = APIHandeling.getData(apiName_StationPlantProduct + "?StationID=" + Station_ID + "&pageSize=" + jtPageSize + "&index=" + jtStartIndex);

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

                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "listStationPlantProduct");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        //Insert
        [HttpPost]
        public JsonResult CreateStationPlantProduct(Station_Plant_ProductDTO model)
        {
            try
            {
                var allErrors = ModelState.Values.SelectMany(x => x.Errors).ToList();

                if (ModelState.IsValid)
                {
                    User_Session Current = User_Session.GetInstance;
                    short user_id = (short)Session["UserId"];
                    DateTime Date_Now = DateTime.Now;
                    model.User_Creation_Id = user_id;
                    model.User_Creation_Date = Date_Now;

                    //check Repeated Data
                    var res = APIHandeling.Post(apiName_StationPlantProduct, model);
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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "CreateStationPlantProduct");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        //UPDATE///
        [HttpPost]
        public JsonResult UpdateStationPlantProduct(Station_Plant_ProductDTO model)
        {
            try
            {
                var allErrors = ModelState.Values.SelectMany(x => x.Errors).ToList();
                if (ModelState.IsValid)
                {
                    User_Session Current = User_Session.GetInstance;
                    short user_id = (short)Session["UserId"];
                    DateTime Date_Now = DateTime.Now;
                    model.User_Updation_Id = user_id;
                    model.User_Updation_Date = Date_Now;
                    //check Repeated Data
                    var res = APIHandeling.Put(apiName_StationPlantProduct, model);
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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "UpdateStationPlantProduct");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }
        //DELETE
        [HttpPost]
        public JsonResult DeleteStationPlantProduct(long ID)
        {
            try
            {
                DeleteParameters obj = new DeleteParameters();
                obj.id = ID;
                User_Session Current = User_Session.GetInstance;
                obj.Userid = (short)Session["UserId"];
                obj._DateNow = DateTime.Now;
                APIHandeling.Delete(apiName_StationPlantProduct, obj);

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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "DeleteStationPlantProduct");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        #endregion

        [HttpPost]
        public JsonResult TreatmentMainType_AddEDIT()
        {
            try
            {
                var res = APIHandeling.getData(apiName + "?Add=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
                //var continentals = lst.Select(c => new CustomOption { DisplayText = c.Name_Ar, Value = c.ID }).OrderByDescending(c => c.DisplayText).ToList();
                //return Json(new { Result = "OK", Options = continentals.OrderBy(a => a.Value).ToList() });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Level_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        public JsonResult TreatmentTypeByTreatmentMain_Id(int TreatmentMain_Id = 0)
        {
            try
            {
                var res = APIHandeling.getData(apiName + "?TreatmentMain_Id=" + TreatmentMain_Id);
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "TreatmentTypeByTreatmentMain_Id");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
    }
}