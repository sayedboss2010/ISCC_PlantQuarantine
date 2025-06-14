using PlantQuar.DTO.DTO.ExportRequest;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using PlantQuar.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using static PlantQuar.DTO.HelperClasses.Enums;

namespace PlantQuar.WEB.Areas.ExportRequest
{
    public class ExRequest_DetailsController : BaseController
    {
        // GET: ExportRequest/ExRequest_Details
        public ActionResult Index(long requestId)
        {
            var res = APIHandeling.getData("Export_CheckRequest_API?requestId=" + requestId);
            var model = res.Content.ReadAsAsync<CheckRequest_GetData_ResultDTO>().Result;//object
            var reasons = APIHandeling.getData("Export_CheckRequest_API?List=1&refuse=1");
            var reasonsList = reasons.Content.ReadAsAsync<List<CustomOption>>().Result;
            ViewBag.ListReasons = reasonsList;
            //eman 
            var res2 = APIHandeling.getData("exportRequestCommitteeResult?requestId=" + requestId);

            var lst = res2.Content.ReadAsAsync<Dictionary<string, List<CheckRequest_ComiteeResult_ResultDTO>>>().Result;//object

            var checkRequestData = lst.ElementAt(0).Value;
            var SampleData = lst.ElementAt(1).Value;
            var Treatment = lst.ElementAt(2).Value;
            requestCommitteeResultDTO committeeResultDTO = new requestCommitteeResultDTO();
            committeeResultDTO.check = checkRequestData;
            committeeResultDTO.withdrowSample = SampleData;
            committeeResultDTO.Treatment = Treatment;
            ViewBag.committeeResult = committeeResultDTO;

            //eman
            return View(model);
        }

        [HttpPost]
        public ActionResult saveReasons(List<short> listIDs, long checkReqId)
        {
            ReasonsListReqIdDTO dto = new ReasonsListReqIdDTO();
            dto.checkReqId = checkReqId;
            dto.refuseReasonsIds = listIDs;
            //User_Session Current = User_Session.GetInstance;
            dto.User_Creation_Id = (short)Session["UserId"];
            dto.User_Creation_Date = DateTime.Now;
            APIHandeling.Post("Export_CheckRequest_API?listt=1", dto);
            return Json("succ");
        }

        public ActionResult Index2(long requestId)
        {
            var res = APIHandeling.getData("Export_CheckRequest_API?requestId=" + requestId);
            var model = res.Content.ReadAsAsync<CheckRequest_GetData_ResultDTO>().Result;//object

            return View(model);
        }
        public ActionResult acceptRequest(CheckRequest_GetData_ResultDTO model)
        {
            Export_CheckRequestDTO dto = new Export_CheckRequestDTO();
            dto.ID = model.CheckRequest_Id;
            if (model.IsAccepted != null)
            {
                dto.IsAcceppted = (bool)model.IsAccepted;
            }
            //---add new row in export committee
            if (model.IsAccepted == true)
            {
                ex_Ex_RequestCommitteeDTO newExportCommittee = new ex_Ex_RequestCommitteeDTO();
                newExportCommittee.ExCheckRequest_ID = model.CheckRequest_Id;
                newExportCommittee.CommitteeType_ID = (byte)CommitteeType.Examination_Committee;
                newExportCommittee.IsApproved = null;
                newExportCommittee.Status = null;
                newExportCommittee.User_Creation_Date = DateTime.Now;
                //User_Session Current = User_Session.GetInstance;
                newExportCommittee.User_Creation_Id = (short)Session["UserId"]; ;

                var res = APIHandeling.Post("EX_Committee_API", newExportCommittee);
               
                //eman
                //Update isApproved 
            }

            APIHandeling.Put("Export_CheckRequest_API?approve=1", dto);

            return RedirectToAction("Index", "List_ExportRequests");
        }
        //eman
        public JsonResult listFileAttachments(long reqId, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {

            try
            {
                var res = APIHandeling.getData("Export_CheckRequest_API" + "?reqId=" + reqId + "&pageSize=" + jtPageSize.ToString() + "&index=" + jtStartIndex.ToString());

                var lst = res.Content.ReadAsAsync<Dictionary<string, object>>().Result;//object

                var StatusCode = lst.ElementAt(0).Value;
                var obj = lst.ElementAt(1).Value;

                JavaScriptSerializer ser = new JavaScriptSerializer();
                var myObj = ser.Deserialize<Dictionary<string, object>>(obj.ToString());

                var count = myObj.ElementAt(0).Value;
                var Lobj = myObj.ElementAt(1).Value;

                return Json(new { Result = "OK", Records = Lobj, TotalRecordCount = count });
            }
            catch (Exception ex)
            {
                if (ex.HResult == -2146233087)
                {
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RelatedData) });
                }
                else
                {
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "listTBLRows");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }
        [HttpPost]
        public JsonResult CreateFileAttachments(long reqId, ex_A_AttachmentDataDTO model, HttpPostedFileBase Picture1)
        {
            try
            {


                if (ModelState.IsValid)
                {
                    string subPath = "~/ExportRequest2";
                    bool exists = Directory.Exists(Server.MapPath(subPath));
                    if (!exists)
                    {
                        Directory.CreateDirectory(Server.MapPath(subPath));
                    }



                    string path = Path.Combine(Server.MapPath(subPath), Path.GetFileName(Picture1.FileName));
                    string imageName = Path.GetFileName(Picture1.FileName);
                    Picture1.SaveAs(path);


                    string imagePath = "/ExportRequest2/" + imageName;
                   // User_Session Current = User_Session.GetInstance;
                    model.User_Creation_Id = (short)Session["UserId"];
                    model.User_Creation_Date = DateTime.Now;

                    model.AttachmentPath = imagePath;
                    model.RowId = reqId;
                    model.A_AttachmentTableNameId = 3;
                    //check Repeated Data
                    var res = APIHandeling.Post("Export_CheckRequest_API?att=1", model);
                    return ((int)res.StatusCode != 409) ? Json(new { Result = "OK", Record = model })
                      : Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });
                }
                else
                {
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
            catch (Exception ex)
            {
                if (ex.HResult == -2146233087)
                {
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RelatedData) });
                }
                else
                {
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "CreateTBLRows");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }
        [HttpPost]
        public JsonResult UpdateFileAttachments(long reqId, ex_A_AttachmentDataDTO model, HttpPostedFileBase Picture1)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    if (Picture1 != null)
                    {


                        string subPath = "~/ExportRequest2";
                        bool exists = Directory.Exists(Server.MapPath(subPath));
                        if (!exists)
                        {
                            Directory.CreateDirectory(Server.MapPath(subPath));
                        }



                        string path = Path.Combine(Server.MapPath(subPath), Path.GetFileName(Picture1.FileName));
                        string imageName = Path.GetFileName(Picture1.FileName);
                        Picture1.SaveAs(path);


                        string imagePath = "/ExportRequest2/" + imageName;
                        model.AttachmentPath = imagePath;
                    }
                    else
                    {
                        model.AttachmentPath = "";
                    }
                    //User_Session Current = User_Session.GetInstance;
                    model.User_Updation_Id = (short)Session["UserId"]; 
                    model.User_Updation_Date = DateTime.Now;

                    model.A_AttachmentTableNameId = 3;
                    model.RowId = reqId;
                    var res = APIHandeling.Put("Export_CheckRequest_API", model);
                    return ((int)res.StatusCode != 409) ? Json(new { Result = "OK" })
                      : Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });
                }
                else
                {
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
            catch (Exception ex)
            {
                if (ex.HResult == -2146233087)
                {
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RelatedData) });
                }
                else
                {
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "CreateTBLRows");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }

        }
        [HttpPost]
        public JsonResult DeleteFileAttachments(long Id)
        {
            try
            {
                DeleteParameters obj = new DeleteParameters();
                obj.id = Id;
                //User_Session Current = User_Session.GetInstance;
                obj.Userid = (short)Session["UserId"];
                obj._DateNow = DateTime.Now;
                APIHandeling.Delete("Export_CheckRequest_API", obj);

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                if (ex.HResult == -2146233087)
                {
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RelatedData) });
                }
                else
                {
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "DeleteTBLRows");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }
    }
}