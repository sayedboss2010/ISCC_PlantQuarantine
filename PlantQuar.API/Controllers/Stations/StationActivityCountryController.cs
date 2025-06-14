using PlantQuar.DTO.DTO;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using System.Web.Http.Cors;
using PlantQuar.BLL.BLL.Stations;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.DTO.DTO.Station;

namespace PlantQuar.API.Controllers.Stations
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class StationActivityCountryController : ApiController
    {
        StationActivityCountryBLL cBLL = new StationActivityCountryBLL();
        public HttpResponseMessage GetStationActivityCountryCount()
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetCount();
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage GetStationActivityCountryList(int pageSize, int index)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetAll(pageSize, index, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage GetObjectById(int Id)
        {
            Dictionary<string, object> dic = cBLL.Find(Id, API_HelperFunctions.Get_DeviceInfo());
            return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
        }
        // GET: api/StationActivityCountry/GetStationActivityCountryName

        //GET Insert A: api/StationActivityCountry/PostCreateStationActivityCountry
        public HttpResponseMessage PostCreateStationActivityCountry(StationActivityCountryDTO Dto)
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

        public HttpResponseMessage PutUpdateStationActivityCountry(StationActivityCountryDTO Dto)
        {
            //EDIT
            try
            {
                Dictionary<string, object> dic = cBLL.Update(Dto, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage PutDeleteStationActivityCountry(int delete, DeleteParameters Dto)
        {
            //Delete            
            try
            {
                Dictionary<string, object> dic = cBLL.Delete(Dto, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage PostStationActivityCountry(short user_id, string Date_Now, long StationActivityID, List<short> lst)
        {
            //INSERT ALL 

            DateTime _Date_Now = DateTime.Parse(Date_Now,
                                        System.Globalization.CultureInfo.CreateSpecificCulture("en-US"));
            try
            {
                Dictionary<string, object> dic = cBLL.InsertRecords(user_id, _Date_Now, StationActivityID, lst, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage PutStationCompany(short user_id, string Date_Now, long StaionActivityID, List<short> lst)
        {
            //Update ALL 
            DateTime _Date_Now = DateTime.ParseExact(Date_Now, "yyyy-MM-dd HH:mm:ss",
                                                   System.Globalization.CultureInfo.InvariantCulture);
            try
            {
                Dictionary<string, object> dic = cBLL.UpdateRecords(user_id, _Date_Now, StaionActivityID, lst, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }
}
