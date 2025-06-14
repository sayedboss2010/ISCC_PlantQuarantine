using PlantQuar.BLL.BLL.Station;
using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PlantQuar.API.Controllers.Station
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class StationAccrediationCommittee_APIController : ApiController
    {
        StationAccrediationCommitteeBLL cBLL = new StationAccrediationCommitteeBLL();
        public HttpResponseMessage getStationAccrediationCommitteeData(string stationCode, int? Status, short? stationActivityType,long Outlit_ID)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetAll(stationCode, Status, stationActivityType, Outlit_ID, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        public HttpResponseMessage getStationAccrediationCommitteeData(string stationCode, int? Status, short? stationActivityType, string DateFrom, string DateEnd, long Outlet_ID, int? stationAccrTypeLstId, int? CompanyNameLst_Id, int? StationActivityType_ID)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetAll2(stationCode, Status, stationActivityType, DateFrom, DateEnd, Outlet_ID, stationAccrTypeLstId, CompanyNameLst_Id, StationActivityType_ID, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        //Eslam Fill Company Name List

        public HttpResponseMessage GetCompany_National_AddEdit(int CompanyNameLst_AddEDIT)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.FillCompanyNameLst_AddEDIT(API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        //Eslam Fill Station_Accreditation_Request_Type List

        public HttpResponseMessage GetStation_Accreditation_Request_Type_AddEdit(int Station_Accreditation_Request_Type_AddEDIT)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.FillStation_Accreditation_Request_Type_AddEDIT(API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        //Eslam Fill StationActivityLst_AddEDIT List
        public HttpResponseMessage GetStationActivityLst_AddEDIT(int StationActivityLst_AddEDIT)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.Fill_StationActivityLst_AddEDIT(API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
