using PlantQuar.BLL.BLL.Farm.FarmCommittee;
using PlantQuar.DTO.DTO.Farm.FarmCommittee;
using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;

namespace PlantQuar.API.Controllers.Farm.FarmCommittee
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class FarmRequestItem_Categories_APIController : ApiController
    {
        FarmRequestItem_CategoriesBLL cBLL = new FarmRequestItem_CategoriesBLL();

        public HttpResponseMessage GetFarmRequestItem_CategoriesBLL(long FarmCommittee_ID, string arName, string enName, int pageSize, int index, string jtSorting)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetAll(FarmCommittee_ID, arName, enName, pageSize, index, jtSorting, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage PutUpdateFarmRequestItem_CategoriesBLL(Farm_Request_ItemCategoriesDTO Dto)
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


    }
}