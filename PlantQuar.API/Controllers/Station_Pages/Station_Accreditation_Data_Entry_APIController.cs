using PlantQuar.BLL.BLL.Station_Pages;
using PlantQuar.DTO.DTO.Station_Pages;
using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PlantQuar.API.Controllers.Station_Pages
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class Station_Accreditation_Data_Entry_APIController : ApiController
    {
        Station_Accreditation_Data_Entry_BLL cBLL = new Station_Accreditation_Data_Entry_BLL();
        public HttpResponseMessage GetBy_Station_Constrain_TypeId(long EX_Constrain_Type_id)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetBy_Station_Constrain_TypeId(EX_Constrain_Type_id, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage Put_Station_Accreditation_Data( Station_Accreditation_Data_Entry_DTO Dto)
        {
            //EDIT
            try
            {
                Dictionary<string, object> dic = cBLL.Insert_Station_Accreditation_Data(Dto, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage GetStationActivityType_List(int? StationActivityType_ID, int? Accreditation_Type_ID)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.FillDrop_List(StationActivityType_ID, Accreditation_Type_ID, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage GetStation_Accreditation_Data_Entry_details(int details, long Id)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetById(Id, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage Put_Station_Accreditation_Data(long Edite,Station_Accreditation_Data_Entry_DTO Dto)
        {
            //EDIT
            try
            {
                Dictionary<string, object> dic = cBLL.Edite_Station_Accreditation_Data(Dto, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }
}
