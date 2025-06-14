using PlantQuar.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.Committees.Controllers
{
    public class RefuseResultReportController :BaseController
    {
        // GET: Committees/RefuseResultReport
        public ActionResult Index()
        {
            return View();
        }
    }
}