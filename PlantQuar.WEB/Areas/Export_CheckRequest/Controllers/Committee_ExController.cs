
using PlantQuar.DTO.DTO.Admin;
using PlantQuar.DTO.DTO.Export_CheckRequest;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using PlantQuar.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.Export_CheckRequest.Controllers
{
    public class Committee_ExController : BaseController
    {
        // GET: Committees/Committee_Ex
        
        string api = "Committee_Ex_API";
        public ActionResult Index(long? requestId, long? OutLet_ID)
        {
            var res = APIHandeling.getData(api + "?requestId=" + requestId);

            var req = res.Content.ReadAsAsync<EX_CheckRequestDTO>().Result;
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
            var resItem = APIHandeling.getData(api+"?ImCheckRequest_Number=" + req.CheckRequest_Number);
            var Lst = resItem.Content.ReadAsAsync<ExRequestDetails_NewDTO>().Result;
            Session["listshiftTimingsession"] = new List<Ex_ShiftDTO>();
           
            Session["listshiftTimingsession_Index"] = 1;
            //Session["Committee_Sample_Lot"] = new List<Committee_Sample_Lot>();
            //var dd = Lst.itemsWithConstrains.ToList();
            //var df = dd.Select(a => a.ItemCategories_lots).ToList();

            
            
           // var SampleList = Lst.itemsWithConstrains.Select(a => a.ItemCategories_lots.Select(b => b.list_Committee_Sample_Lot)).ToList();
           // Session["Committee_Sample_Lot"] = SampleList;
           if(Lst != null)
            { 
            if (Lst.OutLet_ID == OutLet_ID)
            {
                return View(Lst);
            }
            else
            {
                return null;
            }
            }
            else
            {
                return null;
            }
        }


        //shift timing
        [HttpPost]
        public JsonResult listshiftTiming
      (long Ex_RequestCommittee_ID = 0, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = "")
        {
            try
            {               
                var companiescount = Session["listshiftTimingsession"] as List<Ex_ShiftDTO>;
                return Json(new { Result = "OK", Records = companiescount.OrderBy(p => p.Index), TotalRecordCount = companiescount.Count });

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

        [HttpPost]
        public JsonResult CreateshiftTiming(Ex_ShiftDTO model)
        {
            try
            {
                // model.Im_RequestCommittee_ID = long.Parse(Session["reqComm_ID"].ToString());
                // ViewBag.FarmCommittee_ID = req;
                var allErrors = ModelState.Values.SelectMany(x => x.Errors).ToList();

                if (ModelState.IsValid)
                {

                    model.Index = int.Parse(Session["listshiftTimingsession_Index"].ToString());

                    var companies = Session["listshiftTimingsession"] as List<Ex_ShiftDTO>;
                    //model.Amount = companies.
                    companies.Add(model);
                    Session["listshiftTimingsession"] = companies;

                    Session["listshiftTimingsession_Index"] = int.Parse(Session["listshiftTimingsession_Index"].ToString()) + 1;
                    return Json(new { Result = "OK", Record = companies.OrderBy(p => p.Index), TotalRecordCount = companies.Count });                    
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

        [HttpPost]
        public JsonResult getShiftTiming_Lst()
        {
            try
            {
                var res = APIHandeling.getData(api+"?List=1");
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
                var res = APIHandeling.getData(api+"?shiftId=" + shiftId);
                var mony = res.Content.ReadAsAsync<Nullable<double>>().Result;
                return Json(new { Result = "OK", Options = mony }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "getShiftTiming_Lst");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
                     
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
        public JsonResult Save_Committe(byte? CommitteeType_ID, long? CheckRequest_Id,
         DateTime? Check_Date, DateTime? Delegation_Date, TimeSpan? StartTime, TimeSpan? EndTime, List<Ex_EmployeeDTO> emp_Dto,
         List<Ex_Committee_ResultDTO> CheckedItemsList
           , List<Ex_SampleDataDTO> CheckedAnalysisList)
        {
            if (CheckedItemsList != null)
            {
                #region اللجنة
                Ex_RequestCommitteeDTO comm = new Ex_RequestCommitteeDTO();
                comm.Ex_CheckRequest_ID = CheckRequest_Id;
                comm.CommitteeType_ID = CommitteeType_ID;
                comm.IsApproved = true;
                comm.Status = false;
                comm.Delegation_Date = (DateTime)Delegation_Date;
                comm.StartTime = (TimeSpan)StartTime;
                comm.EndTime = (TimeSpan)EndTime;
                User_Session Current = User_Session.GetInstance;
                comm.User_Creation_Id = (short)Session["UserId"];
                comm.User_Creation_Date = DateTime.Now;
                #endregion

                comm.com_emp = emp_Dto;
                // النوبتجية
                List<Ex_ShiftDTO> Shift_Data = new List<Ex_ShiftDTO>();
                Shift_Data = Session["listshiftTimingsession"] as List<Ex_ShiftDTO>;

                //// المعامل السابقه
                // List<Committee_Sample_Lot> SamplesOld = new List<Committee_Sample_Lot>();
                //SamplesOld = Session["Committee_Sample_Lot"] as List<Committee_Sample_Lot>;

                ////var Sampless = (List<CheckRequest_SampleDataDTO>)Session["Committee_Sample_Lot"];


                comm.List_Committee_Shift = Shift_Data;

                #region بيانات الرسالة

                var CheckedAnalysisList_New = new List<Ex_SampleDataDTO>();

                if (CheckedItemsList != null && CheckedItemsList.Count != 0)
                {
                    foreach (var item in CheckedItemsList)
                    {
                        // item.Committee_ID = long.Parse(Session["reqComm_ID"].ToString());
                        item.User_Creation_Id = (short)Session["UserId"];
                        item.User_Creation_Date = DateTime.Now;
                        if (CheckedAnalysisList != null && CheckedAnalysisList.Count != 0)
                        {
                            foreach (var item_Analysis in CheckedAnalysisList)
                            {
                                long Short_ID = item.Item_ShortName_ID;
                                if (item_Analysis.Ex_Request_Item_Id == Short_ID)
                                {
                                    //if (l)
                                    //var asd = Committee_Sample_Lot
                                    var location = new Ex_SampleDataDTO
                                    {
                                        AnalysisLabType_ID = item_Analysis.AnalysisLabType_ID,
                                        // Ex_RequestCommittee_ID = long.Parse(Session["reqComm_ID"].ToString()),
                                        Ex_Request_Item_Id = item.Ex_Request_Item_Id,
                                        LotData_ID = item.LotData_ID,
                                        User_Creation_Id = (short)Session["UserId"],
                                        User_Creation_Date = DateTime.Now,

                                        IS_Total = item.IS_Total,
                                        Item_ShortName_ID = item.Item_ShortName_ID,
                                    };
                                    CheckedAnalysisList_New.Add(location);
                                }
                            }
                        }

                    }
                }
                if (CommitteeType_ID == 11)
                {
                    comm.List_CommitteeResult = CheckedItemsList;
                    comm.List_SampleData = CheckedAnalysisList_New;
                }
                else if (CommitteeType_ID == 13)
                {

                    comm.List_SampleData = CheckedAnalysisList_New;
                }


                #endregion

                var res2 = APIHandeling.Put(api + "?req=1", comm);
                var list = res2.Content.ReadAsAsync<Dictionary<string, object>>().Result;
                return Json("Exist", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return null;
            }
        }
    }
}