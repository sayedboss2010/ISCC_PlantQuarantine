using PlantQuar.BLL.BLL.Export_Constrains;
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

namespace PlantQuar.API.Controllers.Export_Constrains
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ExportConstrainsNew_APIController : ApiController
    {
        // GET: ExportConstrainsNew_API
        ExportConstrainsNewBLL cBLL = new ExportConstrainsNewBLL();
        public HttpResponseMessage Get_CountryConstrainPlants(long ShortName_ID, long catId, int constrainType, int owner)
        {
            try
            {

                Dictionary<string, object> dic =
                    cBLL.GetCustomConstrain_Plant(ShortName_ID, catId, constrainType, owner, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}