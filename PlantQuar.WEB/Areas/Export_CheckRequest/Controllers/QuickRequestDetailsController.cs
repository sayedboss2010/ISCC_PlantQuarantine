using PlantQuar.DTO.DTO.ExportRequest;
using PlantQuar.WEB.App_Start;
using PlantQuar.WEB.Controllers;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.Export_CheckRequest.Controllers
{
    public class QuickRequestDetailsController : BaseController
    {
        // GET: Export_CheckRequest/QuickRequestDetails
        public ActionResult Index(string Ex_CheckRequest_Number)
        {
            var res = APIHandeling.getData("QuickRequestDetails_API?Ex_CheckRequest_Number=" + Ex_CheckRequest_Number);
            var Lst = res.Content.ReadAsAsync<List<QuickRequestDetailsDTO>>().Result;
            return View(Lst);
        }


    }
}