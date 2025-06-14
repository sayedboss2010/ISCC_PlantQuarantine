using PlantQuar.BLL.BLL.Reports.Engineers;
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

namespace PlantQuar.API.Controllers.Reports
{

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class EngineersReportController : ApiController
    {
        EngineersReportBLL cBLL = new EngineersReportBLL();


        public HttpResponseMessage Get_PlantCategory_List(string from, string to, int? operationType, short? empId, short? govID, short? centerID, short? villageId)
        {
            try
            {

                from = from.Split(' ')[0];
                to = to.Split(' ')[0];
               // DateTime datefrom = DateTime.Parse(from,
               // System.Globalization.CultureInfo.CreateSpecificCulture("en-US"));
                DateTime datefrom = DateTime.ParseExact(from, "dd/MM/yyyy",
                                                  System.Globalization.CultureInfo.InvariantCulture);
                DateTime dateto = DateTime.ParseExact(to, "dd/MM/yyyy",
                                                 System.Globalization.CultureInfo.InvariantCulture);
                // DateTime dateto = DateTime.Parse(to,
               // System.Globalization.CultureInfo.CreateSpecificCulture("en-US"));

                Dictionary<string, object> dic = cBLL.GetEngineers(datefrom,dateto, operationType, empId, govID, centerID, villageId, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}