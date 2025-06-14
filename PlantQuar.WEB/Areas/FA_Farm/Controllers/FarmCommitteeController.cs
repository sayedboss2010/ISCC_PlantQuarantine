using PlantQuar.DAL;
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
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace PlantQuar.WEB.Areas.FA_Farm.Controllers
{
    public class FarmCommitteeController : BaseController
    {
        // GET: FA_Farm/FarmCommittee

        public ActionResult Index(long FarmCommittee_ID = 0)
        {
            ViewBag.FarmCommittee_ID = FarmCommittee_ID;
            //get committee type
            if (FarmCommittee_ID != 0)
            {
                var res = APIHandeling.getData("Farm_SampleData_API?FarmCommittee_ID=" + FarmCommittee_ID);

                ViewBag.committeeType = res.Content.ReadAsAsync<Nullable<byte>>().Result;//object
            }
            else
            {
                ViewBag.committeeType = 0;
            }
            return View();
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

                var res = APIHandeling.getData("Farm_Committee_Examination_API?FarmCommittee_ID=" + FarmCommittee_ID + "&arName=" + txt_AR_BTNSearch + "&enName=" + txt_EN_BTNSearch + "&pageSize=" + jtPageSize.ToString() + "&index=" + jtStartIndex.ToString() + "&jtSorting=" + jtSorting.ToString());

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
                var Fill_Farm_Check_List_Admin_Note = myObj.ElementAt(5).Value;
                var Fill_Farm_Check_List_Confirm_Note = myObj.ElementAt(6).Value;
                var Farm_Check_List_AdminQuarantine_Note = myObj.ElementAt(7).Value;
                return Json(new { Result = "OK", Records = Lobj, TotalRecordCount = count, ifAllConfirmed = ifAllConfirmed, ifAppearCategories = ifAppearCategories, _Farm_Check_List = Fill_Farm_Check_List, _Farm_Check_List_Admin_Note = Fill_Farm_Check_List_Admin_Note, _Farm_Check_List_Confirm_Note = Fill_Farm_Check_List_Confirm_Note, _Farm_Check_List_AdminQuarantine_Note = Farm_Check_List_AdminQuarantine_Note }, JsonRequestBehavior.AllowGet);
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

                var res = APIHandeling.getData("Farm_SampleData_API?FarmCommittee_ID=" + FarmCommittee_ID + "&arName=" + txt_AR_BTNSearch + "&enName=" + txt_EN_BTNSearch + "&pageSize=" + jtPageSize.ToString() + "&index=" + jtStartIndex.ToString() + "&jtSorting=" + jtSorting.ToString());

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

                var res = APIHandeling.getData("FarmRequestItem_Categories_API?FarmCommittee_ID=" + FarmCommittee_ID + "&arName=" + txt_AR_BTNSearch + "&enName=" + txt_EN_BTNSearch + "&pageSize=" + jtPageSize.ToString() + "&index=" + jtStartIndex.ToString() + "&jtSorting=" + jtSorting.ToString());

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

                var res = APIHandeling.getData("Farm_Country_API?FarmCommittee_ID=" + FarmCommittee_ID + "&arName=" + txt_AR_BTNSearch + "&enName=" + txt_EN_BTNSearch + "&pageSize=" + jtPageSize.ToString() + "&index=" + jtStartIndex.ToString() + "&jtSorting=" + jtSorting.ToString());

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
            APIHandeling.Put("Farm_Committee_Examination_API", model);
        }

        public void SaveFarm_SampleData(Farm_SampleDataDTO model)
        {
            User_Session Current = User_Session.GetInstance;
            model.Admin_User = (short)Session["UserId"];
            ViewBag.ID = model.ID;
            APIHandeling.Put("Farm_SampleData_API", model);
        }


        public void SaveFarm_Country(Farm_CountryDTO model)
        {
            User_Session Current = User_Session.GetInstance;
            ViewBag.ID = model.ID;
            APIHandeling.Put("Farm_Country_API", model);
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


            APIHandeling.Put("Farm_Country_API", model);
            return Json("success", JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveAllFarm_Country(long? FarmCommittee_ID, DateTime? start, DateTime? end, bool? active, bool? accept)
        {
            Farm_CountryDTO model = new Farm_CountryDTO();
            model.ID = (long)FarmCommittee_ID;
            model.Start_Date = start;
            model.End_Date = end;
            model.IsActive = (bool)active;
            model.IsAcceppted = accept;

            User_Session Current = User_Session.GetInstance;
            //ViewBag.ID = model.ID;
            model.User_Updation_Id = (short)Session["UserId"];
            model.User_Updation_Date = DateTime.Now;


            APIHandeling.Put("Farm_Country_API?allcountries=1", model);
            return Json("success", JsonRequestBehavior.AllowGet);
        }

        public void updateFarm_SampleData(Farm_SampleDataDTO model)
        {
            User_Session Current = User_Session.GetInstance;
            ViewBag.ID = model.ID;
            APIHandeling.Put("Farm_SampleData_API", model);
        }
        public void UpdateFarmRequestItem_Categories(Farm_Request_ItemCategoriesDTO model)
        {
            User_Session Current = User_Session.GetInstance;
            model.Admin_User = (short)Session["UserId"];
            model.Admin_Date = DateTime.Now;

            //ViewBag.ID = model.ID;
            APIHandeling.Put("FarmRequestItem_Categories_API", model);
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

            var res = APIHandeling.Put("Farm_Request_API", dto);
            return Json("succ");
        }
        [HttpPost]
        public JsonResult SaveNotes(long farmCommitteeId, string notes, List<Farm_Committee_CheckList_DTO> CheckListStatus)
        {


            var User_Updation_Id = (short)Session["UserId"];

            APIHandeling.Post("Farm_Committee_Examination_API?user_Id=" + User_Updation_Id + "&farmCommitteeId=" + farmCommitteeId + "&notes=" + notes, CheckListStatus);

            // APIHandeling.Put("Farm_Committee_Examination_API", CheckListStatus);

            return Json("succ");
        }
        public JsonResult SaveAreaAndWightFarmRequestQaur(List<Farm_Request_ItemCategoriesDTO> FinalItemCategoryAreaforAll)
        {


            var User_Updation_Id = (short)Session["UserId"];

            APIHandeling.Post("Farm_Committee_Examination_API?user_id=" + User_Updation_Id, FinalItemCategoryAreaforAll);

            // APIHandeling.Put("Farm_Committee_Examination_API", CheckListStatus);

            return Json("succ");
        }

    }
}