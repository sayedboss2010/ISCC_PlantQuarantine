using PlantQuar.DTO.DTO.Export_Certificate;
using PlantQuar.DTO.DTO.Export_CheckRequest;
using PlantQuar.DTO.DTO.Export_CheckRequest_New;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using PlantQuar.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.Export_Certificate.Controllers
{
    public class CertificateDetailsController : BaseController
    {
        // GET: Export_Certificate/CertificateDetails
        public ActionResult Index(long? certificate_ID)
        {
            var res = APIHandeling.getData("CertificateDetails_API?certificate_ID=" + certificate_ID);
            var Lst = res.Content.ReadAsAsync<CertificateDTO>().Result;
            ViewBag.certificate_ID = certificate_ID;



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
        public ActionResult acceptRequest(CertificateDTO model)
        {
            try
            {
                CheckRequest_GetData_web_ResultDTO dto = new CheckRequest_GetData_web_ResultDTO();
                dto.CertificateID = model.data.CertificateID;
                dto.ISAccepted = (bool)model.data.ISAccepted;
                //dto.User_Creation_Id = (short)Session["UserId"];
                APIHandeling.Put("EX_CheckRequests_API?approve=1", dto);

                return RedirectToAction("Index", "List_EXCheckRequest");
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

        public JsonResult Insert_AddationDeclartionِAdmin(long PlantCertificatesRequestsID,string certificate_AddtionTxt)
        {
            try
            {

                CheckRequest_Getdata_AdditionDec_AdminDTO item = new CheckRequest_Getdata_AdditionDec_AdminDTO();
                item.AdminID = (short)Session["UserId"];
                item.PlantCertificatesRequestsID = PlantCertificatesRequestsID;
                item.Certificate_AddtionUpdateAdmin = certificate_AddtionTxt;
                item.ISAccepted = true;
                item.Date_Accepted = DateTime.Now;

                APIHandeling.Put("CertificateDetails_API?addtion=1", item);
                return Json("succ");



                //if (PlantCertificatesRequestsID != null)
                //{
                //    var res = APIHandeling.Post("EX_Committee_Final_Result_API" + "?EX_CheckRequest_Final_Result=1", PlantCertificatesRequestsID);
                //    return ((int)res.StatusCode != 409) ? Json(new { Result = "OK", Record = PlantCertificatesRequestsID })
                //          : Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });

                //}
                //else
                //{
                //    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                //}

   
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

        public JsonResult acceptCertificate(long ID)
        {
            try
            {
                AcceptCertificate accept = new AcceptCertificate();
                accept.certificateId = ID;
                accept.User_Updation_Date = DateTime.Now;
                accept.User_Updation_Id = (short)Session["UserId"];
                accept.IsAccepted = true;

                var res = APIHandeling.Put("CertificateDetails_API", accept);

                return Json("success");
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "List_ExportRequestsController");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        public JsonResult NotAcceptCertificate(long ID,string rejreasons_text)
        {
            try
            {
                AcceptCertificate accept = new AcceptCertificate();
                accept.certificateId = ID;
                accept.User_Updation_Date = DateTime.Now;
                accept.User_Updation_Id = (short)Session["UserId"];
                accept.IsAccepted = false;
                accept.rejreasons_text = rejreasons_text;

                var res = APIHandeling.Put("CertificateDetails_API", accept);

                return Json("success");
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "List_ExportRequestsController");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
    }
}