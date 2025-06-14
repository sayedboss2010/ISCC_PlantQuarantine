using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using PlantQuar.DTO.DTO.Common;
using PlantQuar.DTO.DTO.Company;
using PlantQuar.DTO.DTO.Farm.FarmCommittee;
using PlantQuar.DTO.DTO.Farm.FarmData;
using PlantQuar.DTO.DTO.Farm.FarmRequest;
using PlantQuar.WEB.Controllers;
using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Station;
using PlantQuar.DTO.DTO.DataEntry.Items.ItemData;

namespace PlantQuar.WEB.Areas.ST_Station.Controllers
{
    public class StationActivityDataController : BaseController
    {
        // GET: ST_Station/StationActivityData
        public static long ID;

        private string apiName = "StationActivityType_API";
        public ActionResult Index()
        {
            var res = APIHandeling.getData(apiName + "?pageSize=1&index=1");
            ViewBag.ActivityType = res.Content.ReadAsAsync<List<StationActivityType>>().Result;
            Fill_Lists();
            return View();
        }

        #region Fill_Lists
        public void Fill_Lists()
        {
            try
            {
                //SystemCode TYpe
                var res = APIHandeling.getData("A_SystemCode_API?AddEdit=1&Sysnum=21");
                ViewBag.OwnerType = res.Content.ReadAsAsync<List<CustomOption>>().Result;



                var res1 = APIHandeling.getData("Union_API?AddEdit=1");
                ViewBag.Unions = res1.Content.ReadAsAsync<List<CustomOption>>().Result;

                var Fees_Process = APIHandeling.
                          getData("StationAccreditionData_API?list=1");
                var lst = Fees_Process.Content.ReadAsAsync<List<Station_CheckList>>().Result;

                ViewBag.checkLists = lst;


                MainClassification_AddEDIT();
                SecondaryClassification_AddEDIT();
                ItemGroup_AddEDIT();
                ItemData_AddEDIT_Known();

                PublicOrg_ByTypes(0);

            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Fill_Lists");
            }
        }   

        [HttpPost]
        public JsonResult GetTreatmentData(int ActivityTypeId)
        {
            var Fees_Process = APIHandeling.
                            getData("StationAccreditionData_API?ActivityTypeId=" + ActivityTypeId);
            var lst = Fees_Process.Content.ReadAsAsync<List<TreatmentDataDto>>().Result;
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetUnoinCountries(int unoinID)
        {
            var Fees_Process = APIHandeling.
                            getData("StationAccreditionData_API?UnoinId=" + unoinID);
            var lst = Fees_Process.Content.ReadAsAsync<List<CustomOption>>().Result;
            return Json(lst, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetCheckLists()
        {
            var Fees_Process = APIHandeling.
                            getData("StationAccreditionData_API?list=1");
            var lst = Fees_Process.Content.ReadAsAsync<List<Station_CheckList>>().Result;
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SendStationAccredition(StationAccreditionDataDTO Dto)
        {
            User_Session current = User_Session.GetInstance;
            // Dto.User_Creation_Id = current.UserId;
            Dto.User_Creation_Id = (short)Session["UserId"]; ;
            Dto.User_Creation_Date = DateTime.Now;

            var res = APIHandeling.Post("StationAccreditionData_API", Dto);
            var lst = res.Content.ReadAsAsync<StationAccreditionDataDTO>().Result;
            ID = lst.ID;
            return ((int)res.StatusCode != 409 || (int)res.StatusCode != 500) ? Json(new { Result = "OK", Record = Dto })
                     : Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });
        }

      
        [HttpPost]
        public JsonResult MainClassification_AddEDIT()
        {
            try
            {
                var res = APIHandeling.getData("MainClassification_API?AddEdit=1&ItemType_ID=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                ViewBag.MainClassification = lst;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "MainClassification_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        [HttpPost]
        public JsonResult SecondaryClassification_AddEDIT(int MainClass_ID = 0)
        {
            try
            {
                var res = APIHandeling.getData("SecondaryClassification_API?AddEdit=1&MainClass_ID=" + MainClass_ID);
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;

                ViewBag.SecondaryClassification = lst;

                return Json(lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "SecondaryClassification_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        [HttpPost]
        public JsonResult ItemGroup_AddEDIT(int SecClass_ID = 0)
        {
            try
            {
                var res = APIHandeling.getData("Group_API?AddEdit=1&SecClass_ID=" + SecClass_ID);
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;

                ViewBag.GroupLst = lst;

                return Json(lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "PlantGroup_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        [HttpPost]
        public JsonResult ItemData_AddEDIT_Known(int Group_ID = 0, bool IsKnown = false)
        {
            try
            {
                var res = APIHandeling.getData("Item_API?Group_ID=" + Group_ID + "&IsKnown=" + IsKnown);
                var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;

                ViewBag.ItemsLst = lst;

                return Json(lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "ItemData_AddEDIT_Known");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        [HttpPost]
        public JsonResult GetItemShortnames(int itemId)
        {
            try
            {
                var res = APIHandeling.getData("Item_ShortName_API?itemIds=" + itemId);
                var lst = res.Content.ReadAsAsync<List<Item_ShortNameDTO>>().Result;

                //  ViewBag.ItemsLst = lst;

                return Json(lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "ItemData_AddEDIT_Known");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        public JsonResult PublicOrg_ByTypes(int orgTypeId = 0)
        {
            try
            {
                var res = APIHandeling.getData(apiName + "?orgType_Id=" + orgTypeId);
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                ViewBag.PublicOrgList = lst;

                return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "PublicOrg_ByTypes");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        #endregion

        [HttpPost]
        public JsonResult SendStationAccreditionCountry(StationAccreditionDataCountryDTO Dtos)
        {
            User_Session current = User_Session.GetInstance;
            Dtos.User_Creation_Id = (short)Session["UserId"];
            //  Dtos.User_Creation_Id = current.UserId;
            Dtos.User_Creation_Date = DateTime.Now;
            Dtos.Station_Accreditation_Data_ID = ID;

            var res = APIHandeling.Post("AcrreditionCountry_API", Dtos);
            return ((int)res.StatusCode != 409 || (int)res.StatusCode != 500) ? Json(new { Result = "OK", Record = Dtos })
                     : Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });
        }

        [HttpPost]
        public JsonResult SendStationAccreditionShortName
          (List<Fees_Constrain_Data_Item_ShortNameDTO> Dtos)
        {
            User_Session current = User_Session.GetInstance;
            //Dtos[0].User_Creation_Id = current.UserId;
            Dtos[0].User_Creation_Id = (short)Session["UserId"];
            Dtos[0].User_Creation_Date = DateTime.Now;
            Dtos[0].Station_Accreditation_Data_ID = ID;
            var res = APIHandeling.Post("AcrreditionShortName_API", Dtos);
            return ((int)res.StatusCode != 409 || (int)res.StatusCode != 500) ? Json(new { Result = "OK", Record = Dtos })
                     : Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });

        }
        [HttpPost]
        public JsonResult SendStationAccreditionConstrains
          (List<Station_Accredition_CheckListDTO> Dtos)
        {
            User_Session current = User_Session.GetInstance;
            //   User_Session current = User_Session.GetInstance;
            //  Dtos[0].User_Creation_Id = current.UserId;
            Dtos[0].User_Creation_Id = (short)Session["UserId"];
            Dtos[0].User_Creation_Date = DateTime.Now;
            Dtos[0].Station_Accreditation_Data_ID = ID;

            var res = APIHandeling.Post("AcrreditionCheckLists_API", Dtos);
            return ((int)res.StatusCode != 409 || (int)res.StatusCode != 500) ? Json(new { Result = "OK", Record = Dtos })
                    : Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });
        }
    }
}