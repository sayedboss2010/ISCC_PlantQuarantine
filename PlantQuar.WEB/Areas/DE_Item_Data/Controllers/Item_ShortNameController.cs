using PlantQuar.DTO.DTO.DataEntry.Items.ItemData;
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

namespace PlantQuar.WEB.Areas.DE_Item_Data.Controllers
{
    public class Item_ShortNameController : BaseController
    {
        // GET: DE_Item_Data/Item_ShortName
        string apiName = "Item_ShortName_API";

        public ActionResult Index()
        {
            return View();
        }

        //LOAD SEARCH
        [HttpPost]
        public JsonResult Item_ShortNameList
        (string txt_AR_BTNSearch, string txt_EN_BTNSearch, long? itemId, byte? itemType,bool? known, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                Session["ItemId"] = itemId;
                Session["itemType"] = itemType;
                Session["known"] = known;
                var lst = new Dictionary<string, object>();

                if (!string.IsNullOrEmpty(txt_AR_BTNSearch) || !string.IsNullOrEmpty(txt_EN_BTNSearch))
                {
                    var res = APIHandeling.getData(apiName + "?arName=" + txt_AR_BTNSearch + "&enName=" + txt_EN_BTNSearch + "&pageSize=" + jtPageSize.ToString() + "&index=" + jtStartIndex.ToString());

                   lst = res.Content.ReadAsAsync<Dictionary<string, object>>().Result;//object

                }
                else
                {
                    var res = APIHandeling.getData(apiName + "?itemId=" + itemId + "&itemType=" + itemType + "&pageSize=" + jtPageSize.ToString() + "&index=" + jtStartIndex.ToString());

                    lst = res.Content.ReadAsAsync<Dictionary<string, object>>().Result;//object

                }
                

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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Item_ShortNameList");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        //public JsonResult Item_ShortNameList
        //(string txt_AR_BTNSearch = null, string txt_EN_BTNSearch = null, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        //{
        //    try
        //    {
        //        var res = APIHandeling.getData(apiName + "?arName=" + txt_AR_BTNSearch + "&enName=" + txt_EN_BTNSearch + "&pageSize=" + jtPageSize.ToString() + "&index=" + jtStartIndex.ToString());

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
        //            APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Item_ShortNameList");
        //            return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
        //        }
        //    }
        //}

        //Insert
        //, int?[] PlantShortNameListPartType_Id
        [HttpPost]
        public JsonResult CreateItem_ShortName(Item_ShortNameDTO model)
        {
            try
            {
                //Session["ItemId"] = itemId;
                //Session["itemType"] = itemType;
                //Session["known"] = known;
                if (Session["ItemId"] == null) model.Item_ID = null;
                else model.Item_ID = long.Parse(Session["ItemId"].ToString());
                if (Session["itemType"] == null) model.Item_Type_ID= null;
                else model.Item_Type_ID = byte.Parse(Session["itemType"].ToString());

                model.IsKnown = bool.Parse(Session["known"].ToString());

                //model.Item_ID = itemId;
                //model.Item_Type_ID = itemType;
                //model.IsKnown = known;
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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "CreateItem_ShortName");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        //UPDATE
        [HttpPost]
        public JsonResult UpdateItem_ShortName(Item_ShortNameDTO model)
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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "UpdateItem_ShortName");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        //DELETE
        [HttpPost]
        public JsonResult DeleteItem_ShortName(long ID)
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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "DeleteItem_ShortName");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }
        //eman

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
        public JsonResult Item_AddEDIT_IsKnown(byte itemTypeId = 0,bool IsKnown=true)
        {
            try
            {
                var res = APIHandeling.getData(apiName + "?itemTypeId=" + itemTypeId+"&IsKnown="+IsKnown);
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
        public JsonResult ItemCategoryGroup_List(long? ItemId)
        {
            try
            {
                var res = APIHandeling.getData("ItemCategories_Group_API?List=1&ItemId="+ItemId);
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
        public JsonResult shortNames_List(long? itemId)
        {
            try
            {
                var res = APIHandeling.getData(apiName + "?shortNByIt=1&itemId=" + itemId );
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


        [HttpPost]
        public ActionResult AllPlantShortName()
        {
            string c = "ddd";
            
            var res = APIHandeling.getData("Item_ShortName_API" + "?c="+c );

           var lst = res.Content.ReadAsAsync<List<Item_ShortNameDTO>>().Result;//object

            //var StatusCode = lst.ElementAt(0).Value;
            //var obj = lst.ElementAt(1).Value;

            return Json(lst, JsonRequestBehavior.AllowGet);
        }

    }
}