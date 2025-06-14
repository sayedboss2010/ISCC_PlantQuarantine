using PlantQuar.BLL.BLL.Station_Pages;
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
    public class Station_Fees_Type_APIController : ApiController
    {
        Station_Fees_Type_BLL cBLL = new Station_Fees_Type_BLL();






         //DROPS
      
        public HttpResponseMessage Get_Station_Fees_Type_List(int Fees_Type)
        {
            try
            {
                List<string> device_data = API_HelperFunctions.Get_DeviceInfo();
                Dictionary<string, object> dic = cBLL.FillDrop_Station_Fees_Type(Fees_Type,API_HelperFunctions.Get_DeviceInfo());
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
        public HttpResponseMessage Get_Station_Fees_Type_Mony(byte Station_Fees_Type_ID)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.Get_Station_Fees_Type_Mony(Station_Fees_Type_ID, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
