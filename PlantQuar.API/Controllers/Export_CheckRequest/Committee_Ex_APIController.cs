using PlantQuar.BLL.BLL.Export_CheckRequest;
using PlantQuar.DTO.DTO.Export_CheckRequest;
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
    public class Committee_Ex_APIController : ApiController
    {
        // GET: Committee_Ex_API
        Committee_Ex_BLL cBLL = new Committee_Ex_BLL();
        public HttpResponseMessage GetRequestCommitteeType(long? requestId)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetCreationDateForRequest(requestId, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        public HttpResponseMessage GetcheckRequestCommitteeExist(int reqComm, long? requestId)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.checkRequestCommitteeExist(requestId, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        public HttpResponseMessage Get_CheckRequestDetails (string ImCheckRequest_Number)
        {
            try
            {
                Dictionary<string, object> dic =
                    cBLL.GetImCheckRequestDetails(ImCheckRequest_Number, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage GetShiftMony(byte shiftId)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetTimingMony(shiftId, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage GetShiftTiming_List(int List)
        {
            try
            {
                List<string> device_data = API_HelperFunctions.Get_DeviceInfo();
                Dictionary<string, object> dic = cBLL.FillDrop_ShiftTiming(API_HelperFunctions.Get_DeviceInfo());
                //for android group
                if (!bool.Parse(device_data[1]))
                {
                    //android
                    return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic);
                }
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage Put_ReqCommittee(int req, Ex_RequestCommitteeDTO Dto)
        {
            //EDIT
            try
            {
                Dictionary<string, object> dic = cBLL.Insert_Committee(Dto, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage Save_Lot(int lotssss, List<Ex_Committee_ResultDTO> CheckedItemsList)
        {
            //add committee results
            try
            {
                Dictionary<string, object> dic = cBLL.Save_Lot(lotssss, CheckedItemsList, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage Save_AnalysisList(int AnalysisList, List<Ex_SampleDataDTO> CheckedAnalysisList)
        {
            //add committee results
            try
            {
                Dictionary<string, object> dic = cBLL.Save_AnalysisList(AnalysisList, CheckedAnalysisList, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
      
    }
}