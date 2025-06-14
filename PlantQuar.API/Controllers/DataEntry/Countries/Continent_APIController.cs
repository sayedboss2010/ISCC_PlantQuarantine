using PlantQuar.BLL.BLL.DataEntry.Countries;
using PlantQuar.DTO.DTO.DataEntry.Countries;
using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;


namespace PlantQuar.API.Controllers.DataEntry.Countries
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class Continent_APIController : ApiController
    {
        ContinentBLL cBLL = new ContinentBLL();

        public HttpResponseMessage GetCustomContinentCount()
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetCount();
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage GetContinentList(int pageSize, int index)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetAll(pageSize, index, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage GetObjectById(int Id)
        {
            Dictionary<string, object> dic = cBLL.Find(Id, API_HelperFunctions.Get_DeviceInfo());
            return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
        }

        // GET: api/Continent/GetContinentName
        /* public HttpResponseMessage GetContinentbyID(string arName, string enName, int pageSize, int index)
         {
             try
             {
                 Dictionary<string, object> dic = cBLL.GetAll(arName, enName, pageSize, index, API_HelperFunctions.Get_DeviceInfo());
                 return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic);
             }
             catch (Exception ex)
             {
                 return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
             }
         }
         */

        // GET: api/Continent/GetContinentName
        public HttpResponseMessage GetContinentName(string arName, string enName, int pageSize, int index, string jtSorting = "")
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetAll(arName, enName, pageSize,
                                   index, jtSorting, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        //GET Insert A: api/CompanyActivityType/PostInsertCompanyActivityTypes
        public HttpResponseMessage PostCreateContinent(ContinentDTO Dto)
        {
            //Create
            try
            {
                Dictionary<string, object> dic = cBLL.Insert(Dto, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        //GET Insert Array

        public HttpResponseMessage PutUpdateContinent(ContinentDTO Dto)
        {
            //EDIT
            try
            {
                Dictionary<string, object> dic = cBLL.Update(Dto, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
  

        public HttpResponseMessage PutDeleteContinent(int delete, DeleteParameters Dto)
        {
            //Delete

            try
            {

                Dictionary<string, object> dic = cBLL.Delete(Dto, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), "");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage GetContinent_List(int List)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.FillDrop_Add(API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage GetContinent_AddEdit(int AddEdit)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.FillDrop_Edit(API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


    }
}