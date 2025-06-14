using PlantQuar.BLL.BLL.Export_CheckRequest;
using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;


namespace PlantQuar.API.Controllers.Export_CheckRequest
{
    public class QuickRequestDetails_APIController : ApiController
    {
        QuickRequestDetails_BLL cBLL = new QuickRequestDetails_BLL();
        public HttpResponseMessage Get_QuickCheckRequestDetails
         (string Ex_CheckRequest_Number)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.QuickGetExCheckRequestDetails(Ex_CheckRequest_Number, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }
}