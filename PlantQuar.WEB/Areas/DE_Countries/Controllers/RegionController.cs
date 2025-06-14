using PlantQuar.DTO.DTO.DataEntry.Countries;
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

namespace PlantQuar.WEB.Areas.DE_Countries.Controllers
{
    public class RegionController : BaseController
    {
        // GET: DE_Countries/Region
        string apiName = "Region_API";

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AllRegionExcell()
        {
            var res = APIHandeling.getData(apiName + "?pageSize=-1&index=-1");

            var lst = res.Content.ReadAsAsync<List<RegionDTO>>().Result;//object

            //var StatusCode = lst.ElementAt(0).Value;
            //var obj = lst.ElementAt(1).Value;

            return Json(lst, JsonRequestBehavior.AllowGet);
        }
        //LOAD SEARCH
        [HttpPost]
        public JsonResult listRegions(string txt_AR_BTNSearch = "", string txt_EN_BTNSearch = "", int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = "")
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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "listCountries");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        ////Insert
        [HttpPost]
        public JsonResult CreateRegions(RegionDTO model)
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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "CreateTBLRows");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        //Update  AnlaysisLab
        [HttpPost]
        public JsonResult UpdateRegions(RegionDTO model)
        {
            try
            {
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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "CreateTBLRows");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        //Delete AnalysisLab 
        [HttpPost]
        public JsonResult DeleteRegions(int ID)
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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "DeleteTBLRows");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }
        //[HttpPost]
        //public JsonResult CreateRegions(RegionDTO model)
        //{
        //    //List<int> ListUnions_Id,RegionDTO model
        //    try
        //    {
        //        var allErrors = ModelState.Values.SelectMany(x => x.Errors).ToList();

        //        if (ModelState.IsValid)
        //        {
        //            User_Session Current = User_Session.GetInstance;

        //            model.User_Creation_Id =(short)Session["Language"];
        //            model.User_Creation_Date = DateTime.Now;


        //            //check Repeated Data
        //            var res = APIHandeling.Post(apiName, model);
        //            if ((int)res.StatusCode != 409)
        //            {
        //                if (model.ListUnions_Id != null)
        //                {
        //                    var region = res.Content.ReadAsAsync<RegionDTO>().Result;
        //                    long regionID = region.ID;
        //                    // model.ListUnions_Id.Remove(0);
        //                    model.ListUnions_Id.Remove(0);
        //                    if (model.ListUnions_Id.Count != 0)
        //                    {
        //                        APIHandeling.Post("Union_Country?user_id=" + (short)Session["UserId"] + "&Date_Now=" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "&RegionID=" + regionID, model.ListUnions_Id);
        //                    }
        //                    return Json(new { Result = "OK", Record = model });
        //                }
        //                return Json(new { Result = "OK", Record = model });
        //            }
        //            else
        //                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });
        //        }
        //        else
        //        {
        //            return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex.HResult == -2146233087)
        //        {
        //            return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RelatedData) });
        //        }
        //        else
        //        {
        //            APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "CreateCountries");
        //            return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
        //        }
        //    }
        //}

        ////UPDATE
        //[HttpPost]

        //public JsonResult UpdateRegions(RegionDTO model)
        //{
        //    try
        //    {
        //        //var allErrors = ModelState.Values.SelectMany(x => x.Errors).ToList();

        //        if (ModelState.IsValid)
        //        {

        //            User_Session Current = User_Session.GetInstance;

        //            model.User_Updation_Id=(short)Session["Language"];
        //            model.User_Updation_Date = DateTime.Now;

        //            //var mynewobj = APIHandeling.Put("Country?ListArrTest=1", model);
        //            //var lst = mynewobj.Content.ReadAsAsync<CustomCountry_UnionList>().Result;


        //            //return Json(new { Result = "OK", Record = model });
        //            //check Repeated Data
        //            model.ListUnions_Id.Remove(0);
        //            var res = APIHandeling.Put("Region?ListArrTest=1", model);
        //            if ((int)res.StatusCode != 409)
        //            {
        //                /*  if (model.ListUnions_Id.Count != 0)
        //                  {
        //                      APIHandeling.Put("Union_Country?user_id=" + (short)Session["UserId"] + "&Date_Now=" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "&CountryID=" + model.ID, model.ListUnions_Id);
        //                  }*/
        //                return Json(new { Result = "OK" });
        //            }
        //            return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });
        //        }
        //        else
        //        {
        //            return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex.HResult == -2146233087)
        //        {
        //            return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RelatedData) });
        //        }
        //        else
        //        {
        //            APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "UpdateCountries");
        //            return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
        //        }
        //    }
        //}

        ////DELETE
        //[HttpPost]
        //public JsonResult DeleteRegions(long ID)
        //{
        //    try
        //    {
        //        DeleteParameters obj = new DeleteParameters();
        //        obj.id = ID;
        //        User_Session Current = User_Session.GetInstance;

        //        obj.Userid = (short)Session["UserId"];
        //        obj._DateNow = DateTime.Now;
        //        APIHandeling.Delete(apiName, obj);

        //        return Json(new { Result = "OK" });
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex.HResult == -2146233087)
        //        {
        //            return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RelatedData) });
        //        }
        //        else
        //        {
        //            APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "DeleteCountries");
        //            return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
        //        }
        //    }
        //}

    }
}