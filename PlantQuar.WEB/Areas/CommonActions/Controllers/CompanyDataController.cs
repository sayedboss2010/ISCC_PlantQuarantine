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
    public class CompanyDataController : BaseController
    {
        // GET: CommonActions/CompanyData
        #region CompanyActivityType
        [HttpPost]
        public JsonResult CompanyMainActivityTypes_List()
        {
            try
            {
                var res = APIHandeling.getData("A_SystemCode_API?Syscode=17");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "CompanyMainActivityTypes_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        [HttpPost]
        public JsonResult CompanyActivityType_List()
        {
            try
            {
                var res = APIHandeling.getData("CompanyActivityType_API?List=1");
                var lst = res.Content.ReadAsAsync<List<CustomOptionShortId>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "CompanyActivityType_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        [HttpPost]
        public JsonResult CompanyActivityType_AddEDIT()
        {
            try
            {
                var res = APIHandeling.getData("CompanyActivityType_API?AddEdit=1");
                var lst = res.Content.ReadAsAsync<List<CustomOptionShortId>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "CompanyActivityType_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        #endregion

        #region Company_National
        [HttpPost]
        public JsonResult Company_National_List()
        {
            try
            {
                var res = APIHandeling.getData("Company_National_API?List=1");
                var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Company_National_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        [HttpPost]
        public JsonResult Company_National_AddEDIT()
        {
            try
            {
                var res = APIHandeling.getData("Company_National_API?AddEdit=1");
                var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Company_National_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        #endregion

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
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "A_SystemCode_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        #region PublicOrganization_Type
        [HttpPost]
        public JsonResult PublicOrganization_Type_List()
        {
            try
            {
                var res = APIHandeling.getData("PublicOrganizationType_API?List=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "PublicOrganizationType_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        [HttpPost]
        public JsonResult PublicOrganization_Type_AddEDIT()
        {
            try
            {
                var res = APIHandeling.getData("PublicOrganizationType_API?AddEdit=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "PublicOrganizationType_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        #endregion

    }
}