using PlantQuar.BLL.BLL.Admin;
using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;

namespace PlantQuar.API.Controllers.Admin
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class userPriviledge_ApiController : ApiController
    {
        userPriviledgeBLL cBLL = new userPriviledgeBLL();
        public HttpResponseMessage Get_UserPriviledge(int userID, int menuId, int modelID, int GroupId)
         
        {
            try
            {
                Dictionary<string, object> dic =
                    cBLL.getUserPermisions(userID, menuId, modelID, GroupId, API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}