using PlantQuar.BLL.BLL.Export_Constrains;

using PlantQuar.DTO.DTO.DataEntry.Committees;
using PlantQuar.DTO.DTO.Export_Constrains;
using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PlantQuar.API.Controllers.ExportConstrains
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class Ex_ConstrainActivation_APIController : ApiController
    {
        Ex_ConstrainActivationBLL cBLL = new Ex_ConstrainActivationBLL();

    
        public HttpResponseMessage GetCommitteeTypeList(int pageSize, int index)
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

        // GET: api/CommitteeResult/GetAnalysisCommitteeTypeName
        //Get CommitteeType List by ArName & EnName
        public HttpResponseMessage GetCommitteeTypeName(long Item_ShortName, long catId, int constrainType, int owner, int pageSize, int index, string jtSorting)
        
            {
                try
                {
                    Dictionary<string, object> dic = cBLL.GetAll(Item_ShortName, catId, constrainType, owner, pageSize, index, jtSorting, API_HelperFunctions.Get_DeviceInfo());
                    return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic);
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
                }
            }

            //Find CommitteeType Obj
            public HttpResponseMessage GetObjectById(int Id)
            {
                Dictionary<string, object> dic = cBLL.Find(Id, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
        public HttpResponseMessage PostCommitteeType(CommitteeTypeDTO Dto)
        {
            //EDIT
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

        //Put Update CommitteType
        public HttpResponseMessage PutUpdateCommitteeType(CommitteeTypeDTO Dto)
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

            //Put Delete CommitteType
            public HttpResponseMessage PutDeleteCommitteeType(int delete, DeleteParameters Dto)
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
            //Get CommitteeType List DDL
            public HttpResponseMessage GetCommitteeType_List(int Lst)
            {
                try
                {
                    //Dictionary<string, object> dic = cBLL.
                    //return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);

                    List<string> device_data = API_HelperFunctions.Get_DeviceInfo();
                    Dictionary<string, object> dic = cBLL.FillCommitteeType_List(Lst, device_data);
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
            //Get CommitteeType Create Update
            public HttpResponseMessage GetCommitteeType_AddEdit(int AddEdit)
            {
                try
                {
                    Dictionary<string, object> dic = cBLL.FillCommitteeType_AddEdit(API_HelperFunctions.Get_DeviceInfo());
                    return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
                }
            }

        public HttpResponseMessage PutUpdate_CountryConstrainActive(int active, Ex_CountryConstrainDTO Dto)
        {
            //EDIT
            try
            {
                Dictionary<string, object> dic = cBLL.Update_Activation(Dto, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
} 






