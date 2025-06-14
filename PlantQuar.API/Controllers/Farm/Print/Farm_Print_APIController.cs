using PlantQuar.BLL.BLL.Farm.Print;
using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PlantQuar.API.Controllers.Farm.Print
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class Farm_Print_APIController : ApiController
    {
        Farm_PrintBLL cBLL = new Farm_PrintBLL();
        public HttpResponseMessage GetFarmById( long Farm_ID,short User_Creation_Id ,long RequestId)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetFarm_Data_PrintDetails(Farm_ID, User_Creation_Id, RequestId, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
