using PlantQuar.BLL.BLL.Farm.FarmRequest;
using PlantQuar.DTO.DTO.Farm.FarmRequest;
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
    public class FarmDetails_APIController : ApiController
    {
        FarmDetailsBLL cBLL = new FarmDetailsBLL();
        public HttpResponseMessage GetFarmDetails(long farmCountryRequestId)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetFarmData(farmCountryRequestId, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage Put_ApproveFarm(int approve, Farm_Get_Data_ResultDTO dto)
        {
            //Create
            try
            {
                Dictionary<string, object> dic = cBLL.ApproveFarm(dto, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage postCommittee(int newCreate,Farm_CommitteeDTO Dto)
        {
            //EDIT
            try
            {
                Dictionary<string, object> dic = cBLL.Insert(Dto, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
