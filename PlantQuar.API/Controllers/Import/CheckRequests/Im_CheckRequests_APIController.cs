using PlantQuar.BLL.BLL.Import.checkRequests;
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

namespace PlantQuar.API.Controllers.Import.CheckRequests
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class Im_CheckRequests_APIController : ApiController
    {
        Im_CheckRequestDetailsBLL cBLL = new Im_CheckRequestDetailsBLL();
        List_Im_checkRequestsBLL cBLLList = new List_Im_checkRequestsBLL();
        public HttpResponseMessage Get_CheckRequestDetails
         (string ImCheckRequest_Number, int Outlet_ID)
        {
            try
            {
                Dictionary<string, object> dic =
                    cBLL.GetImCheckRequestDetails(ImCheckRequest_Number, Outlet_ID, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage Get_CheckRequestList(short IsApproved, short userId, long? outlet)
        {
            long? outlet_ID;
            try
            {
               
                //if (outlet != null)
                //{
                //    outlet_ID = outlet;
                //}
               

                Dictionary<string, object> dic =
                    cBLLList.GetImCheckRequestList_filter(IsApproved, userId, outlet, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage Get_CheckRequestList2(short userId, string DateFrom, string DateEnd, int selectApproveId
            , int FinalResultListId, long outlet, string CheckRequest_Number, long Company_ID )
        {
            long? outlet_ID;
            try
            {

                if (outlet != null)
                {
                    outlet_ID = outlet;
                }


                Dictionary<string, object> dic =
                    cBLLList.GetImCheckRequestList_filter2(userId, outlet, DateFrom, DateEnd, selectApproveId, FinalResultListId, CheckRequest_Number,  Company_ID , API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage Get_CheckRequestList
          (string ImCheckRequest_Number, short IsApproved, short userId)
        {
            try
            {
                Dictionary<string, object> dic =
                    cBLLList.GetImCheckRequestList_filter(IsApproved, ImCheckRequest_Number, userId,API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        //approve request
        public HttpResponseMessage Put_ApproveCheckReq(int approve, Im_CheckRequestDTO dto)
        {
            //Create
            try
            {
                Dictionary<string, object> dic = cBLL.ApproveCheckReq(dto, API_HelperFunctions.Get_DeviceInfo());
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
                Dictionary<string, object> dic = cBLL.saveItemFees(dto, API_HelperFunctions.Get_DeviceInfo());
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
                Dictionary<string, object> dic = cBLL.FillDrop_RefuseReason(refuse, API_HelperFunctions.Get_DeviceInfo());
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

                Dictionary<string, object> dic = cBLL.InsertReasons(Dto, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage GetItemType_AddEdit(long CheckRequestStatusLst)
        {
            try
            {
                Dictionary<string, object> dic = cBLLList.FillItem_TypeDrop_Add( API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}