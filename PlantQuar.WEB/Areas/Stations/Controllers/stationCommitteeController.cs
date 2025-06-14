using PlantQuar.DTO.DTO.Station;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.Stations.Controllers
{
    public class stationCommitteeController : Controller
    {
        // GET: Stations/stationCommittee

        string apiName = "stationCommittee";
        public ActionResult Index(long stationCommitteeId)
        {

            //creation date
            var res = APIHandeling.getData(apiName + "?Id=" + stationCommitteeId);

            var comm = res.Content.ReadAsAsync<Station_Accreditation_CommitteeDTO>().Result;

            ViewBag.stationCode = comm.stationcode;
            ViewBag.stationCommitteeId = stationCommitteeId;
            ViewBag.stationAccrediationId = comm.Station_Accreditation_ID;
            ViewBag.creationDate = comm.User_Creation_Date;

            return View();
        }
        [HttpPost]
        public JsonResult SaveCommitte_Employee(List<EmployeeDTO> dataToSend, List<bool> isAdmins, long? Committe_Id, long? stationAccrediationId,
           DateTime? Delegation_Date, TimeSpan? StartTime, TimeSpan? EndTime)
        {
            var resolveRequest = HttpContext.Request;
            var i = 0;
            foreach (var x in dataToSend)
            {
                x.ISAdmin = isAdmins[i];
                i++;

            }
            Station_Accreditation_CommitteeDTO model = new Station_Accreditation_CommitteeDTO();
            model.ID = (long)Committe_Id;
            model.Station_Accreditation_ID = (long)stationAccrediationId;
            model.Delegation_Date = Delegation_Date;
            //model.CommitteeType_ID = 4;//ask
            model.StartTime = (TimeSpan)StartTime;
            model.EndTime = (TimeSpan)EndTime;
            model.IsApproved = false;
            model.Status = false;
            model.OperationType = 78;
            model.com_emp = dataToSend;
           
            model.User_Updation_Id = (short)Session["UserId"]; 
            model.User_Updation_Date = DateTime.Now;
            dynamic res;

            //create
            res = APIHandeling.Put(apiName, model);

            return ((int)res.StatusCode != 409) ? Json(new { Result = "OK", Record = model })
              : Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });
        }
    }
}