using PlantQuar.BLL.BLL.Im_CheckRequest_General;
using PlantQuar.DTO.DTO.Im_CheckRequest_General;

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

namespace PlantQuar.API.Controllers.Im_CheckRequest_General
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RequestDetailsGeneral_APIController : ApiController
    {
        RequestDetailsGeneralBLL cBLL = new RequestDetailsGeneralBLL();

        public HttpResponseMessage Get_CheckRequestDetails
         (string Request_Number, int Outlet_ID)
        {
            try
            {
                Dictionary<string, object> dic =
                    cBLL.GetImCheckRequestDetails(Request_Number, Outlet_ID, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        //approve request
        public HttpResponseMessage Put_ApproveCheckReq(int approve, Im_CheckRequestGeneralDTO dto)
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
        public HttpResponseMessage Put_saveItemfees(int itemFees, Items_checkReqGeneral dto)
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
        public HttpResponseMessage PostReasons(ReasonsListReqIdGeneralDTO Dto, int listt)
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
    }
}
