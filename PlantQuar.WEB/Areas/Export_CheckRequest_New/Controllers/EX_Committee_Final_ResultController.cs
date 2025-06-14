using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Admin;
using PlantQuar.DTO.DTO.Committee;
using PlantQuar.DTO.DTO.Export_CheckRequest_New;
using PlantQuar.DTO.DTO.Farm.FarmRequest;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using PlantQuar.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.Export_CheckRequest_New.Controllers
{
    public class EX_Committee_Final_ResultController : BaseController
    {
        string api = "EX_Committee_Final_Result_API";

        public ActionResult Index(long? requestId, long? OutLet_ID, long? item_ShortName_ID = null, long? Lots_itemShortName_ID = null, long? CommitteeTypeLst_ID = null)
        {
            Session["requestId"] = requestId;
            ViewBag.Fullname = Session["FullName"];
            if (item_ShortName_ID == null)
            {
                item_ShortName_ID = 0;
            }
            if (Lots_itemShortName_ID == null)
            {
                Lots_itemShortName_ID = 0;
            }
            if (CommitteeTypeLst_ID == null)
            {
                CommitteeTypeLst_ID = 0;
                ViewBag.CommitteeTypeLst_Value = 0;
            }
            //else if (CommitteeTypeLst_ID > 0)
            //{
            ViewBag.CommitteeTypeLst_Value = CommitteeTypeLst_ID;
            //}
            var resItem = APIHandeling.getData("EX_Committee_Final_Result_API?EX_CheckRequest_Number=" + requestId + "&item_ShortName_ID=" + item_ShortName_ID + "&Lots_itemShortName_ID=" + Lots_itemShortName_ID + "&CommitteeTypeLst_ID=" + CommitteeTypeLst_ID);
            var Lst = resItem.Content.ReadAsAsync<EX_Committee_Final_ResultDTO>().Result;

            if (Lst != null)
            {
                if (Lst.OutLet_ID == OutLet_ID)
                {
                    return View(Lst);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public ActionResult Print_Barcode(long Ex_CheckRequest_ID = 0)
        {
            ViewBag.FarmCommittee_ID = Ex_CheckRequest_ID;
            //get committee type
            //if (Ex_CheckRequest_ID != 0)
            //{
            //    var res = APIHandeling.getData(api + "?Ex_CheckRequest_ID=" + Ex_CheckRequest_ID);
            //    ViewBag.committeeType = res.Content.ReadAsAsync<Nullable<byte>>().Result;//object
            //}
            //else
            //{
            //    ViewBag.committeeType = 0;
            //}

            var res_Farm_Data = APIHandeling.getData(api + "?Ex_CheckRequest_ID=" + Ex_CheckRequest_ID);
            var model = res_Farm_Data.Content.ReadAsAsync<List<PlantQuar.DTO.DTO.Export_CheckRequest_New.EX_Committee_Sample_Lot>>().Result;//object

            return View(model);
        }
        [HttpPost]
        public JsonResult ItemShortName_AddEDIT()
        {
            try
            {
                var req = Session["requestId"];
                var res = APIHandeling.getData("EX_Committee_Final_Result_API?ItemShortNameAddEdit=" + req);

                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "ItemType_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        [HttpPost]
        public JsonResult ItemLotName_AddEDIT(long ItemShortName_ID)
        {
            try
            {
                var req = Session["requestId"];
                var res = APIHandeling.getData("EX_Committee_Final_Result_API?Req=" + req + "&ItemShortNameID=" + ItemShortName_ID);

                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "ItemType_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        [HttpPost]
        public JsonResult SearchProducts(long itemShortName, long lots_itemShortName)
        {
            try
            {
                var req = Session["requestId"];
                var userId = (short)Session["UserId"];
                var resItem = APIHandeling.getData("EX_Committee_Final_Result_API?EX_CheckRequest_Number=" + req + "&item_ShortName_ID=" + itemShortName);

                var Lst = resItem.Content.ReadAsAsync<EX_Committee_Final_ResultDTO>().Result;
                return Json(new { Result = "OK", Records = Lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "EX_Committee_Final_ResultController");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        [HttpPost]
        public JsonResult Insert_Lot_Result(long Lot_Category_ID, int IS_Status, string Nots, EX_CheckRequest_Items_Lot_ResultDTO Lot_ResultList)
        {
            try
            {
                var allErrors = ModelState.Values.SelectMany(x => x.Errors).ToList();
                User_Session Current = User_Session.GetInstance;
                Lot_ResultList.EX_CheckRequest_Items_Lot_Category_ID = Lot_Category_ID;
                Lot_ResultList.Nots = Nots;
                Lot_ResultList.User_Creation_Date = DateTime.Now;
                Lot_ResultList.User_Creation_Id = (short)Session["UserId"];
                Lot_ResultList.IS_Status = IS_Status;
                //if (ModelState.IsValid)
                //{
                if (Lot_ResultList != null)
                {
                    var res = APIHandeling.Post("EX_Committee_Final_Result_API" + "?Lot_Result=1", Lot_ResultList);
                    return ((int)res.StatusCode != 409) ? Json(new { Result = "OK", Record = Lot_ResultList })
                      : Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });
                }
                else
                {
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }

                // }
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

        public JsonResult Insert_EX_CheckRequest_Final_Result(int EX_Final_Result_ID, EX_CheckRequest_Final_ResultDTO
            Final_Result_List)
        {
            try
            {
                var allErrors = ModelState.Values.SelectMany(x => x.Errors).ToList();
                User_Session Current = User_Session.GetInstance;



                long req = long.Parse(Session["requestId"].ToString());
                Final_Result_List.Ex_Final_Result_ID = EX_Final_Result_ID;
                Final_Result_List.Date = DateTime.Now;
                Final_Result_List.User_Creation_Date = DateTime.Now;
                Final_Result_List.User_Creation_Id = (short)Session["UserId"];


                Final_Result_List.Ex_CheckRequest_ID = req;
                //if (ModelState.IsValid)
                //{
                if (Final_Result_List != null)
                {
                    var res = APIHandeling.Post("EX_Committee_Final_Result_API" + "?EX_CheckRequest_Final_Result=1", Final_Result_List);
                    return ((int)res.StatusCode != 409) ? Json(new { Result = "OK", Record = Final_Result_List })
                          : Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });

                }
                else
                {
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }

                // }
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

        public JsonResult Insert_EX_CheckRequest_Visa(long Visa_ID, EX_CheckRequest_VisaDTO CheckRequest_VisaList)
        {
            try
            {
                var allErrors = ModelState.Values.SelectMany(x => x.Errors).ToList();
                User_Session Current = User_Session.GetInstance;

                long req = long.Parse(Session["requestId"].ToString());
                CheckRequest_VisaList.Ex_Visa_ID = Visa_ID;
                CheckRequest_VisaList.Date = DateTime.Now;
                CheckRequest_VisaList.User_Creation_Date = DateTime.Now;
                CheckRequest_VisaList.User_Creation_Id = (short)Session["UserId"];


                CheckRequest_VisaList.Ex_CheckRequest_ID = req;
                //if (ModelState.IsValid)
                //{
                if (CheckRequest_VisaList != null)
                {
                    var res = APIHandeling.Post("EX_Committee_Final_Result_API" + "?Visa_ID=1", CheckRequest_VisaList);
                    return ((int)res.StatusCode != 409) ? Json(new { Result = "OK", Record = CheckRequest_VisaList })
                          : Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });

                }
                else
                {
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }

                // }
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

        //noura
        [HttpPost]
        public JsonResult VisaLabResult_AddEDIT(int VisaResult)
        {
            try
            {
                var res = APIHandeling.getData(api + "?VisaResult=" + VisaResult);

                var lst = res.Content.ReadAsAsync<EX_Visa_DataDTO>().Result;
                //ViewBag.Description = lst.Description_Ar;



                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "ItemType_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        //Fill LotStatus
        [HttpPost]
        public JsonResult LotStatusLst_AddEDIT()
        {
            try
            {
                var res = APIHandeling.getData(api + "?Lot_Result_Status=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "ItemType_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }


        public FileResult GetReport(string path1)
        {
            try
            {
                var DomainPath = ConfigurationManager.AppSettings["Path_NetworkShare"].ToString();
                //Encrypt.Decrypt y = new Decrypt();
                var xx = path1.Replace(" ", "+");

                //path1 = y.Decryption(xx);

                int port = Request.Url.Port;
                String SharedIp = "";
                //if (port == 80 || port == 443)
                //{
                //    SharedIp = "10.10.91.11";
                //}
                //else if (port == 8071)
                //{
                //    SharedIp = "10.10.91.11";
                //}
                //else
                //{
                SharedIp = DomainPath;
                //}
                path1 = @"\\" + SharedIp + path1;

                string[] FileArr = path1.Split('.');
                string extention = FileArr[FileArr.Length - 1];

                byte[] fileBytes = System.IO.File.ReadAllBytes(path1);

                if (extention == "pdf" || extention == "PDF")
                {

                    return File(fileBytes, "application/pdf");
                }
                else
                {

                    string fileName = Path.GetFileName(path1);
                    return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
                }



            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetReport");
                // return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                //DataClasses1DataContext data = new DataClasses1DataContext();
                string StoredName = "PersonProfile/GetReport";
                string ErrorName = ex.Message;
                //data.Error_saving_App(ErrorName, StoredName, DateTime.Now, 1);
                return null;

            }

        }


        public JsonResult Update_Status_Confirm(long Status_Id, bool Status_Bool)
        {
            try
            {

                var userId = (short)Session["UserId"];
                var res = APIHandeling.getData("EX_Committee_Final_Result_API" + "?Status_Id=" + Status_Id + "&Status=" + Status_Bool);
                var data = res.Content.ReadAsAsync<CommitteeEmployeeDTO>().Result;

                if (data != null)
                {
                    return Json("Su", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("Invalid", JsonRequestBehavior.AllowGet);

                }
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Delete_Emp_Confirm");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }





        }


        [HttpPost]
        public JsonResult Visa_AddEDIT()
        {
            try
            {

                var res = APIHandeling.getData("EX_Committee_Final_Result_API?VisaAddEdit=1");

                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "ItemType_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        [HttpPost]
        public JsonResult FinalResult_AddEDIT(int FinalResult)
        {
            try
            {
                var res = APIHandeling.getData(api + "?FinalResult=" + FinalResult);

                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "ItemType_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }


        public JsonResult GetPR_User_Id(string FullName, long EmplyeeNo, long OutLet_ID)
        {
            try
            {
                var Fees_Process = APIHandeling.getData("EX_Committee_Final_Result_API?FullName=" + FullName + "&EmplyeeNo=" + EmplyeeNo + "&OutLet_ID=" + OutLet_ID);
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

        public JsonResult Delete_Emp_Confirm(long Committee_ID, long Employee_ID)
        {
            try
            {
                var userId = (short)Session["UserId"];
                var res = APIHandeling.getData("EX_Committee_Final_Result_API?Committee_ID=" + Committee_ID + "&Employee_ID=" + Employee_ID + "&UserId=" + userId);
                var data = res.Content.ReadAsAsync<CommitteeEmployeeDTO>().Result;

                if (data != null)
                {
                    //if (data.User_Deletion_Date == true)
                    //{
                    //    Session["Path_Server"] = data.filePath;
                    //    return Json(new { result = data.result, noteAr = data.noteAr, noteEn = data.noteEn, filePath = data.filePath, labName = data.labName, analysisType = data.analysisType, rejectreason = data.rejectreason, Infection_Name = data.Infection_Name, Result_injury_Name = data.Result_injury_Name, SampleSize = data.SampleSize, farmSampleID = data.farmSampleId }, JsonRequestBehavior.AllowGet);
                    //}
                    //else
                    //{
                    return Json("Su", JsonRequestBehavior.AllowGet);

                    //}

                }
                else
                {
                    return Json("Invalid", JsonRequestBehavior.AllowGet);

                }
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Delete_Emp_Confirm");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }





        }
        [HttpPost]
        public JsonResult DeleteVisa(int id)
        {
            try
            {
                PlantQuarantineEntities db = new PlantQuarantineEntities();
                // Find the visa by its ID
                var visa = db.Ex_CheckRequest_Visa.Find(id);

                if (visa == null)
                {
                    return Json(new { success = false, message = "التأشيرة غير موجودة." });
                }

                // Remove the visa from the database
                db.Ex_CheckRequest_Visa.Remove(visa);
                db.SaveChanges();

                // Return a success response
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                // Return failure if there's an error
                return Json(new { success = false, message = "فشل الحذف، حاول مرة أخرى." });
            }
        }

        [HttpPost]
        public JsonResult DeleteFinalResult(int id)
        {
            try
            {
                PlantQuarantineEntities db = new PlantQuarantineEntities();
                // Find the visa by its ID
                var visa = db.Ex_CheckRequest_Final_Result.Find(id);

                if (visa == null)
                {
                    return Json(new { success = false, message = "الموقف النهائي غير موجودة." });
                }

                // Remove the visa from the database
                db.Ex_CheckRequest_Final_Result.Remove(visa);
                db.SaveChanges();

                // Return a success response
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                // Return failure if there's an error
                return Json(new { success = false, message = "فشل الحذف، حاول مرة أخرى." });
            }
        }

    }
}