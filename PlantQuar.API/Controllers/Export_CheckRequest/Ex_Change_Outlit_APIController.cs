using PlantQuar.BLL.BLL.Export_CheckRequest;
using PlantQuar.DTO.DTO.Export_CheckRequest;
using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;

namespace PlantQuar.API.Controllers.Export_CheckRequest
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class Ex_Change_Outlit_APIController : ApiController
    {
        // GET: List_Ex_checkRequestsOutlet_API
        Ex_Change_OutlitBLL cBLLList = new Ex_Change_OutlitBLL();

        public HttpResponseMessage Get_CheckRequestList  ( short IsApproved, short OutlitUserID )
        {
            try
            {
                Dictionary<string, object> dic =cBLLList.GetExCheckRequestList_filter(IsApproved,  OutlitUserID, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage PutUpdateCheckRequestListOut(List<EX_CheckRequestDTO> Dto)
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