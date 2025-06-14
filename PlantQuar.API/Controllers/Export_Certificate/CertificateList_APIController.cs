
using PlantQuar.BLL.BLL.Export_Certificate;
using PlantQuar.DTO.DTO;
using PlantQuar.DTO.DTO.Export_Certificate;
using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PlantQuar.API.Controllers.CertificateData
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CertificateList_APIController : ApiController
    {
        CertificateListBLL cBLL = new CertificateListBLL();
        //public HttpResponseMessage GetCertificateList(string fromDate, string endDate, byte ISAccepted,string requestNumber)
        //{
        //    //Create
        //    try
        //    {


        //        Dictionary<string, object> dic = cBLL.GetAllCertificates(fromDate, endDate, ISAccepted, requestNumber, API_HelperFunctions.Get_DeviceInfo());
        //        return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
        //    }
        //}


        public HttpResponseMessage GetCertificateList(byte ISAccepted, string requestNumber, short? Country_Id, long? Company_Id, short? companyTypes)
        {
            //string fromDate, string endDate,
            // fromDate, endDate,

            //Create
            try
            {


                Dictionary<string, object> dic = cBLL.GetAllCertificates(ISAccepted, requestNumber, Country_Id, Company_Id, companyTypes, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage putUpdateCertificate(AcceptCertificate accept)
        {
            //Create
            try
            {
                Dictionary<string, object> dic = cBLL.AcceptOrNotAcceptCertificates(accept, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        public HttpResponseMessage GetCertificate(long certificateId, short User_Updation_Id, bool IsAccepted, int ISPrint)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.printCertificates(certificateId, User_Updation_Id, IsAccepted, ISPrint, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        //Company type
        public HttpResponseMessage GetCompany_ID_List(int Company)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetAllCompany(Company, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage GetOrgniztion_ID_List(int Orgniztion)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetAllOrgniztion(Orgniztion, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage GetPerson_ID_List(int Person)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetAllPerson(Person, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }



    }
}