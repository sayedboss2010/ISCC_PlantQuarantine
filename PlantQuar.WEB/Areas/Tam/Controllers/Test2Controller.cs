using PlantQuar.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.Tam.Controllers
{
    public class Test2Controller : Controller
    {
        // GET: Tam/Test2
       
        public ActionResult Index()
        {
            
            return View();
        }
        public ActionResult BarChart()
        {
            PlantQuarantineEntities entities = new PlantQuarantineEntities();
            //CSharpCornerEntities1 entities = new CSharpCornerEntities1();
            return Json(entities.Im_CheckRequest.ToList(), JsonRequestBehavior.AllowGet);
        }
    }

   
}