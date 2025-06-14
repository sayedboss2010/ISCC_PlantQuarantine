using PlantQuar.BLL.BLL.Export_CheckRequest;
using PlantQuar.DTO.DTO.Export_CheckRequest;
using PlantQuar.DTO.DTO.Import.IM_Committee;
using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PlantQuar.API.Controllers.Export_CheckRequest
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CheckRequestChangeLot_APIController : ApiController
    {
        // GET: CheckRequestChangeLot_API
        CheckRequestChangeLot_BLL cBLLList = new CheckRequestChangeLot_BLL();
      
        public HttpResponseMessage GetGeshniCommitteeList(string CheckRequestNumber)
        {
            try
            {
                Dictionary<string, object> dic =
                    cBLLList.GetGeshniCommitteeList(CheckRequestNumber, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage GetEmployeeGeshniChange(string requestNumber)
        {
            try
            {
                Dictionary<string, object> dic =
                    cBLLList.GetEmployeeGeshniChange(requestNumber, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }



        public HttpResponseMessage Put_CheckRequestChangeWeightList(string CheckRequest_Number, List<CheckedItemsListWeightDTO> CheckedItemsListWeight)
        {
            //EDIT
            try
            {
                Dictionary<string, object> dic = cBLLList.CheckRequestChangeWeightList(CheckRequest_Number, CheckedItemsListWeight, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }
}