using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PlantQuar.API.Controllers.Export_CheckRequest
{
    public class Pallet_List_EXCheckRequest_APIController : ApiController
    {
        //public HttpResponseMessage Get_CheckRequestList(short IsApproved, short userId)
        //{
        //    try
        //    {
        //        Dictionary<string, object> dic =
        //            cBLLList.GetImCheckRequestList_filter(IsApproved, userId, API_HelperFunctions.Get_DeviceInfo());
        //        return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
        //    }
        //}
        //public HttpResponseMessage Get_CheckRequestList
        //  (string ImCheckRequest_Number, short IsApproved, short userId)
        //{
        //    try
        //    {
        //        Dictionary<string, object> dic =
        //            cBLLList.GetImCheckRequestList_filter(IsApproved, ImCheckRequest_Number, userId, API_HelperFunctions.Get_DeviceInfo());
        //        return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
        //    }
        //}

    }
}
