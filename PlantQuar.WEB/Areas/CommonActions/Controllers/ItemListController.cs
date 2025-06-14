using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using PlantQuar.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.CommonActions.Controllers
{
    public class ItemListController : BaseController
    {
        // GET: CommonActions/ItemList

        #region تقسيم زراعى

        #region ItemType
        [HttpPost]
        public JsonResult ItemType_List()
        {
            try
            {
                var res = APIHandeling.getData("ItemType_API?ItemTypeList=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "ItemType_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        [HttpPost]
        public JsonResult ItemType_AddEDIT()
        {
            try
            {
                var res = APIHandeling.getData("ItemType_API?ItemTypeAddEdit=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "ItemType_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        //Eslam fill group withou classifaction
        [HttpPost]
        public JsonResult ItemTypeGroup_AddEDIT(int ItemType_ID=0)
        {
            try
            {
                var res = APIHandeling.getData("ItemType_API?ItemTypeGroupAddEdit=1&ItemType_ID=" + ItemType_ID);
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "ItemType_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        #endregion

        #region MainClassification

        [HttpPost]
        public JsonResult MainClassification_List()
        {
            try
            {
                var res = APIHandeling.getData("MainClassification_API?List=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "MainClassification_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        [HttpPost]
        public JsonResult MainClassification_AddEDIT(int ItemType_ID = 0)
        {

            try
            {
                if (ItemType_ID > 0)
                {
                    var res = APIHandeling.getData("MainClassification_API?AddEdit=1&ItemType_ID=" + ItemType_ID);
                    var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                    return Json(new { Result = "OK", Options = lst });
                }
                else
                {
                    List<CustomOption> lst = new List<CustomOption>();
                    lst.Add(new CustomOption() { DisplayText = "---", Value = null });
                    return Json(new { Result = "OK", Options = lst });
                }

            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "MainClassification_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        #endregion

        #region SecondaryClassification


        [HttpPost]
        public JsonResult SecondaryClassification_List()
        {
            try
            {
                var res = APIHandeling.getData("SecondaryClassification_API?List=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "SecondaryClassification_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        [HttpPost]
        public JsonResult SecondaryClassification_AddEDIT(int MainClass_ID = 0)
        {
            try
            {
                if (MainClass_ID > 0)
                {
                    var res = APIHandeling.getData("SecondaryClassification_API?AddEdit=1&MainClass_ID=" + MainClass_ID);
                    var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                    return Json(new { Result = "OK", Options = lst });
                }
                else
                {
                    List<CustomOption> lst = new List<CustomOption>();
                    lst.Add(new CustomOption() { DisplayText = "---", Value = null });
                    return Json(new { Result = "OK", Options = lst });
                }


            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "SecondaryClassification_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        #endregion

        #region Group

        [HttpPost]
        public JsonResult ItemGroup_List()
        {
            try
            {
                var res = APIHandeling.getData("Group_API?List=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "PlantGroup_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        [HttpPost]
        public JsonResult ItemGroup_AddEDIT(int SecClass_ID = 0)
        {
            try
            {
                var res = APIHandeling.getData("Group_API?AddEdit=1&SecClass_ID=" + SecClass_ID);
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
                //if (SecClass_ID > 0)
                //{
                //    var res = APIHandeling.getData("Group_API?AddEdit=1&SecClass_ID=" + SecClass_ID);
                //    var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                //    return Json(new { Result = "OK", Options = lst });
                //}
                //else
                //{
                //    List<CustomOption> lst = new List<CustomOption>();
                //    lst.Add(new CustomOption() { DisplayText = "---", Value = null });
                //    return Json(new { Result = "OK", Options = lst });
               // }
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "PlantGroup_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        #endregion

        #endregion

        #region Item
        [HttpPost]
        public JsonResult ItemData_AddEDIT(int Group_ID = 0)
        {
            try
            {
                var res = APIHandeling.getData("Item_API?AddEdit=1&Group_ID=" + Group_ID);
                var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "ItemData_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        //[HttpPost]
        //public JsonResult ItemData_AddEDIT_Known(int Group_ID = 0, bool IsKnown = false)
        //{
        //    try
        //    {
        //        var res = APIHandeling.getData("Item_API?Group_ID=" + Group_ID + "&IsKnown=" + IsKnown);
        //        var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
        //        return Json(new { Result = "OK", Options = lst });
        //    }
        //    catch (Exception ex)
        //    {
        //        APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "ItemData_AddEDIT_Known");
        //        return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
        //    }
        //}
        //sara by sayed

        [HttpPost]
        public JsonResult ItemData_AddEDIT_Known(int Group_ID = 0,int Family_ID = 0, bool IsKnown = false)
        {
            try
            {
                var res = APIHandeling.getData("Item_API?Group_ID=" + Group_ID + "&Family_ID=" + Family_ID + "&IsKnown=" + IsKnown);
                var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "ItemData_AddEDIT_Known");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }


        [HttpPost]
        public JsonResult ItemFilterByTypeFamilyAndGroup(byte? itemType, int? familyId, int? groupId)
        {
            try
            {

                var res = APIHandeling.getData("Item_API?itemType=" + itemType + "&familyId=" + familyId + "&groupId=" + groupId);
                var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "PlantFamily_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }


        [HttpPost]
        public JsonResult ItemFilterByTypeFamilyAndGroup_AddEdit(int AddEditIFG)
        {
            try
            {

                var res = APIHandeling.getData("Item_API?AddEditIFG=0");
                var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "PlantFamily_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        #endregion

        #region ItemShortName

        [HttpPost]
        public JsonResult ItemShortName_List(int item_Id = 0, bool isProduct = false)
        {
            if (item_Id > 0)
            {
                var res = APIHandeling.getData("Item_ShortName_API?itemId=" + item_Id + "&isProduct=" + isProduct);
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            else
            {
                List<CustomOption> lst = new List<CustomOption>();
                lst.Add(new CustomOption() { DisplayText = "---", Value = null });
                return Json(new { Result = "OK", Options = lst });
            }
        }

        public JsonResult ItemShortName_ListNoImportStatus(int item_Id = 0, bool isProduct = false)
        {
            if (item_Id > 0)
            {
                var res = APIHandeling.getData("Item_ShortName_API?itemId2=" + item_Id + "&isProduct2=" + isProduct);
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            else
            {
                List<CustomOption> lst = new List<CustomOption>();
                lst.Add(new CustomOption() { DisplayText = "---", Value = null });
                return Json(new { Result = "OK", Options = lst });
            }
        }
        [HttpPost]
        public JsonResult ItemShortName_AddEDIT(int item_Id = 0, bool isProduct = false)
        {
            try
            {
                var res = APIHandeling.getData("Item_ShortName_API?itemId=" + item_Id + "&isProduct=" + isProduct);
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "ItemData_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        //public JsonResult ItemFilterForShortName(byte? itemType, int? familyId, int? groupId, bool? known)
        //{
        //    try
        //    {

        //        var res = APIHandeling.getData("Item_API?itemType=" + itemType + "&familyId=" + familyId + "&groupId=" + groupId + "&known=" + known);
        //        var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
        //        return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);

        //    }
        //    catch (Exception ex)
        //    {
        //        APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "PlantFamily_AddEDIT");
        //        return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
        //    }
        //}

        public JsonResult ItemFilterForShortName(byte? itemType, int? familyId, int? groupId, bool? known)
        {
            try
            {

                var res = APIHandeling.getData("Item_API?itemType=" + itemType + "&familyId=" + familyId + "&groupId=" + groupId + "&known=" + known);
                var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "PlantFamily_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        public JsonResult ItemFilterForShortNameUnkown(byte? itemType, int? familyId, int? groupId)
        {
            try
            {

                var res = APIHandeling.getData("Item_API?itemType=" + itemType + "&familyId=" + familyId + "&groupId=" + groupId);
                var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "PlantFamily_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }


        #endregion

        #region ItemCategories
        [HttpPost]
        public JsonResult ItemCategoriesGrp_ByItem(long ItemId = 0)
        {
            try
            {
                var res = APIHandeling.getData("ItemCategories_Group_API?ItemId=" + ItemId);
                var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "ItemCategoriesGrp_ByItem");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        #endregion    

        #region Farm
        [HttpPost]
        public JsonResult Plant_AddEDIT(bool AddEdit)
        {
            try
            {
                var res = APIHandeling.getData("Item_API?AddEdit=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Plant_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        #endregion

        #region تقسيم علمى
        #region Kingdom
        [HttpPost]
        public JsonResult Kingdom_List()
        {
            try
            {
                var res = APIHandeling.getData("Kingdom_API?List=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Kingdom_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        [HttpPost]
        public JsonResult Kingdom_AddEDIT()
        {
            try
            {
                var res = APIHandeling.getData("Kingdom_API?AddEdit=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Kingdom_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        #endregion

        #region Level

        [HttpPost]
        public JsonResult Level_List()
        {
            try
            {
                var res = APIHandeling.getData("Level_API?List=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Level_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        [HttpPost]
        public JsonResult Level_AddEDIT()
        {
            try
            {
                var res = APIHandeling.getData("Level_API?AddEdit=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Level_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        #endregion

        #region PhylumSubphylum

        [HttpPost]
        public JsonResult PhylumSubphylum_List()
        {
            try
            {
                //if (Kingdom_ID > 0)
                //{
                //    var res = APIHandeling.getData("PhylumSubphylum?List=1&Kingdom_ID=" + Kingdom_ID);
                //    var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                //    return Json(new { Result = "OK", Options = lst });
                //}
                //return null;
                var res = APIHandeling.getData("PhylumSubphylum_API?List=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "PhylumSubphylum_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        [HttpPost]
        public JsonResult PhylumSubphylum_AddEDIT(int Kingdom_ID = 0)
        {
            try
            {
                if (Kingdom_ID > 0)
                {
                    var res = APIHandeling.getData("PhylumSubphylum_API?AddEdit=1&Kingdom_ID=" + Kingdom_ID);
                    var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                    return Json(new { Result = "OK", Options = lst });
                }
                else
                {
                    List<CustomOption> lst = new List<CustomOption>();
                    lst.Add(new CustomOption() { DisplayText = "---", Value = null });
                    return Json(new { Result = "OK", Options = lst });
                }
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "PhylumSubphylum_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        #endregion

        #region Order
        [HttpPost]
        public JsonResult Order_List()
        {
            try
            {
                //if (Phylum_ID > 0)
                //{
                //    var res = APIHandeling.getData("PlantOrder?List=1&Phylum_ID=" + Phylum_ID);
                //    var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                //    return Json(new { Result = "OK", Options = lst });
                //}
                //return null;
                var res = APIHandeling.getData("Order_API?List=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });

            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Order_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        [HttpPost]
        public JsonResult Order_AddEDIT(int Phylum_ID = 0)
        {
            try
            {
                if (Phylum_ID > 0)
                {
                    var res = APIHandeling.getData("Order_API?AddEdit=1&Phylum_ID=" + Phylum_ID);
                    var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                    return Json(new { Result = "OK", Options = lst });
                }
                else
                {
                    List<CustomOption> lst = new List<CustomOption>();
                    lst.Add(new CustomOption() { DisplayText = "---", Value = null });
                    return Json(new { Result = "OK", Options = lst });
                }
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Order_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        #endregion

        #region Family
        public JsonResult Family_List()
        {
            try
            {
                //if (Order_ID > 0)
                //{
                //    var res = APIHandeling.getData("PlantFamily?List=1&Order_ID=" + Order_ID);
                //    var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                //    return Json(new { Result = "OK", Options = lst });
                //}
                //return null;
                var res = APIHandeling.getData("Family_API?List=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "PlantFamily_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        [HttpPost]       
        public JsonResult Family_AddEDIT(int Order_ID = 0)
        {
            try
            {
                if (Order_ID > 0 || Order_ID == -1)
                {
                    var res = APIHandeling.getData("Family_API?AddEdit=1&Order_ID=" + Order_ID);
                    var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                    return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var res = APIHandeling.getData("Family_API?AddEdit=1");
                    var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                    return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "PlantFamily_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        //public JsonResult Family_AddEDIT(int Order_ID = 0)
        //{
        //    try
        //    {
        //        if (Order_ID > 0)
        //        {
        //            var res = APIHandeling.getData("Family_API?AddEdit=1&Order_ID=" + Order_ID);
        //            var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
        //            return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //        {
        //            var res = APIHandeling.getData("Family_API?AddEdit=1");
        //            var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
        //            return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "PlantFamily_AddEDIT");
        //        return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
        //    }
        //}
        #endregion

        #endregion

        #region SubItemType
        [HttpPost]
        public JsonResult SubItemType_List()
        {
            try
            {
                var res = APIHandeling.getData("SubPart_Type_API?ListAddEdit=1");
                // var res = APIHandeling.getData("ItemType_API?ItemTypeList=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "ItemType_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        public JsonResult SubItemType_AddEDIT()
        {
            try
            {//SubPart_API
                var res = APIHandeling.getData("SubPart_Type_API?AddEdit=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "ItemType_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        #endregion

        //********************
        [HttpPost]
        public JsonResult Group_ItemType_List(int itemType = 0)
        {
            try
            {
                var res = APIHandeling.getData("Group_API?List=1&itemType=" + itemType);
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "PlantGroup_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        [HttpPost]
        public JsonResult Family_ByItemType(int itemType = 0)
        {
            try
            {

                var res = APIHandeling.getData("Family_API?itemType=" + itemType);
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "PlantFamily_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }


        [HttpPost]
        public JsonResult ItemShortName_AddEdit_List()
        {

            var res = APIHandeling.getData("Item_ShortName_API?AddEdit=" + 1);
            var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
            return Json(new { Result = "OK", Options = lst });

        }
    }
}