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
    public class StationDetails_APIController : ApiController
    {
        // GET: StationDetails
        StationDetails_BLL cBLL = new StationDetails_BLL();
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

        public HttpResponseMessage Post_UpdateStation_Details(Station_Get_Data_ResultDTO Dto)
        {
            //Create
            try
            {
                Dictionary<string, object> dic = cBLL.Update_Station_Details(Dto, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage GetStationActivityType_List1(int oprationTypeID)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.FillDrop_List1(API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        public HttpResponseMessage Insert_Station_Accreditation_Request(long requestId, bool ISActive, List<Station_Request_Fees_DTO> Selection_Fees_List)
        {
            //add committee results
            try
            {
                Dictionary<string, object> dic = cBLL.Insert_Station_Accreditation_Request(requestId, ISActive, Selection_Fees_List, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }



        public HttpResponseMessage GetUpdate_StartStop_Request(long Reqest_id, bool IsAccepted)
        {
            //add committee results
            try
            {
                Dictionary<string, object> dic = cBLL.Update_Start_Stop_Request(Reqest_id,  IsAccepted, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        public HttpResponseMessage GetStation_Print(long stationId, int Print)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetStationData_Print(stationId, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }





    }
}

