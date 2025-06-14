using PlantQuar.BLL.BLL.ExportRequest;
using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;

namespace PlantQuar.API.Controllers.ExportRequest
{

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class Ex_ItemsLots_APIController : ApiController
    {
        // GET: Ex_ItemsLots
        Ex_ItemsLotsBLL cBLL = new Ex_ItemsLotsBLL();
        public HttpResponseMessage Get_Plants(long ItemId)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetItemsLots(ItemId, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}