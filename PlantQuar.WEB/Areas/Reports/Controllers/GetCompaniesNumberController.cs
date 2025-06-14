using PlantQuar.DTO.DTO.Company;
using PlantQuar.DTO.DTO.Farm.FarmRequest;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using PlantQuar.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace PlantQuar.WEB.Areas.Reports.Controllers
{
    public class GetCompaniesNumberController : BaseController
    {
        // GET: Reports/GetCompaniesNumber
        string apiName = "GetCompaniesNumbers_API";
        public ActionResult Index()
        {
            return View();
        }


        //GetCompaniesNumbers
        public JsonResult GetCompaniesNumbers(int rep = 1)
        {
            try
            {
                var res = APIHandeling.getData(apiName + "?rep=1");

                var lst = res.Content.ReadAsAsync<SP_GetCompaniesNumbers_DTO>().Result;//object



                return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetFeesTypeActionByFeesProcessId");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }

        }



    }
}