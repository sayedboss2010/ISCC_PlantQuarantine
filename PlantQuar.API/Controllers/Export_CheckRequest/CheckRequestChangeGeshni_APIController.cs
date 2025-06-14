using PlantQuar.BLL.BLL.Export_CheckRequest;
using PlantQuar.DTO.DTO.Export_CheckRequest;
using PlantQuar.DTO.DTO.Import.IM_Committee;
using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PlantQuar.API.Controllers.Export_CheckRequest
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CheckRequestChangeGeshni_APIController : ApiController
    {
        // GET: CheckRequestChangeGeshni_API
        List_CheckRequestGeshni_BLL cBLLList = new List_CheckRequestGeshni_BLL();
      
        public HttpResponseMessage GetGeshniCommitteeList(string CheckRequestNumber)
        {
            try
            {
                Dictionary<string, object> dic =
                    cBLLList.GetGeshniCommitteeList(CheckRequestNumber, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage GetEmployeeGeshniChange(string requestNumber)
        {
            try
            {
                Dictionary<string, object> dic =
                    cBLLList.GetEmployeeGeshniChange(requestNumber, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage GetGeshniPortsList(int port)
        {
            try
            {
                Dictionary<string, object> dic =
                    cBLLList.GetGeshniPortsList( port, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage GetGeshniStationList(int station)
        {
            try
            {
                Dictionary<string, object> dic =
                    cBLLList.GetGeshniStationList(station, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    
            public HttpResponseMessage Post_UpdateGeshniPort(GeshniPortsDTO geshniPortsLst)
        {
            try
            {
                Dictionary<string, object> dic = cBLLList.PutGeshniPort(geshniPortsLst, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

      public HttpResponseMessage Post_UpdateGeshniStation(int req1, GeshniStationDTO geshniStationLst)
        {
            try
            {
                //Dictionary<string, object> dic =
                //    cBLLList.PutGeshniPort(geshniPortsDTO, API_HelperFunctions.Get_DeviceInfo());
                //return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
                Dictionary<string, object> dic = cBLLList.PutGeshniStation(geshniStationLst, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}