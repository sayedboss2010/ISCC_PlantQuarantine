using PlantQuar.BLL.BLL.Admin;
using PlantQuar.DTO.DTO.Admin;
using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PlantQuar.API.Controllers.Admin
{
    public class AddUser_APIController : ApiController
    {

        AddUserBLL cBLL = new AddUserBLL();
        public HttpResponseMessage GetOutlet_ID_List(int Outlet)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetAll(Outlet, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage GetUser_Id_List(long User)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetPR_User_Id_List(User, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        
        public HttpResponseMessage GetSave_PR_User(short Id, string user_name, string pass, string email,bool IS_Change_Password)
        {
            try
            {
                //, user_name, pass, email, tel
               int dic = cBLL.Save_PR_User(Id, user_name, pass, email, IS_Change_Password, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(HttpStatusCode.OK, dic);
                //return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage GetEmail_Id(short User_Id)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetPR_User_Email_Id(User_Id, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }





    }
}