using PlantQuar.BLL.BLL.Export_CheckRequest;
using PlantQuar.DTO.DTO.Export_CheckRequest;

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

namespace PlantQuar.API.Controllers.Export_CheckRequest
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class EX_CheckRequestsNew_APIController : ApiController
    {
        // EX_CheckRequestDetailsBLL cBLL = new EX_CheckRequestDetailsBLL();
        List_EXCheckRequestNew_BLL cBLLList = new List_EXCheckRequestNew_BLL();
        //public HttpResponseMessage Get_CheckRequestDetails
        //  (string ImCheckRequest_Number)
        //{
        //    try
        //    {
        //        Dictionary<string, object> dic =
        //            cBLLList.GetImCheckRequestDetails(ImCheckRequest_Number, API_HelperFunctions.Get_DeviceInfo());
        //        return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
        //    }
        //}
        public HttpResponseMessage Get_CheckRequestList(short IsApproved, short userId)
        {
            try
            {
                Dictionary<string, object> dic =
                    cBLLList.GetImCheckRequestList_filter(IsApproved, userId, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage Get_CheckRequestList2(long Outlet_User_ID, long Station_User_ID)
        {
            try
            {
                Dictionary<string, object> dic =
                    cBLLList.GetExCheckRequestList_filter(Outlet_User_ID, Station_User_ID, 1, 1, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage Get_User_Station(short IS_Station, short userId)
        {
            try
            {
                Dictionary<string, object> dic =
                    cBLLList.Get_User_Station(userId, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        //public HttpResponseMessage Get_CheckRequestList
        //  (string ImCheckRequest_Number, short IsApproved, short userId)
        //{
        //    try
        //    {
        //        Dictionary<string, object> dic =
        //            cBLLList.GetImCheckRequestList_filter(IsApproved, ImCheckRequest_Number, userId, API_HelperFunctions.Get_DeviceInfo());
        //        return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
        //    }
        //}
        //approve request
        public HttpResponseMessage Put_ApproveCheckReq(int approve, EX_CheckRequestDTO dto)
        {
            //Create
            try
            {
                Dictionary<string, object> dic = cBLLList.ApproveCheckReq(dto, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        //refuse reason
        //Im_CheckRequests_API
        public HttpResponseMessage Put_saveItemfees(int itemFees, Items_checkReq dto)
        {
            //Create
            try
            {
                Dictionary<string, object> dic = cBLLList.saveItemFees(dto, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage GetRefuseReasons(int List, int refuse)
        {
            try
            {
                //if rfuse = 1 ......refuse request .....if refuse = 2  ..stopprequest
                Dictionary<string, object> dic = cBLLList.FillDrop_RefuseReason(refuse, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage PostReasons(ReasonsListReqIdDTO Dto, int listt)
        {
            //Create
            try
            {

                Dictionary<string, object> dic = cBLLList.InsertReasons(Dto, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        //Eslam Get List 13-11-2023
        public HttpResponseMessage Get_CheckRequestList3(long Outlet_User_ID, long Station_User_ID, int? radio_ID, long? Company_ID, long? Country_ID, string ExChechRequest_Num)
        {
          //(long? Outlet_User_ID, long? Station_User_ID, string CanView_Outlet_Examination,
         //string CanAdd_Station_Examination, string CanEdit_Outlet_Genshi, string CanDelete_Station_Genshi, List<string> Device_Info)


            try
            {
                Dictionary<string, object> dic =
                    cBLLList.GetExCheckRequestList_filter2(Outlet_User_ID, Station_User_ID, radio_ID, Company_ID, Country_ID, ExChechRequest_Num, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}