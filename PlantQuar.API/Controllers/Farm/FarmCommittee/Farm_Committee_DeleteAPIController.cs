using PlantQuar.BLL.BLL.Farm.FarmCommittee;
using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PlantQuar.API.Controllers.Farm.FarmCommittee
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class Farm_Committee_DeleteAPIController : ApiController
    {
        Farm_Committee_DeleteBLL cBLL = new Farm_Committee_DeleteBLL();
        public HttpResponseMessage GetFarmCommitteeType(int List)
        {
            try
            {
                try
                {
                    Dictionary<string, object> dic = cBLL.GetAll(API_HelperFunctions.Get_DeviceInfo());
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


        public HttpResponseMessage Put_deleted_committee_ID(List<long> deleted_lst)
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
