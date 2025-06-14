using PlantQuar.BLL.BLL.Import.checkRequests;
using PlantQuar.DTO.DTO.Import.CheckRequests;
using PlantQuar.DTO.DTO.Import.Permissions;
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

namespace PlantQuar.API.Controllers.Import.CheckRequests
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class Im_CheckRequests_New_APIController : ApiController
    {
        Im_CheckRequestDetails_NewBLL cBLL = new Im_CheckRequestDetails_NewBLL();
    
        public HttpResponseMessage Get_CheckRequestDetails
          (string ImCheckRequest_Number)
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
      
    }
}