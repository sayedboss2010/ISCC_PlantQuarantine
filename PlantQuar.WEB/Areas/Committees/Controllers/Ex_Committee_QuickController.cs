using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Admin;
using PlantQuar.DTO.DTO.Committee;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using PlantQuar.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using PlantQuar.DTO.DTO.Farm.FarmRequest;
using static PlantQuar.WEB.Areas.Export_CheckRequest.Controllers.List_EXCheckRequestController;


namespace PlantQuar.WEB.Areas.Committees.Controllers
{
    public class Ex_Committee_QuickController : BaseController
    {
        // GET: Committees/Ex_Committee_Quick
        string api = "Ex_Committee_Quick_API";

        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();
        public ActionResult Index()
        {
            //var res = APIHandeling.getData("Ex_Committee_Quick_API?requestId=" + requestId);
            //var req = res.Content.ReadAsAsync<EX_CheckRequest_Committee_DTO>().Result;
            ////       ViewBag.creationDate = req.User_Creation_Date;
            //ViewBag.requestNumber = req.CheckRequest_Number;
            //ViewBag.requestId = requestId;


            var Fees_Process = APIHandeling.getData("Employee_Track_API?Outlet=-1");
            var lst = Fees_Process.Content.ReadAsAsync<List<CustomOption>>().Result;
            ViewBag.Outlet = lst;


            var resAnalysisType = APIHandeling.getData("AnalysisType_API?List=1");
            var lstAnalysisType = resAnalysisType.Content.ReadAsAsync<List<CustomOption>>().Result;
            ViewBag.lstAnalysisType = lstAnalysisType;

            var resTreatmentMain = APIHandeling.getData("TreatmentMainType_API?List=1");
            var lstTreatmentMain = resTreatmentMain.Content.ReadAsAsync<List<CustomOption>>().Result;
            ViewBag.lstTreatmentMain = lstTreatmentMain;

            List <EX_CheckRequest_Quick_DTO> Requst_List = new List<EX_CheckRequest_Quick_DTO>();
            Requst_List = Session["Requst_List"] as List<EX_CheckRequest_Quick_DTO>;



            //var res2 = APIHandeling.Put("Committee_Farm_API?req=1", Requst_List);
            //var reqCommiteeType = res2.Content.ReadAsAsync<List<Farm_Requst_ListDTO>>().Result;
            //ViewBag.reqCommiteeType = reqCommiteeType;


            var resItem = APIHandeling.Put(api + "?reqQuk=1", Requst_List);
            var Lst = resItem.Content.ReadAsAsync<List<Ex_Committee_Quick_DTO>>().Result;


            Session["listshiftTimingsession"] = new List<EX_RequestCommittee_ShiftDTO>();

            Session["listshiftTimingsession_Index"] = 1;


            return View(Lst);
            //return null;
        } 
        public ActionResult Indexnew()
        {
            //var res = APIHandeling.getData("Ex_Committee_Quick_API?requestId=" + requestId);
            //var req = res.Content.ReadAsAsync<EX_CheckRequest_Committee_DTO>().Result;
            ////       ViewBag.creationDate = req.User_Creation_Date;
            //ViewBag.requestNumber = req.CheckRequest_Number;
            //ViewBag.requestId = requestId;


            var Fees_Process = APIHandeling.getData("Employee_Track_API?Outlet=-1");
            var lst = Fees_Process.Content.ReadAsAsync<List<CustomOption>>().Result;
            ViewBag.Outlet = lst;


            var resAnalysisType = APIHandeling.getData("AnalysisType_API?List=1");
            var lstAnalysisType = resAnalysisType.Content.ReadAsAsync<List<CustomOption>>().Result;
            ViewBag.lstAnalysisType = lstAnalysisType;

            var resTreatmentMain = APIHandeling.getData("TreatmentMainType_API?List=1");
            var lstTreatmentMain = resTreatmentMain.Content.ReadAsAsync<List<CustomOption>>().Result;
            ViewBag.lstTreatmentMain = lstTreatmentMain;

            List <EX_CheckRequest_Quick_DTO> Requst_List = new List<EX_CheckRequest_Quick_DTO>();
            Requst_List = Session["Requst_List"] as List<EX_CheckRequest_Quick_DTO>;



            //var res2 = APIHandeling.Put("Committee_Farm_API?req=1", Requst_List);
            //var reqCommiteeType = res2.Content.ReadAsAsync<List<Farm_Requst_ListDTO>>().Result;
            //ViewBag.reqCommiteeType = reqCommiteeType;


            var resItem = APIHandeling.Put(api + "?reqQuk=1", Requst_List);
            var Lst = resItem.Content.ReadAsAsync<List<Ex_Committee_Quick_DTO>>().Result;


            Session["listshiftTimingsession"] = new List<EX_RequestCommittee_ShiftDTO>();

            Session["listshiftTimingsession_Index"] = 1;


            return View(Lst);
            //return null;
        }


        //shift timing
        [HttpPost]
        public JsonResult listshiftTiming
      (long EX_RequestCommittee_ID = 0, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = "")
        {
            try
            {
                var companiescount = Session["listshiftTimingsession"] as List<EX_RequestCommittee_ShiftDTO>;
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
        public JsonResult CreateshiftTiming(EX_RequestCommittee_ShiftDTO model)
        {
            try
            {
                var allErrors = ModelState.Values.SelectMany(x => x.Errors).ToList();

                if (ModelState.IsValid)
                {

                    model.Index = int.Parse(Session["listshiftTimingsession_Index"].ToString());

                    var companies = Session["listshiftTimingsession"] as List<EX_RequestCommittee_ShiftDTO>;
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
                var res = APIHandeling.getData(api + "?List=1");
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
                var res = APIHandeling.getData(api + "?shiftId=" + shiftId);
                var mony = res.Content.ReadAsAsync<Nullable<double>>().Result;
                return Json(new { Result = "OK", Options = mony }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "getShiftTiming_Lst");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }


        [HttpPost]
        public JsonResult GetPR_User_Id(string FullName, long EmplyeeNo,long Outlet_ID,long OutLet_HR_ID)
        {
            try
            {

                //string FullName, long EmplyeeNo, long OutLet_ID, long OutLet_HR_ID
                var Fees_Process = APIHandeling.getData("AddEmployee_API?FullName=" + FullName + "&EmplyeeNo="
                        + EmplyeeNo + "&OutLet_ID=" + Outlet_ID + "&OutLet_HR_ID="+ OutLet_HR_ID);
                var lst = Fees_Process.Content.ReadAsAsync<List<Ex_GetUserDTO>>().Result;
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
        public JsonResult Save_Committe(List<CommitteeType_Quick_DTO> CommitteeType            
            , List<EX_Employee_QuickDTO> emp_Dto,
            List<Committee_Items_Lot_QuickDTO> CheckedItemsList
           , List<EX_SampleData_QuickDTO> CheckedAnalysisList
            , List<EX_TreatmentMethod_QuicDTO> Checked_TreatmentMethod
            , List<Committee_Shift_QuickDTO> ShiftTiming_List
             , List<Committee_Eng_QuickDTO> EX_Fees_List)
        {

            //if (CheckedItemsList != null)
            //{
            //    bool? _IsApproved = true;

            EX_RequestCommittee_QuickDTO comm = new EX_RequestCommittee_QuickDTO();

         //الطلبات
            List<EX_CheckRequest_Quick_DTO> Requst_List = new List<EX_CheckRequest_Quick_DTO>();
            Requst_List = Session["Requst_List"] as List<EX_CheckRequest_Quick_DTO>;
            comm.ListEX_CheckRequest_Quick = Requst_List;//
            

            // اللجنة
            CommitteeType.FirstOrDefault().User_Creation_Date= DateTime.Now;
            CommitteeType.FirstOrDefault().User_Creation_Id = (short)Session["UserId"];
            comm.ListCommitteeType_Quick = CommitteeType;
            

            if (emp_Dto != null)
            {
                if (emp_Dto.Count > 0)
                {
                    comm.com_emp = emp_Dto;
                }
            }

            if (ShiftTiming_List != null)
            {
                if (ShiftTiming_List.Count > 0)
                {
                    comm.List_Committee_Shift = ShiftTiming_List;
                }
            }
            
            if (EX_Fees_List != null)
            {
                if (EX_Fees_List.Count > 0)
                {
                    comm.List_Committee_Eng = EX_Fees_List;
                }
            } 
            
            if (CheckedItemsList != null)
            {
                if (CheckedItemsList.Count > 0)
                {
                    comm.ListCommittee_Items_Lot = CheckedItemsList;
                }
            } 
            
            if (CheckedAnalysisList != null)
            {
                if (CheckedAnalysisList.Count > 0)
                {
                    comm.List_SampleData = CheckedAnalysisList;
                }
            }  
            if (Checked_TreatmentMethod != null)
            {
                if (Checked_TreatmentMethod.Count > 0)
                {
                    comm.List_TreatmentMethod= Checked_TreatmentMethod;
                }
            }

            #region MyRegion

         
            //comm.List_EX_CommitteeResultDTO = CheckedItemsList;
            //#region بيانات الرسالة

            ////    var CheckedAnalysisList_New = new List<EX_CheckRequest_SampleDataDTO>();
            ////    var Checked_TreatmentMethod_New = new List<EX_Checked_TreatmentMethodDTO>();
            ////    string barcode = "";

            ////    if (CheckedItemsList != null && CheckedItemsList.Count != 0)
            ////    {
            ////        if (CheckedAnalysisList != null)
            ////        {
            ////            if (CheckedItemsList.FirstOrDefault().IS_Total == true)//كلي
            ////            {
            ////                foreach (var item_Analysis in CheckedAnalysisList)
            ////                {
            ////                    string rand = RandomNumber(0, 100000).ToString();
            ////                    var dayofyear = "000" + DateTime.Now.DayOfYear;

            ////                    var zx = DateTime.Now.Year.ToString().Substring(2);
            ////                    var hour = (DateTime.Now.Hour).ToString("D" + 2);
            ////                    var min = (DateTime.Now.Minute).ToString("D" + 2);
            ////                    var sec = (DateTime.Now.Second).ToString("D" + 2);
            ////                    barcode = "73" + rand + dayofyear.Substring(dayofyear.Length - 3) + zx + hour + min + sec;
            ////                    item_Analysis.Sample_BarCode = barcode;
            ////                    //Thread.Sleep(500); // Sleep for 3 seconds
            ////                    //Random rd = new Random();
            ////                    //string rand = rd.Next(0, 100000).ToString("D5");
            ////                    //// save barcode
            ////                    //var dayofyear = "000" + DateTime.Now.DayOfYear;
            ////                    //var zx = DateTime.Now.Year.ToString().Substring(2);
            ////                    //var hour = (DateTime.Now.Hour).ToString("D" + 2);
            ////                    //var min = (DateTime.Now.Minute).ToString("D" + 2);
            ////                    //var sec = (DateTime.Now.Second).ToString("D" + 2);
            ////                    //barcode = "73" + rand + dayofyear.Substring(dayofyear.Length - 3) + zx + hour + min + sec;
            ////                    //item_Analysis.Sample_BarCode = barcode;
            ////                }
            ////            }
            ////        }
            ////        foreach (var item in CheckedItemsList)
            ////        {
            ////            // item.Committee_ID = long.Parse(Session["reqComm_ID"].ToString());
            ////            item.User_Creation_Id = (short)Session["UserId"];
            ////            item.User_Creation_Date = DateTime.Now;
            ////            if (CheckedAnalysisList != null)
            ////            {
            ////                if (CheckedAnalysisList != null && CheckedAnalysisList.Count != 0)
            ////                {
            ////                    Random rd = new Random();
            ////                    foreach (var item_Analysis in CheckedAnalysisList)
            ////                    {
            ////                        if (item.Item_ShortName_ID == item_Analysis.Item_ShortName_ID)
            ////                        {
            ////                            long Short_ID = item.Item_ShortName_ID;
            ////                            //if (item_Analysis.EX_Request_Item_Id == Short_ID)
            ////                            {
            ////                                //var AnalysisLabType_Old = item_Analysis.AnalysisLabType_ID;
            ////                                //الباركود

            ////                                if (item.IS_Total == false) //جزئي
            ////                                {
            ////                                    string rand = RandomNumber(0, 100000).ToString();
            ////                                    var dayofyear = "000" + DateTime.Now.DayOfYear;

            ////                                    var zx = DateTime.Now.Year.ToString().Substring(2);
            ////                                    var hour = (DateTime.Now.Hour).ToString("D" + 2);
            ////                                    var min = (DateTime.Now.Minute).ToString("D" + 2);
            ////                                    var sec = (DateTime.Now.Second).ToString("D" + 2);
            ////                                    barcode = "73" + rand + dayofyear.Substring(dayofyear.Length - 3) + zx + hour + min + sec;
            ////                                    item_Analysis.Sample_BarCode = barcode;
            ////                                    //barcode = "";
            ////                                    ////Thread.Sleep(500); // Sleep for 3 seconds
            ////                                    //string rand = rd.Next(0, 100000).ToString("D5");
            ////                                    //// save barcode
            ////                                    //var dayofyear = "000" + DateTime.Now.DayOfYear;
            ////                                    //var zx = DateTime.Now.Year.ToString().Substring(2);
            ////                                    //var hour = (DateTime.Now.Hour).ToString("D" + 2);
            ////                                    //var min = (DateTime.Now.Minute).ToString("D" + 2);
            ////                                    //var sec = (DateTime.Now.Second).ToString("D" + 2);
            ////                                    //barcode = "73" + rand + dayofyear.Substring(dayofyear.Length - 3) + zx + hour + min + sec;
            ////                                }
            ////                                else
            ////                                {
            ////                                    barcode = item_Analysis.Sample_BarCode;
            ////                                }

            ////                                //if (l)
            ////                                //var asd = Committee_Sample_Lot
            ////                                var location = new EX_CheckRequest_SampleDataDTO
            ////                                {
            ////                                    AnalysisLabType_ID = item_Analysis.AnalysisLabType_ID,
            ////                                    EX_Request_Item_Id = item.EX_Request_Item_Id,
            ////                                    LotData_ID = item.LotData_ID,
            ////                                    User_Creation_Id = (short)Session["UserId"],
            ////                                    User_Creation_Date = DateTime.Now,

            ////                                    IS_Total = item.IS_Total,
            ////                                    Item_ShortName_ID = item.Item_ShortName_ID,
            ////                                    //eslam

            ////                                    Sample_BarCode = barcode,
            ////                                    Amount = item_Analysis.Amount,
            ////                                    Count_Sample = item_Analysis.Count_Sample,
            ////                                };
            ////                                CheckedAnalysisList_New.Add(location);
            ////                            }
            ////                        }
            ////                    }
            ////                }
            ////            }

            ////            if (Checked_TreatmentMethod != null && Checked_TreatmentMethod.Count != 0)
            ////            {
            ////                foreach (var item_Treatment in Checked_TreatmentMethod)
            ////                {
            ////                    long Short_ID = item.Item_ShortName_ID;
            ////                    if (item_Treatment.EX_Request_Item_Id == Short_ID)
            ////                    {
            ////                        //if (l)
            ////                        //var asd = Committee_Sample_Lot
            ////                        var location = new EX_Checked_TreatmentMethodDTO
            ////                        {
            ////                            TreatmentType_ID = item_Treatment.TreatmentType_ID,
            ////                            TreatmentMethod_ID = item_Treatment.TreatmentMethod_ID,

            ////                            EX_Request_Item_Id = item.EX_Request_Item_Id,
            ////                            EX_Request_LotData_ID = item.LotData_ID,
            ////                            User_Creation_Id = (short)Session["UserId"],
            ////                            User_Creation_Date = DateTime.Now,

            ////                            IS_Total = item.IS_Total,
            ////                            Item_ShortName_ID = item.Item_ShortName_ID,
            ////                            Procedures = item_Treatment.Procedures,
            ////                        };
            ////                        Checked_TreatmentMethod_New.Add(location);
            ////                    }
            ////                }
            ////            }
            ////        }
            ////    }

            ////    if (CommitteeType_ID == 1 || CommitteeType_ID == 2)
            ////    {
            ////        comm.List_CommitteeResult = CheckedItemsList;
            ////        //comm.List_SampleData = CheckedAnalysisList_New;
            ////    }
            ////    else if (CommitteeType_ID == 3)
            ////    {
            ////        comm.List_SampleData = CheckedAnalysisList_New;
            ////    }
            ////    else if (CommitteeType_ID == 6)
            ////    {

            ////        //if (Checked_TreatmentMethod != null && Checked_TreatmentMethod.Count != 0)
            ////        //{
            ////        //    foreach (var item_TreatmentMethod in Checked_TreatmentMethod)
            ////        //    {
            ////        //        item_TreatmentMethod.User_Creation_Id = (short)Session["UserId"];
            ////        //        item_TreatmentMethod.User_Creation_Date = DateTime.Now;
            ////        //        long Short_ID = item.Item_ShortName_ID;
            ////        //        //if (item_TreatmentMethod.Im_Request_Item_Id == Short_ID)
            ////        //        //{
            ////        //        //if (l)
            ////        //        //var asd = Committee_Sample_Lot
            ////        //        var location = new Checked_TreatmentMethodDTO
            ////        //            {
            ////        //                Im_Request_Item_Id = item_TreatmentMethod.Im_Request_Item_Id,
            ////        //                // Im_RequestCommittee_ID = long.Parse(Session["reqComm_ID"].ToString()),
            ////        //                // Im_Request_Item_Id = item_TreatmentMethod.Im_Request_Item_Id,
            ////        //                Im_Request_LotData_ID = item_TreatmentMethod.Im_Request_LotData_ID,
            ////        //                User_Creation_Id = (short)Session["UserId"],
            ////        //                User_Creation_Date = DateTime.Now,

            ////        //                IS_Total = item_TreatmentMethod.IS_Total,
            ////        //                Item_ShortName_ID = Short_ID,
            ////        //            };
            ////        //        Checked_TreatmentMethod_New.Add(location);
            ////        //        //}
            ////        //    }
            ////        //}
            ////        comm.List_TreatmentMethod = Checked_TreatmentMethod_New;
            ////    }

            #endregion

            string Mess = "ok";
            var res2 = APIHandeling.Put(api + "?req=1", comm);
            var list = res2.Content.ReadAsAsync<Dictionary<string, object>>().Result;
            if (list != null)
            {
                foreach (var item in list)
                {
                    if (item.Key == "message")
                    {
                        // return Json(item.Value, JsonRequestBehavior.AllowGet);
                        if (item.Value != null)
                        {
                            Mess = item.Value.ToString();
                        }
                    }
                    //item.Value
                }
                return Json(Mess, JsonRequestBehavior.AllowGet);
            }
            else
                return Json("error", JsonRequestBehavior.AllowGet);
        
            //else
            //{
            //return null;
            //}
        }

       
        public static int RandomNumber(int min, int max)
        {
            lock (syncLock)
            { // synchronize
                return random.Next(min, max);
            }
        }

        [HttpPost]
        public JsonResult TreatmentMethod_AddEDIT(long TreatmentTypeId)
        {
            
            try
            {
                var res = APIHandeling.getData("TreatmentMethod_API?TreatmentTypeId="+ TreatmentTypeId);
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "TreatmentMethod_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
    }
}