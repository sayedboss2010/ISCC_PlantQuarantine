using PlantQuar.DTO.DTO.Company;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using PlantQuar.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace PlantQuar.WEB.Areas.CO_Company.Controllers
{
    public class StationAccreditationController : BaseController
    {
        // GET: CO_Company/StationAccreditation
        string apiName = "StationAccreditation_API";
        [HttpPost]
        public JsonResult listStationAccreditation(int Station_ID, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = "")
        {
            try
            {
                var res = APIHandeling.getData(apiName + "?StationID=" + Station_ID + "&pageSize=" + jtPageSize + "&index=" + jtStartIndex);

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

                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "listStationAccreditation");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        //Insert
        [HttpPost]
        public JsonResult CreateStationAccreditation(Station_AccreditationDTO model)
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

                    var data = APIHandeling.Post(apiName, model);
                    if ((int)data.StatusCode != 409)
                    {
                        var dto = data.Content.ReadAsAsync<Station_AccreditationDTO>().Result;
                        long StationAccreditationID = dto.ID;
                        //Treatment api
                        var Treatment_Id = model.Treatment_Id;
                        if (model.TreatmentCheck == 1)
                        {
                            APIHandeling.Post("StationAccreditationTreatment_API?user_id=" + user_id + "&Date_Now=" + Date_Now.ToString("yyyy-MM-dd hh:mm:ss") +
                                "&StationAccreditationID=" + StationAccreditationID + "&Treatment_Id=" + Treatment_Id, "");
                        }
                        //country api
                        model.CountryID.Remove(0);
                        var CountryDto = model.CountryID;

                        if (CountryDto.Count != 0)
                        {
                            APIHandeling.Post("StationAccreditationCountry_API?user_id=" + user_id + "&Date_Now=" + Date_Now.ToString("yyyy-MM-dd hh:mm:ss") + "&StationAccreditationID=" + StationAccreditationID, CountryDto);
                        }
                        return Json(new { Result = "OK", Record = dto });
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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "CreateStationAccreditation");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        //UPDATE///
        [HttpPost]
        public JsonResult UpdateStationAccreditation(Station_AccreditationDTO model)
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
                    var data = APIHandeling.Put(apiName, model);
                    if ((int)data.StatusCode != 409)
                    {
                        //update Treatment api
                        byte? Treatment_Id = model.TreatmentCheck == 0 ? null : model.Treatment_Id;
                        APIHandeling.Put("StationAccreditationTreatment_API?user_id=" + user_id + "&Date_Now=" + Date_Now.ToString("yyyy-MM-dd hh:mm:ss") + "&StationAccreditationID=" + model.ID + "&Treatment_Id=" + Treatment_Id, "");

                        //country api
                        model.CountryID.Remove(0);
                        var CountryDto = model.CountryID;
                        if (CountryDto.Count != 0)
                        {
                            APIHandeling.Put("StationAccreditationCountry_API?user_id=" + user_id + "&Date_Now=" + Date_Now.ToString("yyyy-MM-dd hh:mm:ss") + "&StationAccreditationID=" + model.ID, CountryDto);
                        }
                        return Json(new { Result = "OK" });
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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "UpdateStationAccreditation");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        //DELETE
        [HttpPost]
        public JsonResult DeleteStationAccreditation(long ID)
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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "DeleteStationAccreditation");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }
        public JsonResult IsTreatment(int ActID = 0)
        {
            try
            {
                // Dictionary<string, string> dicData = new Dictionary<string, string>();
                var res = APIHandeling.getData("StationActivityType_API?ActID=" + ActID);
                var check = res.Content.ReadAsAsync<bool>().Result;
                return Json(check, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
        public JsonResult GetProdPlantOptions(int IsPlant = 0)
        {
            //check if product or plant 
            try
            {
                var res = APIHandeling.getData(apiName + "?ProdPlant=" + IsPlant);
                var data = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                return Json(new { Result = "OK", Options = data });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
        public JsonResult GetProdPlantOptions2(int IsPlant = 0)
        {
            //check if product or plant 
            try
            {
                var res = APIHandeling.getData(apiName + "?ProdPlant=" + IsPlant);
                var data = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                return Json(data, JsonRequestBehavior.AllowGet);
                // return Json(new { Result = "OK", Options = data });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
    }
}