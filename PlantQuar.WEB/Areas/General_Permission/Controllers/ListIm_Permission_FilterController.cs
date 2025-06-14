using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using PlantQuar.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.General_Permission.Controllers
{
    public class ListIm_Permission_FilterController : Controller
    {
        // GET: General_Permission/ListIm_Permission_Filter
        public ActionResult Index(int? OperationCode=1)
        
        {
            ViewBag.OperationCode = OperationCode;
            Session["OperationCode"] = OperationCode;

            var Company = APIHandeling.getData("Employee_Track_API?Company=-1");
            var lst_Company_Name = Company.Content.ReadAsAsync<List<CustomOption>>().Result;
            ViewBag.lst_Company_Name = lst_Company_Name;

            return View();
        }

        [HttpPost]
        public JsonResult Country_List()
        {
            try
            {
                var res = APIHandeling.getData("Country_API?Im_Permission=1");
                var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Country_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        [HttpPost]
        public JsonResult ShortName_List()
        {
            try
            {
                var res = APIHandeling.getData("Item_ShortName_API?Im_Permission=1");
                var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "ShortName_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        [HttpPost]
        public JsonResult Company_List()
        {
            try
            {
                var res = APIHandeling.getData("Company_National_API?Im_Permission=1");
                var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Company_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
    }
}