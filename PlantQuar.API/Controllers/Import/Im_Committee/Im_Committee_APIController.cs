using PlantQuar.BLL.BLL.Import.Committee;
using PlantQuar.DTO.DTO.Import.CheckRequests;
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

namespace PlantQuar.API.Controllers.Import.Im_Committee
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class Im_Committee_APIController : ApiController
    {
        Im_CommitteeBLL cBLL = new Im_CommitteeBLL();

        public HttpResponseMessage PostCreateCommittee(Im_RequestCommitteeDTO Dto)
        {
            //Create
            try
            {
                Dictionary<string, object> dic = cBLL.Insert(Dto, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage GetRequestCommitteeType(int type, long requestId)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.getCommitteeTypeForrequest(requestId,API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage GetRequestAnalysisType(int analtype, long requestId)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.getCommitteeAnalysisTypeForrequest(requestId, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage GetRequestAnalysisLabType(int analTypeId)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.getAnalysisLabType(analTypeId, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

   

    }
}