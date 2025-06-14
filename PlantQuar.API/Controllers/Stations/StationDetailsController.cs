using PlantQuar.BLL.BLL.Stations;
using PlantQuar.DTO.DTO;
using PlantQuar.DTO.DTO.Station;
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
    public class StationDetailsController : ApiController
    {
        // GET: StationDetails
        StationDetailsBLL cBLL = new StationDetailsBLL();
        public HttpResponseMessage GetStationDetails(long StationAccrediationRequestId)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetStationData(StationAccrediationRequestId, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage Put_ApproveStation(int approve, Station_Get_Data_ResultDTO dto)
        {
            //Create
            try
            {
                Dictionary<string, object> dic = cBLL.ApproveStation(dto, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }
}