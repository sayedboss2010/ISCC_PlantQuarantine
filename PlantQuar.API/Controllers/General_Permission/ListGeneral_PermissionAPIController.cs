using PlantQuar.BLL.BLL.General_Permission;
using PlantQuar.DTO.DTO.General_Permissions.Permissions;
using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;

using System.Net;
using System.Net.Http;

using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;
using static PlantQuar.DTO.DTO.General_Permissions.ActivePrintDTO;

namespace PlantQuar.API.Controllers.General_Permission
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ListGeneral_PermissionAPIController : ApiController
    {
        // GET: ListIm_Permission
        General_List_ImPermissions_NewBLL cBLL = new General_List_ImPermissions_NewBLL();
        General_Im_PermissionRequestPrintBLL cbll2 = new General_Im_PermissionRequestPrintBLL();
        GeneralIm_PermissionDetailBLL cBLL3 = new GeneralIm_PermissionDetailBLL();
       
        public HttpResponseMessage Get_ImPermissions( int radio_ID, long? Company_ID, long? Country_ID, long? ShortName_ID, int? Type_Item, int? Type_Company, string Im_PermissionRequest_Num, int OperationCode)
        {
            try
            {
                Dictionary<string, object> dic =cBLL.GetImPermissionsList(radio_ID,  Company_ID,  Country_ID,  ShortName_ID, Type_Item, Type_Company, Im_PermissionRequest_Num, OperationCode, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage Get_ImPermissions
          (int List, decimal? ImPermission_Number, int Isacceppted, int OperationCode)
        {
            try
            {
                Dictionary<string, object> dic =
                    cBLL.GetImPermissionsList_filter(Isacceppted, ImPermission_Number, OperationCode,API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        } 
        public HttpResponseMessage Get_ImPermissions
          (int List, decimal? ImPermission_Number, int OperationCode)
        {
            try
            {
                Dictionary<string, object> dic =
                    cBLL.GetImPermissionsList_filter_ActivePrint(  ImPermission_Number, OperationCode, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage Get_ImPermissionRequestPrint
         (int print, decimal? ImPermission_Number,short User_Creation_Id, int OperationCode)
        {
            try
            {
                Dictionary<string, object> dic =
                    cbll2.GetImPermissionPrintDetails(ImPermission_Number, User_Creation_Id, OperationCode, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage Get_ImPermissionRequestDetail
         (int detail, decimal? ImPermission_Number )
        {
            try
            {
                Dictionary<string, object> dic =
                    cBLL3.GetImPermissionDetails(ImPermission_Number,  API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
         public HttpResponseMessage Get_ImPermissionRequestDetail1
         (long id,int detail1, int OperationCode)
        {
            try
            {
                Dictionary<string, object> dic =
                    cBLL3.getSegal(id, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        //approve
        public HttpResponseMessage Put_ApproveImPermision(int approve, ImPermissionPrintDetailsDTO dto)
        {
            
            try
            {
                Dictionary<string, object> dic = cBLL3.ApproveImPermision(dto, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        //refuse reason
        public HttpResponseMessage GetRefuseReasons(int List, int refuse)
        {
            try
            {
                //if rfuse = 1 ......refuse request .....if refuse = 2  ..stopprequest
                Dictionary<string, object> dic = cBLL3.FillDrop_RefuseReason(refuse, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage PostReasons(Im_PermissionRequest_RefuseReasonDTO Dto, int listt)
        {
            //Create
            try
            {

                Dictionary<string, object> dic = cBLL3.InsertReasons(Dto, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        //eman
        public HttpResponseMessage Put_PrintArabic(int printAr, ImPermissionIsPrintDTO dto)
        {
            //Create
            try
            {
                Dictionary<string, object> dic = cbll2.printPermissionArabic(dto, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage Put_PrintEnglish(int printEn, ImPermissionIsPrintDTO dto)
        {
            //Create
            try
            {
                Dictionary<string, object> dic = cbll2.printPermissionEnglish(dto, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage Get_ReasonRefuseByImPermissionRequestId
          (long ImPermissionRequestId )
        {
            try
            {
                Dictionary<string, object> dic =
                    cBLL3.getReafuseReasonByPerrmissionId(ImPermissionRequestId, API_HelperFunctions.Get_DeviceInfo());
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

        public HttpResponseMessage PostNoPaid(ImPermissionPrintDetailsDTO Dto, int NoPaid)
        {
            //Create
            try
            {

                Dictionary<string, object> dic = cBLL3.InsertNoPaid(Dto, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }
}