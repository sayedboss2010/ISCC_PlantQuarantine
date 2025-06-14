using PlantQuar.BLL.BLL.Admin;
using PlantQuar.BLL.BLL.Export_CheckRequest;
using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace PlantQuar.API.Controllers.Admin
{
    public class AcceptAggreementBack_APIController : ApiController
    {
        AcceptAggreementBackBLL cBLLList = new AcceptAggreementBackBLL();
        public HttpResponseMessage GetRequestAggreement(string RequestNumber, int Request_type,short User_Id)
        {
            try
            {
                Dictionary<string, object> dic =
                    cBLLList.GetRequestAggreement(RequestNumber, Request_type, User_Id, API_HelperFunctions.Get_DeviceInfo());
              return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);

       
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage GetEmployeeGeshniChange(string requestNumber ,int requesttype)
        {
            try
            {
                Dictionary<string, object> dic =
                    cBLLList.GetEmployeeGeshniChange(requestNumber, requesttype, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }

}