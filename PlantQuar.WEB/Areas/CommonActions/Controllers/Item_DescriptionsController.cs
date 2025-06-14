using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.CommonActions.Controllers
{
    public class Item_DescriptionsController : Controller
    {
        // GET: CommonActions/Item_Descriptions
        public ActionResult Index()
        {
            return View();
        }

        #region اجزاء الاصناف
        [HttpPost]
        public JsonResult SubPart_List(int Item_Type_ID = 0,int SubPart_Type_ID=0)
        {
            try
            {
                var res = APIHandeling.getData("SubPart_API?PartTypeList=1&ItemType_ID=" + Item_Type_ID + "&SubPart_Type_ID="+ SubPart_Type_ID);
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "SubPart_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        [HttpPost]
        public JsonResult SubPart_AddEDIT(int Item_Type_ID = 0)
        {
            try
            {
                var res = APIHandeling.getData("SubPart_API?PartTypeAddEdit=1&ItemType_ID=" + Item_Type_ID);
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "SubPart_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        #endregion

        #region اغراض الاصناف

        [HttpPost]
        public JsonResult Item_Purpose_List()
        {
            try
            {
                var res = APIHandeling.getData("Item_Purpose_API?PartTypeList=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Item_Purpose_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        [HttpPost]
        public JsonResult Item_Purpose_AddEDIT()
        {
            try
            {
                var res = APIHandeling.getData("Item_Purpose_API?PartTypeAddEdit=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Item_Purpose_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        #endregion

        #region Item_Status اغراض الاصناف
        [HttpPost]
        public JsonResult Item_Status_List()
        {
            try
            {
                var res = APIHandeling.getData("Item_Status_API?PartTypeList=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Item_Status_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        [HttpPost]
        public JsonResult Item_Status_AddEDIT()
        {
            try
            {
                var res = APIHandeling.getData("Item_Status_API?PartTypeAddEdit=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Item_Status_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        #endregion

        #region اجزاء النبات
        [HttpPost]
        public JsonResult ItemPartsList(bool List, int Item_Type_ID = 0)
        {
            try
            {
                if (List == true)
                {
                    var res = APIHandeling.getData("ItemPart_API?List=1");
                    var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                    return Json(new { Result = "OK", Options = lst });
                }
                else
                {
                    var res = APIHandeling.getData("ItemPart_API??List=1&Item_Type_ID=" + Item_Type_ID);
                    var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                    return Json(new { Result = "OK", Options = lst });
                }
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "ItemPartsList");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        #endregion

        #region انواع الاصناف
        [HttpPost]
        public JsonResult ItemCategoryType_List()
        {
            try
            {
                var res = APIHandeling.getData("ItemCategories_TypeAPI?List=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "ItemCategoryType_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        [HttpPost]
        public JsonResult ItemCategoryType_AddEDIT()
        {
            try
            {
                var res = APIHandeling.getData("ItemCategories_TypeAPI?AddEdit=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "ItemCategoryType_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        #endregion

    }
}