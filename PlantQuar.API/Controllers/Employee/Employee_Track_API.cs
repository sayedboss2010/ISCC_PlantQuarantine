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
    public class Employee_Track_APIController : ApiController
    {
        Employee_TrackBLL cBLL = new Employee_TrackBLL();

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



        public HttpResponseMessage GetUser_Id_List(long outletId, int Operation_Type, string Start_Date, string End_Date, long Emp_ID, long Company_ID, byte Committee_TypeId, int PageNo, string request_number)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetPR_User_Id_List(outletId, Operation_Type, Start_Date, End_Date, Emp_ID, Company_ID, Committee_TypeId, PageNo, request_number, API_HelperFunctions.Get_DeviceInfo());
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
                Dictionary<string, object> dic = cBLL.GetAllGeneralAdmin(API_HelperFunctions.Get_DeviceInfo());
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

        public HttpResponseMessage GetEmployee_Edit_ByOutlet(int add, long Outlet_ID)
        {
            try
            {   //get active centers
                Dictionary<string, object> dic = cBLL.GetAllEmployeesForOutlet(Outlet_ID, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage GetCompany_ID_List(int Company)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetAllCompany(Company, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage GetCommittee_ID_List(int Committee)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetAllCommittee(Committee, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        public HttpResponseMessage GetCommitteType_Edit_ByOperation(int add, long Operation_Id)
        {
            try
            {   //get active centers
                Dictionary<string, object> dic = cBLL.GetAllCommitteTypeForOperation(Operation_Id, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage Get_DeleteEmployee_Addmin_Confirm(long Committee_ID, long Employee_ID ,bool Addmin_Confirm)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.DeleteEmployeeConfirm(Committee_ID, Employee_ID, Addmin_Confirm, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        //public HttpResponseMessage GetDataByEmpId(long outletId, int Operation_Type, string Start_Date, string End_Date, long Emp_ID)
        //{
        //    try
        //    {
        //        Dictionary<string, object> dic = cBLL.GetPR_User_Id_List(outletId, Operation_Type, Start_Date, End_Date, Emp_ID, API_HelperFunctions.Get_DeviceInfo());
        //        return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);

        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
        //    }
        //}

        public HttpResponseMessage GetEmployee_Outlet(int Outlet, long Outlet_ID)
        {
            try
            {   //get active centers
                Dictionary<string, object> dic = cBLL.GetAllEmployees_Outlet(Outlet_ID, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }
}
