using PlantQuar.BLL.BLL.SystemCodes;
using PlantQuar.DTO.DTO.SystemCodes;
using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PlantQuar.API.Controllers.SystemCode
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class A_SystemCode_APIController : ApiController
    {
        A_SystemCodeBLL<A_SystemCodeDTO> cBLL = new A_SystemCodeBLL<A_SystemCodeDTO>();

        public HttpResponseMessage GetA_SystemCode_List(int List)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.FillDrop_Add(API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage GetA_SystemCode_ByCode(int Syscode)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.FillDropByCode_Add(Syscode, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        
        public HttpResponseMessage GetA_SystemCode_Plant()
        {
            try
            {
                Dictionary<string, object> dic = cBLL.FillDrop_Plant(API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage GetA_SystemCode_Insects()
        {
            try
            {
                Dictionary<string, object> dic = cBLL.FillDrop_Insects(API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // Mahmoud Saber ...
        public HttpResponseMessage GetA_SystemCode_AddEdit(int AddEdit, int Sysnum)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.FillDrop_Edit(Sysnum, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // Mahmoud Saber ...
        public HttpResponseMessage GetCompany_National_ByActivity(int activityId)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.FillDrop_ByActivity(activityId, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
