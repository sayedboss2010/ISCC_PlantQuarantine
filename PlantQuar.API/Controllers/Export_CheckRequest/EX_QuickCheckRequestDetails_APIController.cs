using PlantQuar.BLL.BLL.Export_CheckRequest;
using PlantQuar.DTO.DTO.Export_CheckRequest;
using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PlantQuar.API.Controllers.Export_CheckRequest
{
    public class EX_QuickCheckRequestDetails_APIController :   ApiController
    {
        // GET: EX_QuickCheckRequestDetails_API
        EX_QuickCheckRequestDetailsBLL cBLL = new EX_QuickCheckRequestDetailsBLL();

        public HttpResponseMessage Get_CheckRequestDetails
          (string ExCheckRequest_Number)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetExCheckRequestDetails(ExCheckRequest_Number, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
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

        //approve request
        public HttpResponseMessage Put_ApproveCheckReq(int approve, EX_CheckRequestDTO dto)
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
    }
}
