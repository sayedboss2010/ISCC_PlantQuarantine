using PlantQuar.BLL.BLL.LaboratoryResult;
using PlantQuar.DTO.DTO;
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

namespace PlantQuar.API.Controllers.LaboratoryResult
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class SampleLABResult_APIController : ApiController
    {

        SampleLABResultBLL cBLL = new SampleLABResultBLL();
        public HttpResponseMessage GetLabResult(string barcode)
        {
            //Create
            try
            {
                Dictionary<string, object> dic = cBLL.GetSampleDataInfo(barcode, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }



        public HttpResponseMessage PostCreateLabResult(LabResultDTO dto)
        {
            //Create
            try
            {
                Dictionary<string, object> dic = cBLL.addSamleLabResult(dto, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}