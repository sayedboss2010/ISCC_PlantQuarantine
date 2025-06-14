using PlantQuar.BLL.BLL.Company;
using PlantQuar.DTO.DTO.Farm.FarmRequest;
using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
namespace PlantQuar.API.Controllers.Company
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class GetCompaniesNumbers_APIController : ApiController
    {
        // GET: GetCompaniesNumbers_API
        GetCompaniesNumbersBLL cBLL = new GetCompaniesNumbersBLL();






        //reports Eslam
        public HttpResponseMessage GetCompaniesNumber(int rep)
        {
            try
            {
                int x = rep;

                Dictionary<string, object> dic = cBLL.GetCompaniesNumber(x, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }



    }
}
