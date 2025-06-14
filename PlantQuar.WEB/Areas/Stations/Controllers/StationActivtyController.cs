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
using static PlantQuar.DTO.HelperClasses.Enums;
using System.Web.Script.Serialization;

namespace PlantQuar.Web.Areas.Stations.Controllers
{
    public class StationActivtyController :BaseController
    {
        string apiName = "StationActivty";
        // GET: Stations/StationActivty///
        [HttpPost]
        public JsonResult listStationActivty(long Station_ID, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting ="")
        {
            try
            {
                var res = APIHandeling.getData(apiName + "?StationID=" + Station_ID+ "&pageSize="+ jtPageSize+ "&index=" + jtStartIndex);
                
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

                    APIHandeling.Insert_Error( Request.Url.AbsoluteUri.ToString(), ex.Message, "listStationActivty");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        //Insert
        [HttpPost]
        public JsonResult CreateStationActivty(StationActivityDTO model)
        {
            try
            {
                var allErrors = ModelState.Values.SelectMany(x => x.Errors).ToList();

                if (ModelState.IsValid)
                {
                    

                    short user_id = (short)Session["UserId"];
                    DateTime Date_Now = DateTime.Now;
                    model.User_Creation_Id = user_id;
                    model.User_Creation_Date = Date_Now;

                    var data = APIHandeling.Post(apiName, model);
                   if ((int)data.StatusCode != 409)
                    {
                        var dto = data.Content.ReadAsAsync<StationActivityDTO>().Result;
                        long StationActivityID = dto.ID;
                        //company api
                        var CompanyDto = model.Company.Where(a => a.DeleteCheck != 1).ToList();
                        if (CompanyDto.Count != 0)
                        {
                            APIHandeling.Post("StationCompany?user_id=" + user_id + "&Date_Now=" + Date_Now.ToString("yyyy-MM-dd hh:mm:ss") + "&StationActivityID=" + StationActivityID, CompanyDto);
                        }
                            //country api
                        model.CountryID.Remove(0);
                        var CountryDto = model.CountryID;
                        if (CountryDto.Count != 0)
                        {
                            var country_data = APIHandeling.Post("StationActivityCountry?user_id=" + user_id + "&Date_Now=" + Date_Now.ToString("yyyy-MM-dd hh:mm:ss") + "&StationActivityID=" + StationActivityID, CountryDto);
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
                    APIHandeling.Insert_Error( Request.Url.AbsoluteUri.ToString(), ex.Message, "CreateStationActivty");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        //UPDATE
        [HttpPost]
        public JsonResult UpdateStationActivty(StationActivityDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    
                    short user_id = (short)Session["UserId"];
                    DateTime Date_Now = DateTime.Now;
                    model.User_Updation_Id = user_id;
                    model.User_Updation_Date = Date_Now;
                   var data= APIHandeling.Put(apiName, model);
                    if ((int)data.StatusCode != 409)
                    {
                        //update company station api
                        if (model.Company.Count != 0)
                        {
                            var comp_data = APIHandeling.Put("StationCompany?user_id=" + user_id + "&Date_Now=" + Date_Now.ToString("yyyy-MM-dd hh:mm:ss") + "&StationID=" + model.ID, model.Company);
                        }
                        //update country station api
                        model.CountryID.Remove(0);
                        if (model.CountryID.Count != 0)
                        {
                            APIHandeling.Put("StationActivityCountry?user_id=" + user_id + "&Date_Now=" + Date_Now.ToString("yyyy-MM-dd hh:mm:ss") + "&StaionActivityID=" + model.ID, model.CountryID);
                        }
                            return Json(new { Result = "OK" });
                    }
                    else
                    {
                        return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RelatedData) });
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
                    APIHandeling.Insert_Error( Request.Url.AbsoluteUri.ToString(), ex.Message, "UpdateStationActivty");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        //DELETE
        [HttpPost]
        public JsonResult DeleteStationActivty(long ID)
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
                    APIHandeling.Insert_Error( Request.Url.AbsoluteUri.ToString(), ex.Message, "DeleteStationActivty");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }
        public JsonResult GetCompanyListByStationActivityID(int StationActivityID=0)
        {
            try
            {
                Dictionary<string, string> dicData = new Dictionary<string, string>();
                var res = APIHandeling.getData("StationCompany?StationActivityID=" + StationActivityID);
                var data = res.Content.ReadAsAsync<List<StationCompanyDTO>>().Result;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
        public JsonResult GetStationActivityWithTypeName(int Station_ID=0)
        {
            try
            {
               // Dictionary<string, string> dicData = new Dictionary<string, string>();
                var res = APIHandeling.getData(apiName+ "?TypeName=1&Station_ID=" + Station_ID);
                var data = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                Session["activtyListt"] = data;
                return Json(new { Result = "OK", Options = data });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
        public JsonResult getActivityDates(int StationActivityID)
        {
            List<CustomOption> activities = Session["activtyListt"] as List<CustomOption>;
            List<CustomOption> yy = new List<CustomOption>();

            CustomOption cc = activities.FirstOrDefault(c => c.Value == StationActivityID);
            yy.Add(cc);

            return Json(yy, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetStationAllActivity(int Station_ID = 0)
        {
            try
            {
                // Dictionary<string, string> dicData = new Dictionary<string, string>();
                var res = APIHandeling.getData(apiName + "?TypeName=1&allActivity=1&Station_ID=" + Station_ID);
                var data = res.Content.ReadAsAsync<List<CustomOption>>().Result;

                return Json(new { Result = "OK", Options = data });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
        
    }
}