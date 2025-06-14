using PlantQuar.DTO.DTO.Farm.FarmRequest;
using PlantQuar.DTO.DTO.labResult;
using PlantQuar.WEB.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.LaboratoryResult.Controllers
{
    public class LaboratoryResult_ReportController : Controller
    {
        // GET: LaboratoryResult/LaboratoryResult_Report
        public ActionResult Index(string barcode)
        {

            var Lst = APIHandeling.getData("LaboratoryResult_Report_API?barcode=" + barcode);
            var model = Lst.Content.ReadAsAsync<LaboratoryResult_ReportDTO>().Result;//object

            return View(model);
        }
        public ActionResult IndexNew(string barcode)
        {
            var Lst = APIHandeling.getData("LaboratoryResult_Report_API?new_p=1&&barcode=" + barcode);
            var model = Lst.Content.ReadAsAsync<List<LaboratoryResult_ReportDTO>>().Result;//object

            return View(model);
        }
        [HttpPost]
        public JsonResult savePrintBarcode(long? farmSampleId)
        {
            Farm_SampleDataDTO fsDto = new Farm_SampleDataDTO();
            fsDto.ID = (long)farmSampleId;
            APIHandeling.Post("LaboratoryResult_Report_API", fsDto);
            return Json("succ", JsonRequestBehavior.AllowGet);
        }
    }
}