using PlantQuar.BLL.BLL.Station_Pages;
using PlantQuar.DTO.DTO.Station_Pages;
using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PlantQuar.API.Controllers.Station_Pages
{
    public class AcceptedStation_Company_APIController : ApiController
    {
        AcceptedStation_Company_BLL cBLL = new AcceptedStation_Company_BLL();
        public HttpResponseMessage GetStation_Company_List(long Company_Id, int Company_Type_Id)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetStation_Company_List(Company_Id, Company_Type_Id,API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        public HttpResponseMessage InsertStations_Company(List<Station_Company_DTO> menus_Status_new)
        {

            Dictionary<string, object> dic = cBLL.Insert_Stations_Company(menus_Status_new, API_HelperFunctions.Get_DeviceInfo());    //send opt with carred data(Id,LoginName,Password,List_Menu) to bll
            return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);

        }

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
