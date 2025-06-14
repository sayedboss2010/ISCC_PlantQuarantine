using PlantQuar.BLL.BLL.Import.Constrains;
using PlantQuar.DTO.DTO.Import.Constrains;
using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;


namespace PlantQuar.API.Controllers.Import.Constrains
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class Im_Constrain_Type_APIController : ApiController
    {
        // GET: Im_Constrain_Type_API
        Im_Constrain_TypeBLL cBLL = new Im_Constrain_TypeBLL();

        public HttpResponseMessage GetCustomConstrainTypeCount()
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

        public HttpResponseMessage GetConstrainTypeList(int pageSize, int index)
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

        // GET: api/ConstrainType/GetConstrainTypeName
        /* public HttpResponseMessage GetConstrainTypebyID(string arName, string enName, int pageSize, int index)
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

        // GET: api/ConstrainType/GetConstrainTypeName
        public HttpResponseMessage GetConstrainTypeName(string arName, string enName, int pageSize, int index, string jtSorting = "")
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
        public HttpResponseMessage PostCreateConstrainType(Im_Constrain_TypeDTO Dto)
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

        public HttpResponseMessage PutUpdateConstrainType(Im_Constrain_TypeDTO Dto)
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


        public HttpResponseMessage PutDeleteConstrainType(int delete, DeleteParameters Dto)
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

        public HttpResponseMessage GetConstrainType_List(int List)
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
        public HttpResponseMessage GetConstrainType_AddEdit(int AddEdit)
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