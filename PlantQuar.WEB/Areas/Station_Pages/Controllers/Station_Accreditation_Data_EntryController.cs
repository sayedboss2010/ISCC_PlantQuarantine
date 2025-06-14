using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

using PlantQuar.WEB.Controllers;
using PlantQuar.DAL;

using PlantQuar.DTO.DTO.Station_Pages;
using PlantQuar.WEB.API;
using PlantQuar.DTO.DTO.Station;

namespace PlantQuar.WEB.Areas.Station_Pages.Controllers
{
    public class Station_Accreditation_Data_EntryController : BaseController
    {
        // GET: Station_Pages/Station_Accreditation_Data_Entry
        //public static long ID;

        private string apiName = "Station_Accreditation_Data_Entry_API";
        public ActionResult Index(long requestId=0 , int Accreditation_Type_ID=0, int ActivityType_List = 0)
        {
            Fill_Lists();
            if (requestId > 0)
            {
                var res = APIHandeling.getData(apiName + "?details=1&Id=" + requestId);
                var list = res.Content.ReadAsAsync<Station_Accreditation_Data_Entry_DTO>().Result;
                //list.List_Station_Country.FirstOrDefault().Union_Id = 0;
                //ViewBag.Accreditation_Type_ID = 0;
               
                return View(list);
            }
            else
            {
                Station_Accreditation_Data_Entry_DTO defaulModel = new Station_Accreditation_Data_Entry_DTO();

                if (Accreditation_Type_ID != 0)
                {
                    defaulModel.Accreditation_Type_ID = Accreditation_Type_ID;

                } if (ActivityType_List != 0)
                {
                    defaulModel.StationActivityType_ID = (byte)ActivityType_List;

                }
                Session["listshiftTimingsession"] = new List<A_AttachmentData_Station_DTO>();
                Session["listshiftTimingsession_Index"] = 1;
                return View(defaulModel);
            }
        }



        #region     Attachment_Station 
        [HttpPost]
        public JsonResult list_A_AttachmentData_Station(long Im_RequestCommittee_ID = 0, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = "")
        {
            try
            {
                var companiescount = Session["listshiftTimingsession"] as List<A_AttachmentData_Station_DTO>;
                return Json(new { Result = "OK", Records = companiescount.OrderBy(p => p.Index), TotalRecordCount = companiescount.Count });

            }
            catch (Exception ex)
            {
                if (ex.HResult == -2146233087)
                {
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RelatedData) });
                }
                else
                {
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "listshiftTiming");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        [HttpPost]
        public JsonResult Create_A_AttachmentData_Station(A_AttachmentData_Station_DTO model
            , HttpPostedFileBase Picture1)
        {
            try
            {            
                var allErrors = ModelState.Values.SelectMany(x => x.Errors).ToList();

                if (ModelState.IsValid)
                {
                    int Count = int.Parse(Session["listshiftTimingsession_Index"].ToString())-1;
                    model.Index = int.Parse(Session["listshiftTimingsession_Index"].ToString());

                    var companies = Session["listshiftTimingsession"] as List<A_AttachmentData_Station_DTO>;
                    //model.Amount = companies.
                    companies.Add(model);
                    if (Picture1 != null)
                    {
                        FileUpload_SaveDataController fileUpload = new FileUpload_SaveDataController();
                        string _Path = fileUpload.Get_Uplood_Imge("Station_", Picture1, "Station", "Station_Accreditation", Request.Url.AbsoluteUri.ToString());
                        companies[Count].AttachmentPath = _Path;
                    }
                    
                    Session["listshiftTimingsession"] = companies;
                 
                    Session["listshiftTimingsession_Index"] = int.Parse(Session["listshiftTimingsession_Index"].ToString()) + 1;
                    return Json(new { Result = "OK", Record = companies.OrderBy(p => p.Index), TotalRecordCount = companies.Count });
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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "CreateshiftTiming");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }
        #endregion

        #region Fill_Lists
        public void Fill_Lists()
        {
            try
            {                      
                var res = APIHandeling.getData("StationActivityType_API?List=1");
                ViewBag.StationActivityType = res.Content.ReadAsAsync<List<CustomOption>>().Result;
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Fill_Lists");
            }
        }

        [HttpPost]
        public JsonResult Union_List()
        {
            try
            {
                var res = APIHandeling.getData("Ex_CountryConstrain_API?Union=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "StationActivityType_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
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
        public JsonResult GetCheckLists()
        {
            var Fees_Process = APIHandeling. getData("StationAccreditionData_API?list=1");
            var lst = Fees_Process.Content.ReadAsAsync<List<Station_CheckList>>().Result;
            return Json(lst, JsonRequestBehavior.AllowGet);
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
        
        public JsonResult Save_Station_Accreditation_Data(
      Station_Accreditation_Data_Entry_DTO CheckedList
     , List<Station_Accreditation_Data_CountryDTO> Checked_Country_List
     , List<Station_Accreditation_Data_Item_ShortNameDTO> Checked_Item_ShortName_List
     , List<Station_CheckList_DTO> Checked_CheckList_List)
        {
            if (CheckedList != null)
            {
                //#region بيانات الاشتراطات الاساسية
                Station_Accreditation_Data_Entry_DTO _Station_Accreditation_Data = new Station_Accreditation_Data_Entry_DTO();

                _Station_Accreditation_Data.StationActivityType_ID = CheckedList.StationActivityType_ID;
                _Station_Accreditation_Data.Accreditation_Type_ID = CheckedList.Accreditation_Type_ID;
                _Station_Accreditation_Data.Name_AR = CheckedList.Name_AR;
                _Station_Accreditation_Data.Name_En = CheckedList.Name_En;
                _Station_Accreditation_Data.Description_Ar = CheckedList.Description_Ar;
                _Station_Accreditation_Data.Description_En = CheckedList.Description_En;
                _Station_Accreditation_Data.DescriptionMore_AR = CheckedList.DescriptionMore_AR;
                _Station_Accreditation_Data.DescriptionMore_EN = CheckedList.DescriptionMore_EN;
                _Station_Accreditation_Data.IsActive = CheckedList.IsActive;
                _Station_Accreditation_Data.User_Creation_Id = (short)Session["UserId"];
                _Station_Accreditation_Data.User_Creation_Date = DateTime.Now;
                // بيانات الدول 
                _Station_Accreditation_Data.List_Station_Country = Checked_Country_List;
                //المسمى المختصر
                _Station_Accreditation_Data.List_Station_Item_ShortName = Checked_Item_ShortName_List;
                //اختيار النصوص
                _Station_Accreditation_Data.List_Station_CheckList = Checked_CheckList_List;

                // تحميل الملفات

                


                var companies = Session["listshiftTimingsession"] as List<A_AttachmentData_Station_DTO>;
                if(companies != null)
                {
                    _Station_Accreditation_Data.List_Station_Attachment = companies;
                }

                var res2 = APIHandeling.Put(apiName + "?req=1", _Station_Accreditation_Data);
                var list = res2.Content.ReadAsAsync<Dictionary<string, object>>().Result;
                string Mess = "ok";
                if (list != null)
                {
                    foreach (var item in list)
                    {
                        if (item.Key == "Message")
                        {
                            // return Json(item.Value, JsonRequestBehavior.AllowGet);
                            if (item.Value != null)
                            {
                                Mess = item.Value.ToString();
                            }
                        }
                        //item.Value
                    }
                    return Json(Mess, JsonRequestBehavior.AllowGet);

                }
                else
                    return Json("error", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return null;
            }
        }

        public JsonResult Edite_Station_Accreditation_Data(
     Station_Accreditation_Data_Entry_DTO CheckedList
    , List<Station_Accreditation_Data_CountryDTO> Checked_Country_List
    , List<Station_Accreditation_Data_Item_ShortNameDTO> Checked_Item_ShortName_List
    , List<Station_CheckList_DTO> Checked_CheckList_List)
        {
            if (CheckedList != null)
            {
                //#region بيانات الاشتراطات الاساسية
                Station_Accreditation_Data_Entry_DTO _Station_Accreditation_Data = new Station_Accreditation_Data_Entry_DTO();

                _Station_Accreditation_Data.ID = CheckedList.ID;
                _Station_Accreditation_Data.StationActivityType_ID = CheckedList.StationActivityType_ID;
                _Station_Accreditation_Data.Accreditation_Type_ID = CheckedList.Accreditation_Type_ID;
                _Station_Accreditation_Data.Name_AR = CheckedList.Name_AR;
                _Station_Accreditation_Data.Name_En = CheckedList.Name_En;
                _Station_Accreditation_Data.Description_Ar = CheckedList.Description_Ar;
                _Station_Accreditation_Data.Description_En = CheckedList.Description_En;
                _Station_Accreditation_Data.DescriptionMore_AR = CheckedList.DescriptionMore_AR;
                _Station_Accreditation_Data.DescriptionMore_EN = CheckedList.DescriptionMore_EN;
                _Station_Accreditation_Data.IsActive = CheckedList.IsActive;
                _Station_Accreditation_Data.User_Creation_Id = (short)Session["UserId"];
                _Station_Accreditation_Data.User_Creation_Date = DateTime.Now;
                // بيانات الدول 
                _Station_Accreditation_Data.List_Station_Country = Checked_Country_List;
                //المسمى المختصر
                _Station_Accreditation_Data.List_Station_Item_ShortName = Checked_Item_ShortName_List;
                //اختيار النصوص
                _Station_Accreditation_Data.List_Station_CheckList = Checked_CheckList_List;

                // تحميل الملفات


                var companies = Session["listshiftTimingsession"] as List<A_AttachmentData_Station_DTO>;
                if (companies != null)
                {
                    _Station_Accreditation_Data.List_Station_Attachment = companies;
                }

                var res2 = APIHandeling.Put(apiName + "?Edite=1", _Station_Accreditation_Data);
                var list = res2.Content.ReadAsAsync<Dictionary<string, object>>().Result;
                string Mess = "ok";
                if (list != null)
                {
                    foreach (var item in list)
                    {
                        if (item.Key == "Message")
                        {
                            // return Json(item.Value, JsonRequestBehavior.AllowGet);
                            if (item.Value != null)
                            {
                                Mess = item.Value.ToString();
                            }
                        }
                        //item.Value
                    }
                    return Json(Mess, JsonRequestBehavior.AllowGet);

                }
                else
                    return Json("error", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return null;
            }
        }


        [HttpPost]
        public JsonResult Station_Accreditation_Data_Name_List(int? StationActivityType_ID,int? Accreditation_Type_ID)
        {
            try
            {
                var res = APIHandeling.getData(apiName + "?StationActivityType_ID="+ StationActivityType_ID + "&&Accreditation_Type_ID="+ Accreditation_Type_ID);
                var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "StationActivityType_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }


    }
}