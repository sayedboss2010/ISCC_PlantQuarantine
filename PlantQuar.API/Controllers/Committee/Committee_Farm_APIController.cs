using PlantQuar.BLL.BLL.Committee;
using PlantQuar.DTO.DTO.Farm.FarmCommittee;
using PlantQuar.DTO.DTO.Farm.FarmRequest;
using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PlantQuar.API.Controllers.Committee
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class Committee_Farm_APIController : ApiController
    {
        Committee_Farm_BLL cBLL = new Committee_Farm_BLL();


        public HttpResponseMessage GetRequestCommitteeType(int type, long Farm_Requst_List)//, List<Farm_Requst_ListDTO> Farm_Requst_List)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.getCommitteeTypeForrequest(Farm_Requst_List, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage Put_RequestAnalysisType(int analtype, List<Farm_Requst_ListDTO> requestId)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.getCommitteeAnalysisTypeForrequest(1, requestId, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage Put_ReqCommittee(int req, List<Farm_Requst_ListDTO> Dto)
        {
            //EDIT
            try
            {
                Dictionary<string, object> dic = cBLL.Get_Data_Committee(Dto, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
                //Dictionary<string, object> dic = cBLL.Insert_Committee(Dto, API_HelperFunctions.Get_DeviceInfo());
                //return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
                //return null;
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage Put_ReqCommittee(int Insert_req, Farm_Committee_Requst_All_DTO Dto)
        {
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
    }
}
