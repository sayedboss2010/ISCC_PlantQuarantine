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
using PlantQuar.DTO.DTO.Company;

namespace PlantQuar.Web.Areas.Stations.Controllers
{
    public class StationActivityTypeController :BaseController
    {
        // GET: Stations/StationActivityType        
        string apiName = "StationActivityType";
        // GET: Lookups/StationActivityType
        public ActionResult Index()
        {
            return View();
        }

        //LOAD SEARCH
        public JsonResult listStationActivityType
           (string txt_AR_BTNSearch ="", string txt_EN_BTNSearch ="", int jtStartIndex = 0, int jtPageSize = 0, string jtSorting ="")
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
                    APIHandeling.Insert_Error( Request.Url.AbsoluteUri.ToString(), ex.Message, "listStationActivityType");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        //NEW
        [HttpPost]
        public JsonResult CreateStationActivityType(StationActivityTypeDTO model)
        {
            try
            {
                var allErrors = ModelState.Values.SelectMany(x => x.Errors).ToList();
                if (ModelState.IsValid)
                {
                    
                    model.User_Creation_Id = (short)Session["UserId"];
                    model.User_Creation_Date = DateTime.Now;

                  var data=  APIHandeling.Post(apiName, model);
                    if ((int)data.StatusCode != 409)
                    {
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
                    APIHandeling.Insert_Error( Request.Url.AbsoluteUri.ToString(), ex.Message, "CreateStationActivityType");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        //UPDATE
        [HttpPost]
        public JsonResult UpdateStationActivityType(StationActivityTypeDTO model)
        {
            try
            {
                //var allErrors = ModelState.Values.SelectMany(x => x.Errors).ToList();
                if (ModelState.IsValid)
                {
                    
                    model.User_Updation_Id = (short)Session["UserId"];
                    model.User_Updation_Date = DateTime.Now;

                    APIHandeling.Put(apiName, model);
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
                    APIHandeling.Insert_Error( Request.Url.AbsoluteUri.ToString(), ex.Message, "UpdateStationActivityType");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        //DELETE
        [HttpPost]
        public JsonResult DeleteStationActivityType(long id)
        {
            try
            {
                DeleteParameters obj = new DeleteParameters();
                obj.id = id;
                
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
                    APIHandeling.Insert_Error( Request.Url.AbsoluteUri.ToString(), ex.Message, "DeleteStationActivityType");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }
        [HttpPost]
        public JsonResult StationActivityType_List()
        {
            try
            {
                var res = APIHandeling.getData(apiName + "?List=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
                 }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error( Request.Url.AbsoluteUri.ToString(), ex.Message, "StationActivityType_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        [HttpPost]
        public JsonResult StationActivityType_AddEDIT()
        {
            try
            {
                var res = APIHandeling.getData(apiName + "?AddEdit=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
                }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error( Request.Url.AbsoluteUri.ToString(), ex.Message, "StationActivityType_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
    }
}