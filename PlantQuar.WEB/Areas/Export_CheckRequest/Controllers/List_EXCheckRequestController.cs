using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Committee;
using PlantQuar.DTO.DTO.Common;
using PlantQuar.DTO.DTO.Export_CheckRequest;
using PlantQuar.DTO.DTO.Farm.FarmRequest;
using PlantQuar.DTO.DTO.Search;
using PlantQuar.DTO.DTO.Station_Pages;
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
    public class List_EXCheckRequestController : BaseController
    {
        // GET: Export_CheckRequest/List_EXCheckRequest
        public ActionResult Index(int CurrentPage = 1, string Search = "")
        {
            try
            {
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


                int pageSize = 10;
                ViewBag.Search = Search;
                //var objlst = res.Content.ReadAsAsync<List<Ex_ListDTO>>().Result;
                var res = APIHandeling.getData("EX_CheckRequests_API?SearchALL=" + Search + "&userId=" + userId + "&Outlet_User_ID=" + Outlet_User_ID
                     + "&Station_User_ID=" + Station_User_ID
                     + "&CanView_Outlet_Examination=" + CanView + "&CanAdd_Station_Examination=" + CanAdd
                     + "&CanEdit_Outlet_Genshi=" + CanEdit + "&CanDelete_Station_Genshi=" + CanDelete
                     + "&CurrentPage=" + CurrentPage + "&pageSize=" + pageSize);
                var objlst = res.Content.ReadAsAsync<List<Ex_ListDTO>>().Result;

                //PlantQuarantineEntities db = new PlantQuarantineEntities();
                //var TotalResults = db.Ex_List.Count();
                if (objlst.Count > 0)
                {
                    ViewBag.TotalResults = objlst.FirstOrDefault().TotalResults;
                    ViewBag.TotalPages = objlst.FirstOrDefault().TotalPages;
                    ViewBag.CurrentPage = CurrentPage;
                }

                return View(objlst);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "List_EXRequestsController");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }

        }
        [HttpPost]
        public JsonResult Im_CheckRequest_List(string IsApproved = "1", string requestnumber = "")
        {
            try
            {
                var userId = (short)Session["UserId"];
                var res = APIHandeling.getData("EX_CheckRequests_API?ImCheckRequest_Number=" + requestnumber +
                   "&IsApproved=" + IsApproved + "&userId=" + userId);
                var modelLst = res.Content.ReadAsAsync<List<EXCheckRequestListDTO>>().Result;

                return Json(new { Result = "OK", Records = modelLst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "List_EXRequestsController");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        public ActionResult Index_ListExportersService()
        {
            try
            {

                var res = APIHandeling.getData("EX_CheckRequests_API?ListExportersService=dd");
                var modelLst = res.Content.ReadAsAsync<List<EXCheckRequestListDTO>>().Result;

                return View(modelLst);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "List_EXRequestsController");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }

        }

        public ActionResult Index_ListAll(int CurrentPage = 1, string Search = "")
        {
            try
            {
                int pageSize = 10;
                ViewBag.Search = Search;
                //var objlst = _se.GetListWithPage(Search, page, pageSize);
                var res = APIHandeling.getData("EX_CheckRequests_API?CurrentPage=" + CurrentPage + "&pageSize=" + pageSize+ "&Search="+ Search);
                var objlst = res.Content.ReadAsAsync<List<Ex_ListDTO>>().Result;

                if (objlst.Count > 0)
                {
                    ViewBag.TotalResults = objlst.FirstOrDefault().TotalResults;
                    ViewBag.TotalPages = objlst.FirstOrDefault().TotalPages;
                    ViewBag.CurrentPage = CurrentPage;
                }
                else
                {
                    ViewBag.TotalResults = 0;
                    ViewBag.TotalPages = 0;
                    ViewBag.CurrentPage = CurrentPage;
                }

                    return View(objlst);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "List_EXRequestsController");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }

        }

        public ActionResult IndexQuick(int CurrentPage = 1, string Search = "")
        {
            try
            {
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


                int pageSize = 10;
                ViewBag.Search = Search;
                //var objlst = res.Content.ReadAsAsync<List<Ex_ListDTO>>().Result;
                var res = APIHandeling.getData("EX_CheckRequests_API?SearchALL=" + Search + "&userId=" + userId + "&Outlet_User_ID=" + Outlet_User_ID
                     + "&Station_User_ID=" + Station_User_ID
                     + "&CanView_Outlet_Examination=" + CanView + "&CanAdd_Station_Examination=" + CanAdd
                     + "&CanEdit_Outlet_Genshi=" + CanEdit + "&CanDelete_Station_Genshi=" + CanDelete
                     + "&CurrentPage=" + CurrentPage + "&pageSize=" + pageSize + "&Quick=1");
                var objlst = res.Content.ReadAsAsync<List<Ex_ListDTO>>().Result;

                //PlantQuarantineEntities db = new PlantQuarantineEntities();
                //var TotalResults = db.Ex_List.Count();
                if (objlst != null)
                {
                    if (objlst.Count > 0)
                    {
                        ViewBag.TotalResults = objlst.FirstOrDefault().TotalResults;
                        ViewBag.TotalPages = objlst.FirstOrDefault().TotalPages;
                        ViewBag.CurrentPage = CurrentPage;
                    }
                }
                return View(objlst);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "List_EXRequestsController");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }

        }



        public JsonResult getListQuick(List<EX_CheckRequest_Quick_DTO> F_List)
        {
            try
            {
                Session["Requst_List"] = new List<EX_CheckRequest_Quick_DTO>();
                Session["Requst_List"] = F_List;
                //var res = APIHandeling.getData("FarmRequest_API?itemId=" + itemId);
                //var modelLst = res.Content.ReadAsAsync<List<FarmsListDTO>>().Result;
                return null;
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "FarmListOnLine");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        public class Requst_List_Qu
        {

            public long reqId { get; set; }
        }
    }
}