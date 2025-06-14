using PlantQuar.DTO.DTO;
using PlantQuar.DTO.DTO.Common;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.API;
using PlantQuar.WEB.App_Start;
using PlantQuar.WEB.Controllers;
using System;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PlantQuar.Web.Areas.LaboratoryResult.Controllers
{
    public class SampleLabResultController : BaseController
    {
        // GET: LaboratoryResult/SampleLabResult
        public ActionResult Index(string BarCode)
        {
            ViewBag.message = "";
            if (BarCode != null)
            {
                ViewBag.BarCode = BarCode.Trim();
                //BarCode = BarCode.Trim();
                //var res = APIHandeling.getData("SampleLABResult_API?barcode=" + BarCode);
                //var data = res.Content.ReadAsAsync<sampleData_Info_ResultDTO>().Result;

                //if (data != null)
                //{
                //    if (data.Confrm_IsAccepted == true)
                //    {
                //        Session["Path_Server"] = data.filePath;
                //        return Json(new
                //        {
                //            result = data.result,
                //            noteAr = data.noteAr,
                //            noteEn = data.noteEn,
                //            filePath = data.filePath,
                //            labName = data.labName,
                //            analysisType = data.analysisType,
                //            rejectreason = data.rejectreason,
                //            Infection_Name = data.Infection_Name,
                //            Result_injury_Name = data.Result_injury_Name,
                //            SampleSize = data.SampleSize,
                //            farmSampleID = data.farmSampleId,
                //            IsFinishedAll = data.IsFinishedAll
                //        }, JsonRequestBehavior.AllowGet);
                //    }
                //    else
                //    {
                //        return Json("Invalid_confirm", JsonRequestBehavior.AllowGet);

                   }

                //}
                //else
                //{
                //    return Json("Invalid", JsonRequestBehavior.AllowGet);

                //}
            //}
            return View();
        }

        public ActionResult IndexNew()
        {
            return View();
        }

        [HttpPost]
        public ActionResult saveLabResult(LabResultDTO dto, HttpPostedFileBase imageresult)
        {
            if (imageresult != null)
            {
                var fileType = "";
                A_AttachmentDataDTO model = new A_AttachmentDataDTO();

                FileUpload_SaveDataController fileUpload = new FileUpload_SaveDataController();
                //model.Picture = fileUpload.Upload_File_Path_NetworkShare(Picture1, "Item_"+itemType);
                if (dto.barcode.StartsWith("74"))// الوارد
                { fileType = "Import"; }
                else if (dto.barcode.StartsWith("73")) // الصادر
                { fileType = "Export"; }
                string _Path = fileUpload.Get_Uplood_Imge("Item_" + "Item_" + model.Id, imageresult, fileType, "SampleData", Request.Url.AbsoluteUri.ToString());

                model.AttachmentPath = _Path;


                User_Session Current = User_Session.GetInstance;
                model.User_Creation_Id = (short)Session["UserId"];
                model.User_Creation_Date = DateTime.Now;
                dto.model = model;
                dto.imageResult = model.AttachmentPath;
            }

            var res = APIHandeling.Post("SampleLABResult_API?", dto);
            var data = res.Content.ReadAsAsync<string>().Result;
            ViewBag.message = data;
            //return Json(data);
            return View("Index");

        }
        public JsonResult getSampleInfo(string barcode)
        {
            try
            {
                barcode = barcode.Trim();
                var res = APIHandeling.getData("SampleLABResult_API?barcode=" + barcode);
                var data = res.Content.ReadAsAsync<sampleData_Info_ResultDTO>().Result;

                if (data != null)
                {
                    if (data.Confrm_IsAccepted == true)
                    {
                        Session["Path_Server"] = data.filePath;
                        return Json(new { result = data.result, noteAr = data.noteAr, noteEn = data.noteEn,
                            filePath = data.filePath, labName = data.labName, 
                            analysisType = data.analysisType, rejectreason = data.rejectreason,
                            Infection_Name = data.Infection_Name, 
                            Result_injury_Name = data.Result_injury_Name,
                            SampleSize = data.SampleSize, farmSampleID = data.farmSampleId,
                        IsFinishedAll= data.IsFinishedAll
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("Invalid_confirm", JsonRequestBehavior.AllowGet);

                    }

                }
                else
                {
                    return Json("Invalid", JsonRequestBehavior.AllowGet);

                }
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "sampleInfo");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }





        }

    }
}