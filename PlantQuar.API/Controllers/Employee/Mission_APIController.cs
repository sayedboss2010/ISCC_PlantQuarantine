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
    public class Mission_APIController : ApiController
    {
        MissionBLL cBLL = new MissionBLL();



        public HttpResponseMessage GetOutlet_ID_List(int Outlet)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetAll(Outlet,API_HelperFunctions.Get_DeviceInfo());
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
        public HttpResponseMessage GetUser_Id_List1(string Start_Date, string End_Date, long User1)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetPR_User_Id_List1(Start_Date, End_Date, User1, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }



        public HttpResponseMessage PostCreatePR_Mission(Classsss Dto)
        {

            Dictionary<string, object> dic = cBLL.Insert(Dto, API_HelperFunctions.Get_DeviceInfo());
            return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);

        }
        // Get PR_Mission by Name 
        public HttpResponseMessage GetPR_MissionName(string Start_Date, string End_Date, long outletId, long outletId1)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetAll(Start_Date, End_Date, outletId, outletId1, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
          public HttpResponseMessage GetGeneralAdmin()
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetAllGeneralAdmin( API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        //nora
        public HttpResponseMessage GetChoosedMaterial_id(long chossedMaterail)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetPR_User_Id_List(chossedMaterail, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }
}
