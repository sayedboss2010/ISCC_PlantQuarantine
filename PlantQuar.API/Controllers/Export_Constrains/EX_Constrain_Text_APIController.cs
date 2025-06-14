using PlantQuar.BLL.BLL.Export_Constrains;
using PlantQuar.BLL.BLL.Import.Constrains;
using PlantQuar.DTO.DTO.Export_Constrains;
using PlantQuar.DTO.DTO.Import.Constrains;
using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PlantQuar.API.Controllers.ExportConstrains
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class EX_Constrain_Text_APIController : ApiController
    {
        EX_Constrain_TextBLL cBLL = new EX_Constrain_TextBLL();
        public HttpResponseMessage GetCentersCount()
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

        public HttpResponseMessage GetCentersList(int pageSize, int index)
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
      
        public HttpResponseMessage GetCentersName(string arName, string enName, int pageSize, int index, string jtSorting, byte Fill_Lists_Type)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetAll(arName, enName, pageSize, index, jtSorting, Fill_Lists_Type, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        //GET Insert A: api/Centers/PostCreateCenters
        public HttpResponseMessage PostCreateCenters(EX_Constrain_Text_DTO Dto)
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

        public HttpResponseMessage PutUpdateCenters(EX_Constrain_Text_DTO Dto)
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

        public HttpResponseMessage PutDeleteCenters(int delete, DeleteParameters Dto)
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

        //DROPS
        public HttpResponseMessage GetIm_Constrain_Type_List(int List)
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

        public HttpResponseMessage GetFarmById(int details, long Id)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetById(Id, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
       
        public HttpResponseMessage GetBy_EX_Constrain_TypeId (long EX_Constrain_Type_id)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetBy_EX_Constrain_TypeId(EX_Constrain_Type_id, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

       
    }
}
