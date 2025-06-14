
using PlantQuar.DTO.DTO.Import.IM_Committee;
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
    public class FeesTotalResultController : BaseController
    {
        // GET: Committees/FeesTotalResult
        public ActionResult Index(string ImCheckRequest_Number)
        {
            var res = APIHandeling.getData("ImCommittee_Final_Result_API?ImCheckRequest_Number=" + ImCheckRequest_Number);
            //var Lst = res.Content.ReadAsAsync<Fees_Item>().Result;

            var Lst = res.Content.ReadAsAsync<Fees_ALL>().Result;

            return View(Lst);
        }




       
    }
}