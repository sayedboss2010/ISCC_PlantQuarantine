
using PlantQuar.DTO.DTO.StationNew;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using PlantQuar.WEB.Controllers;
using System;
using System.Collections.Generic;

using System.Net.Http;
using System.Web.Mvc;


namespace PlantQuar.WEB.Areas.ST_Station.Controllers
{
    public class Station_CheckList_ConstrainController : BaseController
    {
        // GET: ST_Station/Station_CheckList_Constrain
        string apiName = "Station_CheckList_Constrain_API";

        public ActionResult Index(string message)
        {
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
                var lst = res.Content.ReadAsAsync<List<Station_CheckList_Constrain_DTO>>().Result;

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

        public ActionResult Station_CheckList_ConstrainAddEdit(long id = 0, string message = "", int StationConstrainTypeId = 0, int StationConstrainCountryItemID = 0)
        {
            // Fill_Lists();
            ViewBag.message = message;
            if (id > 0)
            {

                var res = APIHandeling.getData("Station_Constrain_Type_API?List=1");
                ViewBag.Station_Constrain_Type_List = res.Content.ReadAsAsync<List<CustomOption>>().Result;

                var model = getStation_CheckList_ConstrainByID(id);
                if (model != null)
                {


                    Session["id"] = model.ID;
                    return View(model);
                }
                else
                {
                    var defaultmodel = new Station_CheckList_Constrain_DTO();
                    if (StationConstrainTypeId != 0)
                    {
                        defaultmodel.Constrain_Type_ID = (byte)StationConstrainTypeId;
                    }
                    if (StationConstrainCountryItemID != 0)
                    {
                        defaultmodel.Station_Constrain_Country_Item_ID = (byte)StationConstrainCountryItemID;
                    }
                    return View(defaultmodel);
                }
            }
            else
            {
                var defaultmodel = new Station_CheckList_Constrain_DTO();
                if (StationConstrainTypeId != 0)
                {
                    defaultmodel.Constrain_Type_ID = (byte)StationConstrainTypeId;
                }
                if (StationConstrainCountryItemID != 0)
                {
                    defaultmodel.Station_Constrain_Country_Item_ID = (byte)StationConstrainCountryItemID;
                }
                return View(defaultmodel);

            }
        }
        //save
        [HttpPost]
        public ActionResult SaveStation_CheckList_Constrain(Station_CheckList_Constrain_DTO model)
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
                return RedirectToAction("Index", "Station_CheckList_Constrain", new { area = "ST_Station", message = msg });
                // return RedirectToAction("Station_CheckList_ConstrainAddEdit", "Station_CheckList_Constrain", new { area = "ST_Station", id = model.ID, message = msg });
            }
            else
            {
                //add
                model.User_Creation_Id = (short)Session["UserId"];
                model.User_Creation_Date = DateTime.Now;
                var res = APIHandeling.Post(apiName, model);

                var countryLst = res.Content.ReadAsAsync<Station_CheckList_Constrain_DTO>().Result;//object

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

        private Station_CheckList_Constrain_DTO getStation_CheckList_ConstrainByID(long id)
        {

            var res = APIHandeling.getData(apiName + "?details=1&Id=" + id);
            var list = res.Content.ReadAsAsync<Station_CheckList_Constrain_DTO>().Result;
            return list;
        }

        [HttpPost]
        public JsonResult Station_CheckList_Constrain_List(long Station_Constrain_Country_Item_ID, bool Android_ID)
        {
            try
            {
                var res = APIHandeling.getData(apiName + "?Station_Constrain_Country_Item_ID=" + Station_Constrain_Country_Item_ID + "&Android_ID=" + Android_ID);
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
                return Json(new
                {
                    Description_Ar = lst["Description_Ar"].ToString()
                    ,
                    Description_En = lst["Description_En"].ToString()
                    ,
                    Number_Check = lst["Number_Check"].ToString()

                }, JsonRequestBehavior.AllowGet);
                //return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetAnalysisTypes");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        //[HttpPost]
        //public JsonResult ItemType_AddEDIT()
        //public void Fill_Lists()
        //{
        //    try
        //    {
        //        var EX_Constrain_Type = APIHandeling.getData("EX_Constrain_Type_API?List=1");
        //        ViewBag.EX_Constrain_Type = EX_Constrain_Type.Content.ReadAsAsync<List<CustomOption>>().Result;

        //        //var EX_Constrain_Country_Item = APIHandeling.getData("EX_Constrain_Country_Item_API?List=1");
        //        //ViewBag.EX_Constrain_Type = EX_Constrain_Country_Item.Content.ReadAsAsync<List<CustomOption>>().Result;
        //        // var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;

        //    }
        //    catch (Exception ex)
        //    {
        //        APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Fill_Lists");
        //        //return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
        //    }
        //}

        //public void Fill_Lists2()
        //{
        //    try
        //    {
        //        var res = APIHandeling.getData(apiName + "?List=1");
        //        ViewBag.Im_Constrain_Type = res.Content.ReadAsAsync<List<CustomOption>>().Result;

        //    }
        //    catch (Exception ex)
        //    {
        //        APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Fill_Lists");
        //    }
        //}
    }
}