using PlantQuar.BLL.BLL.Station_Pages;
using PlantQuar.BLL.BLL.Stations;
using PlantQuar.DTO.DTO;
using PlantQuar.DTO.DTO.Station;
using PlantQuar.DTO.DTO.Station_Pages;
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

namespace PlantQuar.API.Controllers.Station_Pages
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class Station_Final_Result_APIController : ApiController
    {
        // GET: Station_Final_Result_API
        Station_Final_Result_BLL cBLL = new Station_Final_Result_BLL();
        public HttpResponseMessage GetStationDetails(long stationId)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetStationData(stationId, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage GetStationDetailsnew(long stationIdNew)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetStationDataNew(stationIdNew, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        //public HttpResponseMessage Get_StationFees_List(long Station_ID)
        //{
        //    try
        //    {
        //        Dictionary<string, object> dic = cBLL.GetStationFees(Station_ID, API_HelperFunctions.Get_DeviceInfo());
        //        return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
        //    }
        //}
        public HttpResponseMessage Put_Insert_Station_Requeste_Quarantine(int Insert_req, Station_Accreditation_Request_DTO req )
        {
            try
            {
                Dictionary<string, object> dic = cBLL.Insert_Station_Requeste_Quarantine(req, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}

