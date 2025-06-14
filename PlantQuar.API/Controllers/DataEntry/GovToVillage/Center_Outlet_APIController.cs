using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using System.Web.Http.Cors;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.DTO.DTO.DataEntry.GovToVillage;

namespace PlantQuar.API.Controllers.DataEntry.GovToVillage
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class Center_Outlet_APIController : ApiController
    {
        Center_OutletBLL cBLL = new Center_OutletBLL();

        public HttpResponseMessage GetObjectById(int Id)
        {
            Dictionary<string, object> dic = cBLL.Find(Id, API_HelperFunctions.Get_DeviceInfo());
            return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
        }
        public HttpResponseMessage GetCentersList(long outlet_ID,int pageSize, int index)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetAll(outlet_ID,pageSize, index, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage PostCreateCenter_Outlet(Center_OutletDTO Dto)
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

        public HttpResponseMessage PutUpdateCenter_Outlet(Center_OutletDTO Dto)
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

        public HttpResponseMessage PutDeleteCenter_Outlet(int delete, DeleteParameters Dto)
        {
            //Delete            
            try
            {
                Dictionary<string, object> dic = cBLL.Delete2(Dto, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), "");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage PostCenter_Outlet(short user_id, string Date_Now, long Outlet_ID,
            List<short> lst)
        {
            //INSERT ALL 

            DateTime _Date_Now = DateTime.Parse(Date_Now,
                                        System.Globalization.CultureInfo.CreateSpecificCulture("en-US"));
            try
            {
                Dictionary<string, object> dic = cBLL.InsertRecords(user_id, _Date_Now, Outlet_ID, lst, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage PutCenter_Outlet(short user_id, string Date_Now, byte Outlet_ID, List<short> lst)
        {
            //Update ALL 
            DateTime _Date_Now = DateTime.ParseExact(Date_Now, "yyyy-MM-dd HH:mm:ss",
                                                   System.Globalization.CultureInfo.InvariantCulture);
            try
            {
                Dictionary<string, object> dic = cBLL.UpdateRecords(user_id, _Date_Now, Outlet_ID, lst, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        //sayed
        public HttpResponseMessage GetOutlet_GovList(short Gov_ID)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetOutlet_GovList(Gov_ID, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }        
        }
    }
}//
