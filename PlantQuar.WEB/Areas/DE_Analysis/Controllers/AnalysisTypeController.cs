using PlantQuar.DTO.DTO.DataEntry.Analysis;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using PlantQuar.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace PlantQuar.WEB.Areas.DE_Analysis.Controllers
{
    public class AnalysisTypeController : BaseController
    {
        // GET: DE_Analysis/AnalysisType
        string apiName = "AnalysisType_API";
        public ActionResult Index()
        {
            return View();
        }

        //LOAD SEARCH
        [HttpPost]
        public JsonResult listAnalysisType
        (string txt_AR_BTNSearch = "", string txt_EN_BTNSearch = "", int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = "")
        {
            try
            {
                string AR_BTNSearch = txt_AR_BTNSearch, EN_BTNSearch = txt_EN_BTNSearch;
                if (!String.IsNullOrEmpty(txt_AR_BTNSearch))
                    AR_BTNSearch = txt_AR_BTNSearch.Trim();

                if (!String.IsNullOrEmpty(txt_EN_BTNSearch))
                    EN_BTNSearch = txt_EN_BTNSearch.Trim();

                var res = APIHandeling.getData(apiName + "?arName=" + AR_BTNSearch + "&enName=" + EN_BTNSearch + "&pageSize=" + jtPageSize.ToString() + "&index=" + jtStartIndex.ToString() + "&jtSorting=" + jtSorting.ToString());


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
        //[HttpPost]
        //public JsonResult listAnalysisType
        //(string txt_AR_BTNSearch ="", string txt_EN_BTNSearch ="", int jtStartIndex = 0, int jtPageSize = 0, string jtSorting ="")
        //{
        //    try
        //    {
        //        var res = APIHandeling.getData(apiName + "?arName=" + txt_AR_BTNSearch + "&enName=" + txt_EN_BTNSearch + "&pageSize=" + jtPageSize.ToString() + "&index=" + jtStartIndex.ToString());

        //        var lst = res.Content.ReadAsAsync<Data_Count>().Result;//object

        //        //var StatusCode = lst.ElementAt(1).Value;
        //        //var obj = lst.ElementAt(1).Value;

        //        //JavaScriptSerializer ser = new JavaScriptSerializer();
        //        //var myObj = ser.Deserialize<Data_Count>(lst.ToString());

        //        //var count = myObj.ElementAt(0).Value;
        //        //var Lobj = myObj.ElementAt(1).Value;
        //        var x = lst.L_dataDTO;
        //        var y = lst.count;
        //        return Json(new { Result = "OK", Records = lst.L_dataDTO.ToList(), TotalRecordCount = lst.count });
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex.HResult == -2146233087)
        //        {
        //            return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RelatedData) });
        //        }
        //        else
        //        {

        //            APIHandeling.Insert_Error( Request.Url.AbsoluteUri.ToString(), ex.Message, "listAnalysisType");
        //            return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
        //        }
        //    }
        //}
        [HttpPost]
        public JsonResult CreateAnalysisType(AnalysisTypeDTO model)
        {
            try
            {
                var allErrors = ModelState.Values.SelectMany(x => x.Errors).ToList();
                if (ModelState.IsValid)
                {
                    User_Session Current = User_Session.GetInstance;
                    model.User_Creation_Id =(short)Session["UserId"];
                    model.User_Creation_Date = DateTime.Now;
                    model.ListAnalysisLab_Id.Remove(0); // remove default item

                    //check Repeated Data
                    var res = APIHandeling.Post(apiName, model);
                    return ((int)res.StatusCode != 409) ? Json(new { Result = "OK", Record = model })
                      : Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });

                    //var mynewobj = APIHandeling.Post(apiName, model);
                    //var lst = mynewobj.Content.ReadAsAsync<AnalysisTypeDTO>().Result;
                    //var val = lst.ID;

                    ////if (ListAnalysisLab_Id !="")
                    ////{
                    ////    foreach (var item in ListAnalysisLab_Id)
                    ////    {
                    ////        AnalysisLabTypeDTO newobject2 = new AnalysisLabTypeDTO();
                    ////        newobject2.AnalysisLabID = Convert.ToByte(item);
                    ////        newobject2.AnalysisTypeID = val;

                    ////        var AnalysisLabTypeRow = APIHandeling.Post("AnalysisLabType", newobject2);
                    ////    }
                    ////}

                    //return Json(new { Result = "OK", Record = lst });
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

        //  UPDATE
        [HttpPost]
        public JsonResult UpdateAnalysisType(AnalysisTypeDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    User_Session Current = User_Session.GetInstance;

                    model.User_Updation_Id=(short)Session["UserId"];
                    model.User_Updation_Date = DateTime.Now;
                    model.ListAnalysisLab_Id.Remove(0); // remove default item
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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "UpdateAnalysisType");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        [HttpPost]
        public JsonResult DeleteAnalysisType(int ID)
        {
            try
            {

                DeleteParameters obj = new DeleteParameters();
                obj.id = ID;

                User_Session Current = User_Session.GetInstance;

                obj.Userid = (short)Session["UserId"];
                obj._DateNow = DateTime.Now;//.ToString("yyyy-MM-dd hh:mm:ss");
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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "DeleteAnalysisType");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }
        //sayed
        [HttpPost]
        public JsonResult AnalysisLab_AddEDIT()
        {
            try
            {
                var res = APIHandeling.getData("AnalysisLab_API?AddEdit=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "AnalysisLab_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        //Eslam Add Analysis type list
        [HttpPost]
        public JsonResult AnalysisType_AddEDIT()
        {
            try
            {
                var res = APIHandeling.getData(apiName + "?AddEdit=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "AnalysisLab_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        [HttpPost]
        public JsonResult AnalysisLab_List(long AnalysisTypeId)
        {
            try
            {
                var res = APIHandeling.getData("AnalysisLab_API?AnalysisTypeId="+ AnalysisTypeId);
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "AnalysisLab_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        [HttpPost]
        public JsonResult AnalysisType_ByConstrain_EX(long EX_requestId)
        {
            try
            {
                var res = APIHandeling.getData(apiName + "?EX_requestId="+ EX_requestId);
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "AnalysisLab_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        [HttpPost]
        public JsonResult AnalysisType_AddEDIT_Ex(long ExportCountry_Id, long shortnameId)
        {
            try
            {
                var res = APIHandeling.getData(apiName + "?AddEdit_Ex=1&ExportCountry_Id="+ ExportCountry_Id+ "&shortnameId="+ shortnameId);
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "AnalysisLab_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
    }
}