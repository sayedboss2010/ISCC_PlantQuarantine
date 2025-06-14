
using PlantQuar.BLL.BLL.Import.Committee;
using PlantQuar.DTO.DTO.Import.IM_Committee;
using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;

namespace PlantQuar.API.Controllers.Import.Im_Committee
{ 
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class Im_RequestCommittee_Shift_APIController : ApiController
    {
        // GET: Im_RequestCommittee_Shift_API
        private Im_RequestCommittee_ShiftBLL cBLL = new Im_RequestCommittee_ShiftBLL();
        public HttpResponseMessage GetIm_RequestCommittee_ShiftByCommittee(long Im_RequestCommittee_ID, int pageSize, int index)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetByCommittee(Im_RequestCommittee_ID, pageSize, index, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        //eman
        public HttpResponseMessage GetShiftMony(byte shiftId)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetTimingMony(shiftId, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        //Post Create Im_RequestCommittee_Shift
        public HttpResponseMessage PostCreateIm_RequestCommittee_Shift(Im_RequestCommittee_ShiftDTO Dto)
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

        //Put Update Im_RequestCommittee_Shift
        public HttpResponseMessage PutUpdateIm_RequestCommittee_Shift(Im_RequestCommittee_ShiftDTO Dto)
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

        //Put Delete Im_RequestCommittee_Shift
        public HttpResponseMessage PutDeleteIm_RequestCommittee_Shift(int delete, DeleteParameters Dto)
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

        public HttpResponseMessage GetShiftTiming_List(int List)
        {
            try
            {
                List<string> device_data = API_HelperFunctions.Get_DeviceInfo();
                Dictionary<string, object> dic = cBLL.FillDrop_ShiftTiming(API_HelperFunctions.Get_DeviceInfo());
                //for android group
                if (!bool.Parse(device_data[1]))
                {
                    //android
                    return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic);
                }
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}