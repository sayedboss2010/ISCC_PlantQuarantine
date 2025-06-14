using PlantQuar.DTO.DTO.Common;
using PlantQuar.DTO.DTO.Export_CheckRequest;
using PlantQuar.DTO.DTO.Pallet_Export_CheckRequest;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using PlantQuar.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.Pallet_Export_CheckRequest.Controllers
{
    public class Pallet_List_EXCheckRequestController : BaseController
    {
        // GET: Pallet_Export_CheckRequest/Pallet_List_EXCheckRequest
        public ActionResult Index()
        {
            try
            {
                var IsApproved = 1;
                var userId = (short)Session["UserId"];
                var res = APIHandeling.getData("Pallet_EX_CheckRequests_API?IsApproved=" + IsApproved + "&userId=" + userId);
                var modelLst = res.Content.ReadAsAsync<List<Pallet_EXCheckRequestListDTO>>().Result;
                return View(modelLst);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "List_EXRequestsController");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
           
        }
        [HttpPost]
        public JsonResult Im_CheckRequest_List(string IsApproved = "1", string requestnumber = "")
        {
            try
            {
                var userId = (short)Session["UserId"];
                var res = APIHandeling.getData("Pallet_EX_CheckRequests_API?ImCheckRequest_Number=" + requestnumber +
                   "&IsApproved=" + IsApproved +"&userId="+ userId);
                var modelLst = res.Content.ReadAsAsync<List<Pallet_EXCheckRequestListDTO>>().Result;

                return Json(new { Result = "OK", Records = modelLst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "List_EXRequestsController");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

    }
}