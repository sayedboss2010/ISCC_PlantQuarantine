using PlantQuar.BLL.BLL.Export_CheckRequest;
using PlantQuar.BLL.BLL.Pallet_Export_CheckRequest;

using PlantQuar.DTO.DTO.Pallet_Export_CheckRequest;
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

namespace PlantQuar.API.Controllers.Pallet_Export_CheckRequest
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class Pallet_EX_CheckRequests_APIController : ApiController
    {
        // EX_CheckRequestDetailsBLL cBLL = new EX_CheckRequestDetailsBLL();
        Pallet_List_EXCheckRequest_BLL cBLLList = new Pallet_List_EXCheckRequest_BLL();
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
        public HttpResponseMessage Get_CheckRequestList
          (string ImCheckRequest_Number, short IsApproved, short userId)
        {
            try
            {
                Dictionary<string, object> dic =
                    cBLLList.GetImCheckRequestList_filter(IsApproved, ImCheckRequest_Number, userId, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        //approve request
        public HttpResponseMessage Put_ApproveCheckReq(int approve1, Pallet_EX_CheckRequestDTO dto)
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
        public HttpResponseMessage Put_saveItemfees(int itemFees, Items_checkReq_Pallets dto)
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
        public HttpResponseMessage PostReasons(Pallet_ReasonsListReqIdDTO Dto, int listt)
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
    }
}