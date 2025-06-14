
using PlantQuar.DTO.DTO.Export_CheckRequest;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using PlantQuar.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.Export_CheckRequest.Controllers
{
    public class EX_CheckRequestDetailsController : BaseController
    {
        // GET: Export_CheckRequest/EX_CheckRequestDetails

        public ActionResult Index(string ImCheckRequest_Number, long? Station_Examination_ID, long? Outlet_Examination_ID, long? Outlet_Genshi_ID, long? Station_Genshi_ID, int Hide_Button = 0)
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

            ViewBag.Delete_Outlet_Genshi = 1;
            if (Session["CanDelete"].ToString() == "False")// محطة جشني
            {
                ViewBag.Delete_Outlet_Genshi = 0;
            }


            var res = APIHandeling.getData("EX_CheckRequestDetails_API?ExCheckRequest_Number=" + ImCheckRequest_Number);
            //var Lst = res.Content.ReadAsAsync<EXRequestDetailsDTO>().Result;
            /// var Lst = res.Content.ReadAsAsync<>().Result;
            var Lst = res.Content.ReadAsAsync<List<EXRequestDetailsDTO>>().Result;

            var reasons = APIHandeling.getData("EX_CheckRequestDetails_API?List=1&refuse=1");
            var reasonsList = reasons.Content.ReadAsAsync<List<CustomOption>>().Result;
            ViewBag.ListReasons = reasonsList;
            ViewBag.Hide_Button = Hide_Button;
            var User_OutLet_ID = long.Parse(Session["Outlet_ID"].ToString());
            List<long> User_station = new List<long>();


            if (Session["user_Station_ID"] != null)
            {

                User_station = Session["user_Station_ID"] as List<long>;
            }

            //long User_station = 0;
            //if (Session["user_Station_ID"] != null)
            //{
            //    User_station = long.Parse(Session["user_Station_ID"].ToString());
            //}
            ViewBag.User_OutLet_ID = User_OutLet_ID;
            ViewBag.User_station = User_station;
            ViewBag.Station_Examination_ID = Station_Examination_ID;
            ViewBag.Outlet_Examination_ID = Outlet_Examination_ID;




            TempData["Station_Examination_ID"] = Station_Examination_ID;
            TempData["Outlet_Examination_ID"] = Outlet_Examination_ID;
            TempData["Outlet_Genshi_ID"] = Outlet_Genshi_ID;

            TempData["Station_Genshi_ID"] = Station_Genshi_ID;




            return View(Lst);
        }


        public ActionResult IndexNew(string ImCheckRequest_Number, long? Station_Examination_ID, long? Outlet_Examination_ID, long? Outlet_Genshi_ID, long? Station_Genshi_ID, int Hide_Button = 0)
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

            ViewBag.Delete_Outlet_Genshi = 1;
            if (Session["CanDelete"].ToString() == "False")// محطة جشني
            {
                ViewBag.Delete_Outlet_Genshi = 0;
            }


            var res = APIHandeling.getData("EX_CheckRequestDetails_API?ExCheckRequest_Number=" + ImCheckRequest_Number);
            //var Lst = res.Content.ReadAsAsync<EXRequestDetailsDTO>().Result;
            /// var Lst = res.Content.ReadAsAsync<>().Result;
            var Lst = res.Content.ReadAsAsync<List<EXRequestDetailsDTO>>().Result;

            var reasons = APIHandeling.getData("EX_CheckRequestDetails_API?List=1&refuse=1");
            var reasonsList = reasons.Content.ReadAsAsync<List<CustomOption>>().Result;
            ViewBag.ListReasons = reasonsList;
            ViewBag.Hide_Button = Hide_Button;
            var User_OutLet_ID = long.Parse(Session["Outlet_ID"].ToString());
            List<long> User_station = new List<long>();


            if (Session["user_Station_ID"] != null)
            {

                User_station = Session["user_Station_ID"] as List<long>;
            }

            //long User_station = 0;
            //if (Session["user_Station_ID"] != null)
            //{
            //    User_station = long.Parse(Session["user_Station_ID"].ToString());
            //}
            ViewBag.User_OutLet_ID = User_OutLet_ID;
            ViewBag.User_station = User_station;
            ViewBag.Station_Examination_ID = Station_Examination_ID;
            ViewBag.Outlet_Examination_ID = Outlet_Examination_ID;




            TempData["Station_Examination_ID"] = Station_Examination_ID;
            TempData["Outlet_Examination_ID"] = Outlet_Examination_ID;
            TempData["Outlet_Genshi_ID"] = Outlet_Genshi_ID;

            TempData["Station_Genshi_ID"] = Station_Genshi_ID;




            return View(Lst);
        }

        [HttpPost]
        public ActionResult saveReasons(List<short> listIDs, long checkReqId, string rejreasons_text)
        {
            ReasonsListReqIdDTO dto = new ReasonsListReqIdDTO();
            //  EX_CheckRequestDTO dto2 = new EX_CheckRequestDTO();
            dto.checkReqId = checkReqId;
            dto.refuseReasonsIds = listIDs;
            dto.Notes_Reject = rejreasons_text;
            //  dto2.ID = checkReqId;
            User_Session Current = User_Session.GetInstance;
            dto.User_Creation_Id = (short)Session["UserId"];
            dto.User_Id = (short)Session["UserId"];
            dto.User_Creation_Date = DateTime.Now;
            APIHandeling.Post("EX_CheckRequestDetails_API?listt=1", dto);
            return Json("succ");
        }

        [HttpPost]
        public ActionResult acceptRequest(EXRequestDetailsDTO model)
        {
            try
            {
                EX_CheckRequestDTO dto = new EX_CheckRequestDTO();
                dto.ID = model.Ex_CheckRequest_ID;
                dto.IsAccepted = (bool)model.IsAccepted;
                dto.User_Creation_Id = (short)Session["UserId"];
                APIHandeling.Put("EX_CheckRequests_API?approve=1", dto);
                // window.location = '/Committees/Ex_Committee/Index?requestId=' + checkrequestid + '&Outlet_Examination_ID=' + outletexaminationid + '&Station_Examination_ID=' + stationexaminationid + '&Outlet_Genshi_ID=' + outletgenshiid + '&Station_Genshi_ID=' + stationgenshiid;
                // return Json("succ");
                object station_Examination_ID, outlet_Examination_ID, outlet_Genshi_ID, station_Genshi_ID;
                if (TempData["Station_Examination_ID"] != null)
                {
                     station_Examination_ID = TempData["Station_Examination_ID"];
                }
                else
                {
                     station_Examination_ID =0;
                }
                if (TempData["Outlet_Examination_ID"] != null)
                {
                    outlet_Examination_ID = TempData["Outlet_Examination_ID"];
                }
                else
                {
                    outlet_Examination_ID = 0;
                }
                if (TempData["Outlet_Genshi_ID"] != null)
                {
                    outlet_Genshi_ID = TempData["Outlet_Genshi_ID"];
                }
                else
                {
                    outlet_Genshi_ID = 0;
                }
                if (TempData["Station_Genshi_ID"] != null)
                {
                    station_Genshi_ID = TempData["Station_Genshi_ID"];
                }
                else
                {
                    station_Genshi_ID = 0;
                }


                ////var g = Server.Transfer(~/Committees/Ex_Committee/Index?requestId=' + checkrequestid + '&Outlet_Examination_ID=' + outletexaminationid + '&Station_Examination_ID=' + stationexaminationid + '&Outlet_Genshi_ID=' + outletgenshiid + '&Station_Genshi_ID=' + stationgenshiid, false);
                ////return g;


                return RedirectToAction("Index", "Ex_Committee", new
                {
                    area = "Committees",

                    requestId = model.Ex_CheckRequest_ID,
                    Outlet_Examination_ID = outlet_Examination_ID,
                    Station_Examination_ID = station_Examination_ID,
                    Outlet_Genshi_ID = outlet_Genshi_ID,
                    Station_Genshi_ID = station_Genshi_ID
                });
            }
            catch (Exception ex)
            {
                return null;
                // return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RelatedData) });
            }

        }

        [HttpPost]
        public JsonResult saveItemFees(long? itemId, decimal? fees)
        {
            Items_checkReq item = new Items_checkReq();
            item.Ex_Items_checkReqID = itemId;
            item.Fees = fees;
            item.Accept_User_Creation_Id = (short)Session["UserId"];

            APIHandeling.Put("EX_CheckRequests_API?itemFees=1", item);
            return Json("succ");
        }

        public ActionResult GetReport(string path1)
        {
            try
            {

                Session["Path_Server"] = path1;// @"\plant\Import\Ex_CheckRequests\2021\10\Ex_CheckRequestFiles595\1234.jfif";
                return Redirect("~/ASP/DisplayImge.aspx");

            }
            catch (Exception ex)
            {

                return null;

            }

        }
    }
}

