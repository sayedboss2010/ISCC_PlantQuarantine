using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using PlantQuar.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.CommonActions.Controllers
{
    public class Country_UnionController : BaseController
    {
        // GET: CommonActions/Country_Union

        #region Country
        [HttpPost]
        public JsonResult Country_List()
        {
            try
            {
                var res = APIHandeling.getData("Country_API?List=1");
                var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error( Request.Url.AbsoluteUri.ToString(), ex.Message, "Country_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        [HttpPost]
        public JsonResult Country_AddEDIT()
        {
            try
            {
                var res = APIHandeling.getData("Country_API?AddEdit=1");
                var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error( Request.Url.AbsoluteUri.ToString(), ex.Message, "Country_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }



        ///////////////Hadeer//////////////
        /// <summary>
        /// 
        public JsonResult Country_List2()
        {
            try
            {
                var res = APIHandeling.getData("Country_API?List2=1");
                var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                return Json(new
                {
                    Result = "OK",
                    Options = lst
                });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Country_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        [HttpPost]
        public JsonResult Country_AddEDIT2()
        {
            try
            {
                var res = APIHandeling.getData("Country_API?AddEdit2=1");
                var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Country_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }



        ////////////// End Hadeer//////////////////
        /// </summary>
        /// <returns></returns>
        //SARA
        public List<CustomOptionLongId> Country_AddEDIT_AsList()
        {
            var res = APIHandeling.getData("Country_API?AddEdit2=1");
            var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
            return lst;
        }
        //END SARA
        #endregion

        #region Union
        [HttpPost]
        public JsonResult Union_List()
        {
            try
            {
                var res = APIHandeling.getData("Union_API?List=1");
                var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error( Request.Url.AbsoluteUri.ToString(), ex.Message, "Union_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        [HttpPost]
        public JsonResult Union_AddEDIT()
        {
            try
            {
                var res = APIHandeling.getData("Union_API?AddEdit=1");
                var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error( Request.Url.AbsoluteUri.ToString(), ex.Message, "Union_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        #endregion

        #region Continent
     
        [HttpPost]
        public JsonResult Continent_List()
        {
            try
            {
                var res = APIHandeling.getData("Continent_API?List=1");
                var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Continent_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        [HttpPost]
        public JsonResult Continent_AddEDIT()
        {
            try
            {
                var res = APIHandeling.getData("Continent_API?AddEdit=1");
                var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Continent_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        #endregion

        #region Region

        [HttpPost]
        public JsonResult Region_List(short Country_ID=0)
        {
            try
            {
                var res = APIHandeling.getData("Region_API?List=1&&Country_ID=" + Country_ID);
                var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Region_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        [HttpPost]
        public JsonResult Region_AddEDIT( short Country_ID=0)
        {
            try
            {
                var res = APIHandeling.getData("Region_API?AddEdit=1&&Country_ID=" + Country_ID);
                var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Region_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        #endregion
        public JsonResult RegionArea_AddEDIT()
        {
            try
            {
                var res = APIHandeling.getData("RegionArea_API?List=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Region_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
    }
}