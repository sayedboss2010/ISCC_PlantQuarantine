using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Collections.Generic;

using PlantQuar.DTO.HelperClasses;
using PlantQuar.BLL.BLL.DataEntry.Outlets;
using PlantQuar.DTO.DTO.DataEntry.Outlets;
using PlantQuar.DTO.DTO.DataEntry.GovToVillage;

namespace PlantQuar.API.Controllers.DataEntry.Outlets
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class Outlet_APIController : ApiController
    {
        OutletBLL cBLL = new OutletBLL();
        public HttpResponseMessage GetOutletCount()
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
        public HttpResponseMessage GetOutletList(int pageSize, int index )
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

        public HttpResponseMessage GetObjectById(int Id )
        {
            Dictionary<string, object> dic = cBLL.Find(Id, API_HelperFunctions.Get_DeviceInfo());
            return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
        }
        // GET: api/Outlet/GetOutletName
        public HttpResponseMessage GetOutletName(string arName, string enName, int pageSize, int index, string jtSorting)
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

        //GET Insert A: api/Outlet/PostCreateOutlet
        public HttpResponseMessage PostCreateOutlet(OutletDTO Dto )
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

        public HttpResponseMessage PutUpdateOutlet(OutletDTO      Dto )
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
        public HttpResponseMessage PutDeleteOutlet(int delete, DeleteParameters obj )
        {
            //Delete            
            try
            {
                //DateTime _Date_Now = DateTime.Parse(obj._DateNow,
                //System.Globalization.CultureInfo.CreateSpecificCulture("en-US"));
                // Dictionary<string, object> dic = cBLL.Delete(obj.id, obj.Userid, _Date_Now);
                Dictionary<string, object> dic = cBLL.Delete(obj, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), "");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        //SARA
        //GET OUTLET BY GOVERNATE ID
        public HttpResponseMessage GetOutletByGovID(int govID )
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetOutLetByGovId(govID, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage GetOutletByPortID(int Port_National_ID)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetOutLetByPort(Port_National_ID, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage GetOutletByGovID_GeneralAdmin(int govID, int generalAdminId)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetOutLetByGovId_GeneralAdmin(govID, generalAdminId, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage GetOutletBy_GeneralAdmin(int generalAdminId)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetOutLetByGeneralAdmin(generalAdminId, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage GetOutletByCenter_GeneralAdmin(int centerID, int generalAdminId)
        {
            try
            {
                if(generalAdminId > 0)
                {
                    Dictionary<string, object> dic = cBLL.GetOutLetByCenter_GeneralAdmin(centerID, generalAdminId, API_HelperFunctions.Get_DeviceInfo());
                    return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
                }
                else
                {
                    Dictionary<string, object> dic = cBLL.GetOutLetByCenter(centerID, API_HelperFunctions.Get_DeviceInfo());
                    return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        //END SARA

        public HttpResponseMessage GetOutletResult_AddEdit(int AddEdit)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.FillDrop_Edit(API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        //center outlet

        public HttpResponseMessage GetCentersList(long outlet_ID, int pageSize, int index)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetAll(outlet_ID, pageSize, index, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

      

        public HttpResponseMessage PostUpdateCenter_Outlet (int Update, Center_OutletDTO Dto)
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

      
    }
}