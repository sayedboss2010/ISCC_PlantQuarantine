using PagedList;
using PlantQuar.DTO.DTO.Admin;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using PlantQuar.WEB.Controllers;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.Admin.Controllers
{
    public class ErrorDisplayController : BaseController
    { 
        //eslam
        // GET: Admin/ErrorDisplay
        string apiName = "ErrorDisplay";
        public ActionResult Index(int? page)
        {
            int pageSize = 15;
            int pageNumber = (page ?? 1);
            var res = APIHandeling.getData(apiName + "?pageSize=" + pageSize + "&index=" + pageNumber);
            var modelLst = res.Content.ReadAsAsync<List<A__plant_Error_SaveDTO>>().Result;

            return View(modelLst.ToPagedList(pageNumber, pageSize));

        }

        [HttpPost]
        public JsonResult Delete(long RowId = 0)
        {
            DeleteParameters dt = new DeleteParameters();
            dt.id = RowId;
            var res = APIHandeling.Delete(apiName, dt);

            return Json(new { Result = "OK" });
        }
    }
}