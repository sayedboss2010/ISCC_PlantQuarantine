using PlantQuar.DTO.DTO.Admin;
using PlantQuar.DTO.DTO.DataEntry.Fees;
using PlantQuar.DTO.DTO.Farm.FarmRequest;
using PlantQuar.DTO.DTO.Import.CheckRequests;
using PlantQuar.DTO.DTO.Import.IM_Committee;
using PlantQuar.DTO.DTO.Import.Permissions;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace PlantQuar.WEB.Areas.Committees.Controllers
{
    public class ImCommitteeController : Controller
    {
        // GET: Committees/ImCommittee
        string api = "ImRequestCommittee_API";
        public ActionResult Index(long? requestId,long? OutLet_ID)
        {
            var res = APIHandeling.getData(api + "?requestId=" + requestId);

            var req = res.Content.ReadAsAsync<Im_CheckRequestDTO>().Result;
            ViewBag.creationDate = req.User_Creation_Date;
            ViewBag.requestNumber = req.CheckRequest_Number;
            ViewBag.requestId = requestId;
            ViewBag.OutLet_ID = OutLet_ID;
            var req2 = APIHandeling.getData(api + "?reqComm=1&requestId=" + requestId);

            var res2 = req2.Content.ReadAsAsync<bool>().Result;
            if (res2 == true)
            {
                ViewBag.ifRequestCommittee = 1;
                // ViewBag.CommitteeId =0;
            }
            else
            {
                ViewBag.ifRequestCommittee = 2;
                //ViewBag.CommitteeId = ReqCommitteeId;
            }
            var resItem = APIHandeling.getData("Im_CheckRequests_API?ImCheckRequest_Number=" + req.CheckRequest_Number);
            var Lst = resItem.Content.ReadAsAsync<ImRequestDetailsDTO>().Result;
            return View(Lst);
        }

        [HttpPost]
        public JsonResult Save_Committe(byte? CommitteeType_ID, long? CheckRequest_Id,
          DateTime? Check_Date, DateTime? Delegation_Date, TimeSpan? StartTime, TimeSpan? EndTime)
        {
            Session["CommitteeType_ID"] = CommitteeType_ID.ToString();
            if (CommitteeType_ID == 11)
            {

                Im_RequestCommitteeDTO comm = new Im_RequestCommitteeDTO();
                comm.ImCheckRequest_ID = CheckRequest_Id;
                comm.CommitteeType_ID = 11;

                comm.Status = false;
                comm.Delegation_Date = (DateTime)Delegation_Date;
                comm.StartTime = (TimeSpan)StartTime;
                comm.EndTime = (TimeSpan)EndTime;
                User_Session Current = User_Session.GetInstance;
                comm.User_Creation_Id = (short)Session["UserId"];
                comm.User_Creation_Date = DateTime.Now;
                //comm.== uow.Repository<Object>().GetNextSequenceValue_Long("Im_Committee_seq");
                var res2 = APIHandeling.Put(api + "?req=1", comm);
                var list = res2.Content.ReadAsAsync<Dictionary<string, object>>().Result;
                ViewBag.CommitteeId = list["ID"].ToString();
                Session["reqComm_ID"] = list["ID"].ToString();
                return Json("Exist", JsonRequestBehavior.AllowGet);

                // }
                // }


            }
            else if (CommitteeType_ID == 13)
            {

                Im_RequestCommitteeDTO comm = new Im_RequestCommitteeDTO();
                comm.ImCheckRequest_ID = CheckRequest_Id;
                comm.CommitteeType_ID = 13;

                comm.Status = false;
                comm.Delegation_Date = (DateTime)Delegation_Date;
                comm.StartTime = (TimeSpan)StartTime;
                comm.EndTime = (TimeSpan)EndTime;
                User_Session Current = User_Session.GetInstance;
                comm.User_Creation_Id = (short)Session["UserId"];
                comm.User_Creation_Date = DateTime.Now;
                //comm.== uow.Repository<Object>().GetNextSequenceValue_Long("Im_Committee_seq");
                var res2 = APIHandeling.Put(api + "?req=1", comm);
                var list = res2.Content.ReadAsAsync<Dictionary<string, object>>().Result;
                ViewBag.CommitteeId = list["ID"].ToString();
                Session["reqComm_ID"] = list["ID"].ToString();
                return Json(res2, JsonRequestBehavior.AllowGet);




            }
            //else if (CommitteeType_ID == 7)
            //{
            //    //Im_PermissionItem_Division_Custody_DismissCommittee
            //    //Im_RequestCommittee_Id

            //    //var req2 = APIHandeling.getData(api + "?RequestComm=1&requestId=" + CheckRequest_Id);

            //    //var Im_RequestCommittee_Id = req2.Content.ReadAsAsync<long>().Result;
            //    //if(Im_RequestCommittee_Id != 0)
            //    //{
            //    //    Im_PermissionItem_Division_Custody_DismissCommitteeDTO comm = new Im_PermissionItem_Division_Custody_DismissCommitteeDTO();
            //    //    comm.Im_RequestCommittee_Id = Im_RequestCommittee_Id;
            //    //comm.Status = false;
            //    //comm.IsApproved = false;
            //    //    // comm.co = 11;
            //    //    //Im_PermissionItem_Division_Custody_Id ????
            //    //    User_Session Current = User_Session.GetInstance;
            //    //comm.User_Creation_Id = (short)Session["UserId"];
            //    //comm.User_Creation_Date = DateTime.Now;
            //    //comm.com_emp = dataToSend;
            //    //    var res2 = APIHandeling.Post(api + "?dismiss=1", comm);
            //    //    return Json("success", JsonRequestBehavior.AllowGet);
            //    //}
            //    //else
            //    //{
            //    //    return Json("ReqCommNotFound", JsonRequestBehavior.AllowGet);
            //    //}
            //    Im_RequestCommitteeDTO comm = new Im_RequestCommitteeDTO();
            //    comm.ImCheckRequest_ID = CheckRequest_Id;
            //    comm.CommitteeType_ID = 7;
            //    // comm.ID = (long)Committe_Id;
            //    comm.Status = false;
            //    comm.Delegation_Date = (DateTime)Delegation_Date;
            //    comm.StartTime = (TimeSpan)StartTime;
            //    comm.EndTime = (TimeSpan)EndTime;
            //    User_Session Current = User_Session.GetInstance;
            //    comm.User_Creation_Id = (short)Session["UserId"];
            //    comm.User_Creation_Date = DateTime.Now;
            //    comm.com_emp = dataToSend;
            //    var res2 = APIHandeling.Post(api + "?dismiss=1", comm);
            //    var id = res2.Content.ReadAsAsync<long>().Result;
            //    return Json(id, JsonRequestBehavior.AllowGet);


            //}
            //else if (CommitteeType_ID == 8)
            //{
            //    //Im_PermissionItem_Division_Custody_ReceiveCommittee
            //    //Im_RequestCommittee_Id   Im_PermissionItem_Division_Custody_DismissCommittee_Id
            //    //var req2 = APIHandeling.getData(api + "?RequestComm=1&requestId=" + CheckRequest_Id);

            //    //var Im_RequestCommittee_Id = req2.Content.ReadAsAsync<long>().Result;
            //    //if (Im_RequestCommittee_Id != 0)
            //    //{
            //    //    var req3 = APIHandeling.getData(api + "?DismissComm=1&requestCommId=" + Im_RequestCommittee_Id);

            //    //    var Im_PermissionItem_Division_Custody_DismissCommittee_Id = req3.Content.ReadAsAsync<long>().Result;
            //    //    if(Im_PermissionItem_Division_Custody_DismissCommittee_Id != 0)
            //    //    {
            //    //        Im_PermissionItem_Division_Custody_ReceiveCommitteeDTO comm = new Im_PermissionItem_Division_Custody_ReceiveCommitteeDTO();
            //    //        comm.Im_RequestCommittee_Id = Im_RequestCommittee_Id;
            //    //        // comm.co = 11;
            //    //        comm.Im_PermissionItem_Division_Custody_DismissCommittee_Id = Im_PermissionItem_Division_Custody_DismissCommittee_Id;
            //    //        comm.IsApproved = false;
            //    //        comm.Status = false;
            //    //        comm.GrossWeight = 0;
            //    //        User_Session Current = User_Session.GetInstance;
            //    //        comm.User_Creation_Id = (short)Session["UserId"];
            //    //        comm.User_Creation_Date = DateTime.Now;
            //    //        comm.com_emp = dataToSend;
            //    //        var res2 = APIHandeling.Post(api + "?receive=1", comm);
            //    //        return Json("success", JsonRequestBehavior.AllowGet);
            //    //    }
            //    //    else
            //    //    {
            //    //        return Json("DismissCommNotFound", JsonRequestBehavior.AllowGet);
            //    //    }

            //    //}
            //    //else
            //    //{
            //    //    return Json("ReqCommNotFound", JsonRequestBehavior.AllowGet);
            //    //}
            //    Im_RequestCommitteeDTO comm = new Im_RequestCommitteeDTO();
            //    comm.ImCheckRequest_ID = CheckRequest_Id;
            //    comm.CommitteeType_ID = 8;
            //    // comm.ID = (long)Committe_Id;
            //    comm.Status = false;
            //    comm.Delegation_Date = (DateTime)Delegation_Date;
            //    comm.StartTime = (TimeSpan)StartTime;
            //    comm.EndTime = (TimeSpan)EndTime;
            //    User_Session Current = User_Session.GetInstance;
            //    comm.User_Creation_Id = (short)Session["UserId"];
            //    comm.User_Creation_Date = DateTime.Now;
            //    comm.com_emp = dataToSend;
            //    var res2 = APIHandeling.Post(api + "?receive=1", comm);
            //    var id = res2.Content.ReadAsAsync<long>().Result;
            //    return Json(id, JsonRequestBehavior.AllowGet);
            //}
            //else if (CommitteeType_ID == 9)
            //{
            //    //Im_Execution
            //    //var req2 = APIHandeling.getData(api + "?RequestComm=1&requestId=" + CheckRequest_Id);

            //    //var Im_RequestCommittee_Id = req2.Content.ReadAsAsync<long>().Result;
            //    //if (Im_RequestCommittee_Id != 0)
            //    //{
            //    //    Im_ExecutionDTO comm = new Im_ExecutionDTO();
            //    //    comm.Im_RequestCommittee_Id = Im_RequestCommittee_Id;
            //    //    // comm.co = 11;
            //    //    //Im_PermissionItem_Division_Custody_Id ????
            //    //    User_Session Current = User_Session.GetInstance;
            //    //    comm.User_Creation_Id= (short)Session["UserId"];

            //    //    comm.com_emp = dataToSend;
            //    //    var res2 = APIHandeling.Post(api + "?Exec=1", comm);
            //    //    return Json("success", JsonRequestBehavior.AllowGet);
            //    //}
            //    //else
            //    //{
            //    //    return Json("ReqCommNotFound", JsonRequestBehavior.AllowGet);
            //    //}
            //    Im_RequestCommitteeDTO comm = new Im_RequestCommitteeDTO();
            //    comm.ImCheckRequest_ID = CheckRequest_Id;
            //    comm.CommitteeType_ID = 9;
            //    // comm.ID = (long)Committe_Id;
            //    comm.Status = false;
            //    comm.Delegation_Date = (DateTime)Delegation_Date;
            //    comm.StartTime = (TimeSpan)StartTime;
            //    comm.EndTime = (TimeSpan)EndTime;
            //    User_Session Current = User_Session.GetInstance;
            //    comm.User_Creation_Id = (short)Session["UserId"];
            //    comm.User_Creation_Date = DateTime.Now;
            //    comm.com_emp = dataToSend;
            //    var res2 = APIHandeling.Post(api + "?Exec=1", comm);
            //    var id = res2.Content.ReadAsAsync<long>().Result;
            //    return Json(id, JsonRequestBehavior.AllowGet);

            //}
            else
            {
                return Json("fail");
            }

        }

        [HttpPost]
        public JsonResult Save_Employee(List<EmployeeDTO> dataToSend, long? Committe_Id, byte? CommitteeType_ID)
        {
            if (CommitteeType_ID == 11)
            {


                Im_RequestCommitteeDTO comm = new Im_RequestCommitteeDTO();
                comm.CommitteeType_ID = 11;
                //  comm.ID = ViewBag.CommitteeId;
                comm.ID = long.Parse(Session["reqComm_ID"].ToString());
                User_Session Current = User_Session.GetInstance;
                comm.User_Creation_Id = (short)Session["UserId"];
                comm.User_Creation_Date = DateTime.Now;
                comm.com_emp = dataToSend;
                var res2 = APIHandeling.Put(api + "?req1=1", comm);
                var lst = res2.Content.ReadAsAsync<Dictionary<string, object>>().Result;//object

                return Json("SavedEmpolyee", JsonRequestBehavior.AllowGet);
            }
            //else if(CommitteeType_ID==7)
            //{
            //    //Im_PermissionItem_Division_Custody_DismissCommittee
            //    //Im_RequestCommittee_Id

            //    //var req2 = APIHandeling.getData(api + "?RequestComm=1&requestId=" + CheckRequest_Id);

            //    //var Im_RequestCommittee_Id = req2.Content.ReadAsAsync<long>().Result;
            //    //if(Im_RequestCommittee_Id != 0)
            //    //{
            //    //    Im_PermissionItem_Division_Custody_DismissCommitteeDTO comm = new Im_PermissionItem_Division_Custody_DismissCommitteeDTO();
            //    //    comm.Im_RequestCommittee_Id = Im_RequestCommittee_Id;
            //    //comm.Status = false;
            //    //comm.IsApproved = false;
            //    //    // comm.co = 11;
            //    //    //Im_PermissionItem_Division_Custody_Id ????
            //    //    User_Session Current = User_Session.GetInstance;
            //    //comm.User_Creation_Id = (short)Session["UserId"];
            //    //comm.User_Creation_Date = DateTime.Now;
            //    //comm.com_emp = dataToSend;
            //    //    var res2 = APIHandeling.Post(api + "?dismiss=1", comm);
            //    //    return Json("success", JsonRequestBehavior.AllowGet);
            //    //}
            //    //else
            //    //{
            //    //    return Json("ReqCommNotFound", JsonRequestBehavior.AllowGet);
            //    //}
            //    Im_RequestCommitteeDTO comm = new Im_RequestCommitteeDTO();
            //    comm.ImCheckRequest_ID = CheckRequest_Id;
            //    comm.CommitteeType_ID = 7;
            //   // comm.ID = (long)Committe_Id;
            //    comm.Status = false;
            //    comm.Delegation_Date = (DateTime)Delegation_Date;
            //    comm.StartTime = (TimeSpan)StartTime;
            //    comm.EndTime = (TimeSpan)EndTime;
            //    User_Session Current = User_Session.GetInstance;
            //    comm.User_Creation_Id = (short)Session["UserId"];
            //    comm.User_Creation_Date = DateTime.Now;
            //    comm.com_emp = dataToSend;
            //    var res2 = APIHandeling.Post(api + "?dismiss=1", comm);
            //    var id = res2.Content.ReadAsAsync<long>().Result;
            //    return Json(id, JsonRequestBehavior.AllowGet);


            //}
            //else if(CommitteeType_ID== 8)
            //{
            //    //Im_PermissionItem_Division_Custody_ReceiveCommittee
            //    //Im_RequestCommittee_Id   Im_PermissionItem_Division_Custody_DismissCommittee_Id
            //    //var req2 = APIHandeling.getData(api + "?RequestComm=1&requestId=" + CheckRequest_Id);

            //    //var Im_RequestCommittee_Id = req2.Content.ReadAsAsync<long>().Result;
            //    //if (Im_RequestCommittee_Id != 0)
            //    //{
            //    //    var req3 = APIHandeling.getData(api + "?DismissComm=1&requestCommId=" + Im_RequestCommittee_Id);

            //    //    var Im_PermissionItem_Division_Custody_DismissCommittee_Id = req3.Content.ReadAsAsync<long>().Result;
            //    //    if(Im_PermissionItem_Division_Custody_DismissCommittee_Id != 0)
            //    //    {
            //    //        Im_PermissionItem_Division_Custody_ReceiveCommitteeDTO comm = new Im_PermissionItem_Division_Custody_ReceiveCommitteeDTO();
            //    //        comm.Im_RequestCommittee_Id = Im_RequestCommittee_Id;
            //    //        // comm.co = 11;
            //    //        comm.Im_PermissionItem_Division_Custody_DismissCommittee_Id = Im_PermissionItem_Division_Custody_DismissCommittee_Id;
            //    //        comm.IsApproved = false;
            //    //        comm.Status = false;
            //    //        comm.GrossWeight = 0;
            //    //        User_Session Current = User_Session.GetInstance;
            //    //        comm.User_Creation_Id = (short)Session["UserId"];
            //    //        comm.User_Creation_Date = DateTime.Now;
            //    //        comm.com_emp = dataToSend;
            //    //        var res2 = APIHandeling.Post(api + "?receive=1", comm);
            //    //        return Json("success", JsonRequestBehavior.AllowGet);
            //    //    }
            //    //    else
            //    //    {
            //    //        return Json("DismissCommNotFound", JsonRequestBehavior.AllowGet);
            //    //    }

            //    //}
            //    //else
            //    //{
            //    //    return Json("ReqCommNotFound", JsonRequestBehavior.AllowGet);
            //    //}
            //    Im_RequestCommitteeDTO comm = new Im_RequestCommitteeDTO();
            //    comm.ImCheckRequest_ID = CheckRequest_Id;
            //    comm.CommitteeType_ID = 8;
            //    // comm.ID = (long)Committe_Id;
            //    comm.Status = false;
            //    comm.Delegation_Date = (DateTime)Delegation_Date;
            //    comm.StartTime = (TimeSpan)StartTime;
            //    comm.EndTime = (TimeSpan)EndTime;
            //    User_Session Current = User_Session.GetInstance;
            //    comm.User_Creation_Id = (short)Session["UserId"];
            //    comm.User_Creation_Date = DateTime.Now;
            //    comm.com_emp = dataToSend;
            //    var res2 = APIHandeling.Post(api + "?receive=1", comm);
            //    var id = res2.Content.ReadAsAsync<long>().Result;
            //    return Json(id, JsonRequestBehavior.AllowGet);
            //}
            //else if(CommitteeType_ID==9)
            //{
            //    //Im_Execution
            //    //var req2 = APIHandeling.getData(api + "?RequestComm=1&requestId=" + CheckRequest_Id);

            //    //var Im_RequestCommittee_Id = req2.Content.ReadAsAsync<long>().Result;
            //    //if (Im_RequestCommittee_Id != 0)
            //    //{
            //    //    Im_ExecutionDTO comm = new Im_ExecutionDTO();
            //    //    comm.Im_RequestCommittee_Id = Im_RequestCommittee_Id;
            //    //    // comm.co = 11;
            //    //    //Im_PermissionItem_Division_Custody_Id ????
            //    //    User_Session Current = User_Session.GetInstance;
            //    //    comm.User_Creation_Id= (short)Session["UserId"];

            //    //    comm.com_emp = dataToSend;
            //    //    var res2 = APIHandeling.Post(api + "?Exec=1", comm);
            //    //    return Json("success", JsonRequestBehavior.AllowGet);
            //    //}
            //    //else
            //    //{
            //    //    return Json("ReqCommNotFound", JsonRequestBehavior.AllowGet);
            //    //}
            //    Im_RequestCommitteeDTO comm = new Im_RequestCommitteeDTO();
            //    comm.ImCheckRequest_ID = CheckRequest_Id;
            //    comm.CommitteeType_ID = 9;
            //    // comm.ID = (long)Committe_Id;
            //    comm.Status = false;
            //    comm.Delegation_Date = (DateTime)Delegation_Date;
            //    comm.StartTime = (TimeSpan)StartTime;
            //    comm.EndTime = (TimeSpan)EndTime;
            //    User_Session Current = User_Session.GetInstance;
            //    comm.User_Creation_Id = (short)Session["UserId"];
            //    comm.User_Creation_Date = DateTime.Now;
            //    comm.com_emp = dataToSend;
            //    var res2 = APIHandeling.Post(api + "?Exec=1", comm);
            //    var id = res2.Content.ReadAsAsync<long>().Result;
            //    return Json(id, JsonRequestBehavior.AllowGet);

            //}
            else
            {
                return Json("fail");
            }

        }
        //shift timing
        [HttpPost]
        public JsonResult getShiftTiming(byte? dayType)
        {
            try
            {
                var res = APIHandeling.getData("ShiftTiming_API?daytype=" + dayType);
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "reqAnalysisType");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        public JsonResult getShiftTimingDetails(byte ID)
        {
            try
            {
                var res = APIHandeling.getData("ShiftTiming_API?timing=1&Id=" + ID);
                var shift = res.Content.ReadAsAsync<ShiftTimingDTO>().Result;
                return Json(shift, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "reqAnalysisType");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        //*********************************SARA*****************************//
        //Edit Eslam
        [HttpPost]
        public JsonResult getShiftTiming_Lst()
        {
            try
            {
                var res = APIHandeling.getData("Im_RequestCommittee_Shift_API?List=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "getShiftTiming_Lst");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        [HttpPost]
        public JsonResult getShiftTiming_Mony(byte shiftId)
        {
            try
            {
                var res = APIHandeling.getData("Im_RequestCommittee_Shift_API?shiftId=" + shiftId);
                var mony = res.Content.ReadAsAsync<Nullable<double>>().Result;
                return Json(new { Result = "OK", Options = mony }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "getShiftTiming_Lst");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        //shifts table
        //listshiftTiming
        [HttpPost]
        public JsonResult listshiftTiming
        (long Im_RequestCommittee_ID = 0, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = "")
        {
            try
            {
                if (Session["reqComm_ID"] != null)
                {
                    Im_RequestCommittee_ID = long.Parse(Session["reqComm_ID"].ToString());
                    ViewBag.requestId = Im_RequestCommittee_ID;
                    var res = APIHandeling.getData("Im_RequestCommittee_Shift_API?Im_RequestCommittee_ID=" + Im_RequestCommittee_ID
                        + "&pageSize=" + jtPageSize.ToString() + "&index=" + jtStartIndex.ToString());

                    var lst = res.Content.ReadAsAsync<Dictionary<string, object>>().Result;//object

                    var StatusCode = lst.ElementAt(0).Value;
                    var obj = lst.ElementAt(1).Value;

                    JavaScriptSerializer ser = new JavaScriptSerializer();
                    var myObj = ser.Deserialize<Dictionary<string, object>>(obj.ToString());

                    var count = myObj.ElementAt(0).Value;
                    var Lobj = myObj.ElementAt(1).Value;

                    return Json(new { Result = "OK", Records = Lobj, TotalRecordCount = count });
                }
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                if (ex.HResult == -2146233087)
                {
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RelatedData) });
                }
                else
                {
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "listshiftTiming");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        //CreateshiftTiming
        [HttpPost]
        public JsonResult CreateshiftTiming(Im_RequestCommittee_ShiftDTO model)
        {
            try
            {
                model.Im_RequestCommittee_ID = long.Parse(Session["reqComm_ID"].ToString());
                // ViewBag.FarmCommittee_ID = req;
                var allErrors = ModelState.Values.SelectMany(x => x.Errors).ToList();

                if (ModelState.IsValid)
                {
                    User_Session Current = User_Session.GetInstance;
                    model.User_Creation_Id = (short)Session["UserId"];
                    model.User_Creation_Date = DateTime.Now;
                    model.Amount = (decimal?)model.money;
                    //check Repeated Data
                    var res = APIHandeling.Post("Im_RequestCommittee_Shift_API", model);

                    return ((int)res.StatusCode != 409) ? Json(new { Result = "OK", Record = model })
                      : Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });
                }
                else
                {
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
            catch (Exception ex)
            {
                if (ex.HResult == -2146233087)
                {
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RelatedData) });
                }
                else
                {
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "CreateshiftTiming");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }
        //UpdateshiftTiming
        [HttpPost]
        public JsonResult UpdateshiftTiming(Im_RequestCommittee_ShiftDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    User_Session Current = User_Session.GetInstance;
                    model.User_Updation_Id = (short)Session["UserId"];
                    model.User_Updation_Date = DateTime.Now;
                    //check Repeated Data
                    var res = APIHandeling.Put("Im_RequestCommittee_Shift_API", model);
                    return ((int)res.StatusCode != 409) ? Json(new { Result = "OK" })
                      : Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });
                }
                else
                {
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
            catch (Exception ex)
            {
                if (ex.HResult == -2146233087)
                {
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RelatedData) });
                }
                else
                {
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "UpdateshiftTiming");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        //DeleteshiftTiming
        [HttpPost]
        public JsonResult DeleteshiftTiming(int ID)
        {
            try
            {
                DeleteParameters obj = new DeleteParameters();
                obj.id = ID;
                User_Session Current = User_Session.GetInstance;
                obj.Userid = (short)Session["UserId"];
                obj._DateNow = DateTime.Now;
                APIHandeling.Delete("Im_RequestCommittee_Shift_API", obj);

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                if (ex.HResult == -2146233087)
                {
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RelatedData) });
                }
                else
                {
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "DeleteshiftTiming");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }


        public JsonResult GetPR_User_Id(string FullName, long EmplyeeNo,long OutLet_ID)
        {
            try
            {
                var Fees_Process = APIHandeling.getData("AddEmployee_API?FullName="+FullName +"&EmplyeeNo="+EmplyeeNo+"&OutLet_ID="+OutLet_ID);
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

        // savecommitte_Employeee
        [HttpPost]
        public JsonResult SaveCommitte_Employee(List<EmployeeDTO> dataToSend, List<bool> isAdmins, long? Committe_Id, byte? CommitteeType_ID, long? CheckRequest_Id,
    DateTime? Check_Date, DateTime? Delegation_Date, TimeSpan? StartTime, TimeSpan? EndTime)
        {
            var resolveRequest = HttpContext.Request;
            var i = 0;
            foreach (var x in dataToSend)
            {
                x.ISAdmin = isAdmins[i];
                i++;

            }
            Ex_RequestCommitteeDTO model = new Ex_RequestCommitteeDTO();

            model.ExCheckRequest_ID = (long)CheckRequest_Id;
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
            model.com_emp = dataToSend;
            User_Session Current = User_Session.GetInstance;
            model.User_Creation_Id = (short)Session["UserId"];
            model.User_Creation_Date = DateTime.Now;
            dynamic res;
            if (Committe_Id > 0)
            {
                model.ID = (long)Committe_Id;
                //edit
                res = APIHandeling.Put(api, model);
            }
            else
            {
                //create
                res = APIHandeling.Post(api, model);
            }
            return ((int)res.StatusCode != 409) ? Json(new { Result = "OK", Record = model })
              : Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });
        }

        //*******************eslam ****************
        [HttpPost]
        public JsonResult Save_Lot(List<Im_CommitteeResultDTO> CheckedItemsList
            , List<Im_CheckRequest_SampleDataDTO> CheckedAnalysisList)
        {
            try
            {
                var allErrors = ModelState.Values.SelectMany(x => x.Errors).ToList();

                //if (ModelState.IsValid)
                //{
                if (CheckedItemsList != null && CheckedItemsList.Count != 0)
                {
                    User_Session Current = User_Session.GetInstance;

                    foreach (var item in CheckedItemsList)
                    {
                        item.Committee_ID = long.Parse(Session["reqComm_ID"].ToString());
                        //item.im_RequestCommittee.ID= long.Parse(Session["reqComm_ID"].ToString());
                        item.User_Creation_Id = (short)Session["UserId"];
                        item.User_Creation_Date = DateTime.Now;
                        if (CheckedAnalysisList != null && CheckedAnalysisList.Count != 0)
                        {
                            foreach (var item_Analysis in CheckedAnalysisList)
                            {
                                if (item_Analysis.Im_Request_Item_Id == item.Im_Request_Item_Id)
                                {
                                    item_Analysis.Im_RequestCommittee_ID = long.Parse(Session["reqComm_ID"].ToString());
                                    item_Analysis.User_Creation_Id = (short)Session["UserId"];
                                    item_Analysis.User_Creation_Date = DateTime.Now;
                                    item_Analysis.LotData_ID = item.LotData_ID;
                                    item_Analysis.IS_Total = item.IS_Total;
                                    item_Analysis.Item_ShortName_ID = item.Item_ShortName_ID;
                                }
                            }
                        }

                    }
                    var CommitteeType_ID = Session["CommitteeType_ID"].ToString();
                    if (CommitteeType_ID == "11")
                    {
                        // var res = APIHandeling.Post("ImRequestCommittee_API", CheckedItemsList, CheckedAnalysisList);
                        var res = APIHandeling.Post("ImRequestCommittee_API" + "?lotssss=1", CheckedItemsList);
                        var res2 = APIHandeling.Post("ImRequestCommittee_API" + "?AnalysisList=1", CheckedAnalysisList);
                        return ((int)res.StatusCode != 409) ? Json(new { Result = "OK", Record = CheckedItemsList })
                              : Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });
                    }
                   else if (CommitteeType_ID == "13")
                    {
                        
                        var res2 = APIHandeling.Post("ImRequestCommittee_API" + "?AnalysisList=1", CheckedAnalysisList);
                        return ((int)res2.StatusCode != 409) ? Json(new { Result = "OK", Record = CheckedAnalysisList })
                              : Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });
                    }
                    else
                    {
                        return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });

                    }

                }
                else
                {
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }

                // }
                //else
                //{
                //    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                //}
            }
            catch (Exception ex)
            {
                if (ex.HResult == -2146233087)
                {
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RelatedData) });
                }
                else
                {
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "CreateshiftTiming");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }

        }

    }
}