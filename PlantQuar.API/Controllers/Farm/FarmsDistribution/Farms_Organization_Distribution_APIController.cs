using PlantQuar.BLL.BLL.Farm.FarmsDistribution;
using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PlantQuar.API.Controllers.Farm.FarmsDistribution
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class Farms_Organization_Distribution_APIController : ApiController
    {
        Farms_Organization_DistributionBLL cBLL = new Farms_Organization_DistributionBLL();
        public HttpResponseMessage GetFarmsDataList(int Farm)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.FillDrop_Add( API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage getFarmList(int Farm_ID)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetFarms(Farm_ID, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }

}
