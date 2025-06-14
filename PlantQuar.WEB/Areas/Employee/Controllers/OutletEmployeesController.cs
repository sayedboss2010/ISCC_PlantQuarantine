using PlantQuar.DTO.DTO.Admin;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.Employee.Controllers
{
    public class OutletEmployeesController : Controller
    {
        // GET: Employee/OutletEmployees
        string apiName = "Mission_API";

        public ActionResult Index()
        {
            try
            {
                var Fees_Process = APIHandeling.getData(apiName + "?Outlet=-1");
                var lst = Fees_Process.Content.ReadAsAsync<List<CustomOption>>().Result;
                //ViewBag.ddd = new SelectList(lst, "Value", "DisplayText");
                ViewBag.ddd = lst;

                // return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetOutlet_ID");
                // return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
            try
            {
                var Fees_Process = APIHandeling.getData(apiName + "?User=6");
                var lst = Fees_Process.Content.ReadAsAsync<List<CustomOption>>().Result;
                ViewBag.sss = lst;

                // return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                ViewBag.sss = ex.Message;
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetOutlet_ID");
                //  return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
            return View();
        }
        [HttpPost]
        public ActionResult UsersOutlet(long outletID)
        {
            try
            {
                var Fees_Process = APIHandeling.getData(apiName + "?User=" + outletID + "");
                var lst = Fees_Process.Content.ReadAsAsync<List<User>>().Result;
                //  ViewBag.sss = lst;

                return Json(lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                //  ViewBag.sss = ex.Message;
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetOutlet_ID");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

    }






}