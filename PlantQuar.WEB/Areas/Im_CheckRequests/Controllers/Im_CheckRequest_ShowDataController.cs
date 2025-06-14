using PlantQuar.DTO.DTO.Import.CheckRequests;
using PlantQuar.WEB.App_Start;
using PlantQuar.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.Im_CheckRequests
{
    public class Im_CheckRequest_ShowDataController : BaseController
    {
        string apiName = "Im_CheckRequest_ShowData_API";
        // GET: Im_CheckRequests/Im_CheckRequest_ShowData
        public ActionResult Index()
        {
            try
            {
                var Fees_Process = APIHandeling.getData(apiName);
                // var lst = Fees_Process.Content.ReadAsAsync<Im_CheckRequest_ShowDataDTO>().Result;
                var lst = Fees_Process.Content.ReadAsAsync<List<Im_CheckRequest_ShowDataDTO>>().Result;
                // var lst = Fees_Process.Content.ReadAsAsync<List<CustomOption>>().Result;
                //  ViewBag.ddd = lst;
                //return Json(new { Result = "OK", Options = Fees_Process });
                return View(lst);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetOutlet_ID");
                // return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
            return View();
        }
    }
}