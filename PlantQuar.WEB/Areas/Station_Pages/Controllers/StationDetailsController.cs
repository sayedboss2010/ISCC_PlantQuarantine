using PlantQuar.DTO.DTO.Farm.FarmConstrain;
using PlantQuar.DTO.DTO.Station;
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
using static PlantQuar.DTO.HelperClasses.Enums;

namespace PlantQuar.Web.Areas.Station_Pages.Controllers
{
    public class StationDetailsController : BaseController
    {
        // GET: Station_Pages/StationDetails
        public ActionResult Index(long stationId)
        {
            var res = APIHandeling.getData("StationDetails_API?stationId=" + stationId);
            var model = res.Content.ReadAsAsync<List<Station_Get_Data_ResultDTO>>().Result;//object

            var res1 = APIHandeling.getData("StationDetails_API?oprationTypeID=1");
            var lst1 = res1.Content.ReadAsAsync<List<Station_Get_Data_ResultDTO>>().Result;
            ViewBag.StationActivityTypes_Export = lst1.Where(a => a.Station_Fees_Id_Type == 73);
            ViewBag.StationActivityTypes_Import = lst1.Where(a => a.Station_Fees_Id_Type == 74);
            //if (model != null)
            //{
            //    if (model.Count() > 0)
            //    {
            //        if (model != null)
            //        {
            //            //if (model.FirstOrDefault().StationCode == null)
            //            //{
            //            //    //Random rd = new Random();
            //            //    //string rand = rd.Next(0, 100000).ToString("D5");
            //            //    //// save barcode
            //            //    //var dayofyear = "000" + DateTime.Now.DayOfYear;
            //            //    //var zx = DateTime.Now.Year.ToString().Substring(2);
            //            //    //var hour = (DateTime.Now.Hour).ToString("D" + 2);
            //            //    //var min = (DateTime.Now.Minute).ToString("D" + 2);
            //            //    //var sec = (DateTime.Now.Second).ToString("D" + 2);
            //            //    //string barcode = "79" + rand + dayofyear.Substring(dayofyear.Length - 3) + zx + hour + min + sec;
            //            //    //ViewBag.StationCode = barcode;
            //            //}
            //        }
            //    }
            //}
            if (model != null)
            {


                if (model.Count > 0)
                {
                    return View(model);
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

        public ActionResult Station_Print(long stationId, long Station_Accreditation_Data_ID)
        {
            //http://localhost:44392/Station_Pages/StationDetails/Station_Print?stationId=364&Station_Accreditation_Data_ID=72
            var res = APIHandeling.getData("StationDetails_API?stationId=" + stationId + "&Print=1");
            var model = res.Content.ReadAsAsync<List<Station_Get_EN_Print_DTO>>().Result;//object
            if (model != null)
            {
                var Max_Requst = model.Where(a => a.Station_Accreditation_Data_ID == Station_Accreditation_Data_ID).Max(a => a.requestId);
                model = model.Where(a => a.Station_Accreditation_Data_ID == Station_Accreditation_Data_ID
                && a.requestId == Max_Requst).ToList();


                return View(model);
            }
            else
                return (null);

        }

        public ActionResult acceptRequest(Station_Get_Data_ResultDTO model)
        {

            //---add new row in export committee
            if (model.IsApproved == true)
            {
                Station_CommitteeDTO newStationCommittee = new Station_CommitteeDTO();
                newStationCommittee.StationAccrediationRequestId = model.requestId;
                newStationCommittee.CommitteeType_ID = (byte)CommitteeType.StationAccrediation_Committee;
                newStationCommittee.IsApproved = null;
                newStationCommittee.Status = null;
                newStationCommittee.User_Creation_Date = DateTime.Now;

                newStationCommittee.User_Creation_Id = (short)Session["UserId"];

                var res = APIHandeling.Post("StationCommittee?newCreate=1", newStationCommittee);
                //nora
                var dto2 = new DAL.Table_Action_Log_Station();
                dto2.ID_TableActionValue = model.requestId;
                dto2.Station_ID = model.StationId;
                dto2.User_Creation_Id = (short)Session["UserId"];
                dto2.User_Creation_Date = DateTime.Now;
                if (model.IsApproved == true)
                {
                    dto2.ID_Table_Action = 36;
                    dto2.NOTS = " تم الموافقة علي الاعتماد ";
                }
                else
                {
                    dto2.ID_Table_Action = 37;
                    dto2.NOTS = " تم رفض الاعتماد  ";
                }
                dto2.User_Type_ID = 127;
                dto2.Type_log_ID = 135;
                //Update isApproved 
            }

            APIHandeling.Put("StationDetails_API?approve=1", model);

            return RedirectToAction("Index", "StationAccrediationCommittee");
        }

        [HttpPost]
        public ActionResult UpdateStation_Details(string Station_Code, long Station_Id, bool IsActive, string Notes_Reject)
        {
            Random rd = new Random();
            string rand = rd.Next(0, 100000).ToString("D5");
            // save barcode
            var dayofyear = "000" + DateTime.Now.DayOfYear;
            var zx = DateTime.Now.Year.ToString().Substring(2);
            var hour = (DateTime.Now.Hour).ToString("D" + 2);
            var min = (DateTime.Now.Minute).ToString("D" + 2);
            var sec = (DateTime.Now.Second).ToString("D" + 2);
            string barcode = "79" + rand + dayofyear.Substring(dayofyear.Length - 3) + zx + hour + min + sec;

            User_Session Current = User_Session.GetInstance;
            var msg = "";
            var Station_Get_Data_Result = new Station_Get_Data_ResultDTO();
            Station_Get_Data_Result.StationId = Station_Id;
            Station_Get_Data_Result.StationCode = barcode;
            Station_Get_Data_Result.IsActive_Request = IsActive;
            Station_Get_Data_Result.Notes_Reject = Notes_Reject;

            var res = APIHandeling.Post("StationDetails_API", Station_Get_Data_Result);

            #region Log_Station
            var dto2 = new DAL.Table_Action_Log_Station();
            dto2.ID_TableActionValue = Station_Id;
            dto2.Station_ID = Station_Id;
            dto2.User_Creation_Id = (short)Session["UserId"];
            dto2.User_Creation_Date = DateTime.Now;
            if (IsActive == true)
            {
                dto2.ID_Table_Action = 36;
                dto2.NOTS = " تم الموافقة علي المحطة ";
            }
            else
            {
                dto2.ID_Table_Action = 37;
                dto2.NOTS = " تم رفض المحطة  ";
            }
            dto2.User_Type_ID = 127;
            dto2.Type_log_ID = 135;
            APIHandeling.Put("Log_CheckRequest_API?Station_Log=1", dto2);
            #endregion
            var countryLst = res.Content.ReadAsAsync<Station_Get_Data_ResultDTO>().Result;
            if ((int)res.StatusCode != 409)
            {
                msg = "تمت الاضافه";
            }
            else
            {
                msg = "هذا السجل موجود من قبل ";
            }
            return Json(new { Result = "OK", Options = msg });
        }

        [HttpPost]
        public JsonResult SaveStation_Accreditation_Fees(long requestId, bool ISActive, List<Station_Request_Fees_DTO> Selection_Fees_List)
        {
            try
            {
                var res = APIHandeling.Post("StationDetails_API?requestId=" + requestId + "&ISActive=" + ISActive, Selection_Fees_List);
                var lst = res.Content.ReadAsAsync<List<Station_Request_Fees_DTO>>().Result;

                #region Log_Station
                var dto2 = new DAL.Table_Action_Log_Station();
                dto2.ID_TableActionValue = requestId;
                dto2.Station_ID = Selection_Fees_List.FirstOrDefault().Station_ID;
                dto2.User_Creation_Id = (short)Session["UserId"];
                dto2.User_Creation_Date = DateTime.Now;
                if (ISActive == true)
                {
                    dto2.ID_Table_Action = 38;
                    dto2.NOTS = " اضافة رسوم الاعتماد ";
                }
                else
                {
                    dto2.ID_Table_Action = 37;
                    dto2.NOTS = " رفض الحجر للإعتماد  ";
                }
                dto2.User_Type_ID = 127;
                dto2.Type_log_ID = 135;
                APIHandeling.Put("Log_CheckRequest_API?Station_Log=1", dto2);
                #endregion
                return Json(new { Result = "OK", Options = lst });
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


        [HttpPost]
        public JsonResult Update_StartStop_Request(long Reqest_id, bool IsAccepted)
        {
            try
            {
                var res = APIHandeling.getData("StationDetails_API?Reqest_id=" + Reqest_id + "&IsAccepted=" + IsAccepted);
                var lst = res.Content.ReadAsAsync<List<Station_Request_Fees_DTO>>().Result;
                return Json(new { Result = "OK", Options = lst });


            }
            catch (Exception ex)
            {
                if (ex.HResult == -2146233087)
                {
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RelatedData) });
                }
                else
                {
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "DeleteFarm_CheckList_Constrain");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }
    }
}