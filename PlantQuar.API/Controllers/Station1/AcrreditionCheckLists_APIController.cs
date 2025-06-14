using PlantQuar.BLL.BLL.Station;
using PlantQuar.DTO.DTO.Station;
using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PlantQuar.API.Controllers.Station
{
    public class AcrreditionCheckLists_APIController : ApiController
    {
        StationAccreditionDataBLL cBLL = new StationAccreditionDataBLL();
        public HttpResponseMessage PostAccredtionData(List<Station_Accredition_CheckListDTO> Dto)
        {
            //Create
            try
            {
                Dictionary<string, object> dic = cBLL.InsertConstrainListAccredition(Dto, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
