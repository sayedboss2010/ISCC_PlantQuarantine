using PlantQuar.DTO.DTO.Import.Constrains;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using PlantQuar.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.Im_Constrains.Controllers
{
    public class Im_ConstrainsController : BaseController
    {
        // GET: Im_Constrains/Im_Constrains
        public ActionResult Index()
        {
            LoadData();
            return View();
        }
        public void LoadData()
        {
            //ViewBag.ConstrainTypeLst
            var res = APIHandeling.getData("Item_ShortName_API?Types=1");
            ViewBag.ItemTypesLst = res.Content.ReadAsAsync<List<CustomOption>>().Result;

            res = APIHandeling.getData("Item_ShortName_API?qualG=1");
            ViewBag.qualitiveGroup = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;

            //***********************************//
            Session["ImItemsConstrain_Rows"] = new List<ImCustomItemConstrain_Rows>();
            Session["ImItemConRows_Index"] = 1;
            Session["ImDB_ItemConstrainRows"] = new ImCustomCountryConstrain();

            Session["ImDB_ItemConstrainPorts"] = new ImCustomCountryConstrain();
            Session["ImItemsConstrain_Ports"] = new List<ImCustomItemConstrain_ArrivalPorts>();
            Session["ImItemConRows_IndexPorts"] = 1;
            //Qual Group sessions
            Session["ImQualGConstrain_Rows"] = new List<ImCustomItemConstrain_Rows>();
            Session["ImQualGConRows_Index"] = 1;
            Session["ImDB_QualGConstrainRows"] = new ImCustomCountryConstrain();

            Session["ImDB_QualGConstrainPorts"] = new ImCustomCountryConstrain();
            Session["ImQualGConstrain_Ports"] = new List<ImCustomItemConstrain_ArrivalPorts>();
            Session["ImQualGConRows_IndexPorts"] = 1;

            Session["ImDeletedConstrain"] = new List<DeleteParameters>();
        }
        //constrain type 
        public JsonResult constrainTypeList()
        {
            try
            {
                var res = APIHandeling.getData("Im_Constrains_API?conType=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "InitiatorList");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        public JsonResult constrainTextsList(byte conTypeId=0)
        {
            try
            {
                var res = APIHandeling.getData("Im_Constrains_API?conTypeId="+ conTypeId);
                var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "InitiatorList");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        [HttpPost]
        public JsonResult getConstrainTextDetail(long? textId)
        {
            try
            {
                var res = APIHandeling.getData("Im_Constrains_API?textId=" + textId);
                var text = res.Content.ReadAsAsync<string>().Result;
                return Json(text,JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "reqAnalysisType");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        public JsonResult InitiatorList(long itemShortNameId)
        {
            try
            {
                var res = APIHandeling.getData("Item_ShortName_API?itemShortNameId=" + itemShortNameId);
                var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "InitiatorList");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        public JsonResult InitiatorListQualG(short QualGId = 0)
        {
            try
            {
                var res = APIHandeling.getData("Item_ShortName_API?QualGId=" + QualGId);
                var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "InitiatorListQualG");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        public JsonResult MainClassification_List(byte itemTypeId = 0)
        {
            try
            {
                var res = APIHandeling.getData("Item_ShortName_API?mainClass=1&itemTypeId=" + itemTypeId);
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "MainClassification_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        public JsonResult SecClassification_List(int MainClass_ID = 0)
        {
            try
            {
                var res = APIHandeling.getData("Item_ShortName_API?MainClass_ID=" + MainClass_ID);
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "SecClassification_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        public JsonResult Group_List(int SecClass_ID = 0)
        {
            try
            {
                var res = APIHandeling.getData("Item_ShortName_API?SecClass_ID=" + SecClass_ID);
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Group_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        public JsonResult ItemsByGroupId_List(int groupId = 0,bool known = false)
        {
            try
            {
                var res = APIHandeling.getData("Item_ShortName_API?groupId=" + groupId+"&known="+ known);
                var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "ItemsByGroupId_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        public JsonResult ProductsByGroupId_List(int groupId = 0)
        {
            try
            {
                var res = APIHandeling.getData("Item_ShortName_API?prod=1&groupId=" + groupId);
                var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "ProductsByGroupId_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        public JsonResult GetShortNames_List(long itemId = 0, bool isProduct = false)
        {
            try
            {
                var res = APIHandeling.getData("Item_ShortName_API?itemId=" + itemId + "&isProduct=" + isProduct);
                var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetShortNames_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        public JsonResult Products_List(long? itemId)
        {
            try
            {
                var res = APIHandeling.getData("Item_ShortName_API?itemId=" + itemId);
                var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Products_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        //*************************//       
        public JsonResult getConstrainsItems(short? qualGId, string initiatorId,long itemShortNameId = 0, long itemId = 0, long catId = 0)
        {
            try
            {
                var ss = Regex.Replace(initiatorId, "[^a-zA-Z0-9.,]", "");
                //.Split(',').ToList();
                //List<long> longSS = ss.ConvertAll(long.Parse);
                var lst = new ImCustomCountryConstrain();

                if (qualGId >0)
                {

                    var res = APIHandeling.getData("Im_Constrains_API?qualGId=" + qualGId + "&InitiatorQualG=" + ss);
                    lst = res.Content.ReadAsAsync<ImCustomCountryConstrain>().Result;
                }
                else
                {
                    var res = APIHandeling.getData
                    ("Im_Constrains_API?itemShortNameId=" + itemShortNameId + "&itemId=" + itemId + "&catId=" + catId + "&initiatorIds=" + ss);

                    lst = res.Content.ReadAsAsync<ImCustomCountryConstrain>().Result;
                }


                

                Session["ImItemsConstrain_Rows"] = new List<ImCustomItemConstrain_Rows>();
                Session["ImItemConRows_Index"] = 1;

                Session["ImItemsConstrain_Ports"] = new List<ImCustomItemConstrain_ArrivalPorts>();
                Session["ImItemConRows_IndexPorts"] = 1;

                Session["ImDB_ItemConstrainRows"] = lst;
                Session["ImDB_ItemConstrainPorts"] = lst;

                return Json(lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "getConstrainsItems");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        public JsonResult getConstrainsQualG(short qualGId)
        {
            try
            {
                var res = APIHandeling.getData("Im_Constrains_API?qualGId=" + qualGId);

                var lst = res.Content.ReadAsAsync<ImCustomCountryConstrain>().Result;

                Session["ImItemsConstrain_Rows"] = new List<ImCustomItemConstrain_Rows>();
                Session["ImItemConRows_Index"] = 1;

                Session["ImItemsConstrain_Ports"] = new List<ImCustomItemConstrain_ArrivalPorts>();
                Session["ImItemConRows_IndexPorts"] = 1;

                Session["ImDB_ItemConstrainRows"] = lst;
                Session["ImDB_ItemConstrainPorts"] = lst;

                return Json(lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "getConstrainsQualG");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        //***********************//
        [HttpPost]
        public ActionResult constrainSave(ImCustomCountryConstrain countryConstrain)
        {
            var texts = Session["ImItemsConstrain_Rows"] as List<ImCustomItemConstrain_Rows>;
            if (texts.Count > 0)
            {
                Im_Items items = new Im_Items();

                items.ItemConstrain_Rows = Session["ImItemsConstrain_Rows"] as List<ImCustomItemConstrain_Rows>;
                items.ItemConstrain_ArrivalPorts = Session["ImItemsConstrain_Ports"] as List<ImCustomItemConstrain_ArrivalPorts>;

                countryConstrain.items = items;
            }

            User_Session Current = User_Session.GetInstance;
            countryConstrain.User_Creation_Id = (short)Session["UserId"];
            countryConstrain.User_Creation_Date = DateTime.Now;

            APIHandeling.Post("Im_Constrains_API", countryConstrain);

            return RedirectToAction("Index", "Im_Constrains", new { area = "Im_Constrains" });
        }
    }
}