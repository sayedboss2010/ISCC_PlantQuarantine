using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PlantQuar.Web.Controllers;

namespace PlantQuar.Web.Areas.ImportLookups.Controllers
{
    public class LogDataController :BaseController
    {
        // GET: ImportLookups/LogData
        public ActionResult Index()
        {
            return View();
        }
    }
}