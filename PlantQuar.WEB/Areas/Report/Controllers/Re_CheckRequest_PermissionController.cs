using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PlantQuar.WEB.Controllers;

namespace PlantQuar.WEB.Areas.Report.Controllers
{
    public class Re_CheckRequest_PermissionController : BaseController
    {
        // GET: Report/Re_CheckRequest_Permission
        public ActionResult Index()
        {
            @ViewBag.DateTo = DateTime.Now;
            @ViewBag.DateFrom = DateTime.Now.AddDays(-7);
            return View();
        }
    }
}