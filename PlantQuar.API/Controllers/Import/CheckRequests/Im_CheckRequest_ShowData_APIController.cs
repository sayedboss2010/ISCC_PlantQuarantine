using PlantQuar.BLL.BLL.Import.checkRequests;
using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PlantQuar.API.Controllers.Import.CheckRequests
{
    public class Im_CheckRequest_ShowData_APIController : ApiController
    {
        Im_CheckRequest_ShowDataBLL cBLL = new Im_CheckRequest_ShowDataBLL();
        public HttpResponseMessage GetOutlet_ID_List()
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetAll( API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
