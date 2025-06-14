using PlantQuar.BLL.BLL.ExportRequestNew;
using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PlantQuar.API.Controllers.ExportRequestNew
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class List_ExportRequests_APIController : ApiController
    {
        List_ExportRequestsBLL cBLL = new List_ExportRequestsBLL();
        public HttpResponseMessage Get_ExportRequestFor_Admin(short CommitteeType_ID, short IsApproved = -1, string requestnumber = "")
        {
            try
            {
                List<string> device_data = API_HelperFunctions.Get_DeviceInfo();
                Dictionary<string, object> dic = cBLL.Get_ExportRequestFor_Admin(CommitteeType_ID, IsApproved, requestnumber, device_data);
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
