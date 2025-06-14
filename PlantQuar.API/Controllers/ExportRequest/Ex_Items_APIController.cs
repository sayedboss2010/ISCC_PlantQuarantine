using PlantQuar.BLL.BLL.ExportRequest;
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
    public class Ex_Items_APIController : ApiController
    {

        // GET: Ex_Items
        Ex_ItemsBLL cBLL = new Ex_ItemsBLL();
        public HttpResponseMessage Get_Plants(int plant,long requestId)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetPlants(requestId,4,API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage Get_Products(int plant,int product, long requestId)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetPlants(requestId, 5, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage Get_alive(int alive, int product, int plant, long requestId)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetPlants(requestId, 16, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage Get_Notalive(int notalive,int alive, int product, int plant, long requestId)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetPlants(requestId, 33, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}