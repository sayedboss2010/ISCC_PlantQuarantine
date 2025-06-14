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


namespace PlantQuar.WEB.Areas.Committees.Controllers
{
    public class Ex_CommitteeController : BaseController
    {
        // GET: Committees/Ex_Committee
        string api = "Ex_Committee_API";

        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();
        public ActionResult Index(long? requestId, long? Outlet_Examination_ID, long? Station_Examination_ID
            , long? Outlet_Genshi_ID, long? Station_Genshi_ID)
        {
            ViewBag.View_Outlet_Examination = 1;
            if (Session["CanView"].ToString() == "False")// منفذ فحص
            {
                ViewBag.View_Outlet_Examination = 0;
            }
            ViewBag.Add_Station_Examination = 1;
            if (Session["CanAdd"].ToString() == "False")//محطة فحص
            {
                ViewBag.Add_Station_Examination = 0;
            }

            ViewBag.Edit_Outlet_Genshi = 1;
            if (Session["CanEdit"].ToString() == "False")// منفذ جشني
            {
                ViewBag.Edit_Outlet_Genshi = 0;
            }

            ViewBag.Delete_Station_Genshi = 1;
            if (Session["CanDelete"].ToString() == "False")// محطة جشني
            {
                ViewBag.Delete_Station_Genshi = 0;
            }
            // HttpCookie userInfo = new HttpCookie("user_Data");

            Response.Cookies["Outlet_Examination_ID"].Value = Outlet_Examination_ID.ToString();
            Response.Cookies["Station_Examination_ID"].Value = Station_Examination_ID.ToString();
            Response.Cookies["Outlet_Genshi_ID"].Value = Outlet_Genshi_ID.ToString();
            Response.Cookies["Station_Genshi_ID"].Value = Station_Genshi_ID.ToString();


            var res = APIHandeling.getData("Ex_Committee_API?requestId=" + requestId);
            var req = res.Content.ReadAsAsync<EX_CheckRequest_Committee_DTO>().Result;
            //       ViewBag.creationDate = req.User_Creation_Date;
            ViewBag.requestNumber = req.CheckRequest_Number;
            ViewBag.requestId = requestId;

            //nooooooooooo
            var res_Status = APIHandeling.getData(api + "?reqCommitte=1&requestId=" + requestId);
            var return_Status = res_Status.Content.ReadAsAsync<List<Get_Ex_CountryConstrain_DTO>>().Result;
            ViewBag.CommitteeTypeId = return_Status.FirstOrDefault().Count_Type_Commite;

            //nooooooooooo

            //var Fees_Process = APIHandeling.getData("Employee_Track_API?Outlet=-1");
            //var lst = Fees_Process.Content.ReadAsAsync<List<CustomOption>>().Result;
            //ViewBag.Outlet = lst;

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
            var resItem = APIHandeling.getData(api + "?EX_CheckRequest_ID=" + requestId);
            var Lst = resItem.Content.ReadAsAsync<Ex_Committee_DTO>().Result;
            Session["listshiftTimingsession"] = new List<EX_RequestCommittee_ShiftDTO>();

            Session["listshiftTimingsession_Index"] = 1;
            //Session["Committee_Sample_Lot"] = new List<Committee_Sample_Lot>();
            //var dd = Lst.itemsWithConstrains.ToList();
            //var df = dd.Select(a => a.ItemCategories_lots).ToList();



            // var SampleList = Lst.itemsWithConstrains.Select(a => a.ItemCategories_lots.Select(b => b.list_Committee_Sample_Lot)).ToList();
            // Session["Committee_Sample_Lot"] = SampleList;

            return View(Lst);

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

        //public JsonResult GetPR_User_Id(string FullName, long EmplyeeNo, long OutLet_ID)
        //{
        //    try
        //    {
        //        var Fees_Process = APIHandeling.getData("AddEmployee_API?FullName=" + FullName + "&EmplyeeNo=" + EmplyeeNo
        //            + "&OutLet_ID=" + OutLet_ID + "&Type_ID_HR=1");
        //        var lst = Fees_Process.Content.ReadAsAsync<List<User>>().Result;
        //        //  lst.RemoveAt(0);
        //        return Json(lst, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetPR_User_Id");
        //        return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
        //    }
        //}

        [HttpPost]
        public JsonResult GetPR_User_Id(string FullName, long EmplyeeNo, int CommitteeType_ID,
            long Outlet_Examination_ID, long Station_Examination_ID, long Outlet_Genshi_ID, long Station_Genshi_ID)
        {
            try
            {

                var User_OutLet_ID = long.Parse(Session["Outlet_ID"].ToString());

                List<long> User_station =new List<long>();

                
                if (Session["user_Station_ID"] != null)
                {
                    //var list2 = HttpContext.Session["Current"] as (List<long>);
                   // var list = (List<long>)Session["user_Station_ID"] as List<long>;
                    User_station = Session["user_Station_ID"] as List<long>;
                    //User_station = long.Parse(Session["user_Station_ID"].ToString());
                }





                //if (Request.Cookies["Outlet_Examination_ID"].Value != String.Empty)
                //{
                //    Outlet_Examination_ID = Outlet_Examination_ID;
                //}
                //if (Request.Cookies["Station_Examination_ID"].Value != String.Empty)
                //{
                //    Station_Examination_ID = long.Parse(Request.Cookies["Station_Examination_ID"].Value);
                //}
                //if (Request.Cookies["Outlet_Genshi_ID"].Value != String.Empty)
                //{
                //    Outlet_Genshi_ID = long.Parse(Request.Cookies["Outlet_Genshi_ID"].Value);
                //}
                //if (Request.Cookies["Station_Genshi_ID"].Value != String.Empty)
                //{
                //    Station_Genshi_ID = long.Parse(Request.Cookies["Station_Genshi_ID"].Value);
                //}

                if ((Station_Examination_ID > 0 && User_station.Where(a => a == Station_Examination_ID).Count()>0 && (CommitteeType_ID == 1 || CommitteeType_ID == 3 || CommitteeType_ID == 6))
                    || (Station_Genshi_ID > 0 &&  User_station.Where(a => a == Station_Genshi_ID).Count()>0 && (CommitteeType_ID == 2)))

                    //if ((Station_Examination_ID > 0  && (CommitteeType_ID == 1 || CommitteeType_ID == 3 || CommitteeType_ID == 6)) 
                    //|| (Station_Genshi_ID > 0  && (CommitteeType_ID == 2)))
                {
                    long Station_ID = 0;
                    if (CommitteeType_ID == 1 || CommitteeType_ID == 3 || CommitteeType_ID == 6)
                    {
                        Station_ID = Station_Examination_ID;
                    }
                    else if (CommitteeType_ID == 2)
                    {
                        Station_ID = Station_Genshi_ID;
                    }
                    if (!string.IsNullOrEmpty(Session["user_Station_ID"] as string))
                    {
                        long user_Station_ID = long.Parse(Session["user_Station_ID"].ToString());

                    }

                    var Fees_Process2 = APIHandeling.getData("AddEmployee_API?FullName=" + FullName + "&EmplyeeNo="
                 + EmplyeeNo + "&OutLet_ID=0&user_Station_ID=" + Station_ID);
                    var lst2 = Fees_Process2.Content.ReadAsAsync<List<User>>().Result;

                    return Json(lst2, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    long Outlet_ID = 0;
                    if (CommitteeType_ID == 1 || CommitteeType_ID == 3 || CommitteeType_ID == 6)
                    {
                        Outlet_ID = Outlet_Examination_ID;
                    }
                    else if (CommitteeType_ID == 2)
                    {
                        Outlet_ID = Outlet_Genshi_ID;
                    }
                    if (User_OutLet_ID == Outlet_ID)
                    {
                        var Fees_Process = APIHandeling.getData("AddEmployee_API?FullName=" + FullName + "&EmplyeeNo="
                        + EmplyeeNo + "&OutLet_ID=" + Outlet_ID + "&Type_ID_HR=0");
                        var lst = Fees_Process.Content.ReadAsAsync<List<User>>().Result;
                        //  lst.RemoveAt(0);
                        return Json(lst, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        List<User> lst = new List<User>();
                        return Json(lst, JsonRequestBehavior.AllowGet);
                    }

                }

            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetPR_User_Id");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }


        [HttpPost]
        public JsonResult Save_Committe(byte? CommitteeType_ID, long? CheckRequest_Id,
         DateTime? Check_Date, DateTime? Delegation_Date, TimeSpan? StartTime, TimeSpan? EndTime
            , List<EX_EmployeeDTO> emp_Dto,
         List<EX_CommitteeResultDTO> CheckedItemsList
           , List<EX_CheckRequest_SampleDataDTO> CheckedAnalysisList
            , List<EX_Checked_TreatmentMethodDTO> Checked_TreatmentMethod
            , List<EX_RequestCommittee_ShiftDTO> ShiftTiming_List
             , List<Ex_Request_Fees_Eng_DTO> EX_Fees_List)
        {
            if (CheckedItemsList != null)
            {
                bool? _IsApproved = true;
                //if (CommitteeType_ID == 6)

                //    _IsApproved = null;
                //else if (CommitteeType_ID == 2)
                //{
                //    _IsApproved = true;
                //}


                //   ViewBag.StationCode = barcode;

                #region اللجنة
                EX_RequestCommitteeDTO comm = new EX_RequestCommitteeDTO();
                comm.EX_CheckRequest_ID = CheckRequest_Id;
                comm.CommitteeType_ID = CommitteeType_ID;
                comm.IsApproved = _IsApproved;// Checked_TreatmentMethod == null ?true: null;
                comm.Status = false;
                comm.Delegation_Date = (DateTime)Delegation_Date;
                comm.StartTime = (TimeSpan)StartTime;
                comm.EndTime = (TimeSpan)EndTime;
                User_Session Current = User_Session.GetInstance;
                comm.User_Creation_Id = (short)Session["UserId"];
                comm.User_Creation_Date = DateTime.Now;

                //comm.List_SampleData = barcode;
                #endregion

                comm.com_emp = emp_Dto;
                // النوبتجية
                List<EX_RequestCommittee_ShiftDTO> Shift_Data = new List<EX_RequestCommittee_ShiftDTO>();
                Shift_Data = ShiftTiming_List;//= Session["listshiftTimingsession"] as List<EX_RequestCommittee_ShiftDTO>;

                //// المعامل السابقه
                // List<Committee_Sample_Lot> SamplesOld = new List<Committee_Sample_Lot>();
                //SamplesOld = Session["Committee_Sample_Lot"] as List<Committee_Sample_Lot>;

                ////var Sampless = (List<CheckRequest_SampleDataDTO>)Session["Committee_Sample_Lot"];


                comm.List_Committee_Shift = ShiftTiming_List;

                #region البدلات
                if (EX_Fees_List != null)
                {
                    if (EX_Fees_List.Count > 0)
                    {
                        comm.List_Ex_Request_Fees_Eng = EX_Fees_List;
                    }
                }
                #endregion

                #region بيانات الرسالة

                var CheckedAnalysisList_New = new List<EX_CheckRequest_SampleDataDTO>();
                var Checked_TreatmentMethod_New = new List<EX_Checked_TreatmentMethodDTO>();
                string barcode = "";

                if (CheckedItemsList != null && CheckedItemsList.Count != 0)
                {
                    if (CheckedAnalysisList != null)
                    {
                        if (CheckedItemsList.FirstOrDefault().IS_Total == true)//كلي
                        {
                            foreach (var item_Analysis in CheckedAnalysisList)
                            {
                                string rand = RandomNumber(0, 100000).ToString();
                                var dayofyear = "000" + DateTime.Now.DayOfYear;

                                var zx = DateTime.Now.Year.ToString().Substring(2);
                                var hour = (DateTime.Now.Hour).ToString("D" + 2);
                                var min = (DateTime.Now.Minute).ToString("D" + 2);
                                var sec = (DateTime.Now.Second).ToString("D" + 2);
                                barcode = "73" + rand + dayofyear.Substring(dayofyear.Length - 3) + zx + hour + min + sec;
                                item_Analysis.Sample_BarCode = barcode;
                                //Thread.Sleep(500); // Sleep for 3 seconds
                                //Random rd = new Random();
                                //string rand = rd.Next(0, 100000).ToString("D5");
                                //// save barcode
                                //var dayofyear = "000" + DateTime.Now.DayOfYear;
                                //var zx = DateTime.Now.Year.ToString().Substring(2);
                                //var hour = (DateTime.Now.Hour).ToString("D" + 2);
                                //var min = (DateTime.Now.Minute).ToString("D" + 2);
                                //var sec = (DateTime.Now.Second).ToString("D" + 2);
                                //barcode = "73" + rand + dayofyear.Substring(dayofyear.Length - 3) + zx + hour + min + sec;
                                //item_Analysis.Sample_BarCode = barcode;
                            }
                        }
                    }
                    foreach (var item in CheckedItemsList)
                    {
                        // item.Committee_ID = long.Parse(Session["reqComm_ID"].ToString());
                        item.User_Creation_Id = (short)Session["UserId"];
                        item.User_Creation_Date = DateTime.Now;
                        if (CheckedAnalysisList != null)
                        {
                            if (CheckedAnalysisList != null && CheckedAnalysisList.Count != 0)
                            {
                                Random rd = new Random();
                                foreach (var item_Analysis in CheckedAnalysisList)
                                {
                                    if (item.Item_ShortName_ID == item_Analysis.Item_ShortName_ID)
                                    {
                                        long Short_ID = item.Item_ShortName_ID;
                                        //if (item_Analysis.EX_Request_Item_Id == Short_ID)
                                        {
                                            //var AnalysisLabType_Old = item_Analysis.AnalysisLabType_ID;
                                            //الباركود

                                            if (item.IS_Total == false) //جزئي
                                            {
                                                string rand = RandomNumber(0, 100000).ToString();
                                                var dayofyear = "000" + DateTime.Now.DayOfYear;

                                                var zx = DateTime.Now.Year.ToString().Substring(2);
                                                var hour = (DateTime.Now.Hour).ToString("D" + 2);
                                                var min = (DateTime.Now.Minute).ToString("D" + 2);
                                                var sec = (DateTime.Now.Second).ToString("D" + 2);
                                                barcode = "73" + rand + dayofyear.Substring(dayofyear.Length - 3) + zx + hour + min + sec;
                                                item_Analysis.Sample_BarCode = barcode;
                                                //barcode = "";
                                                ////Thread.Sleep(500); // Sleep for 3 seconds
                                                //string rand = rd.Next(0, 100000).ToString("D5");
                                                //// save barcode
                                                //var dayofyear = "000" + DateTime.Now.DayOfYear;
                                                //var zx = DateTime.Now.Year.ToString().Substring(2);
                                                //var hour = (DateTime.Now.Hour).ToString("D" + 2);
                                                //var min = (DateTime.Now.Minute).ToString("D" + 2);
                                                //var sec = (DateTime.Now.Second).ToString("D" + 2);
                                                //barcode = "73" + rand + dayofyear.Substring(dayofyear.Length - 3) + zx + hour + min + sec;
                                            }
                                            else
                                            {
                                                barcode = item_Analysis.Sample_BarCode;
                                            }

                                            //if (l)
                                            //var asd = Committee_Sample_Lot
                                            var location = new EX_CheckRequest_SampleDataDTO
                                            {
                                                AnalysisLabType_ID = item_Analysis.AnalysisLabType_ID,
                                                EX_Request_Item_Id = item.EX_Request_Item_Id,
                                                LotData_ID = item.LotData_ID,
                                                User_Creation_Id = (short)Session["UserId"],
                                                User_Creation_Date = DateTime.Now,

                                                IS_Total = item.IS_Total,
                                                Item_ShortName_ID = item.Item_ShortName_ID,
                                                //eslam

                                                Sample_BarCode = barcode,
                                                Amount = item_Analysis.Amount,
                                                Count_Sample = item_Analysis.Count_Sample,
                                            };
                                            CheckedAnalysisList_New.Add(location);
                                        }
                                    }
                                }
                            }
                        }

                        if (Checked_TreatmentMethod != null && Checked_TreatmentMethod.Count != 0)
                        {
                            foreach (var item_Treatment in Checked_TreatmentMethod)
                            {
                                long Short_ID = item.Item_ShortName_ID;
                                if (item_Treatment.EX_Request_Item_Id == Short_ID)
                                {
                                    //if (l)
                                    //var asd = Committee_Sample_Lot
                                    var location = new EX_Checked_TreatmentMethodDTO
                                    {
                                        TreatmentType_ID = item_Treatment.TreatmentType_ID,
                                        TreatmentMethod_ID = item_Treatment.TreatmentMethod_ID,

                                        EX_Request_Item_Id = item.EX_Request_Item_Id,
                                        EX_Request_LotData_ID = item.LotData_ID,
                                        User_Creation_Id = (short)Session["UserId"],
                                        User_Creation_Date = DateTime.Now,

                                        IS_Total = item.IS_Total,
                                        Item_ShortName_ID = item.Item_ShortName_ID,
                                        Procedures = item_Treatment.Procedures,
                                    };
                                    Checked_TreatmentMethod_New.Add(location);
                                }
                            }
                        }
                    }
                }

                if (CommitteeType_ID == 1 || CommitteeType_ID == 2)
                {
                    comm.List_CommitteeResult = CheckedItemsList;
                    //comm.List_SampleData = CheckedAnalysisList_New;
                }
                else if (CommitteeType_ID == 3)
                {
                    comm.List_SampleData = CheckedAnalysisList_New;
                }
                else if (CommitteeType_ID == 6)
                {

                    //if (Checked_TreatmentMethod != null && Checked_TreatmentMethod.Count != 0)
                    //{
                    //    foreach (var item_TreatmentMethod in Checked_TreatmentMethod)
                    //    {
                    //        item_TreatmentMethod.User_Creation_Id = (short)Session["UserId"];
                    //        item_TreatmentMethod.User_Creation_Date = DateTime.Now;
                    //        long Short_ID = item.Item_ShortName_ID;
                    //        //if (item_TreatmentMethod.Im_Request_Item_Id == Short_ID)
                    //        //{
                    //        //if (l)
                    //        //var asd = Committee_Sample_Lot
                    //        var location = new Checked_TreatmentMethodDTO
                    //            {
                    //                Im_Request_Item_Id = item_TreatmentMethod.Im_Request_Item_Id,
                    //                // Im_RequestCommittee_ID = long.Parse(Session["reqComm_ID"].ToString()),
                    //                // Im_Request_Item_Id = item_TreatmentMethod.Im_Request_Item_Id,
                    //                Im_Request_LotData_ID = item_TreatmentMethod.Im_Request_LotData_ID,
                    //                User_Creation_Id = (short)Session["UserId"],
                    //                User_Creation_Date = DateTime.Now,

                    //                IS_Total = item_TreatmentMethod.IS_Total,
                    //                Item_ShortName_ID = Short_ID,
                    //            };
                    //        Checked_TreatmentMethod_New.Add(location);
                    //        //}
                    //    }
                    //}
                    comm.List_TreatmentMethod = Checked_TreatmentMethod_New;
                }

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
            }
            else
            {
                return null;
            }
        }

        [HttpPost]
        public JsonResult GetAllTreatmentDataForShortnameId(long EX_CheckRequest_ID, long ExportCountry_Id, long EX_shortnameId)

        {
            try
            {
                var res = APIHandeling.getData(api + "?EX_CheckRequest_ID=" + EX_CheckRequest_ID + "&ExportCountry_Id=" + ExportCountry_Id + "&EX_shortnameId=" + EX_shortnameId);
                var lst = res.Content.ReadAsAsync<AllTreatmentDataForShortnameIdDTO>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "getShiftTiming_Lst");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        public static int RandomNumber(int min, int max)
        {
            lock (syncLock)
            { // synchronize
                return random.Next(min, max);
            }
        }
    }
}