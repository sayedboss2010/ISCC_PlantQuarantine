using PlantQuar.DTO.DTO.Export_Certificate;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using PlantQuar.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.Export_Certificate.Controllers
{
    public class CertificateListController : BaseController
    {
        // GET: Export_Certificate/CertificateList
        string apiName = "CertificateList_API";

        public ActionResult Index(string requestNumber)
        {
            ViewBag.requestNumber = requestNumber;
            return View();
        }
        //public JsonResult getCertificateList(string fromDate, string endDate, byte ISAccepted, string requestNumber)
        //{
        //    try
        //    {
        //        var res = APIHandeling.getData(apiName + "?fromDate=" + fromDate +
        //            "&endDate=" + endDate + "&ISAccepted=" + ISAccepted + "&requestNumber=" + requestNumber);
        //        var modelLst = res.Content.ReadAsAsync<List<Certificate_Get_Data_ResultDTO>>().Result;

        //        return Json(modelLst, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "List_ExportRequestsController");
        //        return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
        //    }
        //}

        public JsonResult getCertificateList(byte ISAccepted, string requestNumber, short? Country_Id, long? Company_Id, short? companyTypes)
        {
            //  string fromDate, string endDate,
            //  "?fromDate=" + fromDate + "&endDate=" + endDate +
            try
            {
                var res = APIHandeling.getData(apiName + "?ISAccepted=" + ISAccepted + "&requestNumber=" + requestNumber + "&Country_Id=" + Country_Id + "&Company_Id=" + Company_Id+ "&companyTypes="+ companyTypes);
                var modelLst = res.Content.ReadAsAsync<List<Certificate_Get_Data_ResultDTO>>().Result;

                return Json(modelLst.OrderByDescending(a => a.User_Creation_Date), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "List_ExportRequestsController");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        public JsonResult acceptCertificate(long ID)
        {
            try
            {
                AcceptCertificate accept = new AcceptCertificate();
                accept.certificateId = ID;
                accept.User_Updation_Date = DateTime.Now;
                accept.User_Updation_Id = (short)Session["UserId"];
                accept.IsAccepted = true;

                var res = APIHandeling.Put(apiName, accept);

                return Json("success");
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "List_ExportRequestsController");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        public JsonResult NotAcceptCertificate(long ID)
        {
            try
            {
                AcceptCertificate accept = new AcceptCertificate();
                accept.certificateId = ID;
                accept.User_Updation_Date = DateTime.Now;
                accept.User_Updation_Id = (short)Session["UserId"];
                accept.IsAccepted = false;

                var res = APIHandeling.Put(apiName, accept);

                return Json("success");
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "List_ExportRequestsController");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        public JsonResult putprintCertificates(long ID)
        {
            try
            {
                //AcceptCertificate accept = new AcceptCertificate();
                //accept.certificateId = ID;
                //accept.User_Updation_Date = DateTime.Now;
                //accept.User_Updation_Id = (short)Session["UserId"];
                //accept.IsAccepted = false;
                //accept.ISPrint = 0;


                var certificateId = ID;
                var User_Updation_Date = DateTime.Now;
                var User_Updation_Id = (short)Session["UserId"];
                var IsAccepted = false;
                var ISPrint = 0;
                // var res = APIHandeling.Put( "CertificateList_API?print =1", accept);
                // var res = APIHandeling.Put( "CertificateList_API?print =1", accept);
                var res = APIHandeling.getData(apiName + "?certificateId=" + certificateId + "&User_Updation_Id=" + User_Updation_Id + "&IsAccepted=" + IsAccepted + "&ISPrint=" + ISPrint);
                return Json("success");
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "List_ExportRequestsController");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
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

        [HttpPost]
        public ActionResult Company_Name_Pr(int Company_Type_Id)
        {
            try
            {
                if (Company_Type_Id == 6)
                {
                    var Company = APIHandeling.getData("CertificateList_API?Company=-1");
                    var lst_Company_Name = Company.Content.ReadAsAsync<List<CustomOption>>().Result;
                    return Json(new { Result = "OK", Options = lst_Company_Name });
                }
                else if (Company_Type_Id == 7)
                {
                    var Orgniztion = APIHandeling.getData("CertificateList_API?Orgniztion=-1");
                    var lst_Orgniztion_Name = Orgniztion.Content.ReadAsAsync<List<CustomOption>>().Result;
                    return Json(new { Result = "OK", Options = lst_Orgniztion_Name });
                }
                else if (Company_Type_Id == 8)
                {
                    var Person = APIHandeling.getData("CertificateList_API?Person=-1");
                    var lst_Person_Name = Person.Content.ReadAsAsync<List<CustomOption>>().Result;
                    return Json(new { Result = "OK", Options = lst_Person_Name });
                }
                else
                {
                    return Json(new { Result = "OK", Options = 1 });
                }
            }
            catch (Exception ex)
            {
                //  ViewBag.sss = ex.Message;
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetOutlet_ID");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
    }
}