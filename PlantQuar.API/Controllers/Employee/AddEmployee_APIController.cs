using PlantQuar.BLL.BLL.Employee;
using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PlantQuar.API.Controllers.Employee
{
    public class AddEmployee_APIController : ApiController
    {
        AddEmployeeBLL cBLL = new AddEmployeeBLL();

        public HttpResponseMessage GetOutlet_ID_List(string FullName,int id)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetPR_User_Id_List(FullName, id,API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage GetEmployee_byOutlet_List(string FullName, long EmplyeeNo, long OutLet_ID, int Type_ID_HR)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetEmployee_byOutlet( FullName,  EmplyeeNo, OutLet_ID, Type_ID_HR, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        //ex sayed
        public HttpResponseMessage GetEmployee_by_Station_ID_List(string FullName, long EmplyeeNo, long OutLet_ID, long user_Station_ID)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetEmployee_by_Station_ID_List(FullName, EmplyeeNo, OutLet_ID, user_Station_ID, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        //ex sayed
        public HttpResponseMessage GetEmployee_byOutlet_ListStord(string FullName, long EmplyeeNo, long OutLet_ID, long OutLet_HR_ID)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetEmployee_by_User_List(FullName, EmplyeeNo, OutLet_ID, OutLet_HR_ID, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
