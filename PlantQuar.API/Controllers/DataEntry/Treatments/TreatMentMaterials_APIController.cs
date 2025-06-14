using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PlantQuar.BLL.BLL.DataEntry.Items.Agriculture_Classfication;
using PlantQuar.DTO.DTO.DataEntry.Items.Agriculture_Classfication;
using PlantQuar.DTO.HelperClasses;

using PlantQuar.BLL.BLL.DataEntry.Treatments;
using PlantQuar.DTO.DTO.DataEntry.Treatments;
using PlantQuar.WEB.App_Start;
using PlantQuar.DTO.DTO.DataEntry.Fees;
using System.Linq;
//using AutoMapper;
using PlantQuar.DAL;

namespace PlantQuar.API.Controllers.DataEntry.Treatments
{
    public class TreatMentMaterials_APIController : ApiController
    {
        TreatmentMaterialsBLL cBLL = new TreatmentMaterialsBLL();

        //Get TreatmentMaterial Count
        public HttpResponseMessage GetTreatmentMaterialCount()
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

        //Get TreatmentMaterial List
        public HttpResponseMessage GetTreatmentMaterialList(int pageSize, int index)
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

        //Get TreatmentMaterial List by ArName & EnName 
        // GET: api/TreatmentMaterial/GetTreatmentMaterialName
        public HttpResponseMessage GetTreatmentMaterialName(string arName, string enName, int pageSize, int index, string jtSorting)
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

        //PostCreate TreatmentMaterial 
        //GET Insert A: api/TreatmentMaterial/PostCreateTreatmentMaterial
        public HttpResponseMessage PostCreateTreatmentMaterial(TreatmentMaterialDTO Dto)
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

        //PutUpdate TreatmentMaterial
        public HttpResponseMessage PutUpdateTreatmentMaterial(TreatmentMaterialDTO Dto)
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

        //PutDelete TreatmentMaterial
        public HttpResponseMessage PutDeleteTreatmentMaterial(int delete, DeleteParameters Dto)
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
