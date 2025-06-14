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
    public class PortDataController : Controller
    {
        // GET: CommonActions/PortData
        #region PortType
        [HttpPost]
        public JsonResult PortType_List()
        {
            try
            {
                var res = APIHandeling.getData("PortType_API?List=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "PortType_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        [HttpPost]
        public JsonResult PortType_AddEDIT()
        {
            try
            {
                var res = APIHandeling.getData("PortType_API?AddEdit=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "PortType_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        #endregion

        #region PortOrganization
        [HttpPost]
        public JsonResult PortOrganization_List()
        {
            try
            {
                var res = APIHandeling.getData("PortOrganization_API?List=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "PortOrganization_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        [HttpPost]
        public JsonResult PortOrganization_AddEDIT()
        {
            try
            {
                var res = APIHandeling.getData("PortOrganization_API?AddEdit=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "PortOrganization_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        #endregion

        public JsonResult Portnational(bool IsJtable = true, int portType = 0)
        {
            try
            {
                var res = APIHandeling.getData("PortNational_API?PortNational=1&portType=" + portType);
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                if (IsJtable)
                {
                    return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { Records = lst }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "PortInternational");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        #region PortInternational
        //   [HttpPost]
        public JsonResult PortInternational(int CountryID = 0, bool IsJtable = true)
        {
            try
            {
                var res = APIHandeling.getData("PortInternational_API?CountryID=" + CountryID);
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                if (IsJtable)
                {
                    return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { Records = lst }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "PortInternational");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        public JsonResult PortInternationalWithType(int CountryID = 0, int portType = 0, bool IsJtable = true)
        {
            try
            {
                var res = APIHandeling.getData("PortInternational_API?countryID=" + CountryID + "&portType=" + portType);
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                if (IsJtable)
                {
                    return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { Records = lst }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "PortInternational");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        #endregion
    }
}