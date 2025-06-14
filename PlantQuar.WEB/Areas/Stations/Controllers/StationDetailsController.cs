using PlantQuar.DTO.DTO.Station;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using PlantQuar.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using static PlantQuar.DTO.HelperClasses.Enums;

namespace PlantQuar.Web.Areas.Stations.Controllers
{
    public class StationDetailsController : BaseController
    {
        // GET: Station/StationDetails
        public ActionResult Index(long StationAccrediationRequestId)
        {
            var res = APIHandeling.getData("StationDetails?StationAccrediationRequestId=" + StationAccrediationRequestId);
            var model = res.Content.ReadAsAsync<Station_Get_Data_ResultDTO>().Result;//object
            

            return View(model);
        }

        public ActionResult acceptRequest(Station_Get_Data_ResultDTO model)
        {
           
            //---add new row in export committee
            if (model.IsApproved == true)
            {
                Station_CommitteeDTO newStationCommittee = new Station_CommitteeDTO();
                newStationCommittee.StationAccrediationRequestId = model.requestId;
                newStationCommittee.CommitteeType_ID = (byte)CommitteeType.StationAccrediation_Committee;
                newStationCommittee.IsApproved = null;
                newStationCommittee.Status = null;
                newStationCommittee.User_Creation_Date = DateTime.Now;
                
                newStationCommittee.User_Creation_Id = (short)Session["UserId"];

                var res = APIHandeling.Post("StationCommittee?newCreate=1", newStationCommittee);
                //eman
                //Update isApproved 
            }

            APIHandeling.Put("StationDetails?approve=1", model);

            return RedirectToAction("Index", "StationAccrediationCommittee");
        }
    }
}