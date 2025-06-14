using PlantQuar.BLL.BLL.LaboratoryResult;
using PlantQuar.DTO.DTO.Farm.FarmRequest;
using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PlantQuar.API.Controllers.LaboratoryResult
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LaboratoryResult_Report_APIController : ApiController
    {
        LaboratoryResult_ReportBLL cBLL = new LaboratoryResult_ReportBLL();

        public HttpResponseMessage GetLabResultReport(string barcode)
        {
            //Create
            try
            {
                Dictionary<string, object> dic = cBLL.GetLaboratoryResult_ReportData(barcode, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage GetLabResultReportNew(int new_p, string barcode)
        {
            //Create
            try
            {
                Dictionary<string, object> dic = cBLL.GetLaboratoryResult_ReportDataNew(barcode, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage postPrinrBarcode(Farm_SampleDataDTO dto)
        {
            //Create
            try
            {
                Dictionary<string, object> dic = cBLL.savePrintBarcode(dto.ID, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
