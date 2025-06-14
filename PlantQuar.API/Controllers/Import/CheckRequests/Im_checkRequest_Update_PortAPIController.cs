using PlantQuar.BLL.BLL.Import.checkRequests;
using PlantQuar.DTO.DTO.Import.CheckRequests;
using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PlantQuar.API.Controllers.Import.CheckRequests
{
    public class Im_checkRequest_Update_PortAPIController : ApiController
    {
        Im_checkRequest_Update_PortBLL cBLL = new Im_checkRequest_Update_PortBLL();

        public HttpResponseMessage GetFarmCommitteeType(string CheckNumber)
        {
            try
            {
                try
                {
                    Dictionary<string, object> dic = cBLL.GetAll(API_HelperFunctions.Get_DeviceInfo(), CheckNumber);
                    return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage GetPortID(int government_ID)
        {
            try
            {
                List<string> device_data = API_HelperFunctions.Get_DeviceInfo();

                Dictionary<string, object> dic = cBLL.FillDrop_Port(government_ID, device_data);
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


        public HttpResponseMessage GetInternationalPortID(int country_ID)
        {
            try
            {
                List<string> device_data = API_HelperFunctions.Get_DeviceInfo();

                Dictionary<string, object> dic = cBLL.FillDrop_InterbationalPort(country_ID, device_data);
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

        public HttpResponseMessage GetCountry_ID(int id)
        {
            try
            {
                List<string> device_data = API_HelperFunctions.Get_DeviceInfo();

                Dictionary<string, object> dic = cBLL.FillDrop_Country_ID(device_data);
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

        public HttpResponseMessage GetGovernment_ID(int gov)
        {
            try
            {
                List<string> device_data = API_HelperFunctions.Get_DeviceInfo();

                Dictionary<string, object> dic = cBLL.FillDrop_Government_ID(device_data);
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

        public HttpResponseMessage Putupdate_port(short UserId, List<Im_CheckRequest_PortDTO> model)
        {
            try
            {
                List<string> device_data = API_HelperFunctions.Get_DeviceInfo();

                Dictionary<string, object> dic = cBLL.update_Ports(UserId,model, device_data);
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


    }
}
