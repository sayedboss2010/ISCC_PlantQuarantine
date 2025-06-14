using PlantQuar.BLL.BLL.Committee;
using PlantQuar.DTO.DTO.Committee;
using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static PlantQuar.WEB.Areas.Export_CheckRequest.Controllers.List_EXCheckRequestController;


namespace PlantQuar.API.Controllers.Committee
{
    public class Ex_Committee_Quick_APIController : ApiController
    {
        Ex_Committee_Quick_BLL cBLL = new Ex_Committee_Quick_BLL();
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


        public HttpResponseMessage Put_CheckRequestDetails(int reqQuk,List<Ex_Committee_Quick_DTO> EX_CheckRequest_List)
        {
            try
            {
                Dictionary<string, object> dic =
                    cBLL.GetEX_CheckRequestDetails(EX_CheckRequest_List, API_HelperFunctions.Get_DeviceInfo());
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

        public HttpResponseMessage Put_ReqCommittee(int req, EX_RequestCommittee_QuickDTO Dto)
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
        public HttpResponseMessage Save_Lot(int lotssss, List<EX_CommitteeResultDTO> CheckedItemsList)
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

        public HttpResponseMessage Save_AnalysisList(int AnalysisList, List<EX_CheckRequest_SampleDataDTO> CheckedAnalysisList)
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

        public HttpResponseMessage GetEX_AllTreatmentDataForShortnameId(long EX_CheckRequest_ID, long ExportCountry_Id, long EX_shortnameId)
        {

            try
            {

                Dictionary<string, object> dic = cBLL.GetEX_AllTreatmentDataForShortnameId(EX_CheckRequest_ID, ExportCountry_Id, EX_shortnameId, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


    }
}
