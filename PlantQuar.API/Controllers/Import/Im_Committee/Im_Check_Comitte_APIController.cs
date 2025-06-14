using PlantQuar.BLL.BLL.Import.Committee;
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

namespace PlantQuar.API.Controllers.Import.Im_Committee
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class Im_Check_Comitte_APIController : ApiController
    {
        Im_Check_ComitteBLL cBLL = new Im_Check_ComitteBLL();
        // GET: Im_Check_Comitte_API
        public HttpResponseMessage Get_CheckRequestDetails
         (long ImCheckRequest_Number)
        {
            try
            {
                Dictionary<string, object> dic =
                    cBLL.Im_Check_ComitteResult(ImCheckRequest_Number, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}