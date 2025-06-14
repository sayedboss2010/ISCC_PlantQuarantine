using PlantQuar.BLL.BLL.Export_Certificate;
using PlantQuar.BLL.BLL.Export_CheckRequest;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace PlantQuar.API.Controllers.Export_Certificate
{
    public class ChangeCountryCertificate_APIController : ApiController
    {
        ChangeCountryCertificateBLL cBLL = new ChangeCountryCertificateBLL();

        public HttpResponseMessage GetCountries_Name()
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetCountries_Name(API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage GetPortType(int portType)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetPortType(API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage GetCountry(string CheckRequestNumber)
        {
            try
            {
                Dictionary<string, object> dic =
                    cBLL.GetCountry(CheckRequestNumber, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);


            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage GetchangeImportCountry(string RequestNumber, int newImportPortType, int newImportCountryID, int newImportPortID, int currentImportCountryID, int currentImportPortID, short User_Id)
        {
            try
            {
                Dictionary<string, object> dic =
                    cBLL.changeImportCountry(RequestNumber, newImportPortType, newImportCountryID, newImportPortID, currentImportCountryID, currentImportPortID, User_Id, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);


            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage GetchangePassengerCountry(string RequestNumber, int newPassengerPortType, int newPassengerCountryID, int newPassengerPortID, int currentPassengerCountryID, int currentPassengerPortID, short User_Id)
        {
            try
            {
                Dictionary<string, object> dic =
                    cBLL.changePassengerCountry(RequestNumber, newPassengerPortType, newPassengerCountryID, newPassengerPortID, currentPassengerCountryID, currentPassengerPortID, User_Id, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);


            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}