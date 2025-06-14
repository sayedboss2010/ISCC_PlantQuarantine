
using PlantQuar.BLL.BLL.Stations;
using PlantQuar.DTO.DTO;
using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;

namespace PlantQuar.API.Controllers.Stations
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class StationAccrediationCommitteeController : ApiController
    {
        // GET: StationAccrediationCommittee
        StationAccrediationCommitteeBLL cBLL = new StationAccrediationCommitteeBLL();
        public HttpResponseMessage getStationAccrediationCommitteeData(string stationCode, int? Status, short? stationActivityType)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetAll(stationCode, Status, stationActivityType, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }
}