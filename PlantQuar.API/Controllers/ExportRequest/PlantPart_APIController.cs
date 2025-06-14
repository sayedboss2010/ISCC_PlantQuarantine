using PlantQuar.BLL.BLL.ExportRequest;

using PlantQuar.DTO.DTO;
using PlantQuar.DTO.DTO.ExportRequest;
using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using System.Web.Http.Cors;

namespace PlantQuar.API.Controllers.ExportRequest
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]

    public class PlantPart_APIController : ApiController
    {
        PlantPartBLL cBLL = new PlantPartBLL();
        public HttpResponseMessage GetPlantPartCount()
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
        public HttpResponseMessage GetPlantPartList(int pageSize, int index)
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

        public HttpResponseMessage GetPlantPartListByPlantID(Int64 PlantID, int pageSize, int index)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetAllByPlantID(PlantID, pageSize, index, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic);
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
        // GET: api/PlantPart/GetPlantPartName
        public HttpResponseMessage GetPlantPartName(string arName, string enName, int pageSize, int index)
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

        //GET Insert A: api/PlantPart/PostCreatePlantPart
        public HttpResponseMessage PostCreatePlantPart(PlantPartDTO Dto)
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

        public HttpResponseMessage PutUpdatePlantPart(PlantPartDTO Dto)
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

        public HttpResponseMessage PutDeletePlantPart(int delete, DeleteParameters Dto)
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

        public HttpResponseMessage GetPlantPartDropDownList(int List)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.FillDrop_AddEDITLIST(API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage GetPlantPartDropDownList(int AddEdit, int PlantId)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.FillDrop_ByProduct(PlantId, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage GetPlantPartAllowed(bool allowed, int PlantId)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.FillDrop_Allowed(allowed, PlantId, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        //public HttpResponseMessage GetPlantPartDropDownList_By_Plant(int List, int PlantId)
        //{
        //    try
        //    {
        //        Dictionary<string, object> dic = cBLL.GetAllUsingParamForList(PlantId,   API_HelperFunctions.Get_DeviceInfo());
        //        return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
        //    }
        //}
        //public HttpResponseMessage GetPlantPartDropDownList_By_Plant_Edit(int AddEdit, int PlantId)
        //{
        //    try
        //    {
        //        Dictionary<string, object> dic = cBLL.GetAllUsingParamForAddEdit(PlantId,   API_HelperFunctions.Get_DeviceInfo());
        //        return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
        //    }
        //}

        public HttpResponseMessage GetPlantPart_List(int part, Int64 plantId)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.FillDrop_Add(plantId, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage GetPlantPartType_ByPlant(int type, long plantId)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.PlantPartType_ByPlant(plantId, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage GetPlantPartTypeID_ByPlantPart(long plantPartId)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.PlantPartTypeId_ByPlantPart(plantPartId, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        //DROPS
        public HttpResponseMessage GetProductStatus_List(int List)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.FillDrop_List(API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage GetProductStatus_AddEdit(int AddEdit)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.FillDrop_AddEdit(API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}