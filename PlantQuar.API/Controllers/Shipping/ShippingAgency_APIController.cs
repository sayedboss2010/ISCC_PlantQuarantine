

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

namespace PlantQuar.API.Controllers.Shipping
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ShippingAgency_APIController : ApiController
    {
        // GET: ShippingAgency_API
        ShippingAgencyBLL cBLL = new ShippingAgencyBLL();

        //public HttpResponseMessage GetObjectById(int Id)
        //{
        //    Dictionary<string, object> dic = cBLL.Find(Id, API_HelperFunctions.Get_DeviceInfo());
        //    return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
        //}
        public HttpResponseMessage GetShippingAgencyList(int pageSize, int index)
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

        public HttpResponseMessage GetShippingAgency(string arName, string enName, int pageSize, int index, string jtSorting)
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

        //GET Insert A: api/ShippingAgency/PostCreateShippingAgency
        public HttpResponseMessage PostCreateShippingAgency(ShippingAgencyDTO Dto)
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

        public HttpResponseMessage PutUpdateShippingAgency(ShippingAgencyDTO Dto)
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

        public HttpResponseMessage PutDeleteShippingAgency(int delete, DeleteParameters Dto)
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
    }
}