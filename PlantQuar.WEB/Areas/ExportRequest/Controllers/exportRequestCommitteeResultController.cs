using PlantQuar.DTO.DTO.ExportRequest;
using PlantQuar.WEB.App_Start;
using PlantQuar.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.ExportRequest.Controllers
{
    public class exportRequestCommitteeResultController : BaseController
    {
        // GET: ExportRequest/exportRequestCommitteeResult
        public ActionResult Index(long requestId)
        {
            try
            {
                var res = APIHandeling.getData("exportRequestCommitteeResult?requestId=" + requestId);

                var lst = res.Content.ReadAsAsync<Dictionary<string, List<CheckRequest_ComiteeResult_ResultDTO>>>().Result;//object

                //var StatusCode = lst.ElementAt(0).Value;
                //var obj = lst.ElementAt(1).Value;

                //JavaScriptSerializer ser = new JavaScriptSerializer();
                //var myObj = ser.Deserialize<Dictionary<string, List<CheckRequest_ComiteeResult_ResultDTO>>>(obj.ToString());

                var checkRequestDataResult = lst.ElementAt(0).Value;
                var SampleData = lst.ElementAt(1).Value;
                var TreatmentData = lst.ElementAt(2).Value;
                requestCommitteeResultDTO xx = new requestCommitteeResultDTO();
                xx.check = checkRequestDataResult;
                xx.withdrowSample = SampleData;
                xx.Treatment = TreatmentData;

                return View(xx);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Index");
                return null;
            }
        }
        public ActionResult Ex_committeeResult()
        {
            return View();
        }
        public JsonResult saveAdminResult(bool result, string noteAr, long committeeResultId)
        {
            AdminResultDTO dto = new AdminResultDTO();

            dto.result = result;
            dto.noteAr = noteAr;
            dto.committeeResultId = committeeResultId;

            var res = APIHandeling.Post("exportRequestCommitteeResult?", dto);
            var data = res.Content.ReadAsAsync<string>().Result;
            return Json(data);


        }
    }
}