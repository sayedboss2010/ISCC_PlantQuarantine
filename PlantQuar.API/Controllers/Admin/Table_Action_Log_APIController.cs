using PlantQuar.BLL.BLL.Admin;
using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PlantQuar.API.Controllers.Admin
{
    public class Table_Action_Log_APIController : ApiController
    {

        Table_Action_LogBLL cBLL = new Table_Action_LogBLL();




        public HttpResponseMessage GetAllTablesLogged()
        {


            try
            {
                Dictionary<string, object> dic =
                    cBLL.getAllTableNameLog( API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"] );
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }   
        public HttpResponseMessage GetAllOperationLogged(decimal id,int tableName)
        {


            try
            {
                Dictionary<string, object> dic =
                    cBLL.getLogBYID(id,tableName, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"] );
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
