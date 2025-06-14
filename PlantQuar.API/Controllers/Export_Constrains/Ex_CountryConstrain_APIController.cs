using PlantQuar.BLL.BLL.Export_Constrains;
using PlantQuar.DTO.DTO.Export_Constrains;
using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PlantQuar.API.Controllers.Export_Constrains
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class Ex_CountryConstrain_APIController : ApiController
    {
        Ex_CountryConstrain_BLL cBLL = new Ex_CountryConstrain_BLL();
        public HttpResponseMessage GetAnalysisType_List(int AnalysisType)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.AnalysisTypeFillDrop_Edit(API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage GetCountry_List(int Country)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.CountryFillDrop_Edit(API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage GetUnion_List(int Union)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.UnionFillDrop_Edit(API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage GetCountriesUnion_Name(int CountriesUnion_Id)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetCountriesUnion_Name(CountriesUnion_Id, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage PostCreate_CountryConstrain(Ex_CountryConstrainDTO constrains)
        {
            try
            {
                Dictionary<string, object> dic;
                //if (constrains.CountryConstrainsDTO.TransportCountry_ID != null)
                //{
                    dic = cBLL.InsertCustomConstrainPro(constrains, API_HelperFunctions.Get_DeviceInfo());
                //}
                //else
                //{
                //    dic = cBLL.InsertCustomConstrain(constrains, API_HelperFunctions.Get_DeviceInfo());
                //}


                return Request.CreateResponse(API_HelperFunctions.
                    getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage Get_CountryConstrain(int Import_Country_ID, short TransportCountry_ID,
            long Item_ShortName_id, long ItemCategories_ID
            , bool IsStationAccreditation, bool IsFarmAccreditation, bool IsCompanyAccreditation)
        {
            try
            {

                Dictionary<string, object> dic =
                    cBLL.GetCustomConstrain(Import_Country_ID, TransportCountry_ID,Item_ShortName_id, ItemCategories_ID, IsStationAccreditation, IsFarmAccreditation, IsCompanyAccreditation,
                  API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
