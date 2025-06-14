using PlantQuar.BLL.BLL.ExportRequest;
using PlantQuar.DTO.DTO;
using PlantQuar.DTO.DTO.ExportRequest;
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

namespace PlantQuar.API.Controllers.ExportRequest
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class exportRequestCommitteeResultController : ApiController
    {
        // GET: exportRequestCommitteeResult
        exportRequestCommitteeResultBLL cBLL = new exportRequestCommitteeResultBLL();
        public HttpResponseMessage GetRequestCommitteeResult(long requestId)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetExportCommitteeResult(requestId, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage PostCreateAdmResult(AdminResultDTO dto)
        {
            //Create
            try
            {
                Dictionary<string, object> dic = cBLL.addAdminResult(dto, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}