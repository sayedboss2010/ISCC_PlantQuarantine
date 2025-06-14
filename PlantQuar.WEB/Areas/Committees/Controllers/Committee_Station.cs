using PlantQuar.DTO.DTO.Admin;
using PlantQuar.DTO.DTO.Station_Pages;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using PlantQuar.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.Committees.Controllers
{
    public class Committee_StationController : BaseController
    {
        // GET: Committees/Committee_Station
        string apiName = "Committee_Station_API";

        public ActionResult Index(long Station_Requst_ID, int Accreditation_Type_ID)
        {
            Session["Station_Requst_ID"] = Station_Requst_ID;
            ViewBag.reqCommiteeType = 4;

            ViewBag.Station_Requst_ID = Station_Requst_ID;
            ViewBag.Accreditation_Type_ID = Accreditation_Type_ID;

            //full outlit

            //نريد التأكد من نوع المنفذ اذا كان صادر ام وارد
            //لو صادر نجيب كل المنافذ
            // لو وارد يكون الاختيار حسب المنفذ الخاص بتشكيل اللجنة
            if (Accreditation_Type_ID == 80)//لو صادر
            {
                var Fees_Process = APIHandeling.getData("Employee_Track_API?Outlet=-1");
                var lst = Fees_Process.Content.ReadAsAsync<List<CustomOption>>().Result;
                ViewBag.Outlet = lst;
            }
            else
            {
                ViewBag.Outlet = null;
            }

            //fill shift
            var shiftType = APIHandeling.getData("A_SystemCode_API?AddEdit=1&Sysnum=26");
            var ShiftTypes = shiftType.Content.ReadAsAsync<List<CustomOption>>().Result;
            ViewBag.ShiftTypes = ShiftTypes;          
            return View("Index");
        }
        
     
        [HttpPost]
        public JsonResult GetPR_User_Id(string FullName, long EmplyeeNo, long OutLet_ID)
        {
            try
            {
                if (OutLet_ID == 0)
                {
                    if (Session["Outlet_Hr"] != null)
                    {
                        var _Outlet_ID = long.Parse(Session["Outlet_Hr"].ToString());
                        OutLet_ID = _Outlet_ID;
                    }
                }
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

        public JsonResult Save_Committe(long Station_Requst_ID,DateTime? Check_Date, DateTime? Delegation_Date, TimeSpan? StartTime, TimeSpan? EndTime,
       List<EmployeeDTO> emp_Dto,
       List<Station_Committee_ShiftDTO> ShiftTiming_List,
       List<Station_Request_Fees_DTO> Station_Fees_List)
        {
            //List<Farm_Requst_ListDTO> Farm_Requst_List = new List<Farm_Requst_ListDTO>();
            //Farm_Requst_List = Session["Farm_Requst_List"] as List<Farm_Requst_ListDTO>;

            //Station_Requst_ID = Session["Farm_Requst_List"]
            if (Station_Requst_ID != null)
            {
                
                var Farm_Commity_data_List = new List<Station_Committee_Data_DTO>();
                //foreach (var item in Farm_Requst_List)
                //{
                    byte CommitteeType_ID = 4;

                    var location = new Station_Committee_Data_DTO
                    {
                        //ID = item.Farm_Committee_ID,
                        Station_Request_ID = Station_Requst_ID,
                        Delegation_Date = (DateTime)Delegation_Date,
                        StartTime = StartTime,
                        EndTime = EndTime,
                        User_Updation_Id = (short)Session["UserId"],
                        User_Updation_Date = DateTime.Now,
                        User_Creation_Id = (short)Session["UserId"],
                        User_Creation_Date = DateTime.Now,
                        OperationType = 79,
                        CommitteeType_ID = CommitteeType_ID
                    };
                    Farm_Commity_data_List.Add(location);
                //}
                //    #region اللجنة
                Committee_Station_DTO comm = new Committee_Station_DTO();
                comm.List_Committee_Station = Farm_Commity_data_List;
                comm.List_emp = emp_Dto;
                comm.List_ShiftTiming = ShiftTiming_List;
                comm.List_Station_Request_Fees = Station_Fees_List;
                string Mess = "ok";
                var res2 = APIHandeling.Put("Committee_Station_API?Insert_req=1", comm);
                var list = res2.Content.ReadAsAsync<Dictionary<string, object>>().Result;
                if (list != null)
                {
                    foreach (var item in list)
                    {
                        if (item.Key == "Message")
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
    }
}