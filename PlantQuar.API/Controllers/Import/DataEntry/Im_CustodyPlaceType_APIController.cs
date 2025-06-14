using PlantQuar.BLL.BLL.Import.DataEntry;
using PlantQuar.DTO.DTO.Import.DataEntry;
using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PlantQuar.API.Controllers.Import.DataEntry
{

    // GET: Im_CustodyPlaceType_API
    [EnableCors(origins: "*", headers: "*", methods: "*")]
        public class Im_CustodyPlaceType_APIController : ApiController
        {

            Im_CustodyPlaceTypeBLL<Im_CustodyPlaceTypeDTO> cBLL = new Im_CustodyPlaceTypeBLL<Im_CustodyPlaceTypeDTO>();
            public HttpResponseMessage GetIm_CustodyPlaceCount()
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
            public HttpResponseMessage GetIm_CustodyPlaceList(int pageSize, int index)
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
            // GET: api/Im_CustodyPlace/GetIm_CustodyPlaceName
            public HttpResponseMessage GetIm_CustodyPlaceName(string arName, string enName, int pageSize, int index)
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

            //GET Insert A: api/Im_CustodyPlace/PostCreateIm_CustodyPlace
            public HttpResponseMessage PostCreateIm_CustodyPlace(Im_CustodyPlaceTypeDTO Dto)
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

            public HttpResponseMessage PutUpdateIm_CustodyPlace(Im_CustodyPlaceTypeDTO Dto)
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

            public HttpResponseMessage PutDeleteIm_CustodyPlace(int delete, DeleteParameters Dto)
            {
                //Delete            
                try
                {
                    Dictionary<string, object> dic = cBLL.Delete(Dto, API_HelperFunctions.Get_DeviceInfo());
                    return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
                }
            }

            //DROPS
            //Get AnalysisLab in List
            public HttpResponseMessage GetIm_CustodyPlaceType_List(int List)
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


        }
 
}