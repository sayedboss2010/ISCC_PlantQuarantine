using PlantQuar.BLL.BLL.Station_Pages;
using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PlantQuar.API.Controllers.Station_Pages
{
    public class Station_Committee_Delete_APIController : ApiController
    {
        Station_Committee_Delete_BLL cBLL = new Station_Committee_Delete_BLL();
        public HttpResponseMessage GetStationCommitteeType(int List,long OutLit_ID)
        {
            try
            {
                try
                {
                    Dictionary<string, object> dic = cBLL.GetAll(OutLit_ID,API_HelperFunctions.Get_DeviceInfo());
                    return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage Put_deleted_StationCommittee_ID(List<long> deleted_lst)
        {
            try
            {
                try
                {
                    Dictionary<string, object> dic = cBLL.delete(deleted_lst, API_HelperFunctions.Get_DeviceInfo());
                    return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), 1);
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
