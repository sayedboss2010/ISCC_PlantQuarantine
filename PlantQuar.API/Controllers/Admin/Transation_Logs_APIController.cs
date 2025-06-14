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
    public class Transation_Logs_APIController : ApiController
    {
        Transation_LogsBLL cBLL = new Transation_LogsBLL();

        public HttpResponseMessage Get_Employee_Data_List(int Operation_Type_ID, decimal Order_Permission_Number)
        {
            try
            {
                List<string> device_data = API_HelperFunctions.Get_DeviceInfo();
                Dictionary<string, object> dic = cBLL.Get_Employee_Data_List(Operation_Type_ID, Order_Permission_Number, device_data);
                //for android group
                if (!bool.Parse(device_data[1]))
                {
                    //android
                    return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic);
                }
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
