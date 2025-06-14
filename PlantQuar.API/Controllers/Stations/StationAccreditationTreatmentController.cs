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
    public class StationAccreditationTreatmentController : ApiController 
    {
        StationAccreditationTreatmentBLL cBLL = new StationAccreditationTreatmentBLL();
        public HttpResponseMessage GetStation_AccreditationTreatmentCount()
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
        public HttpResponseMessage GetStation_AccreditationTreatmentList(int pageSize, int index )
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
        // GET: api/Station_AccreditationTreatment/GetStation_AccreditationTreatmentName

        //GET Insert A: api/Station_AccreditationTreatment/PostCreateStation_AccreditationTreatment
        public HttpResponseMessage PostCreateStation_AccreditationTreatment(Station_AccreditationTreatmentDTO Dto )
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

        public HttpResponseMessage PutUpdateStation_AccreditationTreatment(Station_AccreditationTreatmentDTO Dto )
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

        public HttpResponseMessage PutDeleteStation_AccreditationTreatment(int delete, Station_AccreditationTreatmentDTO Dto )
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
        public HttpResponseMessage PostStation_AccreditationTreatment(short user_id, string Date_Now, long StationAccreditationID,
            byte Treatment_Id)
        {
            DateTime _Date_Now = DateTime.Parse(Date_Now,
                                        System.Globalization.CultureInfo.CreateSpecificCulture("en-US"));
            try
            {    Dictionary<string, object> dic = cBLL.Insert(Treatment_Id, user_id, _Date_Now, StationAccreditationID,   API_HelperFunctions.Get_DeviceInfo());
                    return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
                
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage PutStation_AccreditationTreatment(short user_id, string Date_Now, long StationAccreditationID,
             byte? Treatment_Id)
        {
           
            DateTime _Date_Now = DateTime.ParseExact(Date_Now, "yyyy-MM-dd HH:mm:ss",
                                                   System.Globalization.CultureInfo.InvariantCulture);
            try
            {
                    Dictionary<string, object> dic = cBLL.Update(Treatment_Id, user_id, _Date_Now, StationAccreditationID,   API_HelperFunctions.Get_DeviceInfo());
                    return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]); 
                
                
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
