using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using PlantQuar.WEB.App_Start;
using PlantQuar.DTO.HelperClasses;

namespace PlantQuar.WEB.Areas.CommonActions.Controllers
{
    public class ItemPartsController : Controller
    {
        // GET: CommonActions/PlantParts
        #region ProductStatus

        [HttpPost]
        public JsonResult ProductStatus_List()
        {
            try
            {
                var res = APIHandeling.getData("ProductStatus?List=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "ProductStatus_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        [HttpPost]
        public JsonResult ProductStatus_AddEDIT()
        {
            try
            {
                var res = APIHandeling.getData("ProductStatus?AddEdit=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "ProductStatus_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        #endregion

        #region PlantPurpose
        [HttpPost]
        public JsonResult PlantPurpose_List()
        {
            try
            {
                var res = APIHandeling.getData("PlantPurpose?List=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "PlantPurpose_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        [HttpPost]
        public JsonResult PlantPurpose_AddEDIT()
        {
            try
            {
                var res = APIHandeling.getData("PlantPurpose?AddEdit=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "PlantPurpose_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        #endregion

        #region PartType

        [HttpPost]
        public JsonResult PlantPartType_List()
        {
            try
            {
                var res = APIHandeling.getData("PlantPartType?List=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "PlantPartType_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        [HttpPost]
        public JsonResult PlantPartType_AddEDIT(Int64 PlantId = 0, bool IsJtable = true)
        {
            try
            {
                var res = APIHandeling.getData("PlantPart?AddEdit=1&PlantId=" + PlantId);
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                // return Json(new { Result = "OK", Options = lst });
                if (IsJtable)
                {
                    return Json(new { Result = "OK", Options = lst });
                }
                else
                {
                    return Json(lst, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "PlantPartType_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        #endregion

        [HttpPost]
        public JsonResult PlantPart_List(Int64 PlantId = 0)
        {
            try
            {
                var res = APIHandeling.getData("PlantPart?part=1&plantId=" + PlantId);
                var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "PlantPart_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        #region Plant

        [HttpPost]
        public JsonResult Plant_List(bool IsJtable = true)
        {
            try
            {
                var res = APIHandeling.getData("Plant?List=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                //  return Json(new { Result = "OK", Options = lst });
                if (IsJtable)
                {
                    return Json(new { Result = "OK", Options = lst });
                }
                else
                {
                    return Json(lst, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Plant_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }


        // Mahmoud Saber ...
        [HttpPost]
        public JsonResult Plant_AddEDIT()
        {
            try
            {
                var res = APIHandeling.getData("ItemPart_API?AddEdit=1");
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

        #region PlantCategory

        //[HttpPost]
        //public JsonResult PlantCategory_List(int List, int Plant_ID)
        //{
        //    try
        //    {
        //        var res = APIHandeling.getData("PlantCategory", "GetPlantCategoryADDEDITParam?List=1 & Plant_ID = " + Plant_ID);
        //        var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
        //        var data = lst.Select(c => new CustomOption { DisplayText = c.DisplayText, Value = c.Value, }).OrderBy(s => s.DisplayText);
        //        return Json(new { Result = "OK", Options = data });
        //    }
        //    catch (Exception ex)
        //    {
        //        APIHandeling.Insert_Error("PlantCategory", Request.Url.AbsoluteUri.ToString(), ex.Message, "PlantCategory_List");
        //        return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
        //    }
        //}

        #endregion


        #region PlantCategory

        [HttpPost]
        public JsonResult PlantCategory_List()
        {
            try
            {
                var res = APIHandeling.getData("PlantCategory?List=1");
                var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "PlantCategory_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        [HttpPost]
        public JsonResult PlantCategory_AddEDIT()
        {
            try
            {
                var res = APIHandeling.getData("PlantCategory?AddEdit=1");
                var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "PlantCategory_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        #endregion
    }
}