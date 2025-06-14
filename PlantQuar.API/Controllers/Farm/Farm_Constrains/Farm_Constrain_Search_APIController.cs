using PlantQuar.BLL.BLL.Farm.FarmConstrains;
using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PlantQuar.API.Controllers.Farm.Farm_Constrains
{
    public class Farm_Constrain_Search_APIController : ApiController
    {



        SearchFarm cBLL = new SearchFarm();
        public HttpResponseMessage GetCountry(int Item_ID)
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
        public HttpResponseMessage GetFarmCountry(String FarmCode, String FarmCode1, int Item_ID)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetFarmCountry(FarmCode, API_HelperFunctions.Get_DeviceInfo());

                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage GetItem_Name(int countryID, int Item_ID)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetItems(countryID, API_HelperFunctions.Get_DeviceInfo());

                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage GetCountryItemData(long countryID, int Item_ID, int Item_ID1, int Item_ID2)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.getCountryItemData(countryID, Item_ID, API_HelperFunctions.Get_DeviceInfo());

                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        //public HttpResponseMessage GetItem_Name(String FarmCode, String FarmCode1, int Item_ID)
        //{
        //    try
        //    {
        //        Dictionary<string, object> dic = cBLL.GetFarmCode(FarmCode, FarmCode1, API_HelperFunctions.Get_DeviceInfo());

        //        return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);

        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
        //    }
        //}

    }
}
