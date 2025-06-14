using PlantQuar.BLL.BLL.Station;
using PlantQuar.DTO.DTO.Farm.FarmRequest;
using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PlantQuar.API.Controllers.Station
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class Committee_APIController : ApiController
    {
        CommitteeBLL cBLL = new CommitteeBLL();
        public HttpResponseMessage GetObjectById(long Id)
        {
            Dictionary<string, object> dic = cBLL.FindStation(Id, API_HelperFunctions.Get_DeviceInfo());
            return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
        }

        public HttpResponseMessage GetCommitteeType_List(int Lst)
        {
            try
            {
                List<string> device_data = API_HelperFunctions.Get_DeviceInfo();
                Dictionary<string, object> dic = cBLL.FillCommitteeType_List(Lst, device_data);
                //for android group
                if (!bool.Parse(device_data[1]))
                {
                    //android
                    return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic);
                }

                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage GetReqNumberById(int num, long requestId)
        {
            try
            {
                List<string> device_data = API_HelperFunctions.Get_DeviceInfo();
                Dictionary<string, object> dic = cBLL.FindReqNumber(requestId, device_data);

                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage GetReqCreationDateId(int num, int create, long requestId)
        {
            try
            {
                List<string> device_data = API_HelperFunctions.Get_DeviceInfo();
                Dictionary<string, object> dic = cBLL.FindCreationDate(requestId, device_data);

                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage PostCreateCommittee(Ex_RequestCommitteeDTO Dto)
        {
            //Create
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
