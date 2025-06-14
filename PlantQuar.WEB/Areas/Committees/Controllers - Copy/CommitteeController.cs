using PlantQuar.DTO.DTO.Admin;
using PlantQuar.DTO.DTO.DataEntry.Fees;
using PlantQuar.DTO.DTO.Farm.FarmCommittee;
using PlantQuar.DTO.DTO.Farm.FarmData;
using PlantQuar.DTO.DTO.Farm.FarmRequest;
using PlantQuar.DTO.DTO.Import.IM_Committee;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
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
    public class CommitteeController : Controller
    {
        // GET: Committees/Committee
        string apiName = "Committee_API";

        public ActionResult Index(long committeeId, long requestId, byte CommitteeType_ID)
        {
            //get request number
            //emab
            var res_CommitteeType = APIHandeling.getData(apiName + "?Lst=1");
            var lst_CommitteeType = res_CommitteeType.Content.ReadAsAsync<List<CustomOptionShortId>>().Result;

            ViewBag.CommitteeTypee_ID = lst_CommitteeType.ToList();


            var shiftType = APIHandeling.getData("A_SystemCode_API?AddEdit=1&Sysnum=26");
            var ShiftTypes = shiftType.Content.ReadAsAsync<List<CustomOption>>().Result;

            ViewBag.ShiftTypes = ShiftTypes;

            ViewBag.CommitteeId = 16;
            ViewBag.checkRequestId = 68;
            var res_reqNumber = APIHandeling.getData(apiName + "?num=1&requestId=" + requestId);
            var creationDate = APIHandeling.getData(apiName + "?num=1&create=1&requestId=" + requestId);
            var checkReqNum = res_reqNumber.Content.ReadAsAsync<string>().Result;
            ViewBag.data = checkReqNum;
            ViewBag.type = 1;
            ViewBag.CommitteeTypeId = CommitteeType_ID;
            ViewBag.CommitteeType_ID = new SelectList(lst_CommitteeType.ToList(), "Value", "DisplayText", CommitteeType_ID);
            ViewBag.sId = requestId;
            ViewBag.creationDate = creationDate.Content.ReadAsAsync<string>().Result;


            return View();
        }

        public ActionResult IndexFarm(long farmCommitteeId, long? reqId)
        {

            var ReqCommType = APIHandeling.getData("Im_Committee_API?type=1&requestId=" + reqId);
            var reqCommiteeType = ReqCommType.Content.ReadAsAsync<farmCountryConstrainReturnDTO>().Result;

            ViewBag.reqCommiteeType = reqCommiteeType;
            var shiftType = APIHandeling.getData("A_SystemCode_API?AddEdit=1&Sysnum=26");
            var ShiftTypes = shiftType.Content.ReadAsAsync<List<CustomOption>>().Result;

            ViewBag.ShiftTypes = ShiftTypes;
            //creation date
            var res = APIHandeling.getData("FarmCommittee_API?Id=" + farmCommitteeId);

            var comm = res.Content.ReadAsAsync<Farm_CommitteeDTO>().Result;

            var res_CommitteeType = APIHandeling.getData("CommitteeType?Lst=1");
            var lst_CommitteeType = res_CommitteeType.Content.ReadAsAsync<List<CustomOptionShortId>>().Result;

            ViewBag.CommitteeTypee_ID = lst_CommitteeType.ToList();

            //ViewBag.data = comm.farmcode;
            ViewBag.data = comm.farmName;
            ViewBag.CommitteeId = farmCommitteeId;
            ViewBag.farmrequestId = reqId;

            ViewBag.sId = comm.Farm_Request_ID;
            ViewBag.creationDate = comm.User_Creation_Date;
            ViewBag.CommitteeType_ID = "";
            ViewBag.type = 2;
            ViewBag.checkRequestId = "";
            ViewBag.CommitteeTypeId = "";
            return View("Index");
        }
        [HttpPost]
        public JsonResult reqAnalysisType_List(long requestId = 0)
        {
            try
            {
                var res = APIHandeling.getData("Im_Committee_API?analtype=1&requestId=" + requestId);
                var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
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

        [HttpPost]
        public JsonResult SaveStationCommitte_Employee(List<EmployeeDTO> dataToSend, List<bool> isAdmins, long? Committe_Id, long? stationAccrediationId,
           DateTime? Delegation_Date, TimeSpan? StartTime, TimeSpan? EndTime)
        {
            var resolveRequest = HttpContext.Request;
            var i = 0;
            foreach (var x in dataToSend)
            {
                x.ISAdmin = isAdmins[i];
                i++;

            }
            Station_Accreditation_CommitteeDTO model = new Station_Accreditation_CommitteeDTO();
            model.ID = (long)Committe_Id;
            model.Station_Accreditation_ID = (long)stationAccrediationId;
            model.Delegation_Date = Delegation_Date;
            //model.CommitteeType_ID = 4;//ask
            model.StartTime = (TimeSpan)StartTime;
            model.EndTime = (TimeSpan)EndTime;
            model.IsApproved = null;
            model.Status = false;
            model.OperationType = 79;
            model.com_emp = dataToSend;
            User_Session Current = User_Session.GetInstance;
            model.User_Updation_Id = (short)Session["UserId"];
            model.User_Updation_Date = DateTime.Now;
            dynamic res;

            //create
            res = APIHandeling.Put("stationCommittee_API", model);

            return ((int)res.StatusCode != 409) ? Json(new { Result = "OK", Record = model })
              : Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });
        }
        //request committee analysis type
        public JsonResult listfarmReqAnalysisType
        (long farmCommitteeId = 0, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                var lst = new Dictionary<string, object>();


                var res = APIHandeling.getData("farmReqAnalysisType_API?farmCommitteeId=" + farmCommitteeId
                + "&pageSize=" + jtPageSize.ToString()
                + "&index=" + jtStartIndex.ToString());
                lst = res.Content.ReadAsAsync<Dictionary<string, object>>().Result;//object

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

                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "listItem");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }



        //Insert
        [HttpPost]
        public JsonResult CreatefarmReqAnalysisType(Farm_SampleData2DTO model)
        {
            try
            {
                var allErrors = ModelState.Values.SelectMany(x => x.Errors).ToList();

                if (ModelState.IsValid)
                {
                    User_Session Current = User_Session.GetInstance;

                    model.User_Creation_Id = (short)Session["UserId"];
                    model.User_Creation_Date = DateTime.Now;


                    var mynewobj = APIHandeling.Post("farmReqAnalysisType_API", model);

                    return ((int)mynewobj.StatusCode != 409) ? Json(new { Result = "OK", Record = model })
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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "CreateItem");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        //UPDATE
        [HttpPost]
        public JsonResult UpdatefarmReqAnalysisType(Farm_SampleData2DTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    User_Session Current = User_Session.GetInstance;

                    model.User_Updation_Id = (short)Session["UserId"];
                    model.User_Updation_Date = DateTime.Now;

                    var res = APIHandeling.Put("farmReqAnalysisType_API", model);
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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "UpdateItem");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        //DELETE
        [HttpPost]
        public JsonResult DeletefarmReqAnalysisType(long ID)
        {
            try
            {
                DeleteParameters obj = new DeleteParameters();
                obj.id = ID;
                User_Session Current = User_Session.GetInstance;

                obj.Userid = (short)(short)Session["UserId"];
                obj._DateNow = DateTime.Now;
                APIHandeling.Delete("farmReqAnalysisType_API", obj);

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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "DeleteItem");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        //*********************************SARA*****************************//
        [HttpPost]
        public JsonResult getShiftTiming_Lst()
        {
            try
            {
                var res = APIHandeling.getData("Farm_Committee_Shift_API?List=1");
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
                var res = APIHandeling.getData("Farm_Committee_Shift_API?shiftId=" + shiftId);
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
        (long farmCommitteeId = 0, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = "")
        {
            try
            {
                var res = APIHandeling.getData("Farm_Committee_Shift_API?FarmCommittee_ID=" + farmCommitteeId
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
        public JsonResult CreateshiftTiming(Farm_Committee_ShiftDTO model)
        {
            try
            {
                var allErrors = ModelState.Values.SelectMany(x => x.Errors).ToList();

                if (ModelState.IsValid)
                {
                    User_Session Current = User_Session.GetInstance;
                    model.User_Creation_Id = (short)Session["UserId"];
                    model.User_Creation_Date = DateTime.Now;
                    model.Amount = (decimal?)model.money;
                    //check Repeated Data
                    var res = APIHandeling.Post("Farm_Committee_Shift_API", model);

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
        public JsonResult UpdateshiftTiming(Farm_Committee_ShiftDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    User_Session Current = User_Session.GetInstance;
                    model.User_Updation_Id = (short)Session["UserId"];
                    model.User_Updation_Date = DateTime.Now;
                    //check Repeated Data
                    var res = APIHandeling.Put("Farm_Committee_Shift_API", model);
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
                APIHandeling.Delete("Farm_Committee_Shift_API", obj);

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

        [HttpPost]

        public JsonResult GetPR_User_Id(string FullName, long EmplyeeNo)
        {
            try
            {
                var Fees_Process = APIHandeling.getData("AddEmployee_API?FullName=" + FullName + "&id=" + EmplyeeNo);
                var lst = Fees_Process.Content.ReadAsAsync<List<User>>().Result;
                if (lst.Count > 1)
                {
                    lst.RemoveAt(0);
                }
                return Json(lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetPR_User_Id");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }

        }

    }
}