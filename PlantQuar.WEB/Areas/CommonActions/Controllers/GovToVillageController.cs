using Microsoft.Ajax.Utilities;
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
    public class GovToVillageController : Controller
    {
        // GET: CommonActions/GovToVillage
        [HttpPost]
        public JsonResult GetGovOptions()
        {
            try
            {
                Dictionary<string, int> dic = new Dictionary<string, int>();
                dic.Add("AddEdit", 1);
                var res = APIHandeling.getDataByParamter("Governate_API", dic);
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;

                var Governaments = lst.Select(c => new { DisplayText = c.DisplayText, Value = c.Value, }).OrderBy(s => s.DisplayText);

                return Json(new { Result = "OK", Options = Governaments });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Govern_AddEDIT");
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult Gov_List()
        {
            try
            {
                var res = APIHandeling.getData("Governate_API?List=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Gov_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }        

        public List<CustomOption> Govern_AddEDIT_AsList()
        {
            var res = APIHandeling.getData("Governate_API?AddEdit=1");
            var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
            return lst;
        }
        [HttpPost]
        public JsonResult GetCenterOptions(int? Govern_ID)
        {
            try
            {
                Dictionary<string, int> dicData = new Dictionary<string, int>();
                dicData.Add("GovId", Govern_ID == null ? 0 : Govern_ID.Value);
                var res = APIHandeling.getDataByParamter("Centers_API", dicData);
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                var Centers = lst.Select(c => new { DisplayText = c.DisplayText, Value = c.Value, }).OrderBy(s => s.DisplayText);

                return Json(new { Result = "OK", Options = Centers.DistinctBy(a => a.DisplayText) });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult GetCenterOptionsOutlet(int? Govern_ID, long outletId)
        {
            try
            {

                var res = APIHandeling.getData("Centers_API?GovId=" + Govern_ID + "&outletId=" + outletId);
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                //var Centers = lst.Select(c => new { DisplayText = c.DisplayText, Value = c.Value, }).OrderBy(s => s.DisplayText);

                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult Center_List(int Govern_ID)
        {
            try
            {
                var res = APIHandeling.getData("Centers_API?GovId=" + Govern_ID);
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Gov_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        public JsonResult Center_AddEdit(int Govern_ID = 0)
        {
            try
            {
                var res = APIHandeling.getData("Centers_API?add=1&GovId=" + Govern_ID);
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Gov_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        [HttpPost]
        public JsonResult Center_ListAll()
        {
            try
            {
                var res = APIHandeling.getData("Centers_API?List=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Gov_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        #region Village

        [HttpPost]
        public JsonResult Village_List()
        {
            try
            {
                var res = APIHandeling.getData("Village_API?List=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Village_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        [HttpPost]
        public JsonResult Village_AddEDIT()
        {
            try
            {
                var res = APIHandeling.getData("Village_API?AddEdit=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Village_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }



        [HttpPost]
        public JsonResult GetVillageOptions(int Center_ID = 0)
        {
            try
            {
                var res = APIHandeling.getData("Village_API?CenterId=" + Center_ID);
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                var x = API_HelperFunctions.Get_DeviceInfo();
                var lang = x[2];
                var Centers = lst.Select(c => new { DisplayText = c.DisplayText, Value = c.Value, }).OrderBy(s => s.DisplayText);
                return Json(new { Result = "OK", Options = Centers });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
        #endregion
    }
}
