using PlantQuar.DTO.DTO.Admin;
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
    public class Total_Fees_TypeController : BaseController
    {
        // GET: Admin/Total_Fees_Type/Index
        public ActionResult Index()
        {
            var res = APIHandeling.getData("Total_Fees_Type_API");
            var lst = res.Content.ReadAsAsync<List<Total_Fees_Type_DTO>>().Result;
            return View(lst);
        }
     
    }
}