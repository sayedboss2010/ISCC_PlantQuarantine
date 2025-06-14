using PlantQuar.DTO.DTO;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using System.Web.Http.Cors;
using PlantQuar.BLL.BLL.Stations;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.DTO.DTO.Company;

namespace PlantQuar.API.Controllers.Stations
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class StationAccreditationCountryController : ApiController
    {
        StationAccreditationCountryBLL cBLL = new StationAccreditationCountryBLL();
        public HttpResponseMessage GetStation_AccreditationCountryCount()
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
        public HttpResponseMessage GetStation_AccreditationCountryList(int pageSize, int index )
        {
            try
            {
Dictionary<string, object> dic = cBLL.GetAll( pageSize, index, API_HelperFunctions.Get_DeviceInfo());
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
        // GET: api/Station_AccreditationCountry/GetStation_AccreditationCountryName

        //GET Insert A: api/Station_AccreditationCountry/PostCreateStation_AccreditationCountry
        public HttpResponseMessage PostCreateStation_AccreditationCountry(StationAccrediationCountryDTO Dto )
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

        public HttpResponseMessage PutUpdateStation_AccreditationCountry(StationAccrediationCountryDTO Dto )
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

        public HttpResponseMessage PutDeleteStation_AccreditationCountry(int delete, StationAccrediationCountryDTO Dto )
        {
            //Delete            
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
        public HttpResponseMessage PostStation_AccreditationCountry(short user_id, string Date_Now, long StationAccreditationID, 
            List<short> lst)
        {
            //INSERT ALL 

            DateTime _Date_Now = DateTime.Parse(Date_Now,
                                        System.Globalization.CultureInfo.CreateSpecificCulture("en-US"));
            try
            {
                Dictionary<string, object> dic = cBLL.InsertRecords(user_id, _Date_Now, StationAccreditationID, lst,   API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage PutStation_AccreditationCountry(short user_id, string Date_Now, long StationAccreditationID, List<short> lst)
        {
            //Update ALL 
            DateTime _Date_Now = DateTime.ParseExact(Date_Now, "yyyy-MM-dd HH:mm:ss",
                                                   System.Globalization.CultureInfo.InvariantCulture);
            try
            {
                Dictionary<string, object> dic = cBLL.UpdateRecords(user_id, _Date_Now, StationAccreditationID, lst,   API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
