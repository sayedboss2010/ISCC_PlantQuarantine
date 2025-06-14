using PlantQuar.BLL.BLL.Import.checkRequests;
using PlantQuar.DTO.DTO.Import.CheckRequests;
using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;

namespace PlantQuar.API.Controllers.Import.CheckRequests
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class List_Im_checkRequestsOutlet_APIController : ApiController
    {
        // GET: List_Im_checkRequestsOutlet_API
        List_Im_checkRequestsOutletBLL cBLLList = new List_Im_checkRequestsOutletBLL();

        public HttpResponseMessage Get_CheckRequestList  ( short IsApproved, short OutlitUserID )
        {
            try
            {
                Dictionary<string, object> dic =cBLLList.GetImCheckRequestList_filter(IsApproved,  OutlitUserID, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage PutUpdateCheckRequestListOut(List<Im_CheckRequestDTO> Dto)
        {
            //EDIT
            try
            {
                Dictionary<string, object> dic = cBLLList.Update(Dto, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}