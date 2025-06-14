using PlantQuar.DTO.DTO.Common;
using PlantQuar.DTO.DTO.Company;
using PlantQuar.DTO.DTO.Farm.FarmCommittee;
using PlantQuar.DTO.DTO.Farm.FarmData;
using PlantQuar.DTO.DTO.Farm.FarmRequest;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.API;
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
    public class FarmDataController : BaseController
    {
        private string apiName = "FarmData_API";

        // GET: FA_Farm/FarmData
        public ActionResult Index()
        {
            return View();
        }

        //GetFarmsList
        public JsonResult GetFarmsList(string FarmCode_14)
        {
            var res = APIHandeling.getData(apiName + "?FarmCode_14=" + FarmCode_14);
            var lst = res.Content.ReadAsAsync<Dictionary<string, object>>().Result;//object

            //var StatusCode = lst.ElementAt(0).Value;
            var obj = lst.ElementAt(1).Value;

            JavaScriptSerializer ser = new JavaScriptSerializer();
            var myObj = ser.Deserialize<Dictionary<string, object>>(obj.ToString());

            var count = myObj.ElementAt(0).Value;
            var Lobj = myObj.ElementAt(1).Value;

            return Json(new { Result = "OK", Records = Lobj, TotalRecordCount = count }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FarmDetails(long FarmId, long RequestId = 0)
        {
            var model = new FarmsDataDTO();

            if (RequestId == 0)
            {
                model = getFarmByID(FarmId);
            }
            else
            {
                model = getFarmByRequest(FarmId, RequestId);

                if (model != null)
                {
                    model.RequestId = RequestId;
                    model.requestAccepted = model.requestLst.Where(r => r.ID == RequestId).Select(r => r.IsAcceppted).SingleOrDefault();
                    //eman
                    model.requestPaid = model.requestLst.Where(r => r.ID == RequestId).Select(r => r.IsPaid).SingleOrDefault();
                    model.requestLst = model.requestLst.OrderByDescending(r => r.User_Creation_Date).ToList();

                    var reasons = APIHandeling.getData(apiName + "?List=1&refuse=1");
                    var reasonsList = reasons.Content.ReadAsAsync<List<CustomOption>>().Result;
                    ViewBag.ListReasons = reasonsList;
                }
            }

            return View(model);
        }
        //Hadeer
        public JsonResult ItemCategoryList_ByItemID(long ItemId)
        {
            try
            {
                //  var res = APIHandeling.getData("ItemCategory_API?ItemId=" + ItemId);

                var res = APIHandeling.getData("ItemCategory_API?ItemId=" + ItemId);
                var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                // var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                ViewBag.VillageList = lst;

                return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "ItemCategoryList_ByItemID");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        //open FarmEdit
        public ActionResult FarmAddEdit(long FarmId = 0, long RequestId = 0)
        {
            Fill_Lists();

            if (FarmId > 0)
            {
                var model = new FarmsDataDTO();

                if (RequestId == 0)
                {
                    model = getFarmByID(FarmId);
                }
                else
                {
                    model = getFarmByRequest(FarmId, RequestId);

                    if (model != null)
                    {
                        model.RequestId = RequestId;
                        model.requestAccepted = model.requestLst.Where(r => r.ID == RequestId).Select(r => r.IsAcceppted).SingleOrDefault();
                        //eman
                        model.requestPaid = model.requestLst.Where(r => r.ID == RequestId).Select(r => r.IsPaid).SingleOrDefault();
                        model.requestLst = model.requestLst.OrderByDescending(r => r.User_Creation_Date).ToList();
                    }
                }
                // var model = getFarmByID(FarmId);
                Session["FarmID"] = model.ID;

                if (model.Item_ID > 0)
                {
                    Session["PlantId"] = model.Item_ID;
                    Session["NewPlant"] = 1;

                    MainClassification_AddEDIT();
                    SecondaryClassification_AddEDIT(model.mainClass_Id);
                    ItemGroup_AddEDIT(model.secClass_Id);
                    ItemData_AddEDIT_Known(model.group_Id, model.isKnown);

                    getScientificName((long)model.Item_ID);
                }
                else
                {
                    Session["PlantId"] = 0;
                    Session["NewPlant"] = 0;

                    MainClassification_AddEDIT();
                    SecondaryClassification_AddEDIT(0);
                    ItemGroup_AddEDIT(0);
                    ItemData_AddEDIT_Known(0, false);

                    getScientificName(0);
                }
                if (RequestId == 0)
                {
                    if (model.IsApproved == true)
                    {
                        var res = APIHandeling.getData(apiName + "?request=" + RequestId + "&farmId=" + FarmId);
                        var request = res.Content.ReadAsAsync<FarmRequestDTO>().Result;
                        if (request != null)
                            Session["RequestID"] = request.ID;
                        else
                            Session["RequestID"] = 0;
                    }
                    else
                    {
                        Session["RequestID"] = 0;
                    }
                }
                else
                {
                    Session["RequestID"] = RequestId;
                }



                Gov_List();
                CenterList_ByGov(model.Govern_ID);
                VillageList_ByCenter(model.Center_Id);

                return View(model);
            }
            else
            {
                Gov_List();
                CenterList_ByGov(0);
                VillageList_ByCenter(0);

                MainClassification_AddEDIT();
                SecondaryClassification_AddEDIT(0);
                ItemGroup_AddEDIT(0);
                ItemData_AddEDIT_Known(0);

                Session["FarmID"] = 0;
                Session["PlantId"] = 0;
                Session["RequestID"] = 0;
                Session["NewPlant"] = 0;

                return View(new FarmsDataDTO());
            }
        }

        //CheckFarmCode        
        public JsonResult CheckFarmCode(string FarmCode_14, long FarmId)
        {
            var res = APIHandeling.getData(apiName + "?check=1&FarmCode_14=" + FarmCode_14 + "&FarmId=" + FarmId);
            var codeFound = res.Content.ReadAsAsync<bool>().Result;//object

            return Json(new { Result = "OK", codeFound }, JsonRequestBehavior.AllowGet);
        }
        //save
        [HttpPost]
        public ActionResult SaveFarm(FarmsDataDTO model)
        {

            if (model.ID > 0)
            {

                //edit
                //farmdata                
                model.User_Updation_Id = (short)Session["UserId"];
                model.User_Updation_Date = DateTime.Now;

                APIHandeling.Put(apiName, model);

                //farm Company
                Farm_CompanyDTO company = model.ownerData;
                company.Farm_ID = model.ID;
                company.User_Updation_Id = (short)Session["UserId"];
                company.User_Updation_Date = DateTime.Now;

                APIHandeling.Put(apiName + "?editCompany=1", company);
            }
            else
            {
                //add
                model.User_Creation_Id = (short)Session["UserId"];
                model.User_Creation_Date = DateTime.Now;
                var res = APIHandeling.Post(apiName, model);

                var farm = res.Content.ReadAsAsync<FarmsDataDTO>().Result;//object                                
                model.ID = farm.ID;

                //farm Company
                Farm_CompanyDTO company = model.ownerData;
                company.Farm_ID = model.ID;
                company.User_Creation_Id = (short)Session["UserId"];
                company.User_Creation_Date = DateTime.Now;

                APIHandeling.Post(apiName + "?creatCompany=1", company);
            }

            return RedirectToAction("FarmAddEdit", "FarmData", new { area = "FA_Farm", FarmId = model.ID });
        }

        //Delete       
        public ActionResult Delete(long FarmId)
        {
            try
            {
                DeleteParameters obj = new DeleteParameters();
                obj.id = FarmId;

                User_Session Current = User_Session.GetInstance;
                obj.Userid = (short)Session["UserId"];
                obj._DateNow = DateTime.Now;

                APIHandeling.Delete(apiName, obj);

                return RedirectToAction("Index", "FarmData", new { area = "FA_Farm" });
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
        //*****************************//***************************************//
        private FarmsDataDTO getFarmByID(long FarmId)
        {
            var res = APIHandeling.getData(apiName + "?details=1&Id=" + FarmId);
            return res.Content.ReadAsAsync<FarmsDataDTO>().Result;
        }

        private FarmsDataDTO getFarmByRequest(long FarmId, long RequestId)
        {
            var res = APIHandeling.getData(apiName + "?details=1&FarmId=" + FarmId + "&RequestId=" + RequestId);
            return res.Content.ReadAsAsync<FarmsDataDTO>().Result;
        }
        //*****************************//***************************************//
        //listFarmCountry
        [HttpPost]
        public JsonResult listFarmCountry()
        {
            try
            {
                var farmId = long.Parse(Session["FarmID"].ToString());

                if (farmId > 0)
                {
                    var RequestID = long.Parse(Session["RequestID"].ToString());

                    var res = APIHandeling.getData(apiName + "?country=1&farmId=" + farmId + " &RequestID=" + RequestID);
                    //var countryLst = res.Content.ReadAsAsync<List<customCountry_Farm>>().Result;

                    var lst = res.Content.ReadAsAsync<Dictionary<string, object>>().Result;//object

                    var StatusCode = lst.ElementAt(0).Value;
                    var obj = lst.ElementAt(1).Value;

                    JavaScriptSerializer ser = new JavaScriptSerializer();
                    var countryLst = ser.Deserialize<List<FarmCountryDTO>>(obj.ToString());

                    return Json(new { Result = "OK", Records = countryLst.OrderBy(p => p.ID), TotalRecordCount = countryLst.Count });
                }

                return Json(new { Result = "OK", Records = new List<FarmCountryDTO>(), TotalRecordCount = 0 });
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
        //CreateFarmCountry
        [HttpPost]
        public JsonResult CreateFarmCountry(FarmCountryDTO farmCountry)
        {
            var farmId = long.Parse(Session["FarmID"].ToString());

            if (farmId > 0)
            {
                var allErrors = ModelState.Values.SelectMany(x => x.Errors).ToList();

                if (ModelState.IsValid)
                {
                    var model = new FarmCountryDTO();
                    User_Session Current = User_Session.GetInstance;

                    //farm req id
                    var farmreqId = long.Parse(Session["RequestID"].ToString());

                    if (farmreqId > 0)
                    {
                        model.Farm_Request_ID = farmreqId;
                    }
                    else
                    {
                        //create request
                        var req = new FarmRequestDTO();

                        req.FarmsData_ID = farmId;
                        req.IsAcceppted = true;
                        req.IsActive = true;
                        req.IsPaid = true;
                        req.IS_OnlineOffline = false;

                        req.User_Creation_Id = (short)Session["UserId"];
                        req.User_Creation_Date = DateTime.Now;

                        var res1 = APIHandeling.Post(apiName + "?creatRequest=1", req);
                        var farmReq = res1.Content.ReadAsAsync<FarmRequestDTO>().Result;//object                                
                        model.Farm_Request_ID = farmReq.ID;

                        Session["RequestID"] = farmReq.ID;
                    }

                    model.ID = farmCountry.ID;
                    model.UnionId = farmCountry.UnionId;
                    model.Country_ID = farmCountry.Country_ID;
                    model.Start_Date = farmCountry.Start_Date;
                    model.End_Date = farmCountry.End_Date;
                    model.IsAcceppted = farmCountry.IsAcceppted;
                    model.IsActive = farmCountry.IsActive;

                    model.User_Creation_Id = (short)Session["UserId"];
                    model.User_Creation_Date = DateTime.Now;

                    //check Repeated Data
                    var res = APIHandeling.Post(apiName + "?creatCountry=1", model);

                    return ((int)res.StatusCode != 409) ? Json(new { Result = "OK", Record = model })
                      : Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });
                }
                else
                {
                    return Json(new { Result = "Error", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });
                }
            }
            return Json(new { Result = "Error", Message = "يجب حفظ بيانات المزرعة أولا" });
        }
        //UpdateFarmCountry
        [HttpPost]
        public JsonResult UpdateFarmCountry(FarmCountryDTO farmCountry)
        {
            var farmId = long.Parse(Session["FarmID"].ToString());

            if (farmId > 0)
            {
                var allErrors = ModelState.Values.SelectMany(x => x.Errors).ToList();

                if (ModelState.IsValid)
                {
                    var model = new FarmCountryDTO();
                    model.ID = farmCountry.ID;
                    model.Farm_Request_ID = long.Parse(Session["RequestID"].ToString());
                    model.Country_ID = farmCountry.Country_ID;
                    model.Start_Date = farmCountry.Start_Date;
                    model.End_Date = farmCountry.End_Date;
                    model.IsAcceppted = farmCountry.IsAcceppted;
                    model.IsActive = farmCountry.IsActive;

                    User_Session Current = User_Session.GetInstance;
                    model.User_Updation_Id = (short)Session["UserId"];
                    model.User_Updation_Date = DateTime.Now;

                    //check Repeated Data
                    var res = APIHandeling.Put(apiName + "?editCountry=1", model);
                    return ((int)res.StatusCode != 409) ? Json(new { Result = "OK", Record = model })
                      : Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });
                }
                else
                {
                    return Json(new { Result = "Error", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });
                }
            }
            return Json(new { Result = "Error", Message = "يجب حفظ بيانات المزرعة أولا" });
        }
        //DeleteFarmCountry
        [HttpPost]
        public JsonResult DeleteFarmCountry(long ID)
        {
            try
            {
                if (ID > 0)
                {
                    DeleteParameters obj = new DeleteParameters();
                    obj.id = ID;
                    User_Session Current = User_Session.GetInstance;
                    obj.Userid = (short)Session["UserId"];
                    obj._DateNow = DateTime.Now;

                    APIHandeling.Put(apiName + "?deleteCountry=1", obj);
                }
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
        //*****************************//***************************************//
        public void Fill_Lists()
        {
            try
            {
                //Exporter TYpe
                var res = APIHandeling.getData("A_SystemCode_API?AddEdit=1&Sysnum=3");
                ViewBag.OwnerType = res.Content.ReadAsAsync<List<CustomOption>>().Result;

                //Compay
                res = APIHandeling.getData(apiName + "?company=1");
                ViewBag.CompanyList = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;

                //ViewBag.UnionList
                res = APIHandeling.getData("Union_API?AddEdit=1");
                ViewBag.UnionList = res.Content.ReadAsAsync<List<CustomOption>>().Result;

                //ViewBag.PublicOrgTypesList
                res = APIHandeling.getData(apiName + "?orgsType=1");
                ViewBag.PublicOrgTypesList = res.Content.ReadAsAsync<List<CustomOption>>().Result;

                res = APIHandeling.getData(apiName + "?person=1");
                ViewBag.PersonLst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;

                PublicOrg_ByTypes(0);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Fill_Lists");
            }
        }

        public JsonResult Gov_List()
        {
            try
            {
                var res = APIHandeling.getData("Governate_API?AddEdit=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                ViewBag.GovList = lst;

                return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Gov_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        public JsonResult CenterList_ByGov(int Govern_ID = 0)
        {
            try
            {
                var res = APIHandeling.getData("Centers_API?add=1&GovId=" + Govern_ID);
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                ViewBag.CenterList = lst;

                return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "CenterList_ByGov");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        public JsonResult VillageList_ByCenter(int Center_ID = 0)
        {
            try
            {
                var res = APIHandeling.getData("Village_API?CenterId=" + Center_ID);
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                ViewBag.VillageList = lst;

                return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "VillageList_ByCenter");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        //********************//
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

                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "SecondaryClassification_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        public JsonResult ItemGroup_AddEDIT(int SecClass_ID = 0)
        {
            try
            {
                var res = APIHandeling.getData("Group_API?AddEdit=1&SecClass_ID=" + SecClass_ID);
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;

                ViewBag.GroupLst = lst;

                return Json(new { Result = "OK", Options = lst });
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

                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "ItemData_AddEDIT_Known");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        //*******************//
        public JsonResult GetCompanyData(long compId = 0)
        {
            try
            {
                var res = APIHandeling.getData(apiName + "?companyId=" + compId);
                var dto = res.Content.ReadAsAsync<CompanyNationalDTO>().Result;

                return Json(new
                {
                    Result = "OK",
                    compOwnerName = dto.compOwnerName,
                    companyGoveName = dto.GoveName,
                    companyCenterName = dto.CenterName,
                    companyVillageName = dto.VillageName,
                    companyAddress = dto.address

                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "VillageList_ByCenter");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        //********************//
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
        public JsonResult GetPublicOrgData(long publicOrgId = 0)
        {
            try
            {
                var res = APIHandeling.getData(apiName + "?publicOrgId=" + publicOrgId);
                var dto = res.Content.ReadAsAsync<Public_OrganizationDTO>().Result;

                return Json(new
                {
                    Result = "OK",
                    Address = dto.orgAddress,
                    IsNational = dto.IsNational

                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetPublicOrgData");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        //*********************//
        public JsonResult GetAllCountries()
        {
            try
            {
                var res = APIHandeling.getData(apiName + "?country=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;

                return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetAllCountries");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        //GetUnionCountries_List
        public JsonResult GetUnionCountries_List(int unionId = 0)
        {
            try
            {
                var res = APIHandeling.getData(apiName + "?list=1&unionId=" + unionId);
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;

                return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetUnionCountries");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        public JsonResult GetUnionCountries(int unionId = 0)
        {
            try
            {
                var res = APIHandeling.getData(apiName + "?unionId=" + unionId);
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;

                return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetUnionCountries");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        //****************************************//       
        public JsonResult GetPersonData(long personId = 0, string personIdNum = "")
        {
            try
            {
                var res = APIHandeling.getData(apiName + "?personId=" + personId + "&&personIdNum=" + personIdNum);
                var dto = res.Content.ReadAsAsync<PersonDTO>().Result;

                if (dto != null)
                {
                    return Json(new
                    {
                        Result = "OK",
                        ID = dto.ID,
                        Name = dto.Name,
                        personIdType_Name = dto.personIdType_Name,
                        IDNumber = dto.IDNumber,
                        nationality = dto.nationality,
                        Address = dto.Address,
                        IsActive = dto.IsActive,
                        Job = dto.Job,
                        Phone = dto.Phone,
                        Email = dto.Email

                    }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RecordNotExist) });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetPersonData");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        //*****************************************************************//
        //palnt category
        public JsonResult PlantCategoryList(long ItemId = 0)
        {
            try
            {
                var res = APIHandeling.getData(apiName + "?category=1&plantId=" + ItemId);
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;

                return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "PlantCategoryList");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        //plant category by grp
        public JsonResult PlantCategoryListByGrp(long catGroupId = 0, long ItemId = 0)
        {
            try
            {
                var res = APIHandeling.getData(apiName + "?categoryGrp=" + catGroupId + "&plantId=" + ItemId);
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;

                return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "PlantCategoryListByGrp");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        [HttpPost]
        public void setPlantIDSession(long PlantId = 0)
        {
            Session["PlantId"] = PlantId;
        }

        [HttpPost]
        public JsonResult getScientificName(long PlantId = 0)
        {
            try
            {
                var res = APIHandeling.getData(apiName + "?scientific=1&PlantId=" + PlantId);
                var dto = res.Content.ReadAsAsync<string>().Result;

                return Json(new
                {
                    Result = "OK",
                    ScientificName = dto

                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "VillageList_ByCenter");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        [HttpPost]
        public JsonResult listFarmPlantCategories()
        {
            try
            {
                var farmId = long.Parse(Session["FarmID"].ToString());

                if (farmId > 0)
                {
                    //ss


                    var RequestID = long.Parse(Session["RequestID"].ToString());
                    var res = APIHandeling.getData(apiName + "?plant=1&farmId=" + farmId + " &RequestID=" + RequestID);

                    var lst = res.Content.ReadAsAsync<Dictionary<string, object>>().Result;//object

                    var StatusCode = lst.ElementAt(0).Value;
                    var obj = lst.ElementAt(1).Value;

                    JavaScriptSerializer ser = new JavaScriptSerializer();
                    var countryLst = ser.Deserialize<List<Farm_Request_ItemCategoriesDTO>>(obj.ToString());

                    return Json(new { Result = "OK", Records = countryLst.OrderBy(p => p.ID), TotalRecordCount = countryLst.Count });
                }

                return Json(new { Result = "OK", Records = new List<Farm_Request_ItemCategoriesDTO>(), TotalRecordCount = 0 });
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
        //CreateFarmCountry
        [HttpPost]
        public JsonResult CreateFarmPlantCategories(Farm_ItemCategoriesDTO farmPlant)
        {
            var farmId = long.Parse(Session["FarmID"].ToString());

            if (farmId > 0)
            {
                var allErrors = ModelState.Values.SelectMany(x => x.Errors).ToList();

                if (ModelState.IsValid)
                {
                    User_Session Current = User_Session.GetInstance;

                    var newPlant = int.Parse(Session["NewPlant"].ToString());

                    if (newPlant == 0)
                    {
                        var farmUpdatePlant = new FarmsDataDTO();

                        farmUpdatePlant.ID = farmId;
                        farmUpdatePlant.Item_ID = long.Parse(Session["PlantId"].ToString());
                        farmUpdatePlant.User_Updation_Id = (short)Session["UserId"];
                        farmUpdatePlant.User_Updation_Date = DateTime.Now;

                        APIHandeling.Put(apiName + "?farmPlant=1", farmUpdatePlant);

                        Session["NewPlant"] = 1;
                    }

                    var model = new Farm_ItemCategoriesDTO();

                    model.Farm_ID = farmId;
                    model.Area_Acres = farmPlant.Area_Acres;
                    model.Quantity_Ton = farmPlant.Quantity_Ton;
                    model.ItemCategories_ID = farmPlant.ItemCategories_ID;
                    model.StartDate = farmPlant.StartDate;
                    model.EndDate = farmPlant.EndDate;
                    model.IsActive = farmPlant.IsActive;

                    model.User_Creation_Id = (short)Session["UserId"];
                    model.User_Creation_Date = DateTime.Now;

                    //check Repeated Data
                    var res = APIHandeling.Post(apiName + "?createPlant=1", model);
                    return ((int)res.StatusCode != 409) ? Json(new { Result = "OK", Record = model })
                      : Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });
                }
                else
                {
                    return Json(new { Result = "Error", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });
                }
            }
            return Json(new { Result = "Error", Message = "يجب حفظ بيانات المزرعة أولا" });
        }
        //UpdateFarmCountry
        [HttpPost]
        public JsonResult UpdateFarmPlantCategories(Farm_ItemCategoriesDTO farmPlant)
        {
            var farmId = long.Parse(Session["FarmID"].ToString());

            if (farmId > 0)
            {
                var allErrors = ModelState.Values.SelectMany(x => x.Errors).ToList();

                if (ModelState.IsValid)
                {
                    var model = new Farm_ItemCategoriesDTO();
                    model.ID = farmPlant.ID;
                    model.Farm_ID = farmId;
                    model.Area_Acres = farmPlant.Area_Acres;
                    model.Quantity_Ton = farmPlant.Quantity_Ton;
                    //model.Plant_ID = long.Parse(Session["PlantId"].ToString());
                    model.ItemCategories_ID = farmPlant.ItemCategories_ID;
                    model.StartDate = farmPlant.StartDate;
                    model.EndDate = farmPlant.EndDate;
                    model.IsActive = farmPlant.IsActive;

                    User_Session Current = User_Session.GetInstance;
                    model.User_Updation_Id = (short)Session["UserId"];
                    model.User_Updation_Date = DateTime.Now;

                    //check Repeated Data
                    var res = APIHandeling.Put(apiName + "?editPlant=1", model);

                    return ((int)res.StatusCode != 409) ? Json(new { Result = "OK", Record = model })
                      : Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });
                }
                else
                {
                    return Json(new { Result = "Error", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });
                }
            }
            return Json(new { Result = "Error", Message = "يجب حفظ بيانات المزرعة أولا" });
        }
        //DeleteFarmCountry
        [HttpPost]
        public JsonResult DeleteFarmPlantCategories(long ID)
        {
            try
            {
                if (ID > 0)
                {
                    DeleteParameters obj = new DeleteParameters();
                    obj.id = ID;
                    User_Session Current = User_Session.GetInstance;
                    obj.Userid = (short)Session["UserId"];
                    obj._DateNow = DateTime.Now;

                    APIHandeling.Put(apiName + "?deletePlant=1", obj);
                }
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
        //*****************************************************************//
        [HttpPost]
        public JsonResult listFileAttachments()
        {
            //attach
            try
            {
                var farmId = long.Parse(Session["FarmID"].ToString());

                if (farmId > 0)
                {
                    var res = APIHandeling.getData(apiName + "?attach=1&farmId=" + farmId);

                    var lst = res.Content.ReadAsAsync<Dictionary<string, object>>().Result;//object

                    var StatusCode = lst.ElementAt(0).Value;
                    var obj = lst.ElementAt(1).Value;

                    JavaScriptSerializer ser = new JavaScriptSerializer();
                    var attachmentLst = ser.Deserialize<List<A_AttachmentDataDTO>>(obj.ToString());

                    return Json(new { Result = "OK", Records = attachmentLst.OrderBy(p => p.Id), TotalRecordCount = attachmentLst.Count });
                }

                return Json(new { Result = "OK", Records = new List<A_AttachmentDataDTO>(), TotalRecordCount = 0 });
            }
            catch (Exception ex)
            {
                if (ex.HResult == -2146233087)
                {
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RelatedData) });
                }
                else
                {
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "listFileAttachments");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        [HttpPost]
        public JsonResult CreateFileAttachments(A_AttachmentDataDTO model, HttpPostedFileBase Picture1)
        {
            var farmId = long.Parse(Session["FarmID"].ToString());

            if (farmId > 0)
            {
                var allErrors = ModelState.Values.SelectMany(x => x.Errors).ToList();

                if (ModelState.IsValid)
                {
                    //FileUpload_SaveDataController upload = new FileUpload_SaveDataController();
                    //model.AttachmentPath = upload.Upload_File_Data(Picture1, "FarmData");

                    FileUpload_SaveDataController fileUpload = new FileUpload_SaveDataController();
                    // model.AttachmentPath = fileUpload.Upload_File_Path_NetworkShare(Picture1, "Farm_" + farmId);

                    model.RowId = farmId;
                    model.A_AttachmentTableNameId = 5;

                    User_Session Current = User_Session.GetInstance;
                    model.User_Creation_Id = (short)Session["UserId"];
                    model.User_Creation_Date = DateTime.Now;

                    //check Repeated Data
                    var res = APIHandeling.Post(apiName + "?createAttachment=1", model);

                    return ((int)res.StatusCode != 409) ? Json(new { Result = "OK", Record = model })
                      : Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });
                }
                else
                {
                    return Json(new { Result = "Error", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });
                }
            }
            return Json(new { Result = "Error", Message = "يجب حفظ بيانات المزرعة أولا" });
        }
        [HttpPost]
        public JsonResult UpdateFileAttachments(A_AttachmentDataDTO model, HttpPostedFileBase Picture1)
        {
            var farmId = long.Parse(Session["FarmID"].ToString());

            if (farmId > 0)
            {
                var allErrors = ModelState.Values.SelectMany(x => x.Errors).ToList();

                if (ModelState.IsValid)
                {
                    //FileUpload_SaveDataController upload = new FileUpload_SaveDataController();
                    //model.AttachmentPath = upload.Upload_File_Data(Picture1, "FarmData");
                    if (Picture1 != null)
                    {
                        FileUpload_SaveDataController fileUpload = new FileUpload_SaveDataController();
                        // model.AttachmentPath = fileUpload.Upload_File_Path_NetworkShare(Picture1, "Farm_" + farmId);

                        string _Path = fileUpload.Get_Uplood_Imge("Farm_" + model.Id, Picture1, "Farm", "Farm_Data", Request.Url.AbsoluteUri.ToString());

                        model.AttachmentPath = _Path;
                    }
                    model.RowId = farmId;
                    model.A_AttachmentTableNameId = 5;

                    User_Session Current = User_Session.GetInstance;
                    model.User_Updation_Id = (short)Session["UserId"];
                    model.User_Updation_Date = DateTime.Now;

                    //check Repeated Data
                    var res = APIHandeling.Put(apiName + "?editAttachment=1", model);
                    return ((int)res.StatusCode != 409) ? Json(new { Result = "OK", Record = model })
                      : Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });
                }
                else
                {
                    return Json(new { Result = "Error", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });
                }
            }
            return Json(new { Result = "Error", Message = "يجب حفظ بيانات المزرعة أولا" });
        }
        [HttpPost]
        public JsonResult DeleteFileAttachments(int Id)
        {
            //deleteAttachment
            try
            {
                if (Id > 0)
                {
                    DeleteParameters obj = new DeleteParameters();
                    obj.id = Id;
                    User_Session Current = User_Session.GetInstance;
                    obj.Userid = (short)Session["UserId"];
                    obj._DateNow = DateTime.Now;

                    APIHandeling.Put(apiName + "?deleteAttachment=1", obj);
                }
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
        //***********************************************************//
        //AcceptRequest

        public ActionResult AcceptRequest(int accept, long farmId, long requestId)
        {
            var model = new FarmRequestDTO();
            model.ID = requestId;
            model.FarmsData_ID = farmId;
            model.IsAcceppted = (accept == 1 ? true : false);

            //accept/reject request
            User_Session Current = User_Session.GetInstance;

            model.User_Updation_Id = (short)Session["UserId"];
            model.User_Updation_Date = DateTime.Now;

            APIHandeling.Put(apiName + "?accept=1", model);
            //var lst = res.Content.ReadAsAsync<FarmRequestDTO>().Result;//object

            //---add new row in export committee
            if (model.IsAcceppted == true)
            {
                Farm_CommitteeDTO newFarmCommittee = new Farm_CommitteeDTO();
                newFarmCommittee.FarmsData_ID = farmId;
                newFarmCommittee.Farm_Request_ID = requestId;
                newFarmCommittee.CommitteeType_ID = null;
                newFarmCommittee.IsApproved = null;
                newFarmCommittee.Status = null;
                newFarmCommittee.User_Creation_Date = DateTime.Now;
                newFarmCommittee.User_Creation_Id = (short)Session["UserId"];


                var res = APIHandeling.Post("FarmDetails_API?newCreate=1", newFarmCommittee);
            }

            return RedirectToAction("Index", "FarmListOnLine", new { area = "FA_Farm" });
        }

        [HttpPost]
        public JsonResult SaveCountryAccreditation
            (long requestId, short countryId, DateTime? StartDate, DateTime? EndDate, bool isAccept = false, bool isActive = false)
        {
            try
            {
                var model = new FarmCountryDTO();
                model.Farm_Request_ID = requestId;
                model.Country_ID = countryId;
                model.IsAcceppted = isAccept;
                model.IsActive = isActive;
                model.Start_Date = StartDate;
                model.End_Date = EndDate;

                //accept/reject request
                User_Session Current = User_Session.GetInstance;

                model.User_Updation_Id = (short)Session["UserId"];
                model.User_Updation_Date = DateTime.Now;

                APIHandeling.Put(apiName + "?editCountry=1", model);

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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "SaveCountryAccreditation");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        [HttpPost]
        public ActionResult SaveApprovedFarm(FarmsDataDTO model)
        {
            User_Session Current = User_Session.GetInstance;

            //edit
            //farmdata                
            model.User_Updation_Id = (short)Session["UserId"];
            model.User_Updation_Date = DateTime.Now;

            APIHandeling.Put(apiName + "?saveApproved=1", model);

            return RedirectToAction("FarmDetails", "FarmData", new { area = "FA_Farm", FarmId = model.ID, RequestId = model.RequestId });
        }


        public ActionResult GetReport(string path1)
        {
            try
            {

                Session["Path_Server"] = path1;
                return Redirect("~/ASP/DisplayImge.aspx");

            }
            catch (Exception ex)
            {

                return null;

            }

        }

        //******************** sayed 25-8-2022 *************************//

        [HttpPost]
        public ActionResult saveReasons(List<short> listIDs, long Farm_Request_ID, string message)
        {
            ReasonsList_FarmDTO dto = new ReasonsList_FarmDTO();
            dto.Farm_Request_ID = Farm_Request_ID;
            dto.refuseReasonsIds = listIDs;
            dto.Nots = message;
            dto.User_Creation_Id = (short)Session["UserId"];
            dto.User_Creation_Date = DateTime.Now;
            APIHandeling.Post(apiName + "?listt=1", dto);
            return Json("succ");
        }
    }
}