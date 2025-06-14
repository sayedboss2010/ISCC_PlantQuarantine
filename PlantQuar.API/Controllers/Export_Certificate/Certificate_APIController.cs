using PlantQuar.BLL.BLL.Export_Certificate;
using PlantQuar.DTO.DTO.Export_Certificate;
using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PlantQuar.API.Controllers.CertificateData
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class Certificate_APIController : ApiController
    {
        CertificateBLL cBLL = new CertificateBLL();
        public HttpResponseMessage GetContainerCatagoryLotByExNumber(long? certificateId)
        {
            //Create
            try
            {
                Dictionary<string, object> dic = cBLL.GetContainerCatagoryLotByExNumber(certificateId, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage putprintCertificates(AcceptCertificate accept)
        {
            //Create
            try
            {
                Dictionary<string, object> dic = cBLL.printCertificates(accept, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        //public HttpResponseMessage PostCreateCertificate(int x)
        //{
        //    //Create
        //    try
        //    {
        //        Dictionary<string, object> dic = cBLL.CreateCertificate();
        //        return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
        //    }
        //}

    }
}
