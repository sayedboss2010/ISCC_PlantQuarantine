

using PlantQuar.BLL.BLL.Import.DataEntry;
using PlantQuar.BLL.BLL.Shipping;
using PlantQuar.DTO.DTO.Shipping;
using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PlantQuar.API.Controllers.Import.DataEntry
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class Im_FinalResult_Data_APIController : ApiController
    {
        // GET: Im_FinalResult_Data_API
        Im_Final_Result_DataBLL cBLL = new Im_Final_Result_DataBLL();

        //public HttpResponseMessage GetObjectById(int Id)
        //{
        //    Dictionary<string, object> dic = cBLL.Find(Id, API_HelperFunctions.Get_DeviceInfo());
        //    return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
        //}
        public HttpResponseMessage GetIm_FinalResult_DataList(int pageSize, int index)
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

        public HttpResponseMessage GetIm_FinalResult_Data(string arName, string enName, int pageSize, int index, string jtSorting)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetAll(arName, enName, pageSize, index, jtSorting, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        //GET Insert A: api/Im_FinalResult_Data/PostCreateIm_FinalResult_Data
        public HttpResponseMessage PostCreateIm_FinalResult_Data(Im_FinalResult_DataDTO Dto)
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

        public HttpResponseMessage PutUpdateIm_FinalResult_Data(Im_FinalResult_DataDTO Dto)
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

        public HttpResponseMessage PutDeleteIm_FinalResult_Data(int delete, DeleteParameters Dto)
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




        //noura
        public HttpResponseMessage GetFinalResult_AddEdit(int FinalResult)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.FillFinalResultDrop_Edit(FinalResult, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        //noura




    }
}