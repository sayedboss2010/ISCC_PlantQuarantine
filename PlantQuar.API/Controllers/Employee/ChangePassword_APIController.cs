using PlantQuar.BLL.BLL.Employee;
using PlantQuar.DTO.DTO.Admin;
using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PlantQuar.API.Controllers.Employee
{
    public class ChangePassword_APIController : ApiController
    {
        ChangePasswordBLL cBLL = new ChangePasswordBLL();
        public HttpResponseMessage GetUserValid(string LoginName , string Password)
        {
            try
            {
                int dic = cBLL.IsUserlogin12(LoginName, Password, API_HelperFunctions.Get_DeviceInfo());
                return Request. CreateResponse( HttpStatusCode.OK,  dic );

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage PutUserData(CustomUserLogin Dto)
        {
            //EDIT
            try
            {
                Dictionary<string, object> dic = cBLL.Update(Dto, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }
}
