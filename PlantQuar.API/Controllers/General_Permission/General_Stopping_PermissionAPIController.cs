using PlantQuar.BLL.BLL.General_PermissionBLL;

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
using static PlantQuar.DTO.DTO.General_Permissions.ActivePrintDTO;

namespace PlantQuar.API.Controllers.General_Permission
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class General_Stopping_PermissionAPIController : ApiController
    {
        // GET: ListIm_Permission
        General_Stopping_PermissionBLL cBLL = new General_Stopping_PermissionBLL();       
        public HttpResponseMessage Get_ImPermissions
          (int List, long? ImPermission_Number,int OperationCode)
        {
            try
            {
                Dictionary<string, object> dic =
                    cBLL.GetImPermissionsList_filter_ActivePrint(ImPermission_Number,OperationCode, API_HelperFunctions.Get_DeviceInfo());



                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        //public HttpResponseMessage PutUserData(ActivePrintDto dto,int OperationCode)
        public HttpResponseMessage PutUserData(ActivePrintDto dto,int OperationCode)
        {
            //EDIT
            try
            {
                Dictionary<string, object> dic = cBLL.Update(dto, API_HelperFunctions.Get_DeviceInfo());
                //Dictionary<string, object> dic = cBLL.Update(dto,OperationCode, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}