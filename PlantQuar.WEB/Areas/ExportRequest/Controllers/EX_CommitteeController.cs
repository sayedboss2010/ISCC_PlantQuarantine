using PlantQuar.DTO.DTO.Admin;
using PlantQuar.DTO.DTO.ExportRequest;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using PlantQuar.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace PlantQuar.WEB.Areas.ExportRequest.Controllers
{
    public class EX_CommitteeController : BaseController

    {
        string apiName = "EX_Committee_API";

        // GET: ExportRequest/EX_Committee
        public ActionResult Index(long committeeId, long requestId, byte CommitteeType_ID)
        {
            //get request number
            //emab

            // Fill Committee Name
            var res_CommitteeType = APIHandeling.getData(apiName + "?Lst=1");
            var lst_CommitteeType = res_CommitteeType.Content.ReadAsAsync<List<CustomOptionShortId>>().Result;

            ViewBag.CommitteeTypee_ID = lst_CommitteeType.ToList();

            ViewBag.CommitteeId = committeeId;
            ViewBag.checkRequestId = requestId;

            var res_reqNumber = APIHandeling.getData(apiName + "?num=1&requestId=" + requestId);
            var creationDate = APIHandeling.getData(apiName + "?num=1&create=1&requestId=" + requestId);
            //var res_reqNumber = APIHandeling.getData("Export_CheckRequest?requestId=" + requestId);
            var checkReqNum = res_reqNumber.Content.ReadAsAsync<string>().Result;
            ViewBag.data = checkReqNum;
            ViewBag.type = 1;
            ViewBag.CommitteeTypeId = CommitteeType_ID;
            ViewBag.CommitteeType_ID = new SelectList(lst_CommitteeType.ToList(), "Value", "DisplayText", CommitteeType_ID);
            ViewBag.sId = requestId;
            ViewBag.creationDate = creationDate.Content.ReadAsAsync<string>().Result;


            var Fees_Process = APIHandeling.getData("Mission_API?Outlet=5");
            var lst = Fees_Process.Content.ReadAsAsync<List<CustomOption>>().Result;
            //ViewBag.ddd = new SelectList(lst, "Value", "DisplayText");
            ViewBag.ddd = lst;

            return View();
        }
        #region sayed

        public JsonResult GetPR_User_Id(string FullName, long EmplyeeNo, long OutLet_ID)
        {
            try
            {

                var Fees_Process = APIHandeling.getData("AddEmployee_API?FullName=" + FullName + "&EmplyeeNo=" + EmplyeeNo + "&OutLet_ID=" + OutLet_ID);
                var lst = Fees_Process.Content.ReadAsAsync<List<User>>().Result;
                //  lst.RemoveAt(0);
                return Json(lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetPR_User_Id");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }


        [HttpPost]
        public JsonResult Save_Committe(byte? CommitteeType_ID, long? Committe_Id, long? Ex_CheckRequest_Id,
    DateTime? Check_Date, DateTime? Delegation_Date, TimeSpan? StartTime, TimeSpan? EndTime, List<EX_EmployeeDTO> emp_Dto)
        {

            EX_CommitteeDTO model = new EX_CommitteeDTO();
            model.ExCheckRequest_ID = (long)Ex_CheckRequest_Id;
            model.CommitteeType_ID = (byte)CommitteeType_ID;
            model.Delegation_Date = Delegation_Date;
            //model.Check_Date = (DateTime)Check_Date;
            model.StartTime = (TimeSpan)StartTime;
            model.EndTime = (TimeSpan)EndTime;
            if (CommitteeType_ID == 2)
            {
                model.IsApproved = true;

            }
            else
            {
                model.IsApproved = null;
            }

            model.Status = false;
            model.OperationType = 73;
            model.com_emp = emp_Dto;

            model.User_Creation_Id = (short)Session["UserId"];
            model.User_Creation_Date = DateTime.Now;
            dynamic res;
            if (Committe_Id > 0)
            {
                model.ID = (long)Committe_Id;
                //edit
                res = APIHandeling.Put(apiName, model);
            }
            else
            {
                //create
                res = APIHandeling.Post(apiName, model);
            }

            return ((int)res.StatusCode != 409) ? Json(new { Result = "OK", Record = model })
              : Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });

            //#region اللجنة
            //RequestCommitteeDTO comm = new RequestCommitteeDTO();
            //comm.ImCheckRequest_ID = CheckRequest_Id;
            //comm.CommitteeType_ID = CommitteeType_ID;
            //comm.IsApproved = true;
            //comm.Status = false;
            //comm.Delegation_Date = (DateTime)Delegation_Date;
            //comm.StartTime = (TimeSpan)StartTime;
            //comm.EndTime = (TimeSpan)EndTime;
            //User_Session Current = User_Session.GetInstance;
            //comm.User_Creation_Id = (short)Session["UserId"];
            //comm.User_Creation_Date = DateTime.Now;
            //#endregion

            //comm.com_emp = emp_Dto;
            ////// النوبتجية
            ////List<RequestCommittee_ShiftDTO> Shift_Data = new List<RequestCommittee_ShiftDTO>();
            ////Shift_Data = Session["listshiftTimingsession"] as List<RequestCommittee_ShiftDTO>;

            ////comm.List_Committee_Shift = Shift_Data;

       

            //var res2 = APIHandeling.Put(api + "?req=1", comm);
            //var list = res2.Content.ReadAsAsync<Dictionary<string, object>>().Result;
            //return Json("Exist", JsonRequestBehavior.AllowGet);

        }

        #endregion
    }
}