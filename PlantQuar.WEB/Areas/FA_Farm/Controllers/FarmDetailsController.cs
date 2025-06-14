using PlantQuar.DTO.DTO.Farm.FarmRequest;
using PlantQuar.WEB.App_Start;
using PlantQuar.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.FA_Farm.Controllers
{
    public class FarmDetailsController : BaseController
    {
        // GET: FA_Farm/FarmDetails
        public ActionResult Index(long farmCountryRequestId)
        {
            var res = APIHandeling.getData("FarmDetails_API?farmCountryRequestId=" + farmCountryRequestId);
            var model = res.Content.ReadAsAsync<Farm_Get_Data_ResultDTO>().Result;//object
            return View(model);
        }

        public ActionResult acceptRequest(Farm_Get_Data_ResultDTO model)
        {

            //---add new row in export committee
            if (model.IsAcceppted == true)
            {
                Farm_CommitteeDTO newFarmCommittee = new Farm_CommitteeDTO();
                newFarmCommittee.Farm_Request_ID = model.requestId;
                newFarmCommittee.CommitteeType_ID = 5;
                newFarmCommittee.IsApproved = null;
                newFarmCommittee.Status = null;
                newFarmCommittee.User_Creation_Date = DateTime.Now;
                User_Session Current = User_Session.GetInstance;
                newFarmCommittee.FarmsData_ID = model.farmId;
                newFarmCommittee.User_Creation_Id = (short)Session["UserId"];

                var res = APIHandeling.Post("FarmDetails_API?newCreate=1", newFarmCommittee);

                //#region Log_Farm
                //var dto2 = new DAL.Table_Action_Log_Farm();
                //dto2.ID_TableActionValue = model.requestId;
                //dto2.Farm_ID = model.farmId;
                //dto2.User_Creation_Id = (short)Session["UserId"];
                //dto2.User_Creation_Date = DateTime.Now;
                //if (IsAcceppted == true)
                //{
                //    dto2.ID_Table_Action = 36;
                //    dto2.NOTS = " تم الموافقة علي المحطة ";
                //}
                //else
                //{
                //    dto2.ID_Table_Action = 37;
                //    dto2.NOTS = " تم رفض المحطة  ";
                //}
                //dto2.User_Type_ID = 127;
                //dto2.Type_log_ID = 135;
                //APIHandeling.Put("Log_CheckRequest_API?Station_Log=1", dto2);
                //#endregion
                //eman
                //Update isApproved 
            }

            APIHandeling.Put("FarmDetails_API?approve=1", model);

            return RedirectToAction("Index", "FarmRequest");
        }
    }
}