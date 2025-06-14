using PlantQuar.BLL.BLL.DataEntry.Fees;
using PlantQuar.DTO.DTO.DataEntry.Fees;
using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PlantQuar.API.Controllers.DataEntry.Fees
{

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class Im_Fees_Actions_APIController : ApiController
    {

        Im_Fees_ActionsBLL cBLL = new Im_Fees_ActionsBLL();


        public HttpResponseMessage Get_UserPriviledge()

        {

            try
            {
                Dictionary<string, object> dic =
                    cBLL.Find_Fees_Request_Actions(API_HelperFunctions.Get_DeviceInfo());

               // Dictionary<string, object> dic2 = cBLL.FillDrop_Fees_Money_List(API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"] );
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        //new task
        public HttpResponseMessage GetFees_Money_List(int index)
        {
            try
            {
                Dictionary<string, object> dic = cBLL.FillDrop_Fees_Money_List(API_HelperFunctions.Get_DeviceInfo());
                return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }



        public HttpResponseMessage InsertFeesMoney(List<Fees_ActionDTO> Dto)
        {

            Dictionary<string, object> dic = cBLL.Insert(Dto, API_HelperFunctions.Get_DeviceInfo());    //send opt with carred data(Id,LoginName,Password,List_Menu) to bll
            return Request.CreateResponse(API_HelperFunctions.getStatusCode(int.Parse(dic["state_Code"].ToString())), dic["obj"]);

        }





    }

}
