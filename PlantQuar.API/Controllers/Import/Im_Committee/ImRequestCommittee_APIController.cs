 using PlantQuar.BLL.BLL.Import.Committee;
using PlantQuar.DTO.DTO.Import.CheckRequests;
using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PlantQuar.API.Controllers.Import.Im_Committee
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ImRequestCommittee_APIController : ApiController
    {
        ImRequestCommitteeBLL cBLL = new ImRequestCommitteeBLL();
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
        //checkRequestCommitteeExist
        public HttpResponseMessage GetcheckRequestCommitteeExist(int reqComm,long? requestId)
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
        public HttpResponseMessage GetRequestCommitteeExist(int RequestComm, long? requestId)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetReqCommittee(requestId, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage GetDismissCommitteeExist(int DismissComm, long? requestCommId)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetDismissCommittee(requestCommId, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage Put_ReqCommittee(int req, Im_RequestCommitteeDTO Dto)
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

        public HttpResponseMessage Put_Employee(int req1, Im_RequestCommitteeDTO Dto)
        {
            //EDIT
            try
            {
                Dictionary<string, object> dic = cBLL.Insert_Employee(Dto, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage postdismissCommittee(int dismiss, Im_PermissionItem_Division_Custody_DismissCommitteeDTO Dto)
        {
            //EDIT
            try
            {
                Dictionary<string, object> dic = cBLL.InsertDismissCommittee(Dto, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage postdismissCommittee(int dismiss, Im_RequestCommitteeDTO Dto)
        {
            //EDIT
            try
            {
                Dictionary<string, object> dic = cBLL.InsertDismissCommittee(Dto, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage postReceiveCommittee(int receive, Im_PermissionItem_Division_Custody_ReceiveCommitteeDTO Dto)
        {
            //EDIT
            try
            {
                Dictionary<string, object> dic = cBLL.InserReceiveCommittee(Dto, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage postReceiveCommittee(int receive, Im_RequestCommitteeDTO Dto)
        {
            //EDIT
            try
            {
                Dictionary<string, object> dic = cBLL.InserReceiveCommittee(Dto, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage posExecCommittee(int Exec, Im_ExecutionDTO Dto)
        {
            //EDIT
            try
            {
                Dictionary<string, object> dic = cBLL.InserExecutionCommittee(Dto, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage posExecCommittee(int Exec, Im_RequestCommitteeDTO Dto)
        {
            //EDIT
            try
            {
                Dictionary<string, object> dic = cBLL.InserExecutionCommittee(Dto, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage SaveCommittee(string CheckRequest_Number, byte CommitteeType_ID, bool WithEmployee = false)
        {
            try
            {
                List<string> Device_Info = API_HelperFunctions.Get_DeviceInfo();
                Dictionary<string, object> dic = cBLL.SaveCommittee(CheckRequest_Number, CommitteeType_ID, WithEmployee, Device_Info);
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage Save_Lot(int lotssss, List<Im_CommitteeResultDTO> CheckedItemsList )
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
        public HttpResponseMessage Save_AnalysisList(int AnalysisList , List<Im_CheckRequest_SampleDataDTO> CheckedAnalysisList)
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
