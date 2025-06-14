using PagedList;
using PlantQuar.DTO.DTO.Log;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using PlantQuar.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.Admin.Controllers
{
    public class LoggingController : BaseController
    {
        // GET: Admin/Logging
        string apiName = "Login_out";
        public ActionResult Index(int? page, DateTime? Login_Date = null)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            int pageSize = 15;
            int pageNumber = (page ?? 1);
            Login_Date = (Login_Date != null) ? Login_Date : DateTime.Now;

            dic.Add("pageSize", pageSize);
            dic.Add("pageNumber", pageNumber);
            dic.Add("Login_Date", Login_Date);

            var res = APIHandeling.Post(apiName + "?logout=1", dic);
            var modelLst = res.Content.ReadAsAsync<List<User_LoginDTO>>().Result;
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