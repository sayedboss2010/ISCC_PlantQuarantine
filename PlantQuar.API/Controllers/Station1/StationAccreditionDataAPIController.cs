using PlantQuar.BLL.BLL.Station;
using PlantQuar.DTO.DTO.Station;
using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PlantQuar.API.Controllers.Station
{
    public class StationAccreditionData_APIController : ApiController
    {

        StationAccreditionDataBLL cBLL = new StationAccreditionDataBLL();


        public HttpResponseMessage GetStation_CheckListList()
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetAccreditationTypes(API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())),
                    dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        } 
        
        public HttpResponseMessage GetStation_CheckLists(int list)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetStationCheckLists(API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())),
                    dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        } 
        
        
       

        public HttpResponseMessage PostAccredtionData(StationAccreditionDataDTO Dto)
        {
            //Create
            try
            {
                Dictionary<string, object> dic = cBLL.Insert(Dto, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        //public HttpResponseMessage PostAccredtionDataCountry(StationAccreditionDataCountryDTO Dtos)
        //{
        //    Create
        //    try
        //    {
        //        Dictionary<string, object> dic = cBLL.InsertCountryAccredition(Dtos, API_HelperFunctions.Get_DeviceInfo());
        //        return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
        //    }
        //}
        public HttpResponseMessage GetUnoinCountries(int UnoinId)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetCountryUnion(UnoinId, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())),
                    dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }
}
