using PlantQuar.BLL.BLL.Import_Custody;
using PlantQuar.DTO.DTO.Import_Custody;
using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace PlantQuar.API.Controllers.Import_Custody
{
    public class Im_CustodyPlace_APIController : ApiController
    {
        Im_CustodyPlace_BLL cBLL = new Im_CustodyPlace_BLL();

        //Get Count AnalysisLab
        public HttpResponseMessage Getm(int m)
        {
            //string host = Dns.GetHostName();
            //IPHostEntry ip = Dns.GetHostEntry(host);
            //string IP=ip.AddressList[0].ToString();
            // var s = Dns.GetHostEntry(Dns.GetHostName());
            //Request.ServerVariables["LOCAL_ADDR"];
            string ipAddress = HttpContext.Current.Request.UserHostAddress;
            return Request.CreateResponse(ipAddress);
        }
        public HttpResponseMessage GetIm_CustodyPlaceCount()
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetCount(API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        //Get List AnalysisLab
        public HttpResponseMessage GetIm_CustodyPlace(int pageSize, int index)
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
        // Get AnalysisLab by Name 
        public HttpResponseMessage GetIm_CustodyPlaceName(string permissionId, int pageSize, int index)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.GetAll(permissionId, pageSize, index, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        //Post Create AnalysisLab
        public HttpResponseMessage PostCreateIm_CustodyPlace(Im_CustodyPlace_DTO Dto)
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

        //Put Update AnalysisLab
        public HttpResponseMessage PutUpdateIm_CustodyPlace(Im_CustodyPlace_DTO Dto)
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

        //Put Delete AnalysisLab
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
    }
}
