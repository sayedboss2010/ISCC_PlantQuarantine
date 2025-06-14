using PlantQuar.BLL.BLL.Import.Constrains;
using PlantQuar.DTO.DTO.Import.Constrains;
using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PlantQuar.API.Controllers.Import.Constrains
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class Im_Constrains_APIController : ApiController
    {
        Im_ConstrainBLL cBLL = new Im_ConstrainBLL();
        public HttpResponseMessage PostCreate_CountryConstrain(ImCustomCountryConstrain constrains)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.InsertCustomConstrain(constrains, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage Get_CountryConstrainItems(long itemShortNameId, long itemId, long catId, string initiatorIds)
        {
            try
            {
               var ss= initiatorIds.Split(',').ToList();
                List<long> longSS = ss.ConvertAll(long.Parse);

                Dictionary<string, object> dic =
                    cBLL.GetCustomConstrain_Item(itemShortNameId, itemId, catId, longSS, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        
        public HttpResponseMessage Get_CountryConstrainQualG(short qualGId,string InitiatorQualG)
        {
            try
            {
                var ss = InitiatorQualG.Split(',').ToList();
                List<long> longSS = ss.ConvertAll(long.Parse);

                Dictionary<string, object> dic = cBLL.GetCustomConstrain_QualG(qualGId, longSS, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage Get_ImConstrainType(int conType)
        {
            try
            {
                
                Dictionary<string, object> dic = cBLL.FillDrop_ConstrainType( API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage Get_ImConstrainTexts(byte conTypeId)
        {
            try
            {

                Dictionary<string, object> dic = cBLL.FillDrop_ConstrainTexts(conTypeId,API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage Get_ImConstrainTextDetails(long? textId)
        {
            try
            {

                Dictionary<string, object> dic = cBLL.Get_ConstrainTextDetails(textId, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }
}