using PlantQuar.DTO.DTO.Admin;
using PlantQuar.DTO.DTO.DataEntry.Fees;
using PlantQuar.DTO.DTO.Farm.FarmCommittee;
using PlantQuar.DTO.DTO.Farm.FarmData;
using PlantQuar.DTO.DTO.Farm.FarmRequest;
using PlantQuar.DTO.DTO.Import.IM_Committee;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using PlantQuar.WEB.Areas.FA_Farm.Controllers;
using PlantQuar.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace PlantQuar.WEB.Areas.Committees.Controllers
{
    public class Committee_FarmController : BaseController
    {
        // GET: Committees/Committee_Farm
        string apiName = "Committee_API";

        //public ActionResult Index(long committeeId, long requestId, byte CommitteeType_ID)
        //{
        //    //get request number
        //    //emab
        //    var res_CommitteeType = APIHandeling.getData(apiName + "?Lst=1");
        //    var lst_CommitteeType = res_CommitteeType.Content.ReadAsAsync<List<CustomOptionShortId>>().Result;

        //    ViewBag.CommitteeTypee_ID = lst_CommitteeType.ToList();


        //    var shiftType = APIHandeling.getData("A_SystemCode_API?AddEdit=1&Sysnum=26");
        //    var ShiftTypes = shiftType.Content.ReadAsAsync<List<CustomOption>>().Result;

        //    ViewBag.ShiftTypes = ShiftTypes;

        //    ViewBag.CommitteeId = 16;
        //    ViewBag.checkRequestId = 68;
        //    var res_reqNumber = APIHandeling.getData(apiName + "?num=1&requestId=" + requestId);
        //    var creationDate = APIHandeling.getData(apiName + "?num=1&create=1&requestId=" + requestId);
        //    var checkReqNum = res_reqNumber.Content.ReadAsAsync<string>().Result;
        //    ViewBag.data = checkReqNum;
        //    ViewBag.type = 1;
        //    ViewBag.CommitteeTypeId = CommitteeType_ID;
        //    ViewBag.CommitteeType_ID = new SelectList(lst_CommitteeType.ToList(), "Value", "DisplayText", CommitteeType_ID);
        //    ViewBag.sId = requestId;
        //    ViewBag.creationDate = creationDate.Content.ReadAsAsync<string>().Result;


        //    return View();
        //}

        public ActionResult Index()
        {

            //fill التحاليل 
            List <Farm_Requst_ListDTO> Farm_Requst_List = new List<Farm_Requst_ListDTO>();
            Farm_Requst_List = Session["Farm_Requst_List"] as List<Farm_Requst_ListDTO>;
            var res2 = APIHandeling.Put("Committee_Farm_API?req=1", Farm_Requst_List);
            var reqCommiteeType = res2.Content.ReadAsAsync <List< Farm_Requst_ListDTO >>().Result;
            ViewBag.reqCommiteeType = reqCommiteeType;


           
            //full outlit
            var Fees_Process = APIHandeling.getData("Employee_Track_API?Outlet=-1");
            var lst = Fees_Process.Content.ReadAsAsync<List<CustomOption>>().Result;
            ViewBag.Outlet = lst;

         
            //fill shift
            var shiftType = APIHandeling.getData("A_SystemCode_API?AddEdit=1&Sysnum=26");
            var ShiftTypes = shiftType.Content.ReadAsAsync<List<CustomOption>>().Result;
            ViewBag.ShiftTypes = ShiftTypes;
            //creation date

            //var res = APIHandeling.getData("FarmCommittee_API?Id=" + farmCommitteeId);
            //var comm = res.Content.ReadAsAsync<Farm_CommitteeDTO>().Result;

            //var res_CommitteeType = APIHandeling.getData("CommitteeType?Lst=1");
            //var lst_CommitteeType = res_CommitteeType.Content.ReadAsAsync<List<CustomOptionShortId>>().Result;
            //ViewBag.CommitteeTypee_ID = lst_CommitteeType.ToList();

            ////ViewBag.data = comm.farmcode;
            //ViewBag.data = comm.farmName;
            //ViewBag.CommitteeId = farmCommitteeId;
            //ViewBag.farmrequestId = reqId;

            //ViewBag.sId = comm.Farm_Request_ID;
            //ViewBag.creationDate = comm.User_Creation_Date;
            //ViewBag.CommitteeType_ID = "";
            //ViewBag.type = 2;
            //ViewBag.checkRequestId = "";
            //ViewBag.CommitteeTypeId = "";
            return View("Index");
        }
        [HttpPost]
        public JsonResult reqAnalysisType_List(long requestId = 0)
        {
            try
            {
                List<Farm_Requst_ListDTO> Farm_Requst_List = new List<Farm_Requst_ListDTO>();
                Farm_Requst_List = Session["Farm_Requst_List"] as List<Farm_Requst_ListDTO>;

                var res = APIHandeling.Put("Committee_Farm_API?analtype=1" , Farm_Requst_List);
                var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                ViewBag.reqCommiteeType = lst;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "reqAnalysisType");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        [HttpPost]
        public JsonResult reqAnalysisLabType_List(int analType = 0)
        {
            try
            {
                var res = APIHandeling.getData("Im_Committee_API?analTypeId=" + analType);
                var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "reqAnalysisType");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
       
        public ActionResult IndexStation(long stationCommitteeId)
        {
            var shiftType = APIHandeling.getData("A_SystemCode_API?AddEdit=1&Sysnum=26");
            var ShiftTypes = shiftType.Content.ReadAsAsync<List<CustomOption>>().Result;

            ViewBag.ShiftTypes = ShiftTypes;
            //creation date
            var res = APIHandeling.getData(apiName + "?Id=" + stationCommitteeId);

            var comm = res.Content.ReadAsAsync<Station_Accreditation_CommitteeDTO>().Result;

            ViewBag.data = comm.stationcode;
            ViewBag.CommitteeId = stationCommitteeId;
            ViewBag.sId = comm.Station_Accreditation_ID;
            ViewBag.creationDate = comm.User_Creation_Date;
            ViewBag.CommitteeType_ID = "";
            ViewBag.type = 3;
            ViewBag.checkRequestId = "";
            ViewBag.CommitteeTypeId = "";
            return View("Index");
        }

        [HttpPost]
        public JsonResult listCommittee
        (string txt_AR_BTNSearch = "", string txt_EN_BTNSearch = "", int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = "")
        {
            try
            {
                var res = APIHandeling.getData(apiName + "?pageSize=" + jtPageSize.ToString() + "&index=" + jtStartIndex.ToString() + "&jtSorting=" + jtSorting.ToString());


                var lst = res.Content.ReadAsAsync<Dictionary<string, object>>().Result;//object

                var StatusCode = lst.ElementAt(0).Value;
                var obj = lst.ElementAt(1).Value;

                JavaScriptSerializer ser = new JavaScriptSerializer();
                var myObj = ser.Deserialize<Dictionary<string, object>>(obj.ToString());

                var count = myObj.ElementAt(0).Value;
                var Lobj = myObj.ElementAt(1).Value;

                return Json(new { Result = "OK", Records = Lobj, TotalRecordCount = count });


            }
            catch (Exception ex)
            {
                if (ex.HResult == -2146233087)
                {
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RelatedData) });
                }
                else
                {

                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "listCommittee");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        [HttpPost]
        public JsonResult GetCommittee(string CheckRequest_Number, byte CommitteeType_ID)
        {
            try
            {
                Dictionary<string, string> dicData = new Dictionary<string, string>();
                var res = APIHandeling.getData(apiName + "?CheckRequest_Number=" + CheckRequest_Number +
                    "&CommitteeType_ID=" + CommitteeType_ID + "&WithEmployee=true");
                //  var lst = res.Content.ReadAsAsync<Dictionary<string,object>>().Result;
                //List<CheckRequest_GetCommitte_Data_ResultDTO> request = lst["obj"] as List<CheckRequest_GetCommitte_Data_ResultDTO>;
                //List<EmployeeDTO> EmployeeList = lst["EmployeeList"] as List<EmployeeDTO>;
                var lst = res.Content.ReadAsAsync<CheckRequest_GetCommitte_Data_ResultDTO>().Result;

                return (lst != null) ? Json(new { Result = 1, Request_committe = lst }) : Json(new { Result = -1 });
            }
            catch (Exception ex)
            {
                if (ex.HResult == -2146233087)
                {
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RelatedData) });
                }
                else
                {
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetCommittee");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        public JsonResult ListEmployee(string Employee_name = "", string Employee_No = "", int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = "")
        {
            try
            {
                //   var countRes = APIHandeling.getData("Employee");

                var res = APIHandeling.getData("Employee_API?Employee_name=" + Employee_name +
                            "&Employee_No=" + Employee_No + "&pageSize=" + jtPageSize.ToString() + "&index=" + jtStartIndex.ToString() /*+ "&jtSorting=" + jtSorting.ToString()*/);


                var lst = res.Content.ReadAsAsync<Dictionary<string, object>>().Result;//object

                var StatusCode = lst.ElementAt(0).Value;
                var obj = lst.ElementAt(1).Value;

                JavaScriptSerializer ser = new JavaScriptSerializer();
                var myObj = ser.Deserialize<Dictionary<string, object>>(obj.ToString());

                var count = myObj.ElementAt(0).Value;
                var Lobj = myObj.ElementAt(1).Value;

                return Json(new { Result = "OK", Records = Lobj, TotalRecordCount = count });
            }
            catch (Exception ex)
            {
                if (ex.HResult == -2146233087)
                {
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RelatedData) });
                }
                else
                {

                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetCommittee");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        //Insert
        [HttpPost]
        public JsonResult CreateCommittee(Ex_RequestCommitteeDTO model)
        {
            try
            {
                var allErrors = ModelState.Values.SelectMany(x => x.Errors).ToList();

                if (ModelState.IsValid)
                {
                    User_Session Current = User_Session.GetInstance;

                    model.User_Creation_Id = (short)Session["UserId"];
                    model.User_Creation_Date = DateTime.Now;
                    var res = APIHandeling.Post(apiName, model);
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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "CreateCommittee");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        [HttpPost]
        public JsonResult UpdateCommittee(Ex_RequestCommitteeDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    User_Session Current = User_Session.GetInstance;

                    model.User_Updation_Id = (short)Session["UserId"];
                    model.User_Updation_Date = DateTime.Now;
                    //check Repeated Data
                    var res = APIHandeling.Put(apiName, model);
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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "UpdateCommittee");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        //DELETE
        [HttpPost]
        public JsonResult DeleteCommittee(byte ID)
        {
            try
            {
                DeleteParameters obj = new DeleteParameters();
                obj.id = ID; User_Session Current = User_Session.GetInstance;

                obj.Userid = (short)(short)Session["UserId"];
                obj._DateNow = DateTime.Now;
                APIHandeling.Delete(apiName, obj);

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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "DeleteCommittee");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        public JsonResult CommitteeType_List()
        {
            try
            {
                Dictionary<string, int> dic = new Dictionary<string, int>();
                dic.Add("List", 1);
                var res = APIHandeling.getDataByParamter("CommitteeType_API", dic);
                var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;

                var List = lst.Select(c => new { DisplayText = c.DisplayText, Value = c.Value, }).OrderBy(s => s.DisplayText);

                return Json(new { Result = "OK", Options = List });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        public JsonResult ListEmployee_AddEDIT()
        {
            try
            {
                var res = APIHandeling.getData("Employee_API?AddEdit=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Employee");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

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
                res = APIHandeling.Put(apiName, model);
            }
            else
            {
                //create
                res = APIHandeling.Post(apiName, model);
            }
            return ((int)res.StatusCode != 409) ? Json(new { Result = "OK", Record = model })
              : Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });
        }

        [HttpPost]
        public JsonResult SavefarmCommitte_Employee(List<EmployeeDTO> dataToSend/*, List<bool> isAdmins*/, long? Committe_Id, long? FarmId,
           DateTime? Delegation_Date, TimeSpan? StartTime, TimeSpan? EndTime, byte? committeetype)
        {
            var resolveRequest = HttpContext.Request;
            //var i = 0;
            //foreach (var x in dataToSend)
            //{
            //    x.ISAdmin = isAdmins[i];
            //    i++;

            //}
            TimeSpan duration = new TimeSpan(1, 12, 23, 62);

            Farm_CommitteeDTO model = new Farm_CommitteeDTO();
            model.ID = (long)Committe_Id;
            model.Farm_Request_ID = (long)FarmId;
            model.Delegation_Date = Delegation_Date;
            model.CommitteeType_ID = (byte)committeetype;
            model.StartTime = StartTime;
            model.EndTime = EndTime;
            //  model.EndTime = (TimeSpan)EndTime;
            model.IsApproved = null;
            model.Status = false;
            model.OperationType = 78;
            model.com_emp = dataToSend;
            User_Session Current = User_Session.GetInstance;
            model.User_Updation_Id = (short)Session["UserId"];
            model.User_Updation_Date = DateTime.Now;
            // model.ShiftTiming_ID = shiftId;
            dynamic res;

            //create
            res = APIHandeling.Put("FarmCommittee_API", model);

            return ((int)res.StatusCode != 409) ? Json(new { Result = "OK", Record = model })
              : Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });
        }



        //*************************sayed **************
        [HttpPost]
        public JsonResult GetPR_User_Id(string FullName, long EmplyeeNo, long OutLet_ID)
        {
            try
            {
                var Fees_Process = APIHandeling.getData("AddEmployee_API?FullName=" + FullName + "&EmplyeeNo="
                    + EmplyeeNo + "&OutLet_ID=" + OutLet_ID+ "&Type_ID_HR=1");
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

        public JsonResult Save_Committe(DateTime? Check_Date, DateTime? Delegation_Date, TimeSpan? StartTime, TimeSpan? EndTime,
       List<EmployeeDTO> emp_Dto,
      // List<Farm_Committee_ExaminationDTO> Farm_Committee_Examination_List,
       List<Farm_SampleData2DTO> Farm_SampleData_List,
       List<Farm_Committee_ShiftDTO> ShiftTiming_List,
       List<Farm_Committee_CheckList_DTO> CheckList_List,
       List<Farm_Committee_ConstrainDTO> Constrain_Text_List)
        {
            List<Farm_Requst_ListDTO> Farm_Requst_List = new List<Farm_Requst_ListDTO>();
            Farm_Requst_List = Session["Farm_Requst_List"] as List<Farm_Requst_ListDTO>;
             if (Farm_Requst_List != null)
            {
               
                var Farm_Commity_data_List = new List<Farm_Committee_Data_DTO>();
                foreach (var item in Farm_Requst_List)
                {
                    byte CommitteeType_ID = 5;
                    var _CommitteeType_ID = Constrain_Text_List.Where(a => a.Farm_Committee_ID == item.Farm_Committee_ID && a.Farm_type_ID == 12).Any();
                    if (_CommitteeType_ID == true)
                        CommitteeType_ID = 12;
                    //if(_CommitteeType_ID.)

                    //(from ctl in Constrain_Text_List
                    //                     where ctl.Farm_Committee_ID == item.Farm_Committee_ID
                    //                     group ctl by new
                    //                     {
                    //                         farm_Id = ctl.Farm_Committee_ID,                                             
                    //                     } into grp
                    //                     select new Farm_Committee_ConstrainDTO
                    //                     {
                    //                         Farm_Committee_ID = grp.Key.farm_Id,
                    //                         Farm_type_ID=grp.Max(x => x.Farm_type_ID), 
                    //                     }).ToList();


                    var location = new Farm_Committee_Data_DTO
                    {
                        ID = item.Farm_Committee_ID,
                        Farm_Request_ID = item.reqId,
                        Delegation_Date = (DateTime)Delegation_Date,
                        StartTime = StartTime,
                        EndTime = EndTime,
                        User_Updation_Id = (short)Session["UserId"],
                        User_Updation_Date = DateTime.Now,
                        User_Creation_Id = (short)Session["UserId"],
                        User_Creation_Date = DateTime.Now,
                        OperationType =78,
                        CommitteeType_ID= CommitteeType_ID
                    };
                    Farm_Commity_data_List.Add(location);
                }
                //    #region اللجنة
                Farm_Committee_Requst_All_DTO comm = new Farm_Committee_Requst_All_DTO();
                comm.List_Committee = Farm_Commity_data_List;
                
                comm.List_emp = emp_Dto;
                comm.List_Farm_SampleData = Farm_SampleData_List;
                comm.List_ShiftTiming = ShiftTiming_List;
                comm.List_CheckList = CheckList_List;
                comm.List_Constrain_Text = Constrain_Text_List;

                var res2 = APIHandeling.Put("Committee_Farm_API?Insert_req=1", comm);
                var list = res2.Content.ReadAsAsync<Dictionary<string, object>>().Result;
                if (list != null)
                    return Json("Exist", JsonRequestBehavior.AllowGet);
                else
                    return Json("error", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return null;
            }
#region old
            //    comm.ImCheckRequest_ID = CheckRequest_Id;
            //    comm.CommitteeType_ID = CommitteeType_ID;
            //    comm.IsApproved = _IsApproved;// Checked_TreatmentMethod == null ?true: null;
            //    comm.Status = false;
            //    comm.Delegation_Date = (DateTime)Delegation_Date;
            //    comm.StartTime = (TimeSpan)StartTime;
            //    comm.EndTime = (TimeSpan)EndTime;
            //    User_Session Current = User_Session.GetInstance;
            //    comm.User_Creation_Id = (short)Session["UserId"];
            //    comm.User_Creation_Date = DateTime.Now;
            //    #endregion

            //    comm.com_emp = emp_Dto;
            //    // النوبتجية
            //    List<RequestCommittee_ShiftDTO> Shift_Data = new List<RequestCommittee_ShiftDTO>();
            //    Shift_Data = Session["listshiftTimingsession"] as List<RequestCommittee_ShiftDTO>;

            //    //// المعامل السابقه
            //    // List<Committee_Sample_Lot> SamplesOld = new List<Committee_Sample_Lot>();
            //    //SamplesOld = Session["Committee_Sample_Lot"] as List<Committee_Sample_Lot>;

            //    ////var Sampless = (List<CheckRequest_SampleDataDTO>)Session["Committee_Sample_Lot"];


            //    comm.List_Committee_Shift = Shift_Data;

            //    #region بيانات الرسالة

            //    var CheckedAnalysisList_New = new List<CheckRequest_SampleDataDTO>();
            //    var Checked_TreatmentMethod_New = new List<Checked_TreatmentMethodDTO>();

            //    if (CheckedItemsList != null && CheckedItemsList.Count != 0)
            //    {
            //        foreach (var item in CheckedItemsList)
            //        {
            //            // item.Committee_ID = long.Parse(Session["reqComm_ID"].ToString());
            //            item.User_Creation_Id = (short)Session["UserId"];
            //            item.User_Creation_Date = DateTime.Now;
            //            if (CheckedAnalysisList != null && CheckedAnalysisList.Count != 0)
            //            {
            //                foreach (var item_Analysis in CheckedAnalysisList)
            //                {
            //                    long Short_ID = item.Item_ShortName_ID;
            //                    if (item_Analysis.Im_Request_Item_Id == Short_ID)
            //                    {
            //                        //if (l)
            //                        //var asd = Committee_Sample_Lot
            //                        var location = new CheckRequest_SampleDataDTO
            //                        {
            //                            AnalysisLabType_ID = item_Analysis.AnalysisLabType_ID,
            //                            // Im_RequestCommittee_ID = long.Parse(Session["reqComm_ID"].ToString()),
            //                            Im_Request_Item_Id = item.Im_Request_Item_Id,
            //                            LotData_ID = item.LotData_ID,
            //                            User_Creation_Id = (short)Session["UserId"],
            //                            User_Creation_Date = DateTime.Now,

            //                            IS_Total = item.IS_Total,
            //                            Item_ShortName_ID = item.Item_ShortName_ID,
            //                        };
            //                        CheckedAnalysisList_New.Add(location);
            //                    }
            //                }
            //            }


            //            if (Checked_TreatmentMethod != null && Checked_TreatmentMethod.Count != 0)
            //            {
            //                foreach (var item_Treatment in Checked_TreatmentMethod)
            //                {
            //                    long Short_ID = item.Item_ShortName_ID;
            //                    if (item_Treatment.Im_Request_Item_Id == Short_ID)
            //                    {
            //                        //if (l)
            //                        //var asd = Committee_Sample_Lot
            //                        var location = new Checked_TreatmentMethodDTO
            //                        {
            //                            TreatmentType_ID = item_Treatment.TreatmentType_ID,
            //                            TreatmentMethod_ID = item_Treatment.TreatmentMethod_ID,
            //                            // Im_RequestCommittee_ID = long.Parse(Session["reqComm_ID"].ToString()),
            //                            Im_Request_Item_Id = item.Im_Request_Item_Id,
            //                            Im_Request_LotData_ID = item.LotData_ID,
            //                            User_Creation_Id = (short)Session["UserId"],
            //                            User_Creation_Date = DateTime.Now,

            //                            IS_Total = item.IS_Total,
            //                            Item_ShortName_ID = item.Item_ShortName_ID,
            //                            Procedures = item_Treatment.Procedures,

            //                        };
            //                        Checked_TreatmentMethod_New.Add(location);
            //                    }
            //                }
            //            }



            //        }
            //    }
            //    if (CommitteeType_ID == 11)
            //    {
            //        comm.List_CommitteeResult = CheckedItemsList;
            //        comm.List_SampleData = CheckedAnalysisList_New;
            //    }
            //    else if (CommitteeType_ID == 13)
            //    {

            //        comm.List_SampleData = CheckedAnalysisList_New;
            //    }
            //    else if (CommitteeType_ID == 14)
            //    {

            //        //if (Checked_TreatmentMethod != null && Checked_TreatmentMethod.Count != 0)
            //        //{
            //        //    foreach (var item_TreatmentMethod in Checked_TreatmentMethod)
            //        //    {
            //        //        item_TreatmentMethod.User_Creation_Id = (short)Session["UserId"];
            //        //        item_TreatmentMethod.User_Creation_Date = DateTime.Now;
            //        //        long Short_ID = item.Item_ShortName_ID;
            //        //        //if (item_TreatmentMethod.Im_Request_Item_Id == Short_ID)
            //        //        //{
            //        //        //if (l)
            //        //        //var asd = Committee_Sample_Lot
            //        //        var location = new Checked_TreatmentMethodDTO
            //        //            {
            //        //                Im_Request_Item_Id = item_TreatmentMethod.Im_Request_Item_Id,
            //        //                // Im_RequestCommittee_ID = long.Parse(Session["reqComm_ID"].ToString()),
            //        //                // Im_Request_Item_Id = item_TreatmentMethod.Im_Request_Item_Id,
            //        //                Im_Request_LotData_ID = item_TreatmentMethod.Im_Request_LotData_ID,
            //        //                User_Creation_Id = (short)Session["UserId"],
            //        //                User_Creation_Date = DateTime.Now,

            //        //                IS_Total = item_TreatmentMethod.IS_Total,
            //        //                Item_ShortName_ID = Short_ID,
            //        //            };
            //        //        Checked_TreatmentMethod_New.Add(location);
            //        //        //}
            //        //    }
            //        //}

            //        comm.List_TreatmentMethod = Checked_TreatmentMethod_New;
            //    }


#endregion


        }

    }
}