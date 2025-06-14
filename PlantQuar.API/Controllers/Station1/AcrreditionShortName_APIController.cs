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
    public class AcrreditionShortName_APIController : ApiController
    {
        StationAccreditionDataBLL cBLL = new StationAccreditionDataBLL();

        public HttpResponseMessage PostAccredtionDataShortName
            (List<Fees_Constrain_Data_Item_ShortNameDTO> Dtos)
        {
            //Create
            try
            {
                Dictionary<string, object> dic = cBLL.InsertShortNameAccredition(Dtos, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }
}
