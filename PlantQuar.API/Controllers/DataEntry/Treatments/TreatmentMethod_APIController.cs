
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
 
using System.Web.Http.Cors;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.BLL.BLL.DataEntry.Treatments;
using PlantQuar.DTO.DTO.DataEntry.Treatments;

namespace PlantQuar.API.Controllers.DataEntry.Treatments
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TreatmentMethod_APIController : ApiController
    {
        TreatmentMethodBLL cBLL = new TreatmentMethodBLL();

        //Get TreatmentMethod Count
        public HttpResponseMessage GetTreatmentMethodCount()
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

        //Get TreatmentMethod List
        public HttpResponseMessage GetTreatmentMethodList(int pageSize, int index)
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


        public HttpResponseMessage GetTreatmentMethodList_TreatmentType(int TreatmentType_ID)
        {
            try
            {
                List<string> device_data = API_HelperFunctions.Get_DeviceInfo();
                Dictionary<string, object> dic = cBLL.GetAllBy_TreatmentType(TreatmentType_ID, device_data);
                //for android group
                if (!bool.Parse(device_data[1]))
                {
                    //android
                    return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic);
                }
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        //Find TreatmentMethod by ID
        public HttpResponseMessage GetObjectById(int Id)
        {
                   Dictionary<string, object> dic = cBLL.Find(Id, API_HelperFunctions.Get_DeviceInfo());
            return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
        }

        //Get TreatmentMethod List by ArName & EnName 
        // GET: api/TreatmentMethod/GetTreatmentMethodName
        public HttpResponseMessage GetTreatmentMethodName(string arName, string enName, int pageSize, int index, string jtSorting)
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

        //PostCreate TreatmentMethod 
        //GET Insert A: api/TreatmentMethod/PostCreateTreatmentMethod
        public HttpResponseMessage PostCreateTreatmentMethod(TreatmentMethodDTO Dto )
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

        //PutUpdate TreatmentMethod
        public HttpResponseMessage PutUpdateTreatmentMethod(TreatmentMethodDTO Dto )
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

        //PutDelete TreatmentMethod
        public HttpResponseMessage PutDeleteTreatmentMethod(int delete, DeleteParameters Dto )
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

        public HttpResponseMessage GetTreatmentMethod_List(short methodList)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.FillMethod_List(API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage GetTreatmentMethodByTypeId(int TreatmentTypeId)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetTreatmentMethodByTypeId(TreatmentTypeId);
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        //Get TreatmentType List DropDownList
        public HttpResponseMessage GetTreatmentType_List(int List )
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

        //Get TreatmentType Create Update DropDownList
        public HttpResponseMessage GetTreatmentType_AddEdit(int AddEdit )
        {
            try
            {
                   List<string> device_data = API_HelperFunctions.Get_DeviceInfo();
                Dictionary<string, object> dic = cBLL.FillDrop_AddEdit(device_data);
                //for android group
                if (!bool.Parse(device_data[1]))
                {
                    //android
                    return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic);
                }
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        //nora
        public HttpResponseMessage GetID_TreatmentType(int ID_TreatmentType)
        {
            try
            {
                List<string> device_data = API_HelperFunctions.Get_DeviceInfo();
                Dictionary<string, object> dic = cBLL.GetID_TreatmentType(ID_TreatmentType, device_data);
                //for android group
                if (!bool.Parse(device_data[1]))
                {
                    //android
                    return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic);
                }
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }




        public HttpResponseMessage GetTreatmentMaterialList(long TreatmentMethods_ID, int pageSize, int index)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetAll(TreatmentMethods_ID, pageSize, index, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        //PutUpdate TreatmentMaterial
        public HttpResponseMessage PostInsertTreatmentMaterial(int Insert ,TreatmentMaterialDTO Dto)
        {
            //EDIT
            try
            {
                Dictionary<string, object> dic = cBLL.InsertTreatmentMaterial(Dto, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage PostUpdateTreatmentMaterial(int Update, TreatmentMaterialDTO Dto)
        {
            //EDIT
            try
            {
                Dictionary<string, object> dic = cBLL.UpdateTreatmentMaterial(Dto, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }









    }
}
