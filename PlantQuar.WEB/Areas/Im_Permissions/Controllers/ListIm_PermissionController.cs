//using PagedList;
using PlantQuar.DTO.DTO;
using PlantQuar.DTO.DTO.Common;
using PlantQuar.DTO.DTO.Export_CheckRequest;
using PlantQuar.DTO.DTO.Import.Permissions;
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

namespace PlantQuar.Web.Areas.Im_Permissions.Controllers
{
    public class ListIm_PermissionController : BaseController
    {
        // GET: Im_Permissions/ListIm_Permission
        // radio_ID=2&Company_ID=&Country_ID=&ShortName_ID=null\

        public ActionResult Index(int CurrentPage = 1, string Search = "")
        {

            try
            {
                int radio_ID=0; long Company_ID=0; long? Country_ID=0; long? ShortName_ID = 0; int? Type_Item = 0; int? Type_Company = 0; string Im_PermissionRequest_Num="";


                int pageSize = 15;
                //var res = APIHandeling.getData(apiName + "?CommitteeType_ID=-1&IsApproved=-2&IS_OnlineOffline=-1");
                //var modelLst = res.Content.ReadAsAsync<List<CustomeEx_RequestList_Admin>>().Result;

                //int pageSize = 15;
                //int pageNumber = (page ?? 1);

                int userId = int.Parse(Session["UserId"].ToString());
                int MenuId = Session["MenuId"] == null ? 0 : (int)Session["MenuId"];
                int ModuleId = Session["ModuleId"] == null ? 0 : (int)Session["ModuleId"];
                int GroupId = Session["GroupId"] == null ? 0 : (int)Session["GroupId"];
                var res2 = APIHandeling.getData("userPriviledge_Api?userID=" + userId + "&menuId=" + MenuId + "&modelID=" + ModuleId + "&GroupId=" + GroupId); ;
                var Lst2 = res2.Content.ReadAsAsync<userPriviledgeDTO>().Result;


             
                var res = APIHandeling.getData("ListIm_PermissionAPI?SearchALL=" + Search + "&radio_ID=" + radio_ID + "&Company_ID=" + Company_ID
                        + "&Country_ID=" + Country_ID + "&ShortName_ID=" + ShortName_ID
                        + "&Type_Item=" + Type_Item + "&Type_Company=" + Type_Company + "&Im_PermissionRequest_Num="+Im_PermissionRequest_Num+"&CurrentPage=" + CurrentPage + "&pageSize=" + pageSize);
                var Lst = res.Content.ReadAsAsync<List<ImPermissionsListDTO>>().Result;
                if (Lst.Count > 0)
                {
                    ViewBag.TotalResults = Lst.FirstOrDefault().TotalResults;
                    ViewBag.TotalPages = Lst.FirstOrDefault().TotalPages;
                    ViewBag.CurrentPage = CurrentPage;
                }
                //return View(Lst.ToPagedList(pageNumber, pageSize));
                // call api here 
                if (Lst2 != null)
                {
                    Session["CanPrint"] = Lst2.CanPrint;
                    Session["CanView"] = Lst2.CanView;

                    ViewBag.CanPrint = Lst2.CanPrint;
                    ViewBag.CanView = Lst2.CanView;
                }
                else
                {
                    return View();
                }
                // add can print here
                if (Lst2.CanPrint == null || Lst2.CanView == true)
                {
                    return View(Lst);
                }
                else
                {
                    return View();
                }
            }
            catch (Exception)
            {

                return View();
            }

        }
        public ActionResult Index2(int radio_ID, long? Company_ID, long? Country_ID, long? ShortName_ID, int? Type_Item, int? Type_Company, string Im_PermissionRequest_Num)
        {
            try
            {
                //var res = APIHandeling.getData(apiName + "?CommitteeType_ID=-1&IsApproved=-2&IS_OnlineOffline=-1");
                //var modelLst = res.Content.ReadAsAsync<List<CustomeEx_RequestList_Admin>>().Result;

                //int pageSize = 15;
                //int pageNumber = (page ?? 1);

                int userId = int.Parse(Session["UserId"].ToString());
                int MenuId = Session["MenuId"] == null ? 0 : (int)Session["MenuId"]  ;
                int ModuleId = Session["ModuleId"] == null ? 0 : (int)Session["ModuleId"];
                int GroupId = Session["GroupId"] == null ? 0 : (int)Session["GroupId"];
                var res2 = APIHandeling.getData("userPriviledge_Api?userID=" + userId + "&menuId=" + MenuId + "&modelID=" + ModuleId + "&GroupId=" + GroupId); ;
                var Lst2 = res2.Content.ReadAsAsync<userPriviledgeDTO>().Result;



                var res = APIHandeling.getData("ListIm_PermissionAPI?radio_ID=" + radio_ID + "&Company_ID=" + Company_ID
                        + "&Country_ID=" + Country_ID + "&ShortName_ID=" + ShortName_ID
                        + "&Type_Item="+ Type_Item + "&Type_Company="+ Type_Company+ "&Im_PermissionRequest_Num="+  Im_PermissionRequest_Num);
                var Lst = res.Content.ReadAsAsync<List<ImPermissionsListDTO>>().Result;
                //return View(Lst.ToPagedList(pageNumber, pageSize));
                // call api here 
                if (Lst2 != null)
                {
                    Session["CanPrint"] = Lst2.CanPrint;
                    Session["CanView"] = Lst2.CanView;

                    ViewBag.CanPrint = Lst2.CanPrint;
                    ViewBag.CanView = Lst2.CanView;
                }
                else
                {
                    return View();
                }
                // add can print here
                if (Lst2.CanPrint == null || Lst2.CanView == true)
                {
                    return View(Lst);
                }
                else
                {
                    return View();
                }
            }
            catch (Exception)
            {

                return View();
            }          
        }
        [HttpPost]
        public JsonResult GetPermissionsList(decimal? ImPermission_Number, int Isacceppted)
        {
            //User_Session user_Session = User_Session.GetInstance;
            int userId = int.Parse(Session["UserId"].ToString());
            int MenuId = (int)Session["MenuId"];
            int ModuleId = (int)Session["ModuleId"];
            int GroupId = (int)Session["GroupId"];
            var res = APIHandeling.getData("ListIm_PermissionAPI?List=1&ImPermission_Number="+ ImPermission_Number+ "&Isacceppted=" + Isacceppted);
            var Lst = res.Content.ReadAsAsync<List<ImPermissionsListDTO>>().Result;
            var res2 = APIHandeling.getData("userPriviledge_Api?userID=" + userId + "&menuId=" + MenuId + "&modelID=" + ModuleId + "&GroupId=" + GroupId); ;
            var Lst2 = res2.Content.ReadAsAsync<userPriviledgeDTO>().Result;
            //return View(Lst.ToPagedList(pageNumber, pageSize));
            return Json(new { Lst= Lst , Lst2= Lst2 }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Get_PermissionsList(decimal? ImPermission_Number, int Isacceppted)
        {
            //User_Session user_Session = User_Session.GetInstance;
            int userId = int.Parse(Session["UserId"].ToString());
            int MenuId = (int)Session["MenuId"];
            int ModuleId = (int)Session["ModuleId"];
            int GroupId = (int)Session["GroupId"];
            var res = APIHandeling.getData("ListIm_PermissionAPI?List=1&ImPermission_Number=" + ImPermission_Number + "&Isacceppted=" + Isacceppted);
            var Lst = res.Content.ReadAsAsync<List<ImPermissionsListDTO>>().Result;
            var res2 = APIHandeling.getData("userPriviledge_Api?userID=" + userId + "&menuId=" + MenuId + "&modelID=" + ModuleId + "&GroupId=" + GroupId); ;
            var Lst2 = res2.Content.ReadAsAsync<userPriviledgeDTO>().Result;
            //return View(Lst.ToPagedList(pageNumber, pageSize));
            return Json(new { Lst = Lst, Lst2 = Lst2 }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
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

        [HttpPost]
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

        [HttpPost]
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
    }
}