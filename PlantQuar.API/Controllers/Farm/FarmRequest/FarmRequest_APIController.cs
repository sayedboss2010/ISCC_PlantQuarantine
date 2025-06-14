using PlantQuar.BLL.BLL.Farm.FarmRequest;
using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PlantQuar.API.Controllers.Farm.FarmRequest
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class FarmRequest_APIController : ApiController
    {
        FarmRequestBLL cBLL = new FarmRequestBLL();
        public HttpResponseMessage GetFarmsCommittee(int all)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetAll("", -1, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage getFarmCommitteeData(string farmcode, int? Status)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetAll(farmcode, Status, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage getFarmCommitteeDataByFarmId(string farmcode, int? Status, long? farmId)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetAllByFarmId(farmcode, Status, farmId, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage getFarmList(int List)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetFarms(API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage getFarmList(bool? Is_Status)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetFarms(Is_Status, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage getFarmListByItem(long? itemId)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetFarmsByItem(itemId, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        //any change
        public HttpResponseMessage getFarmList(int Is_Status, long? itemId, int? govId, int? centerId, int? villageId, string Date_From, string Date_End)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetFarms(Is_Status, itemId, govId, centerId, villageId, Date_From, Date_End, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
