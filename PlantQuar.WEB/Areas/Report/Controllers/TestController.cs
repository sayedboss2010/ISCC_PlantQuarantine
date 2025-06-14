using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.Report.Controllers
{
    public class TestController : Controller
    {
        // GET: Report/Test
        public ActionResult Index()
        {
            @ViewBag.DateTo = DateTime.Now;
            @ViewBag.DateFrom = DateTime.Now.AddDays(-7);
            return View();
        }
    }
}