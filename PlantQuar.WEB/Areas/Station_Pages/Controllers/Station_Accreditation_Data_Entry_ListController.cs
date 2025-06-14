using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Station_Pages;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using PlantQuar.WEB.Controllers;
using System;
using System.Collections.Generic;

using System.Net.Http;
using System.Web.Mvc;


namespace PlantQuar.WEB.Areas.Station_Pages.Controllers
{
    public class Station_Accreditation_Data_Entry_ListController : BaseController
    {
        // GET: Station_Pages/Station_Accreditation_Data_Entry_List
        string apiName = "Station_Accreditation_Data_Entry_List_API";

        public ActionResult Index(string message)
        {
            @ViewBag.DateTo = DateTime.Now;
            @ViewBag.DateFrom = DateTime.Now.AddDays(-7);
            ViewBag.message = message;
            return View();
        }

        //LOAD SEARCH
        public JsonResult listStation_CheckList_Constrain
        (string txt_AR_BTNSearch = "", string txt_EN_BTNSearch = "", int jtStartIndex = 0
            , int jtPageSize = 0, string jtSorting = "", byte Fill_Lists_Type = 0)
        {
            try
            {

                var res = APIHandeling.getData(apiName + "?arName=" + txt_AR_BTNSearch
                    + "&enName=" + txt_EN_BTNSearch + "&pageSize=" + jtPageSize.ToString()
                    + "&index=" + jtStartIndex.ToString() + "&jtSorting=" + jtSorting.ToString() + "&Fill_Lists_Type=" + Fill_Lists_Type);
                var lst = res.Content.ReadAsAsync<List<Station_Accreditation_Data_Entry_List_DTO>>().Result;

                return Json(lst, JsonRequestBehavior.AllowGet);
                //    var lst = res.Content.ReadAsAsync<Dictionary<string, object>>().Result;//object
                //var StatusCode = lst.ElementAt(0).Value;
                //var obj = lst.ElementAt(1).Value;

                //JavaScriptSerializer ser = new JavaScriptSerializer();
                //var myObj = ser.Deserialize<Dictionary<string, object>>(obj.ToString());

                //var count = myObj.ElementAt(0).Value;
                //var Lobj = myObj.ElementAt(1).Value;
                //return Json(new { Result = "OK", Records = Lobj, TotalRecordCount = count }, JsonRequestBehavior.AllowGet);
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

        public ActionResult Station_CheckList_ConstrainAddEdit(long id = 0, string message = "")
        {
            // Fill_Lists();
            ViewBag.message = message;
            if (id > 0)
            {
                var model = getStation_CheckList_ConstrainByID(id);
                Session["id"] = model.ID;
                return View(model);
            }
            else
            {
                return View(new Station_Accreditation_Data_Entry_List_DTO());
            }
        }

        //save
        [HttpPost]
        public ActionResult SaveStation_CheckList_Constrain(Station_Accreditation_Data_Entry_List_DTO model)
        {
            User_Session Current = User_Session.GetInstance;
            var msg = "";
            if (model.ID > 0)
            {
                //edit

                model.User_Updation_Id = (short)Session["UserId"];
                model.User_Updation_Date = DateTime.Now;

                var mynewobj = APIHandeling.Put(apiName, model);

                if ((int)mynewobj.StatusCode != 409)
                {
                    msg = "تم التعديل ";
                }
                else
                {
                    msg = "هذا السجل موجود من قبل ";
                }

                return RedirectToAction("EX_ConstrainAddEdit", "Station_CheckList_Constrain", new { area = "ST_Station", id = model.ID, message = msg });
            }
            else
            {
                //add
                model.User_Creation_Id = (short)Session["UserId"];
                model.User_Creation_Date = DateTime.Now;
                var res = APIHandeling.Post(apiName, model);

                var countryLst = res.Content.ReadAsAsync<Station_Accreditation_Data_Entry_List_DTO>().Result;//object

                //model.ID = countryLst.ID;
                if ((int)res.StatusCode != 409)
                {
                    msg = "تمت الاضافه";
                }
                else
                {
                    msg = "هذا السجل موجود من قبل ";
                }


                return RedirectToAction("Index", "Station_CheckList_Constrain", new { area = "ST_Station", message = msg });
            }

        }

        public ActionResult DeleteStation_CheckList_Constrain(long id)
        {
            try
            {
                DeleteParameters obj = new DeleteParameters();
                obj.id = id;

                User_Session Current = User_Session.GetInstance;
                obj.Userid = (short)Session["UserId"];
                obj._DateNow = DateTime.Now;
                var msg = "";
                APIHandeling.Delete(apiName, obj);
                msg = "تم الحذف";
                return RedirectToAction("Index", "Station_CheckList_Constrain", new { area = "ST_Station", message = msg });
            }
            catch (Exception ex)
            {
                if (ex.HResult == -2146233087)
                {
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RelatedData) });
                }
                else
                {
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "DeleteStation_CheckList_Constrain");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        private Station_Accreditation_Data_Entry_List_DTO getStation_CheckList_ConstrainByID(long id)
        {

            var res = APIHandeling.getData(apiName + "?details=1&Id=" + id);
            var list = res.Content.ReadAsAsync<Station_Accreditation_Data_Entry_List_DTO>().Result;
            return list;
        }

        [HttpPost]
        public JsonResult Station_CheckList_Constrain_List(long Station_Constrain_Country_Item_ID)
        {
            try
            {
                var res = APIHandeling.getData(apiName + "?Station_Constrain_Country_Item_ID=" + Station_Constrain_Country_Item_ID);
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "StationActivityType_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        public JsonResult Get_Station_CheckList_Constrain_Detiles(int ID)
        {
            try
            {
                var res = APIHandeling.getData(apiName + "?details=0&&Id=" + ID);
                var lst = res.Content.ReadAsAsync<Dictionary<string, string>>().Result;
                //var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Description_Ar = lst["Description_Ar"].ToString(), Description_En = lst["Description_En"].ToString() }, JsonRequestBehavior.AllowGet);
                //return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetAnalysisTypes");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        [HttpPost]
        public JsonResult Get_Station_Accreditation_List(int Accreditation_Type_ID, int _StationActivityType_ID
            , int? Station_Accreditation_Data_ID, string DateFrom = "", string DateEnd = "")
        {
            try
            {
                var res = APIHandeling.getData(apiName + "?Accreditation_Type_ID=" + Accreditation_Type_ID 
                    + "&_StationActivityType_ID=" + _StationActivityType_ID 
                    + "&Station_Accreditation_Data_ID=" + Station_Accreditation_Data_ID
                    + "&DateFrom=" + DateFrom
                    + "&DateEnd=" + DateEnd);
                var lst = res.Content.ReadAsAsync<List<Station_Accreditation_Data_Entry_DTO>>().Result;
                return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "CenterList_ByGov");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
    }
}