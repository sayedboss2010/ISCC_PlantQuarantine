using PlantQuar.DTO.DTO.DataEntry.Items.Item_Descriptions;
using PlantQuar.DTO.DTO.DataEntry.Items.ItemData;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.API;
using PlantQuar.WEB.App_Start;
using PlantQuar.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace PlantQuar.WEB.Areas.DE_Item_Data.Controllers
{
    public class ItemController : BaseController
    {
        // GET: DE_Item_Data/Item
        string apiName = "Item_API";
       
        public ActionResult Index()
        {
             return View();
        }
        public ActionResult ItemFilter()
        {
            return View();
        }
        //LOAD SEARCH
        [HttpPost]
        public JsonResult listItem
        (string txt_AR_BTNSearch , string txt_EN_BTNSearch ,byte ? itemType, int? familyId, int? groupId, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                var lst = new Dictionary<string, object>();

                if (!string.IsNullOrEmpty(txt_AR_BTNSearch) || !string.IsNullOrEmpty(txt_EN_BTNSearch))
                {
                    var res = APIHandeling.getData(apiName + "?arName=" + txt_AR_BTNSearch
                     + "&enName=" + txt_EN_BTNSearch + "&pageSize=" + jtPageSize.ToString()
                    + "&index=" + jtStartIndex.ToString()+ "&jtSorting="+ jtSorting.ToString());
                    lst = res.Content.ReadAsAsync<Dictionary<string, object>>().Result;//object
                }
                else
                {
                    var res = APIHandeling.getData(apiName + "?itemType=" + itemType
                    + "&familyId=" + familyId + "&groupId=" + groupId + "&pageSize=" + jtPageSize.ToString()
                    + "&index=" + jtStartIndex.ToString() + "&jtSorting=" + jtSorting.ToString());
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

                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "listItem");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        //public JsonResult listItem
        //(string txt_AR_BTNSearch = null, string txt_EN_BTNSearch = null, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        //{
        //    try
        //    {
        //        var res = APIHandeling.getData(apiName + "?arName=" + txt_AR_BTNSearch 
        //            + "&enName=" + txt_EN_BTNSearch + "&pageSize=" + jtPageSize.ToString()
        //            + "&index=" + jtStartIndex.ToString()+ "&jtSorting="+ jtSorting.ToString());

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

        //            APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "listItem");
        //            return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
        //        }
        //    }
        //}

        //Insert
        [HttpPost]
        public JsonResult CreateItem(byte? itemType,int? familyId,int?groupId,ItemDTO model, HttpPostedFileBase Picture1)
        {
            try
            {
                if (model.Is_known_item == null)
                {
                    model.Is_known_item = false;
                }
                var allErrors = ModelState.Values.SelectMany(x => x.Errors).ToList();
                if (ModelState.IsValid)
                {
                    User_Session Current = User_Session.GetInstance;
                    model.User_Creation_Id =(short)Session["UserId"];
                    model.User_Creation_Date = DateTime.Now;
                    model.Item_Type_ID = itemType;
                    model.Family_ID = familyId;
                    model.Group_ID = groupId;

                    //if (ListItemPartType_Id.Contains(null))
                    //{
                    //    //remove null value
                    //    ListItemPartType_Id.RemoveAll(x => x == null);
                    //}
                    if (Picture1 != null)
                    {

                        FileUpload_SaveDataController fileUpload = new FileUpload_SaveDataController();
                      
                        string _Path = fileUpload.Get_Uplood_Imge("Item_" + "Item_" + model.ID, Picture1, "Item", "ItemData", Request.Url.AbsoluteUri.ToString());

                        model.Picture = _Path;
                      
                    }
                    //model.ListItemPartType_Id = ListItemPartType_Id;
                    var mynewobj = APIHandeling.Post(apiName, model);

                    return ((int)mynewobj.StatusCode != 409) ? Json(new { Result = "OK", Record = model })
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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "CreateItem");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        //UPDATE
        [HttpPost]
        public JsonResult UpdateItem(ItemDTO model, HttpPostedFileBase Picture1)
        {
            try
            {
                if(model.Is_known_item == null)
                {
                    model.Is_known_item = false;
                }
                if (ModelState.IsValid)
                {
                    User_Session Current = User_Session.GetInstance;

                    model.User_Updation_Id=(short)Session["UserId"];
                    model.User_Updation_Date = DateTime.Now;
                    
                    if (Picture1 != null)
                    {
                        FileUpload_SaveDataController fileUpload = new FileUpload_SaveDataController();
                        //model.Picture = fileUpload.Upload_File_Path_NetworkShare(Picture1, "Item_"+itemType);
                        string _Path = fileUpload.Get_Uplood_Imge("Item_" + "Item_" + model.ID, Picture1, "Item", "ItemData", Request.Url.AbsoluteUri.ToString());

                        model.Picture = _Path;                       
                    }
                  
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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "UpdateItem");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        //DELETE
        [HttpPost]
        public JsonResult DeleteItem(long ID)
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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "DeleteItem");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        //**************************************************************************//       

        [HttpPost]
        public JsonResult listItemParts
        (Int64 ItemId, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                var res = APIHandeling.getData("ItemPart_API?ItemID=" + ItemId + "&pageSize=" + jtPageSize.ToString() + "&index=" + jtStartIndex.ToString());

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

                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "listItemParts");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        [HttpPost]
        public JsonResult CreateItemParts(Int64 ItemId, ItemPartDTO model)
        {
            try
            {
                var allErrors = ModelState.Values.SelectMany(x => x.Errors).ToList();

                if (ModelState.IsValid)
                {
                    User_Session Current = User_Session.GetInstance;

                    model.User_Creation_Id =(short)Session["UserId"];
                    model.User_Creation_Date = DateTime.Now;
                    model.Item_ID = ItemId;

                    var mynewobj = APIHandeling.Post("ItemPart_API", model);

                    return ((int)mynewobj.StatusCode != 409) ? Json(new { Result = "OK", Record = model })
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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "CreateItemParts");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        [HttpPost]
        public JsonResult UpdateItemParts(Int64 ItemId, ItemPartDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    User_Session Current = User_Session.GetInstance;

                    model.User_Updation_Id=(short)Session["UserId"];
                    model.User_Updation_Date = DateTime.Now;
                    model.Item_ID = ItemId;

                    //check Repeated Data
                    var res = APIHandeling.Put("ItemPart_API", model);
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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "UpdateItemParts");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        [HttpPost]
        public JsonResult DeleteItemParts(long ID)
        {
            try
            {
                DeleteParameters obj = new DeleteParameters();
                obj.id = ID;
                User_Session Current = User_Session.GetInstance;

                obj.Userid = (short)Session["UserId"];
                obj._DateNow = DateTime.Now;
                APIHandeling.Delete("ItemPart_API", obj);

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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "DeleteItemParts");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        [HttpGet]
        public ActionResult AllItem()
        {
            var res = APIHandeling.getData(apiName + "?pageSize=-1&index=-1");
        
           var   lst = res.Content.ReadAsAsync<List<ItemDTO>> ().Result;//object

            //var StatusCode = lst.ElementAt(0).Value;
            //var obj = lst.ElementAt(1).Value;

            return Json(lst, JsonRequestBehavior.AllowGet);
        }


        //public JsonResult PictureItem(string path)
        //{
        //    try
        //    {
        //        DeleteParameters obj = new DeleteParameters();
        //        obj.id = ID;
        //        User_Session Current = User_Session.GetInstance;

        //        obj.Userid = (short)Session["UserId"];
        //        obj._DateNow = DateTime.Now;
        //        APIHandeling.Delete("ItemPart_API", obj);

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
        //            APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "DeleteItemParts");
        //            return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
        //        }
        //    }
        //}

        public ActionResult GetReport(string path1)
        {
            try
            {
                Session["Path_Server"] = path1;
                return Redirect("../../DisplayImge.aspx");            
            }
            catch (Exception ex)
            {
                
                return null;

            }

        }
    }
}