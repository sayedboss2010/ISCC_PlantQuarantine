using PlantQuar.BLL.BLL.Admin;
using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PlantQuar.API.Controllers.Admin
{       
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class Total_Fees_Type_APIController : ApiController
    {
        Total_Fees_Type_BLL cBLL = new Total_Fees_Type_BLL();
        public HttpResponseMessage Get_Total_Fees_Type()
        {
            try
            {
                List<string> device_data = API_HelperFunctions.Get_DeviceInfo();
                Dictionary<string, object> dic = cBLL.Get_Total_Fees_Type( device_data);                
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
