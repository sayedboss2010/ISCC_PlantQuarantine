using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using PlantQuar.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.Employee.Controllers
{
    public class Employee_TrackController : BaseController
    {
        // GET: Employee/Employee_Track
        string apiName = "Employee_Track_API";

        public ActionResult Index(int page = 0)
        {
            try
            {
                var Fees_Process = APIHandeling.getData(apiName + "?Outlet=1");
                var lst = Fees_Process.Content.ReadAsAsync<List<CustomOption>>().Result;
                ViewBag.ddd = lst;
                ViewBag.DateTo = DateTime.Now;
                ViewBag.DateFrom = DateTime.Now.AddDays(-7);

                var Company = APIHandeling.getData(apiName + "?Company=-1");
                var lst_Company_Name = Company.Content.ReadAsAsync<List<CustomOption>>().Result;
                ViewBag.lst_Company_Name = lst_Company_Name;


                // return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetOutlet_ID");
                // return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
            try
            {
                var Fees_Process = APIHandeling.getData(apiName + "?User=6");
                var lst = Fees_Process.Content.ReadAsAsync<List<CustomOption>>().Result;
                ViewBag.sss = lst;

                // return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                ViewBag.sss = ex.Message;
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetOutlet_ID");
                //  return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
            ViewBag.page = page;
            return View();
        }


        public JsonResult EmployeeList_ByOutlet(long Outlet_ID)
        {
            try
            {
                var res = APIHandeling.getData("Employee_Track_API?Outlet=1&Outlet_ID=" + Outlet_ID);
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                ViewBag.CenterList = lst;

                return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "CenterList_ByGov");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }


        public JsonResult CompanyList_ByOutlet(long Outlet_ID)
        {
            try
            {
                var res = APIHandeling.getData("Employee_Track_API?add=1&Outlet_ID=" + Outlet_ID);
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                ViewBag.CenterList = lst;

                return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "CenterList_ByGov");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }


        public JsonResult CommitteeTypeList_ByOperation(long Operation_Id)
        {
            try
            {
                var Committee = APIHandeling.getData(apiName + "?Committee=" + Operation_Id);
                var lst = Committee.Content.ReadAsAsync<List<CustomOption>>().Result;


                //var res = APIHandeling.getData("Employee_Track_API?add=1&Operation_Id=" + Operation_Id);
                //var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;


                return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "CenterList_ByGov");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        //public ActionResult Confirm_Delete(int page= 2)
        //{
        //    try
        //    {
        //        var Fees_Process = APIHandeling.getData(apiName + "?Outlet=-1");
        //        var lst = Fees_Process.Content.ReadAsAsync<List<CustomOption>>().Result;
        //        ViewBag.ddd = lst;
        //        ViewBag.DateTo = DateTime.Now;
        //        ViewBag.DateFrom = DateTime.Now.AddDays(-7);
        //        var Company = APIHandeling.getData(apiName + "?Company=-1");
        //        var lst_Company_Name = Company.Content.ReadAsAsync<List<CustomOption>>().Result;
        //        ViewBag.lst_Company_Name = lst_Company_Name;


        //        // return Json(new { Result = "OK", Options = lst });
        //    }
        //    catch (Exception ex)
        //    {
        //        APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetOutlet_ID");
        //        // return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
        //    }
        //    try
        //    {
        //        var Fees_Process = APIHandeling.getData(apiName + "?User=6");
        //        var lst = Fees_Process.Content.ReadAsAsync<List<CustomOption>>().Result;
        //        ViewBag.sss = lst;

        //        // return Json(new { Result = "OK", Options = lst });
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.sss = ex.Message;
        //        APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetOutlet_ID");
        //        //  return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
        //    }

        //    return View();
        //}
        //public ActionResult Committee_Delete()
        //{
        //    try
        //    {
        //        var Fees_Process = APIHandeling.getData(apiName + "?Outlet=-1");
        //        var lst = Fees_Process.Content.ReadAsAsync<List<CustomOption>>().Result;
        //        ViewBag.ddd = lst;
        //        ViewBag.DateTo = DateTime.Now;
        //        ViewBag.DateFrom = DateTime.Now.AddDays(-7);
        //        var Company = APIHandeling.getData(apiName + "?Company=-1");
        //        var lst_Company_Name = Company.Content.ReadAsAsync<List<CustomOption>>().Result;
        //        ViewBag.lst_Company_Name = lst_Company_Name;


        //        // return Json(new { Result = "OK", Options = lst });
        //    }
        //    catch (Exception ex)
        //    {
        //        APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetOutlet_ID");
        //        // return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
        //    }
        //    try
        //    {
        //        var Fees_Process = APIHandeling.getData(apiName + "?User=6");
        //        var lst = Fees_Process.Content.ReadAsAsync<List<CustomOption>>().Result;
        //        ViewBag.sss = lst;

        //        // return Json(new { Result = "OK", Options = lst });
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.sss = ex.Message;
        //        APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetOutlet_ID");
        //        //  return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
        //    }

        //    return View();
        //}

    }
}