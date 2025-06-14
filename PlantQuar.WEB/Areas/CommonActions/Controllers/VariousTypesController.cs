using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

using PlantQuar.WEB.Controllers;
using PlantQuar.WEB.App_Start;

namespace PlantQuar.Web.Areas.CommonActions.Controllers
{
    public class VariousTypesController : BaseController
    {
        // GET: CommonActions/VariousTypes

        // AYM 21Feb2019
        #region GeneralAdmin
        
        [HttpPost]
        public JsonResult GeneralAdmin_AddEDIT()
        {
            try
            {
                var res = APIHandeling.getData("GeneralAdmin_API?AddEdit=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(  Request.Url.AbsoluteUri.ToString(), ex.Message, "GeneralAdmin_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        #endregion

       

        #region A_SystemCode
        [HttpPost]
       
        public JsonResult A_SystemCode_AddEDIT(int Syscodenum, bool IsJtable = true)
        {
            try
            {
                var res = APIHandeling.getData("A_SystemCode_API?AddEdit=1&Sysnum=" + Syscodenum);
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
                APIHandeling.Insert_Error(  Request.Url.AbsoluteUri.ToString(), ex.Message, "A_SystemCode_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        #endregion

        #region TreatmentMainType
        [HttpPost]
        public JsonResult TreatmentMainType_List()
        {
            try
            {
                var res = APIHandeling.getData("TreatmentMainType_API?List=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "TreatmentMainType_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        [HttpPost]
        public JsonResult TreatmentMainType_AddEDIT()
        {
            try
            {
                var res = APIHandeling.getData("TreatmentMainType_API?AddEdit=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "TreatmentMainType_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        #endregion

    }
}