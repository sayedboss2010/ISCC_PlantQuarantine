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
    public class User_Privilage_APIController : ApiController
    {

        User_PrivilageBLL cBLL = new User_PrivilageBLL();
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


        public HttpResponseMessage GetGroup_List(int Group)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetAll_Group(Group, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage GetModule_List(long PR_GroupId)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetAll_Module(PR_GroupId, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage GetMenu_List(long PR_ModuleId, long PR_GroupId)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetAll_Menu(PR_ModuleId, PR_GroupId, API_HelperFunctions.Get_DeviceInfo());
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



        public HttpResponseMessage InsertEmpPrivilage(User_PrivilageDTO menus)
        {

            Dictionary<string, object> dic = cBLL.Insert_EmpPrivilage(menus, API_HelperFunctions.Get_DeviceInfo());    //send opt with carred data(Id,LoginName,Password,List_Menu) to bll
            return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);

        }


        public HttpResponseMessage GetPriv_Old_List(short checkedEmpId)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetPR_User_Old_List(checkedEmpId, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);


            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }



        public HttpResponseMessage Get_Priv_Emp_Old2(long Check_Delete_id)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.Get_Priv_Emp_Old2(Check_Delete_id, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);


            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }





    }
}