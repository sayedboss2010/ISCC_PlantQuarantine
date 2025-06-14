using PlantQuar.DTO.DTO.General_Permissions.Permissions;
using PlantQuar.WEB.App_Start;
using PlantQuar.WEB.Controllers;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.General_Permission.Controllers
{
    public class Im_PermissionRequestPrintController : BaseController
    {
        // GET: General_Permission/Im_PermissionRequestPrint
        public ActionResult Index(decimal? ImPermission_Number,int? OperationCode=13)
        {
            bool checkPrint = false;
            if (Session["CanPrint"]=="1")
            {
                checkPrint = true;
            }
            User_Session Current = User_Session.GetInstance;
            var us = (short)Session["UserId"];
            var name = Session["FullName"];
            var canprint = checkPrint;
            var CanView = Session["CanView"];
            var User_Creation_Id = (short)Session["UserId"];
            // get operation Code From Session
            var OperationCode1 = (int)Session["OperationCode"];

            var res = APIHandeling.getData("ListGeneral_PermissionAPI?print=1&ImPermission_Number=" + ImPermission_Number+ "&User_Creation_Id="+ User_Creation_Id + "&OperationCode=" + OperationCode);
            var Lst = res.Content.ReadAsAsync<ImPermissionPrintDetailsDTO>().Result;
            Lst.CanPrint = canprint;
            return View(Lst);
        }
        [HttpPost]
        public ActionResult PrintArabic(long Im_PermissionRequest_ID)
        {
            ImPermissionIsPrintDTO printArabic = new ImPermissionIsPrintDTO();
            printArabic.Im_PermissionRequest_ID = Im_PermissionRequest_ID;
            printArabic.IS_Print_Ar = true;
            printArabic.User_Creation_Id =(short) Session["UserId"];

            var res = APIHandeling.Put("ListGeneral_PermissionAPI?printAr=1", printArabic);
            return Json("success");
        }
        [HttpPost]
        public ActionResult PrintEnglish(long Im_PermissionRequest_ID)
        {
            ImPermissionIsPrintDTO printEnglish = new ImPermissionIsPrintDTO();
            printEnglish.Im_PermissionRequest_ID = Im_PermissionRequest_ID;
            printEnglish.IS_Print_EN = true;
            printEnglish.User_Creation_Id = (short)Session["UserId"];

            var res = APIHandeling.Put("ListGeneral_PermissionAPI?printEn=1", printEnglish);
            return Json("success");
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ExportToPDF(string richTxt, decimal? permissionNumber,int lang)
        {
            try
            {
                HtmlToPdf converter = new HtmlToPdf();

                converter.Options.PdfPageSize = PdfPageSize.A4;
                converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
                var DomainPath = ConfigurationManager.AppSettings["applicationUrl"].ToString();
                var html = richTxt.Replace("src=\"", "src=\"" + DomainPath);
                //.Replace("{pagebreak}", "<div style='page-break-before: always'></ div>");
                //// return resulted pdf document
                //FileResult fileResult = new FileContentResult(pdf, "application/pdf");
                //fileResult.FileDownloadName = "Document.pdf";
                //return fileResult;
                string newFileLocation = "";
                string FileName = "";
                ////D:\ImpermisionsUpload
                //  newFileLocation = Server.MapPath("~/Upload/" + permissionNumber + "-AR.pdf");
                // var Folder_Name = "";
                if (lang == 1)
                {
                    FileName = permissionNumber + "-AR.pdf";
                }
                else
                {
                    FileName = permissionNumber + "-EN.pdf";
                }
                var permissionPath = ConfigurationManager.AppSettings["permissionsUpload"].ToString();
                //if (!Directory.Exists(permissionPath))
                //{
                //    //Directory.CreateDirectory(fPath);
                //}

                newFileLocation = Path.Combine(permissionPath, FileName);
                //  FileName = permissionNumber + "-AR.pdf";
                if (System.IO.File.Exists(newFileLocation))
                {
                    System.IO.File.Delete(newFileLocation);
                }
                PdfDocument doc = converter.ConvertHtmlString(html);

                // save pdf document
                byte[] pdf = doc.Save();

                // close pdf document
                doc.Close();


                using (var fs = new FileStream(newFileLocation, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(pdf, 0, pdf.Length);
                }

                return new JsonResult() { Data = new { filename = FileName } };
            }
            catch
            {
                return new JsonResult() { Data = new { filename = "" } };
            }
           




        }

    }
}