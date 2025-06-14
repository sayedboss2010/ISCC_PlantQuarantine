
using Microsoft.Ajax.Utilities;
using PlantQuar.DTO.DTO.Committee;

using PlantQuar.WEB.App_Start;
using PlantQuar.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.Committees.Controllers
{
    
    public class Ex_FeesTotalResultController : BaseController
    {
        // GET: Committees/Ex_FeesTotalResult
        public ActionResult Index(string Ex_CheckRequest_Number)
        {
            var res = APIHandeling.getData("Ex_Committee_API?Ex_CheckRequest_Number=" + Ex_CheckRequest_Number);
            //var Lst = res.Content.ReadAsAsync<Fees_Item>().Result;

            var Lst = res.Content.ReadAsAsync<Fees_ALL>().Result;

            return View(Lst);
        }

        public ActionResult IndexNew(long Ex_CheckRequest_ID)
        {
            var res = APIHandeling.getData("Ex_Committee_API?CheckRequest_ID_Ex=" + Ex_CheckRequest_ID + "&FeesEx=1");
            //var Lst = res.Content.ReadAsAsync<Fees_Item>().Result;
          
            var Lst = res.Content.ReadAsAsync<List<FeesExDTO>>().Result;

            return View(Lst);
        }

        [HttpPost]
        public JsonResult Save_FeesEX(List<FeesExDTO> Fees_List)
        {
            return Json( JsonRequestBehavior.AllowGet);
           
        }
    }
}