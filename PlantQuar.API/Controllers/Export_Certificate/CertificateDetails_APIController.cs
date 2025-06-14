using PlantQuar.BLL.BLL.Export_Certificate;
using PlantQuar.DTO.DTO.Export_Certificate;
using PlantQuar.DTO.DTO.Export_CheckRequest;
using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PlantQuar.API.Controllers.Export_Certificate
{
    public class CertificateDetails_APIController : ApiController
    {
        // GET: CertificateDetails_API
        CertificateDetailsBLL cBLL = new CertificateDetailsBLL();

        public HttpResponseMessage Get_CheckRequestDetails
          (long? certificate_ID)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetExCheckRequestDetails(certificate_ID, API_HelperFunctions.Get_DeviceInfo());
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

        public HttpResponseMessage Put_AddationDeclartionِAdmin(int addtion, CheckRequest_Getdata_AdditionDec_AdminDTO dto)
        {
            //Create
            try
            {
                Dictionary<string, object> dic = cBLL.AddationDeclartionِAdmin(dto, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage putUpdateCertificate(AcceptCertificate accept)
        {
            //Create
            try
            {
                Dictionary<string, object> dic = cBLL.AcceptOrNotAcceptCertificates(accept, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}