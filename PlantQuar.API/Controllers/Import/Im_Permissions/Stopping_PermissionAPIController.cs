using PlantQuar.BLL.BLL.Im_Permissions;
using PlantQuar.BLL.BLL.Import.Permissions;
using PlantQuar.DTO.DTO.Im_Permissions;
using PlantQuar.DTO.DTO.Import.CheckRequests;
using PlantQuar.DTO.DTO.Import.Permissions;
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
using static PlantQuar.DTO.DTO.Im_Permissions.ActivePrintDTO;

namespace PlantQuar.API.Controllers.Im_Permissions
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class Stopping_PermissionAPIController : ApiController
    {
        // GET: ListIm_Permission
        Stopping_PermissionBLL cBLL = new Stopping_PermissionBLL();       
        public HttpResponseMessage Get_ImPermissions
          (int List, long? ImPermission_Number)
        {
            try
            {
                Dictionary<string, object> dic =
                    cBLL.GetImPermissionsList_filter_ActivePrint(ImPermission_Number, API_HelperFunctions.Get_DeviceInfo());



                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage PutUserData(ActivePrintDto dto)
        {
            //EDIT
            try
            {
                Dictionary<string, object> dic = cBLL.
                    Update(dto, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}