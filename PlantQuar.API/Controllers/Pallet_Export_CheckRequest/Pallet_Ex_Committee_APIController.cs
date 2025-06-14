using PlantQuar.BLL.BLL.Pallet_Export_CheckRequest;
using PlantQuar.DTO.DTO.Pallet_Export_CheckRequest;
using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PlantQuar.API.Controllers.Pallet_Export_CheckRequest
{
    public class Pallet_Ex_Committee_APIController : ApiController
    {
        Pallet_Ex_Committee_BLL cBLL = new Pallet_Ex_Committee_BLL();
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


        public HttpResponseMessage Get_CheckRequestDetails(long EX_CheckRequest_ID)
        {
            try
            {
                Dictionary<string, object> dic =
                    cBLL.GetEX_CheckRequestDetails(EX_CheckRequest_ID, API_HelperFunctions.Get_DeviceInfo());
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

        public HttpResponseMessage Put_ReqCommittee(int req, Pallet_EX_RequestCommitteeDTO Dto)
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
        //adefasdfasdf
        public HttpResponseMessage Save_Lot(int lotssss, List<Pallet_EX_CommitteeResultDTO> CheckedItemsList)
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

        public HttpResponseMessage Save_AnalysisList(int AnalysisList, List<Pallet_EX_CheckRequest_SampleDataDTO> CheckedAnalysisList)
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

        public HttpResponseMessage GetStationDetails(int reqCommitte, long requestId)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetountryConstrainStatus(requestId, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage GetAllTreatmentDataForShortnameId(long EX_CheckRequest_ID, long ExportCountry_Id, long shortnameId)
        {

            try
            {

                Dictionary<string, object> dic = cBLL.GetAllTreatmentDataForShortnameId(EX_CheckRequest_ID, ExportCountry_Id, shortnameId, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        //noura

        public HttpResponseMessage Get_Fees(string Ex_CheckRequest_Number)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.Get_Fees(Ex_CheckRequest_Number, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }
}