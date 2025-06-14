using PlantQuar.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlantQuar.Web.Areas.ImportLookups.Controllers
{
    public class ReceivingComitteController : BaseController
    {
        // GET: ImportRequest/ReceivingComitte
        public ActionResult Index()
        {
            return View();
        }
    }
}