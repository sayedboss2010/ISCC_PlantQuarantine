using PlantQuar.BLL.BLL.Export_CheckRequest;
using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PlantQuar.API.Controllers.Export_CheckRequest
{
    public class EX_CheckRequestCertificate_APIController : ApiController
    {
        EX_CheckRequestCertificatesBLL cBLL = new EX_CheckRequestCertificatesBLL();

        public HttpResponseMessage Get_CheckRequestDetails
          (string ExCheckRequest_Number)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetExCheckRequestDetails(ExCheckRequest_Number, API_HelperFunctions.Get_DeviceInfo());
              //  var objResponse1 =
    //JsonConvert.DeserializeObject<List<object>>(CheckRequestDetails.ToString());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
