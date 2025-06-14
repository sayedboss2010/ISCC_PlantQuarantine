using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Collections.Generic;

using PlantQuar.DTO.HelperClasses;
using PlantQuar.BLL.BLL.DataEntry.Outlets;
using PlantQuar.DTO.DTO.DataEntry.Outlets;

namespace PlantQuar.API.Controllers.DataEntry.Outlets
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class HagrContactController : ApiController
    {
        HagrContactBLL cBLL = new HagrContactBLL();

        public HttpResponseMessage GetHagrContactCount()
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
        public HttpResponseMessage GetHagrContactList(int pageSize, int index )
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

        public HttpResponseMessage GetObjectById(int Id )
        {
            Dictionary<string, object> dic = cBLL.Find(Id, API_HelperFunctions.Get_DeviceInfo());
            return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
        }
        // GET: api/HagrContact/GetHagrContactName
        public HttpResponseMessage GetHagrContactName(string arName, string enName, int pageSize, int index )
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetAll(arName, enName, pageSize, index, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        //GET Insert A: api/HagrContact/PostCreateHagrContact
        public HttpResponseMessage PostCreateHagrContact(HagrContactDTO      Dto )
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

        public HttpResponseMessage PutUpdateHagrContact(HagrContactDTO      Dto )
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

        public HttpResponseMessage PutDeleteHagrContact(int delete, DeleteParameters      Dto )
        {
            //Delete            
            try
            {
                Dictionary<string, object> dic = cBLL.Delete(Dto, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), "");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage PostHagrContact(short user_id, int OutlitAdmin, string Date_Now, long ContactOwnerID, List<HagrContactDTO> lst )
        {
            //INSERT ALL 

            DateTime _Date_Now = DateTime.Parse(Date_Now,
                                        System.Globalization.CultureInfo.CreateSpecificCulture("en-US"));
            try
            {
                Dictionary<string, object> dic = cBLL.InsertRecords(user_id, ContactOwnerID, _Date_Now, OutlitAdmin, lst, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage PutHagrContact_UpdateData(short user_id, int OutlitAdmin, string Date_Now, long ContactOwnerID, List<HagrContactDTO> lst )
        {
            //Update ALL 
            DateTime _Date_Now = DateTime.ParseExact(Date_Now, "yyyy-MM-dd HH:mm:ss",
                                                   System.Globalization.CultureInfo.InvariantCulture);
            try
            {
                Dictionary<string, object> dic = cBLL.UpdateRecords(user_id, ContactOwnerID, _Date_Now, OutlitAdmin, lst, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage GetHagrContact(long ContactOwnerID,int outletadmin)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetHagrContactByOutlitAdminID(ContactOwnerID,outletadmin);
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}