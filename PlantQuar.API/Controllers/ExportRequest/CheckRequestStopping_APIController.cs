using PlantQuar.BLL.BLL.ExportRequest;
using PlantQuar.DTO.DTO.ExportRequest;
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

namespace PlantQuar.API.Controllers.ExportRequest
{ 
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CheckRequestStopping_APIController : ApiController
    {
        // GET: CheckRequestStopping_API
        CheckRequestStoppingBLL cBLL = new CheckRequestStoppingBLL();

        public HttpResponseMessage GetRefuseReasons(int List, int refuse)
            {
                try
                {
                    //if rfuse = 1 ......refuse request .....if refuse = 2  ..stopprequest
                    Dictionary<string, object> dic = cBLL.FillDrop_RefuseReason(refuse, API_HelperFunctions.Get_DeviceInfo());
                    return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
                }
            }

            public HttpResponseMessage PostStoppingReasons(CheckRequestStoppingDTO Dto, int liststop)
            {
                //Create
                try
                {

                    Dictionary<string, object> dic = cBLL.InsertStoppingReasons(Dto, API_HelperFunctions.Get_DeviceInfo());
                    return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
                }
            }
       
    }
}