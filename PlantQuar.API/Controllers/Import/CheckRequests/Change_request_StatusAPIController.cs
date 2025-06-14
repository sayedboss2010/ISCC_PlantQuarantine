using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PlantQuar.BLL.BLL.Import.checkRequests;
using PlantQuar.DTO.DTO.Import.CheckRequests;
using PlantQuar.DTO.HelperClasses;

namespace PlantQuar.API.Controllers.Import.CheckRequests
{
    public class Change_request_StatusAPIController : ApiController
    {
        

        Change_request_StatusBLL cBLL = new Change_request_StatusBLL();

        public HttpResponseMessage GetRequestStatus(string CheckNumber)
        {
            try
            {
                try
                {
                    Dictionary<string, object> dic = cBLL.GetAll(API_HelperFunctions.Get_DeviceInfo(), CheckNumber);
                    return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        public HttpResponseMessage Putupdaterequeststatus(Change_request_StatusDTO model)
        {
            try
            {
                try
                {
                    Dictionary<string, object> dic = cBLL.update(API_HelperFunctions.Get_DeviceInfo(), model);
                    return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
