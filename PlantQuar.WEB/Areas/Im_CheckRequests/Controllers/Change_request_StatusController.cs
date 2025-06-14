using PlantQuar.DTO.DTO.Import.CheckRequests;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.Im_CheckRequests.Controllers
{
    public class Change_request_StatusController : Controller
    {
        // GET: Im_CheckRequests/Change_request_Status
        public ActionResult Index()
        {
            return View();
        }
        
        public JsonResult requst_List(string CheckNumber)
        {
            try
            {
                var res = APIHandeling.getData("Change_request_StatusAPI?CheckNumber=" + CheckNumber);
                var lst = res.Content.ReadAsAsync<List<Change_request_StatusDTO>>().Result;//object
                //return Json(new { Result = "OK", Options = lst });
                return Json(new { Result = lst.FirstOrDefault().Message, Records = lst }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Country_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }


        public JsonResult update_request_status(string CheckNumber2,long CheckNumber_ID)
        {
            Change_request_StatusDTO model = new Change_request_StatusDTO();
            model.CheckRequest_Number = CheckNumber2;
            model.ID = CheckNumber_ID;
            model.user_id = (short)Session["UserId"];
            try
            {
                var res = APIHandeling.Put("Change_request_StatusAPI?", model);
                var lst = res.Content.ReadAsAsync<List<Change_request_StatusDTO>>().Result;//object
                //return Json(new { Result = "OK", Options = lst });
                return Json(new { Result = lst.FirstOrDefault().Message, Records = lst }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Country_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
    }
}