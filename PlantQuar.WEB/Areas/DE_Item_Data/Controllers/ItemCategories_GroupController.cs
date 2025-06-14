using PlantQuar.DTO.DTO.DataEntry.Items.ItemData;
using PlantQuar.DTO.HelperClasses;
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

namespace PlantQuar.WEB.Areas.DE_Item_Data.Controllers
{
    public class ItemCategories_GroupController : BaseController
    {
        // GET: DE_Item_Data/ItemCategories_Group1
        string apiName = "ItemCategories_Group_API";

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AllPlantCategory()
        {
            var res = APIHandeling.getData(apiName + "?pageSize=-1&index=-1");

            var lst = res.Content.ReadAsAsync<List<ItemCategories_GroupDTO>>().Result;//object

            //var StatusCode = lst.ElementAt(0).Value;
            //var obj = lst.ElementAt(1).Value;

            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        //LOAD SEARCH
        [HttpPost]
        public JsonResult ItemCategories_GroupList
        (string txt_AR_BTNSearch = "", string txt_EN_BTNSearch = "", long itemId = 0, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = "")
        {
            try
            {
                Session["ItemId"] = itemId;
                
                var res = APIHandeling.getData(apiName + "?itemId=" + itemId + "&arName=" + txt_AR_BTNSearch + "&enName=" + txt_EN_BTNSearch
                    + "&pageSize=" + jtPageSize.ToString() + "&index=" + jtStartIndex.ToString() + "&jtSorting=" + jtSorting);


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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "ItemCategories_GroupList");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        //Insert
        [HttpPost]
        public JsonResult CreateItemCategories_Group(ItemCategories_GroupDTO model, HttpPostedFileBase Protect_Property)
        {
            try
            {
                var allErrors = ModelState.Values.SelectMany(x => x.Errors).ToList();

                if (ModelState.IsValid)
                {
                    User_Session Current = User_Session.GetInstance;

                    if (Protect_Property != null)
                    {
                        string fName = model.ID + Path.GetFileName(Guid.NewGuid() + Protect_Property.FileName);
                        var ext = Path.GetExtension(Protect_Property.FileName);

                        string varlname = "Upload";
                        string fPath = Server.MapPath("~/" + varlname);

                        if (!Directory.Exists(fPath))
                        {
                            Directory.CreateDirectory(fPath);
                        }
                        string fPathName = Path.Combine(fPath, fName);
                        Protect_Property.SaveAs(fPathName);
                       // model.Protect_Property = varlname + "/" + fName;
                    }
                    model.User_Creation_Id =(short)Session["UserId"];
                    model.User_Creation_Date = DateTime.Now;

                   // model.Item_ID = long.Parse(Session["ItemID"].ToString());
                    if (Session["ItemId"] == null) model.Item_ID = null;
                    else model.Item_ID = long.Parse(Session["ItemId"].ToString());
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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "CreateItemCategories_Group");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        //UPDATE
        [HttpPost]
        public JsonResult UpdateItemCategories_Group(ItemCategories_GroupDTO model, HttpPostedFileBase Protect_Property)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    User_Session Current = User_Session.GetInstance;
                    model.User_Updation_Id=(short)Session["UserId"];
                    model.User_Updation_Date = DateTime.Now;

                    if (Protect_Property != null)
                    {
                        string fName = model.ID + Path.GetFileName(Guid.NewGuid() + Protect_Property.FileName);
                        var ext = Path.GetExtension(Protect_Property.FileName);

                        string varlname = "Upload";
                        string fPath = Server.MapPath("~/" + varlname);

                        if (!Directory.Exists(fPath))
                        {
                            Directory.CreateDirectory(fPath);
                        }
                        string fPathName = Path.Combine(fPath, fName);
                        Protect_Property.SaveAs(fPathName);
                       // model.Protect_Property = varlname + "/" + fName;
                    }
                    //check Repeated Data

                    // model.Item_ID = long.Parse(Session["ItemID"].ToString());
                    //if (Session["ItemId"] == null) model.Item_ID = null;
                    //else model.Item_ID = long.Parse(Session["ItemId"].ToString());

                    var res = APIHandeling.Put(apiName, model);
                    return ((int)res.StatusCode != 409) ? Json(new { Result = "OK", Record = model })
                      : Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });
                }
                else
                {
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });
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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "UpdateItemCategories_Group");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        //DELETE
        [HttpPost]
        public JsonResult DeleteItemCategories_Group(long ID)
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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "DeleteItemCategories_Group");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        //AYM
        #region DROP DOWN LISTS

        [HttpPost]
        public JsonResult CompanyNational_List()
        {
            try
            {
                var res = APIHandeling.getData("Company_National?List=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "CompanyNational_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        [HttpPost]
        public JsonResult CompanyNational_AddEDIT()
        {
            try
            {
                var res = APIHandeling.getData("Company_National?AddEdit=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "CompanyNational_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        #endregion

        //#region old        

        //[HttpPost]
        //public JsonResult Item_Categories_GroupList
        //(long? itemId, byte? itemType, bool? known, string txt_AR_BTNSearch, string txt_EN_BTNSearch, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        //{
        //    try
        //    {
        //        Session["ItemId"] = itemId;
        //        Session["known"] = known;

        //        var res = (System.Net.Http.HttpResponseMessage)null;
        //        if (txt_AR_BTNSearch != null || txt_EN_BTNSearch != null)
        //            res = APIHandeling.getData(apiName + "?itemId=" + itemId + "&itemType=" + itemType + "&arName=" + txt_AR_BTNSearch + "&enName=" + txt_EN_BTNSearch + "&pageSize=" + jtPageSize.ToString() + "&index=" + jtStartIndex.ToString());
        //        else
        //            res = APIHandeling.getData(apiName + "?itemId=" + itemId + "&itemType=" + itemType + "&pageSize=" + jtPageSize.ToString() + "&index=" + jtStartIndex.ToString());

        //        var lst = res.Content.ReadAsAsync<Dictionary<string, object>>().Result;//object

        //        var StatusCode = lst.ElementAt(0).Value;
        //        var obj = lst.ElementAt(1).Value;

        //        JavaScriptSerializer ser = new JavaScriptSerializer();
        //        var myObj = ser.Deserialize<Dictionary<string, object>>(obj.ToString());

        //        var count = myObj.ElementAt(0).Value;
        //        var Lobj = myObj.ElementAt(1).Value;

        //        return Json(new { Result = "OK", Records = Lobj, TotalRecordCount = count });
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex.HResult == -2146233087)
        //        {
        //            return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RelatedData) });
        //        }
        //        else
        //        {
        //            APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Item_Categories_GroupList");
        //            return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
        //        }
        //    }
        //}

        ////[HttpPost]
        ////public JsonResult CreateItem_Categories_Group(ItemCategories_GroupDTO model)
        ////{
        ////    try
        ////    {
        ////        if (Session["ItemId"] == null) model.Item_ID = null;
        ////        else model.Item_ID = long.Parse(Session["ItemId"].ToString());

        ////        model.IsKnown = bool.Parse(Session["known"].ToString());

        ////        var allErrors = ModelState.Values.SelectMany(x => x.Errors).ToList();

        ////        if (ModelState.IsValid)
        ////        {
        ////            User_Session Current = User_Session.GetInstance;

        ////            model.User_Creation_Id =(short)Session["Language"];
        ////            model.User_Creation_Date = DateTime.Now;


        ////            //check Repeated Data
        ////            var res = APIHandeling.Post(apiName, model);
        ////            return ((int)res.StatusCode != 409) ? Json(new { Result = "OK", Record = model })
        ////              : Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });
        ////        }
        ////        else
        ////        {
        ////            return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        if (ex.HResult == -2146233087)
        ////        {
        ////            return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RelatedData) });
        ////        }
        ////        else
        ////        {
        ////            APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "CreateItem_ShortName");
        ////            return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
        ////        }
        ////    }
        ////}

        ////UPDATE
        //[HttpPost]
        //public JsonResult UpdateItem_Categories_Group(ItemCategories_GroupDTO model)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            User_Session Current = User_Session.GetInstance;

        //            model.User_Updation_Id=(short)Session["Language"];
        //            model.User_Updation_Date = DateTime.Now;


        //            //check Repeated Data
        //            var res = APIHandeling.Put(apiName, model);
        //            return ((int)res.StatusCode != 409) ? Json(new { Result = "OK", Record = model })
        //              : Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });
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
        //            APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "UpdateItem_Categories_Group");
        //            return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
        //        }
        //    }
        //}

        ////DELETE
        //[HttpPost]
        //public JsonResult DeleteItem_Categories_Group(long ID)
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
        //            APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "DeleteItem_Categories_Group");
        //            return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
        //        }
        //    }
        //}
#region 
        public JsonResult Item_AddEDIT(byte itemTypeId = 0)
        {
            try
            {
                var res = APIHandeling.getData(apiName + "?itemTypeId=" + itemTypeId);
                var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Item_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        public JsonResult Item_AddEDIT_IsKnown(byte itemTypeId = 0, bool IsKnown = true)
        {
            try
            {
                var res = APIHandeling.getData(apiName + "?itemTypeId=" + itemTypeId + "&IsKnown=" + IsKnown);
                var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Item_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        [HttpPost]
        public JsonResult ItemsTypes()
        {
            try
            {
                var res = APIHandeling.getData(apiName + "?Types=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "ItemPart_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        [HttpPost]
        public JsonResult ItemParts(long ItemId = 0, byte itemTypeId = 0)
        {
            try
            {
                var res = APIHandeling.getData(apiName + "?ItemId=" + ItemId + "&itemTypeId=" + itemTypeId);
                var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "ItemPart_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        [HttpPost]
        public JsonResult ItemStatus_List(byte itemTypeId = 0)
        {
            try
            {
                var res = APIHandeling.getData(apiName + "?stat=1&itemTypeId=" + itemTypeId);
                var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "ProductStatus_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        [HttpPost]
        public JsonResult ItemPurpose_List(byte itemTypeId = 0)
        {
            try
            {
                var res = APIHandeling.getData(apiName + "?purpose=1&itemTypeId=" + itemTypeId);
                var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "ProductStatus_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        [HttpPost]
        public JsonResult Products_List(long? itemId)
        {
            try
            {
                var res = APIHandeling.getData(apiName + "?itemId=" + itemId);
                var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "ProductStatus_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        [HttpPost]
        public JsonResult TotalItems_List()
        {
            try
            {
                var res = APIHandeling.getData(apiName + "?tot=1");
                var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "ProductStatus_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        [HttpPost]
        public JsonResult QualitiveGroups_List()
        {
            try
            {
                var res = APIHandeling.getData(apiName + "?qualG=1");
                var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "ProductStatus_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
#endregion
    }
}