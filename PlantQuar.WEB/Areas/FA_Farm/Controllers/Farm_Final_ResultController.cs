using PlantQuar.DTO.DTO.Farm.FarmCommittee;
using PlantQuar.DTO.DTO.Farm.FarmData;
using PlantQuar.DTO.DTO.Farm.FarmRequest;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using PlantQuar.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace PlantQuar.WEB.Areas.FA_Farm.Controllers
{
    public class Farm_Final_ResultController : BaseController
    {
        // GET: FA_Farm/Farm_Final_Result
        string api_Name = "Farm_Final_Result_API";
        public ActionResult Index(long FarmCommittee_ID = 0)
        {
            ViewBag.FarmCommittee_ID = FarmCommittee_ID;
            //get committee type
            if (FarmCommittee_ID != 0)
            {
                var res = APIHandeling.getData(api_Name + "?FarmCommittee_ID=" + FarmCommittee_ID);
                ViewBag.committeeType = res.Content.ReadAsAsync<Nullable<byte>>().Result;//object
            }
            else
            {
                ViewBag.committeeType = 0;
            }

            var res_Farm_Data = APIHandeling.getData(api_Name + "?FarmsData_ID=1&FarmCommittee_ID=" + FarmCommittee_ID);
            var model = res_Farm_Data.Content.ReadAsAsync<List<FarmCommitteeExaminationAndSampleDataVM>>().Result;//object

            return View(model);
        }

        public ActionResult Print_Barcode(long FarmCommittee_Print_ID = 0)
        {
            ViewBag.FarmCommittee_ID = FarmCommittee_Print_ID;
            //get committee type
            if (FarmCommittee_Print_ID != 0)
            {
                var res = APIHandeling.getData(api_Name + "?FarmCommittee_ID=" + FarmCommittee_Print_ID);
                ViewBag.committeeType = res.Content.ReadAsAsync<Nullable<byte>>().Result;//object
            }
            else
            {
                ViewBag.committeeType = 0;
            }

            var res_Farm_Data = APIHandeling.getData(api_Name + "?FarmsData_ID=1&FarmCommittee_ID=" + FarmCommittee_Print_ID);
            var model = res_Farm_Data.Content.ReadAsAsync<List<FarmCommitteeExaminationAndSampleDataVM>>().Result;//object

            return View(model);
        }

        public ActionResult Farm_SampleData(long FarmCommittee_ID = 0)
        {
            ViewBag.FarmCommittee_ID = FarmCommittee_ID;
            return View();
        }

        //LOAD SEARCH
        public JsonResult listFarm_Committee_Examination
        (long FarmCommittee_ID = 0, string txt_AR_BTNSearch = "", string txt_EN_BTNSearch = "", int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = "")
        {
            try
            {

                var res = APIHandeling.getData(api_Name + "?Farm_Committee_Examination=1&FarmCommittee_ID=" + FarmCommittee_ID + "&arName=" + txt_AR_BTNSearch + "&enName=" + txt_EN_BTNSearch + "&pageSize=" + jtPageSize.ToString() + "&index=" + jtStartIndex.ToString() + "&jtSorting=" + jtSorting.ToString());

                var lst = res.Content.ReadAsAsync<Dictionary<string, object>>().Result;//object
                var StatusCode = lst.ElementAt(0).Value;
                var obj = lst.ElementAt(1).Value;

                JavaScriptSerializer ser = new JavaScriptSerializer();
                var myObj = ser.Deserialize<Dictionary<string, object>>(obj.ToString());

                var count = myObj.ElementAt(0).Value;
                var Lobj = myObj.ElementAt(1).Value;
                var ifAllConfirmed = myObj.ElementAt(2).Value;
                var ifAppearCategories = myObj.ElementAt(3).Value;
                var Fill_Farm_Check_List = myObj.ElementAt(4).Value;
                var Fill_Farm_Check_List_All_Note = myObj.ElementAt(5).Value;
                //var Fill_Farm_Check_List_Admin_Note = myObj.ElementAt(6).Value;
                //var Fill_Farm_Check_List_Confirm_Note = myObj.ElementAt(7).Value;
                //var Farm_Check_List_AdminQuarantine_Note = myObj.ElementAt(8).Value;
                return Json(new { Result = "OK", Records = Lobj, TotalRecordCount = count, ifAllConfirmed = ifAllConfirmed, ifAppearCategories = ifAppearCategories, _Farm_Check_List = Fill_Farm_Check_List, _Farm_Check_List_All_Note = Fill_Farm_Check_List_All_Note }, JsonRequestBehavior.AllowGet);
                // , _Farm_Check_List_Admin_Note = Fill_Farm_Check_List_Admin_Note, _Farm_Check_List_Confirm_Note = Fill_Farm_Check_List_Confirm_Note, _Farm_Check_List_AdminQuarantine_Note= Farm_Check_List_AdminQuarantine_Note }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                if (ex.HResult == -2146233087)
                {
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RelatedData) });
                }
                else
                {
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "listCenter");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        public JsonResult listFarm_SampleData
       (long FarmCommittee_ID = 0, string txt_AR_BTNSearch = "", string txt_EN_BTNSearch = "", int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = "")
        {
            try
            {

                var res = APIHandeling.getData(api_Name + "?Farm_SampleData=1&FarmCommittee_ID=" + FarmCommittee_ID + "&arName=" + txt_AR_BTNSearch + "&enName=" + txt_EN_BTNSearch + "&pageSize=" + jtPageSize.ToString() + "&index=" + jtStartIndex.ToString() + "&jtSorting=" + jtSorting.ToString());

                var lst = res.Content.ReadAsAsync<Dictionary<string, object>>().Result;//object
                var StatusCode = lst.ElementAt(0).Value;
                var obj = lst.ElementAt(1).Value;

                JavaScriptSerializer ser = new JavaScriptSerializer();
                var myObj = ser.Deserialize<Dictionary<string, object>>(obj.ToString());

                var count = myObj.ElementAt(0).Value;
                var Lobj = myObj.ElementAt(1).Value;
                var ifAllConfirmed = myObj.ElementAt(2).Value;
                var status = myObj.ElementAt(3).Value;
                var ifAppearCategories = myObj.ElementAt(4).Value;

                return Json(new { Result = "OK", Records = Lobj, TotalRecordCount = count, ifAllConfirmed = ifAllConfirmed, status = status, ifAppearCategories = ifAppearCategories }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                if (ex.HResult == -2146233087)
                {
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RelatedData) });
                }
                else
                {
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "listCenter");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }
        public JsonResult listFarmRequestItem_Categories
      (long FarmCommittee_ID = 0, string txt_AR_BTNSearch = "", string txt_EN_BTNSearch = "", int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = "")
        {
            try
            {

                var res = APIHandeling.getData(api_Name + "?FarmRequestItem_Categories=1&FarmCommittee_ID=" + FarmCommittee_ID + "&arName=" + txt_AR_BTNSearch + "&enName=" + txt_EN_BTNSearch + "&pageSize=" + jtPageSize.ToString() + "&index=" + jtStartIndex.ToString() + "&jtSorting=" + jtSorting.ToString());

                var lst = res.Content.ReadAsAsync<Dictionary<string, object>>().Result;//object
                var StatusCode = lst.ElementAt(0).Value;
                var obj = lst.ElementAt(1).Value;

                JavaScriptSerializer ser = new JavaScriptSerializer();
                var myObj = ser.Deserialize<Dictionary<string, object>>(obj.ToString());

                var count = myObj.ElementAt(0).Value;
                var Lobj = myObj.ElementAt(1).Value;
                var allIsActive = myObj.ElementAt(2).Value;
                var status = myObj.ElementAt(3).Value;
                return Json(new { Result = "OK", Records = Lobj, TotalRecordCount = count, allIsActive = allIsActive, status = status }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                if (ex.HResult == -2146233087)
                {
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RelatedData) });
                }
                else
                {
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "listCenter");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        public JsonResult listFarm_Country
          (long FarmCommittee_ID = 0, string txt_AR_BTNSearch = "", string txt_EN_BTNSearch = "", int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = "")
        {
            try
            {

                var res = APIHandeling.getData(api_Name + "?Farm_Country=1&FarmCommittee_ID=" + FarmCommittee_ID + "&arName=" + txt_AR_BTNSearch + "&enName=" + txt_EN_BTNSearch + "&pageSize=" + jtPageSize.ToString() + "&index=" + jtStartIndex.ToString() + "&jtSorting=" + jtSorting.ToString());

                var lst = res.Content.ReadAsAsync<Dictionary<string, object>>().Result;//object
                var StatusCode = lst.ElementAt(0).Value;
                var obj = lst.ElementAt(1).Value;

                JavaScriptSerializer ser = new JavaScriptSerializer();
                var myObj = ser.Deserialize<Dictionary<string, object>>(obj.ToString());

                var count = myObj.ElementAt(0).Value;
                var Lobj = myObj.ElementAt(1).Value;
                return Json(new { Result = "OK", Records = Lobj, TotalRecordCount = count }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                if (ex.HResult == -2146233087)
                {
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RelatedData) });
                }
                else
                {
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "listCenter");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        public void SaveFarm_Committee_Examination(Farm_Committee_ExaminationDTO model)
        {
            User_Session Current = User_Session.GetInstance;
            model.Admin_User = (short)Session["UserId"];
            ViewBag.ID = model.ID;
            APIHandeling.Put(api_Name + "", model);
        }

        public void SaveFarm_SampleData(Farm_SampleDataDTO model)
        {
            User_Session Current = User_Session.GetInstance;
            model.Admin_User = (short)Session["UserId"];
            ViewBag.ID = model.ID;
            APIHandeling.Put(api_Name + "", model);
        }


        public void SaveFarm_Country(Farm_CountryDTO model)
        {
            User_Session Current = User_Session.GetInstance;
            ViewBag.ID = model.ID;
            APIHandeling.Put(api_Name, model);
        }
        //eman
        public JsonResult SaveFarm_Country2(long? ID, DateTime? start, DateTime? end, bool? active, bool? accept)
        {
            Farm_CountryDTO model = new Farm_CountryDTO();
            model.ID = (long)ID;
            model.Start_Date = start;
            model.End_Date = end;
            model.IsActive = (bool)active;
            model.IsAcceppted = accept;

            User_Session Current = User_Session.GetInstance;
            //ViewBag.ID = model.ID;
            model.User_Updation_Id = (short)Session["UserId"];
            model.User_Updation_Date = DateTime.Now;


            APIHandeling.Put(api_Name, model);
            return Json("success", JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveAllFarm_Country(List<No_Insert_Farm_Country_DTO> CountryNo_Save_List, long? FarmCommittee_ID,
            string start, string end, bool? active, bool? accept)
        {

            DateTime Start_Date = Convert.ToDateTime(start);
            DateTime End_Date = Convert.ToDateTime(end);
            Farm_CountryDTO model = new Farm_CountryDTO();
            model.ID = (long)FarmCommittee_ID;
            model.Start_Date = Start_Date;
            model.End_Date = End_Date;
            model.IsActive = (bool)active;
            model.IsAcceppted = accept;


            //ViewBag.ID = model.ID;
            model.User_Updation_Id = (short)Session["UserId"];
            model.User_Updation_Date = DateTime.Now;
            model.No_Insert_Farm_Country = CountryNo_Save_List;
            APIHandeling.Put(api_Name + "?allcountries=1", model);
            return Json("success", JsonRequestBehavior.AllowGet);
        }

        public void updateFarm_SampleData(Farm_SampleDataDTO model)
        {
            User_Session Current = User_Session.GetInstance;
            ViewBag.ID = model.ID;
            APIHandeling.Put(api_Name + "", model);
        }
        public void UpdateFarmRequestItem_Categories(Farm_Request_ItemCategoriesDTO model)
        {
            User_Session Current = User_Session.GetInstance;
            model.Admin_User = (short)Session["UserId"];
            model.Admin_Date = DateTime.Now;

            //ViewBag.ID = model.ID;
            APIHandeling.Put(api_Name + "?Item_Categories=1", model);
        }
        public ActionResult updateFarm_Request_IsStatus(int accept, long Id)
        {
            FarmRequestDTO dto = new FarmRequestDTO();
            User_Session Current = User_Session.GetInstance;
            if (accept == 1)
            {
                dto.IsStatus = true;
            }
            else
            {
                dto.IsStatus = false;
            }
            dto.User_Updation_Id = (short)Session["UserId"];
            dto.User_Updation_Date = DateTime.Now;
            //ViewBag.ID =313;
            dto.ID = Id;

            var res = APIHandeling.Put(api_Name + "?Request_IsStatus=1", dto);
            return Json("succ");
        }
        [HttpPost]
        public JsonResult SaveNotes(long farmCommitteeId, string notes, List<Farm_Committee_CheckList_DTO> CheckListStatus)
        {


            var User_Updation_Id = (short)Session["UserId"];

            APIHandeling.Post(api_Name + "?user_Id=" + User_Updation_Id + "&farmCommitteeId=" + farmCommitteeId + "&notes=" + notes, CheckListStatus);

            // APIHandeling.Put(api_Name+"", CheckListStatus);

            return Json("succ");
        }
        public JsonResult SaveAreaAndWightFarmRequestQaur(List<Farm_Request_ItemCategoriesDTO> FinalItemCategoryAreaforAll)
        {
            var User_Updation_Id = (short)Session["UserId"];
            APIHandeling.Post(api_Name + "?user_id=" + User_Updation_Id, FinalItemCategoryAreaforAll);
            // APIHandeling.Put(api_Name+"", CheckListStatus);

            return Json("succ");
        }


        public ActionResult UpdateFarmsData(UpdateFarmModelDTO model)
        {
            FarmsDataDTO farmsDataDTO = new FarmsDataDTO();
            farmsDataDTO.print_text = model.print_text;
            farmsDataDTO.ID = model.ID;
            farmsDataDTO.FarmCode_14 = " ";
            farmsDataDTO.IsApproved = model.IsApproved;
            farmsDataDTO.IsActive = model.IsActive;
            farmsDataDTO.Farm_Request_ID = model.Farm_Request_ID;
            farmsDataDTO.User_Updation_Id = (short)Session["UserId"];
            farmsDataDTO.User_Updation_Date = DateTime.Now;
            APIHandeling.Put(api_Name + "?Update_Farm_Data=1", farmsDataDTO);
            return Json("succ");
        }


    }
}