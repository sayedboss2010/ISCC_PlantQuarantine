using PlantQuar.BLL.BLL.Committee;
using PlantQuar.DTO.DTO.Station_Pages;
using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PlantQuar.API.Controllers.Committee
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class Committee_Station_APIController : ApiController
    {
        Committee_Station_BLL cBLL = new Committee_Station_BLL();               

        public HttpResponseMessage Put_Insert_Committee(int Insert_req, Committee_Station_DTO Dto)
        {            
            try
            {
                Dictionary<string, object> dic = cBLL.Insert_Committee(Dto, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
