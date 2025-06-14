using PlantQuar.DTO.DTO.Export_CheckRequest;
using PlantQuar.DTO.DTO.Import.CheckRequests;
using PlantQuar.DTO.DTO.Station_Pages;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using PlantQuar.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.Export_CheckRequest.Controllers
{
    public class List_EXCheckRequestNewController : BaseController
    {
        // GET: Export_CheckRequest/List_EXCheckRequestNew

        public ActionResult Index()
        {
            try
            {

                ViewBag.Outlet_ID = Session["Outlet_ID"].ToString();

                @ViewBag.DateTo = DateTime.Now;
                @ViewBag.DateFrom = DateTime.Now.AddDays(-30);
                return View();
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "List_EXRequestsController");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }

        }
       
        public JsonResult Country_List()
        {
            try
            {
                var res = APIHandeling.getData("Country_API?Im_Permission=1");
                var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Country_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

     
        public JsonResult ShortName_List()
        {
            try
            {
                var res = APIHandeling.getData("Item_ShortName_API?Im_Permission=1");
                var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "ShortName_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

  
        public JsonResult Company_List()
        {
            try
            {
                var res = APIHandeling.getData("Company_National_API?Im_Permission=1");
                var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Company_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        //////////////////////////////
        public JsonResult GetStation()
        {
            try
            {
                var res = APIHandeling.getData( "CheckRequestChangeGeshni_API?station=1");
                var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;


                return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetGeshniStation");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }


        [System.Web.Http.HttpGet]
        public ActionResult EX_CheckRequest_List(int? radio_ID, long? Company_ID, long? Country_ID
            , long? StationId, string ExChechRequest_Num, string dateFrom="", string dateEnd = "")
        {
            try
            {
                @ViewBag.DateTo = DateTime.Now;
                @ViewBag.DateFrom = DateTime.Now.AddDays(-30);

                var IsApproved = 1;
                var userId = (short)Session["UserId"];

                long Station_User_ID = 0;
                var user_Station = APIHandeling.getData("EX_CheckRequests_API?IS_Station=" + IsApproved + "&userId=" + userId);
                var List_Station = user_Station.Content.ReadAsAsync<List<Station_Emp_DTO>>().Result;
                if (List_Station.Count > 0)
                {
                    Station_User_ID = long.Parse(List_Station.FirstOrDefault().Station_Id.ToString());
                    List<long> lst = List_Station.Select(a => a.Station_Id).ToList() as List<long>;

                    Session["user_Station_ID"] = lst;
                }
                var Outlet_User_ID = Session["Outlet_ID"];
                var CanView = 1;
                if (Session["CanView"].ToString() == "False")// منفذ فحص
                {
                    CanView = 0;
                }
                var CanAdd = 1;
                if (Session["CanAdd"].ToString() == "False")//محطة فحص
                {
                    CanAdd = 0;
                }

                var CanEdit = 1;
                if (Session["CanEdit"].ToString() == "False")// منفذ جشني
                {
                    CanEdit = 0;
                }

                var CanDelete = 1;
                if (Session["CanDelete"].ToString() == "False")// محطة جشني
                {
                    CanDelete = 0;
                }

                var res = APIHandeling.getData("EX_CheckRequests_API?userId=" + userId + "&Outlet_User_ID=" + Outlet_User_ID
                                  + "&Station_User_ID=" + Station_User_ID
                                  + "&CanView_Outlet_Examination=" + CanView + "&CanAdd_Station_Examination=" + CanAdd
                                  + "&CanEdit_Outlet_Genshi=" + CanEdit + "&CanDelete_Station_Genshi=" + CanDelete + "" +
                    "&radio_ID=" + radio_ID + "&Company_ID=" + Company_ID +
                    "&Country_ID=" + Country_ID + "&StationId=" + StationId + "&ExChechRequest_Num=" + ExChechRequest_Num+ "&dateFrom=" + dateFrom + "&dateEnd=" + dateEnd);
                var modelLst = res.Content.ReadAsAsync<List<EXCheckRequestListDTO>>().Result;
                
                return View(modelLst);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "List_EXRequestsController");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }

        }

    }
}