
using PlantQuar.BLL.BLL.Export_Constrains;
using PlantQuar.DTO.DTO.Export_Constrains;
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
    public class Export_Constrains_APIController : ApiController
    {
        Export_ConstrainsBLL cBLL = new Export_ConstrainsBLL();
        public HttpResponseMessage Get_CountryConstrainPlants(long ShortName_ID, long catId, int constrainType, int owner)
        {
            try
            {

                Dictionary<string, object> dic =
                    cBLL.GetCustomConstrain_Plant(ShortName_ID, catId, constrainType, owner, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage Get_CountryConstrain(int ConstrainOwner_ID
            ,
            int CountryConstrain_Type
            , long Item_ShortName_id, long ItemCategories_ID, bool IsStationAccreditation, bool IsFarmAccreditation, bool IsCompanyAccreditation
            )
        {
            try
            {

                Dictionary<string, object> dic =
                    cBLL.GetCustomConstrain(ConstrainOwner_ID, CountryConstrain_Type
                    , Item_ShortName_id, ItemCategories_ID, IsStationAccreditation, IsFarmAccreditation, IsCompanyAccreditation,
                  API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage Get_CountryConstrainPro(int ConstrainOwner_ID
            ,
            int TransportCountry_ID
            , long Item_ShortName_id, long ItemCategories_ID, bool IsStationAccreditation, bool IsFarmAccreditation, bool IsCompanyAccreditation
            )
        {
            try
            {

                Dictionary<string, object> dic =
                    cBLL.GetCustomConstrainProc(ConstrainOwner_ID, TransportCountry_ID
                    , Item_ShortName_id, ItemCategories_ID, IsStationAccreditation, IsFarmAccreditation, IsCompanyAccreditation,
                  API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage PostCreate_CountryConstrain(ConstrainCountryDTO constrains)
        {
            try
            {
                Dictionary<string, object> dic;
                if (constrains.CountryConstrainsDTO.TransportCountry_ID != null)
                {
                    dic = cBLL.InsertCustomConstrainPro(constrains, API_HelperFunctions.Get_DeviceInfo());
                }
                else
                {
                    dic = cBLL. InsertCustomConstrain(constrains, API_HelperFunctions.Get_DeviceInfo());
                }


                return Request.CreateResponse(API_HelperFunctions.
                    getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        //public HttpResponseMessage Get_CountryConstrainPlants_Activation
        //   (int active, long plantId, byte purposeId, byte statusId, byte partType, int catId, int constrainType, int owner)
        //{
        //    try
        //    {
        //        Dictionary<string, object> dic =
        //            cBLL.GetPlantConstrain_Activation(plantId, purposeId, statusId, partType, catId, constrainType, owner, API_HelperFunctions.Get_DeviceInfo());
        //        return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
        //    }
        //}

        ////********************************//
        //public HttpResponseMessage Get_CountryConstrainProducts
        //   (long productId, byte purposeId, byte statusId, int constrainType, int owner)
        //{
        //    try
        //    {
        //        Dictionary<string, object> dic =
        //            cBLL.GetCustomConstrain_Product(productId, purposeId, statusId, constrainType, owner, API_HelperFunctions.Get_DeviceInfo());
        //        return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
        //    }
        //}

        //public HttpResponseMessage Get_CountryConstrainProducts_Activation
        //   (int active, long productId, byte purposeId, byte statusId, int constrainType, int owner)
        //{
        //    try
        //    {
        //        Dictionary<string, object> dic =
        //            cBLL.GetConstrain_Product_Activation(productId, purposeId, statusId, constrainType, owner, API_HelperFunctions.Get_DeviceInfo());
        //        return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
        //    }
        //}

        //public HttpResponseMessage Get_CountryConstrainLiableAlive
        //  (long aliveLiableId, byte purposeId, int statusId, int phaseId, int constrainType, int owner)
        //{
        //    try
        //    {
        //        Dictionary<string, object> dic =
        //            cBLL.GetCustomConstrain_LiablAlive(aliveLiableId, purposeId, statusId, phaseId, constrainType, owner, API_HelperFunctions.Get_DeviceInfo());
        //        return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
        //    }
        //}

        //public HttpResponseMessage Get_CountryConstrainLiableAlive_Activation
        //  (int active, long aliveLiableId, byte purposeId, int statusId, int phaseId, int constrainType, int owner)
        //{
        //    try
        //    {
        //        Dictionary<string, object> dic =
        //            cBLL.GetConstrain_LiablAlive_Activation(aliveLiableId, purposeId, statusId, phaseId, constrainType, owner, API_HelperFunctions.Get_DeviceInfo());
        //        return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
        //    }
        //}

        //public HttpResponseMessage Get_CountryConstrainLiableNotAlive
        //  (long notAliveLiableId, byte purposeId, int statusId, int constrainType, int owner)
        //{
        //    try
        //    {
        //        Dictionary<string, object> dic =
        //            cBLL.GetCustomConstrain_LiableNotAlive(notAliveLiableId, purposeId, statusId, constrainType, owner, API_HelperFunctions.Get_DeviceInfo());
        //        return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
        //    }
        //}

        //public HttpResponseMessage Get_CountryConstrainLiableNotAlive_Activation
        // (int active ,long notAliveLiableId, byte purposeId, int statusId, int constrainType, int owner)
        //{
        //    try
        //    {
        //        Dictionary<string, object> dic =
        //            cBLL.GetConstrain_LiableNotAlive_Activation(notAliveLiableId, purposeId, statusId, constrainType, owner, API_HelperFunctions.Get_DeviceInfo());
        //        return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
        //    }
        //}

        ////************************************************************


        //public HttpResponseMessage PutDelete_CountryConstrain(List<DeleteParameters> deletedConstrains)
        //{
        //    try
        //    {
        //        Dictionary<string, object> dic = cBLL.DeleteConstrains(deletedConstrains, API_HelperFunctions.Get_DeviceInfo());
        //        return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), "");
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
        //    }
        //}

        ////***********************************************************//

        //public HttpResponseMessage PutUpdate_CountryConstrainActive(int active , Ex_CountryConstrainDTO Dto)
        //{
        //    //EDIT
        //    try
        //    {
        //        Dictionary<string, object> dic = cBLL.Update_Activation(Dto, API_HelperFunctions.Get_DeviceInfo());
        //        return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
        //    }

        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
        //    }
        //}
    }
}