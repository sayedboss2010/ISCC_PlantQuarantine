using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.Station_Pages.Controllers
{
    public class Station_Fees_TypeController : Controller
    {
        // GET: Station_Pages/Station_Fees_Type

        string api = "Station_Fees_Type_API";
        public ActionResult Index()
        {
            return View();
        }


        //DROPS

        [HttpPost]
        public JsonResult get_Station_Fees_Type_Lst(int Fees_Type)
        {
            try
            {
                var res = APIHandeling.getData(api+ "?Fees_Type="+ Fees_Type);
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "getShiftTiming_Lst");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        [HttpPost]
        public JsonResult get_Station_Fees_Type_Mony(byte Station_Fees_Type_ID)
        {
            try
            {
                var res = APIHandeling.getData(api+ "?Station_Fees_Type_ID=" + Station_Fees_Type_ID);
                var mony = res.Content.ReadAsAsync<Nullable<double>>().Result;
                return Json(new { Result = "OK", Options = mony }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "getShiftTiming_Lst");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
    }
}