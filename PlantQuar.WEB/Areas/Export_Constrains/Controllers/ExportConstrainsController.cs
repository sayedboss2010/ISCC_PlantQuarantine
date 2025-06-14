using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Export_Constrains;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using PlantQuar.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.Export_Constrains.Controllers
{
    public class ExportConstrainsController : BaseController
    {
        // GET: Export_Constrains/ExportConstrains
        public ActionResult Index()
        {
            LoadData();
            return View();
        }

        public void LoadData()
        {
            //ViewBag.ConstrainTypeLst
            //var res = APIHandeling.getData("A_SystemCode_API?Syscode=1");
            //ViewBag.ConstrainTypeLst = res.Content.ReadAsAsync<List<CustomOption>>().Result;

            //ViewBag.CountriesLst
           var res = APIHandeling.getData("Country_API?AddEdit=1");
            ViewBag.CountriesLst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;

            //ViewBag.UnionsLst
            res = APIHandeling.getData("Union_API?AddEdit=1");
            ViewBag.UnionsLst = res.Content.ReadAsAsync<List<CustomOption>>().Result;

            //***********************************//          
            //ViewBag.PlantLst
            res = APIHandeling.getData("Plant_API?plant=1");
            ViewBag.PlantLst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
           
            //***********************************//
            Session["PlantsConstrain_Rows"] = new List<CustomPlantConstrain_Rows>();
            Session["PlantConRows_Index"] = 1;
            Session["DB_PlantConstrainRows"] = new CustomCountryConstrain();
            Session["DB_PlantConstrainTreatment"] = new CustomCountryConstrain();
            Session["PlantsConstrain_Treatments"] = new List<CustomPlantConstrain_Treatments>();
            Session["PlantConRows_IndexTreatments"] = 1;
            Session["DB_PlantConstrainAnalysis"] = new CustomCountryConstrain();
            Session["PlantsConstrain_Analysis"] = new List<CustomPlantConstrain_Analysis>();
            Session["PlantConRows_IndexAnalysis"] = 1;
            Session["DB_PlantConstrainPorts"] = new CustomCountryConstrain();
            Session["PlantsConstrain_Ports"] = new List<CustomPlantConstrain_ArrivalPorts>();
            Session["PlantConRows_IndexPorts"] = 1;
          

            
           

            Session["DeletedConstrain"] = new List<DeleteParameters>();
        }
        //***********boss*****************//
       

        //*********sayed ***************//
        #region ShortName _Category
        public JsonResult PlantCategoryList(int plantId = 0)
        {
            try
            {
                var res = APIHandeling.getData("PlantCategory_API?plantId=" + plantId);
                var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "PlantCategoryList");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        public JsonResult GetPlantShortName(long Item_Id = 0)
        {
            try
            {
                var res = APIHandeling.getData("Item_ShortName_API?shortNByIt=1&itemId=" + Item_Id);
                var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                Session["PlantId"] = Item_Id;
                return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
               


                //var res = APIHandeling.getData("Item_ShortName_API?shortNByIt=1&itemId=" + Item_Id);
                //var data = res.Content.ReadAsAsync<Dictionary<string, string>>().Result;


                ////Session["PlantPurposeId"] = purposeId;
                ////Session["PlantStatusId"] = statusId;

                ////res = APIHandeling.getData("PlantPart_API?plantPartId=" + partType);
                ////Session["PlantPartType"] = res.Content.ReadAsAsync<byte>().Result;

                ////Session["CategoryId"] = catId;

                //return Json(new { shortName = data["shortName"].ToString(), hsCode = data["HSCODE"].ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "plantId_ShortName");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        public JsonResult Get_ShortNameDetails(int ShortName)
        {
            try
            {
                var res = APIHandeling.getData("Item_ShortName_API?ShortName=" + ShortName);
                var data = res.Content.ReadAsAsync<Dictionary<string, string>>().Result;


                return Json(new { SubPart_Name = data["SubPart_Name"].ToString(), Status_Name = data["Status_Name"].ToString(), Purpose_Name = data["Purpose_Name"].ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "plantId_ShortName");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        #endregion

        //*************************************//        
        [HttpPost]
        public ActionResult constrainSave(CustomCountryConstrain countryConstrain)
        {
            //ask if that check true
            if (Session["PlantId"] != null)
            {
                var texts = Session["PlantsConstrain_Rows"] as List<CustomPlantConstrain_Rows>;
                if (texts.Count > 0)
                {
                    plants plantt = new plants();

                    plantt.Item_ShortName_id = (long)Session["PlantId"];
                    //plantt.ItemCategories_ID = (long)Session["PlantStatusId"];
                    //plantt.IsCertificate_Addtion = (bool)Session["PlantPurposeId"];


                    plantt.PlantConstrain_Rows = Session["PlantsConstrain_Rows"] as List<CustomPlantConstrain_Rows>;
                    plantt.PlantConstrain_Analysis = Session["PlantsConstrain_Analysis"] as List<CustomPlantConstrain_Analysis>;
                    plantt.PlantConstrain_ArrivalPorts = Session["PlantsConstrain_Ports"] as List<CustomPlantConstrain_ArrivalPorts>;

                    plantt.PlantConstrain_Treatments = Session["PlantsConstrain_Treatments"] as List<CustomPlantConstrain_Treatments>;
                    countryConstrain.plants = plantt;
                }

            }
           
            countryConstrain.User_Creation_Id = (short)Session["UserId"];
            countryConstrain.User_Creation_Date = DateTime.Now;

            APIHandeling.Post("Export_Constrains_API", countryConstrain);

            if (Session["DeletedConstrain"] != null)
            {
                var deleteList = Session["DeletedConstrain"] as List<DeleteParameters>;
                if (deleteList.Count > 0)
                {
                    APIHandeling.Put("Export_Constrains", deleteList);
                }
            }

            return RedirectToAction("Index", "ExportConstrains", new { area = "Export_Constrains" });
        }
        //*************************************//
        //************************//   

        public JsonResult ProductList(int plantId = 0)
        {
            try
            {
                var res = APIHandeling.getData("Product?plantId=" + plantId);
                var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "PlantPartsList");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        public JsonResult GetProductHSCode(long productId = 0, byte purposeId = 0, byte statusId = 0)
        {
            try
            {
                Session["ProductId"] = productId;
                Session["ProductPurposeId"] = purposeId;
                Session["ProductstatusId"] = statusId;

                var res = APIHandeling.getData("Product?productId=" + productId);
                var HSCODE = res.Content.ReadAsAsync<string>().Result;

                return Json(new { hsCode = HSCODE }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetProductHSCode");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        //************************//       
        public JsonResult PlantPartsList(int plantId = 0)
        {
            try
            {
                var res = APIHandeling.getData("PlantPart_API?AddEdit=1&PlantId=" + plantId);
                var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "PlantPartsList");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        //*************************************//      
        public JsonResult GetLiableAliveShortName(long liableId = 0, byte purposeId = 0, int statusId = 0, int phaseId = 0)
        {
            try
            {
                Session["AliveId"] = liableId;
                Session["AlivePurposeId"] = purposeId;
                Session["AliveStatusId"] = statusId;
                Session["AliveBiologicalPhase"] = phaseId;

                var res = APIHandeling.getData("LiableItems_ShortName?liableId=" + liableId + "&purposeId=" + purposeId + "&statusId=" + statusId + "&phaseId=" + phaseId);
                var data = res.Content.ReadAsAsync<Dictionary<string, string>>().Result;

                return Json(new { shortName = data["shortName"].ToString(), hsCode = data["HSCODE"].ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "liableShortName_ShortName");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        //*************************************//      
        public JsonResult GetLiableNotAliveShortName(long liableId = 0, byte purposeId = 0, int statusId = 0, int phaseId = 0)
        {
            try
            {
                Session["NotAliveId"] = liableId;
                Session["NotAlivePurposeId"] = purposeId;
                Session["NotAliveStatusId"] = statusId;

                var res = APIHandeling.getData("LiableItems_ShortName?liableId=" + liableId + "&purposeId=" + purposeId + "&statusId=" + statusId);
                var data = res.Content.ReadAsAsync<Dictionary<string, string>>().Result;

                return Json(new { shortName = data["shortName"].ToString(), hsCode = data["HSCODE"].ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetLiableNotAliveShortName");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        //*************************************//
        public JsonResult GetAnalysisTypes(int type)
        {
            try
            {
                var res = APIHandeling.getData("AnalysisType_API?AddEdit=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetAnalysisTypes");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        public JsonResult GetAnalysisTypes_Jtable(int type)
        {
            try
            {
                var res = APIHandeling.getData("AnalysisType_API?AddEdit=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetAnalysisTypes");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        public JsonResult GetAnalysisLab(int analysisType = 0)
        {
            try
            {
                var res = APIHandeling.getData("AnalysisLab_API?AnalysisTypeId=" + analysisType);
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetAnalysisLab");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        public JsonResult GetAnalysisLab_Jtable(int analysisType = 0)
        {
            try
            {
                var res = APIHandeling.getData("AnalysisLab_API?AnalysisTypeId=" + analysisType);
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetAnalysisLab");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        //*************************************//
        //********************Traetment****************//
        public JsonResult GetTraetmentMainType(int type = 0)
        {
            try
            {
                var res = APIHandeling.getData("TreatmentMainType_API?AddEdit=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetTraetmentMainType");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        public JsonResult GetTraetmentMainType_Jtable(int type = 0)
        {
            try
            {
                var res = APIHandeling.getData("TreatmentMainType_API?AddEdit=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetTraetmentMainType");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        public JsonResult GetTraetmentType(int mainType = 0)
        {
            try
            {
                var res = APIHandeling.getData("TreatmentType_API?TreatmentMain_Id=" + mainType);
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetTraetmentType");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        public JsonResult GetTraetmentType_Jtable(int mainType = 0)
        {
            try
            {
                var res = APIHandeling.getData("TreatmentType_API?TreatmentMain_Id=" + mainType);
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetTraetmentType");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        public JsonResult GetTraetmentMethod(int treatmentType = 0)
        {
            try
            {
                var res = APIHandeling.getData("TreatmentMethod_API?TreatmentTypeId=" + treatmentType);
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetTraetmentMethod");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        public JsonResult GetTraetmentMethod_Jtable(int treatmentType = 0)
        {
            try
            {
                var res = APIHandeling.getData("TreatmentMethod_API?TreatmentTypeId=" + treatmentType);
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetTraetmentMethod");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        public JsonResult GetTraetmentMaterial(int treatmentType = 0)
        {
            try
            {
                var res = APIHandeling.getData("TreatmentMaterial_API?TreatmentTypeId=" + treatmentType);
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetTraetmentMaterial");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        public JsonResult GetTraetmentMaterial_Jtable(int treatmentType = 0)
        {
            try
            {
                var res = APIHandeling.getData("TreatmentMainType_API?TreatmentTypeId=" + treatmentType);
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetTraetmentMaterial");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
       
        public JsonResult getConstrainsPlants
            ( long ShortName_ID = 0, long catId=0, int constrainType=0, int owner=0)
        {
            try
            {
                //var res = APIHandeling.getData("PlantPart_API?plantPartId=" + partType);
                //byte partTypeId = res.Content.ReadAsAsync<byte>().Result;

                var res = APIHandeling.getData
                   ("Export_Constrains_API?ShortName_ID=" + ShortName_ID + "&catId="+ catId + "&constrainType=" + constrainType + "&owner=" + owner);

                var lst = res.Content.ReadAsAsync<CustomCountryConstrain>().Result;

                Session["PlantsConstrain_Rows"] = new List<CustomPlantConstrain_Rows>();
                Session["PlantConRows_Index"] = 1;
                Session["PlantsConstrain_Treatments"] = new List<CustomPlantConstrain_Treatments>();
                Session["PlantConRows_IndexTreatments"] = 1;
                Session["PlantsConstrain_Analysis"] = new List<CustomPlantConstrain_Analysis>();
                Session["PlantConRows_IndexAnalysis"] = 1;
                Session["PlantsConstrain_Ports"] = new List<CustomPlantConstrain_ArrivalPorts>();
                Session["PlantConRows_IndexPorts"] = 1;

                Session["DB_PlantConstrainRows"] = lst;
                Session["DB_PlantConstrainTreatment"] = lst;
                Session["DB_PlantConstrainAnalysis"] = lst;
                Session["DB_PlantConstrainPorts"] = lst;

                return Json(lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "getConstrainsPlants");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        //public JsonResult getConstrainsProduct
        //   (long productId = 0, byte purposeId = 0, byte statusId = 0, int constrainType = 0, int owner = 0)
        //{
        //    try
        //    {
        //        var res = APIHandeling.getData
        //            ("Export_Constrains?productId=" + productId + "&purposeId=" + purposeId + "&statusId=" + statusId + "&constrainType=" + constrainType + "&owner=" + owner);

        //        var lst = res.Content.ReadAsAsync<CustomCountryConstrain>().Result;
        //        Session["ProductConstrain_Rows"] = new List<CustomPlantConstrain_Rows>();
        //        Session["ProductConRows_Index"] = 1;
        //        Session["ProductConstrain_Treatments"] = new List<CustomPlantConstrain_Treatments>();
        //        Session["ProductConRows_IndexTreatments"] = 1;
        //        Session["ProductConstrain_Analysis"] = new List<CustomPlantConstrain_Analysis>();
        //        Session["ProductConRows_IndexAnalysis"] = 1;
        //        Session["ProductConstrain_Ports"] = new List<CustomPlantConstrain_ArrivalPorts>();
        //        Session["ProductConRows_IndexPorts"] = 1;

        //        Session["DB_ProductConstrainRows"] = lst;
        //        Session["DB_ProductConstrainTreatment"] = lst;
        //        Session["DB_ProductConstrainAnalysis"] = lst;
        //        Session["DB_ProductConstrainPorts"] = lst;



        //        return Json(lst, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "getConstrainsProduct");
        //        return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
        //    }
        //}

        //public JsonResult getConstrainsAliveLiable
        //   (long aliveLiableId = 0, byte purposeId = 0, int statusId = 0, int phaseId = 0, int constrainType = 0, int owner = 0)
        //{
        //    try
        //    {
        //        var res = APIHandeling.getData
        //            ("Export_Constrains?aliveLiableId=" + aliveLiableId + "&purposeId=" + purposeId + "&statusId=" + statusId + "&phaseId=" + phaseId + "&constrainType=" + constrainType + "&owner=" + owner);

        //        var lst = res.Content.ReadAsAsync<CustomCountryConstrain>().Result;

        //        Session["AliveConstrain_Rows"] = new List<CustomPlantConstrain_Rows>();
        //        Session["AliveConRows_Index"] = 1;
        //        Session["AliveConstrain_Treatments"] = new List<CustomPlantConstrain_Treatments>();
        //        Session["AliveConRows_IndexTreatments"] = 1;
        //        Session["AliveConstrain_Analysis"] = new List<CustomPlantConstrain_Analysis>();
        //        Session["AliveConRows_IndexAnalysis"] = 1;
        //        Session["AliveConstrain_Ports"] = new List<CustomPlantConstrain_ArrivalPorts>();
        //        Session["AliveConRows_IndexPorts"] = 1;

        //        Session["DB_AliveConstrainRows"] = lst;
        //        Session["DB_AliveConstrainTreatment"] = lst;
        //        Session["DB_AliveConstrainAnalysis"] = lst;
        //        Session["DB_AliveConstrainPorts"] = lst;

        //        return Json(lst, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "getConstrainsAliveLiable");
        //        return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
        //    }
        //}

        //public JsonResult getConstrainsNotAliveLiable
        //   (long notAliveLiableId = 0, byte purposeId = 0, int statusId = 0, int constrainType = 0, int owner = 0)
        //{
        //    try
        //    {
        //        var res = APIHandeling.getData
        //            ("Export_Constrains?notAliveLiableId=" + notAliveLiableId + "&purposeId=" + purposeId + "&statusId=" + statusId + "&constrainType=" + constrainType + "&owner=" + owner);

        //        var lst = res.Content.ReadAsAsync<CustomCountryConstrain>().Result;

        //        Session["NotAliveConstrain_Rows"] = new List<CustomPlantConstrain_Rows>();
        //        Session["NotAliveConRows_Index"] = 1;
        //        Session["NotAliveConstrain_Treatments"] = new List<CustomPlantConstrain_Treatments>();
        //        Session["NotAliveConRows_IndexTreatments"] = 1;
        //        Session["NotAliveConstrain_Analysis"] = new List<CustomPlantConstrain_Analysis>();
        //        Session["NotAliveConRows_IndexAnalysis"] = 1;
        //        Session["NotAliveConstrain_Ports"] = new List<CustomPlantConstrain_ArrivalPorts>();
        //        Session["NotAliveConRows_IndexPorts"] = 1;

        //        Session["DB_NotAliveConstrainRows"] = lst;
        //        Session["DB_NotAliveConstrainTreatment"] = lst;
        //        Session["DB_NotAliveConstrainAnalysis"] = lst;
        //        Session["DB_NotAliveConstrainPorts"] = lst;

        //        return Json(lst, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "getConstrainsNotAliveLiable");
        //        return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
        //    }
        //}








        ////////// nabila
        public JsonResult postExConstrain
               (EX_CountryConstrainsDTO EX_CountryConstrain
            , List<Ex_CountryConstrain_TextDTO> CountryConstrain_TextDTO
            ,List<Ex_CountryConstrain_AnalysisLabTypeDTO> AnalysisLabType
            ,List<Ex_CountryConstrain_ArrivalPortDTO> ConstraintAirPortInternational)
        {
            try
            {
                EX_CountryConstrain.User_Creation_Id = (short)Session["UserId"];
                EX_CountryConstrain.User_Creation_Date = DateTime.Now;








                ConstrainCountryDTO DTO = new ConstrainCountryDTO();
                DTO.CountryConstrain_TextDTO = CountryConstrain_TextDTO ;
                DTO.CountryConstrainsDTO = EX_CountryConstrain;
                DTO.ConstraintAirPortInternational = ConstraintAirPortInternational;
                DTO.AnalysisLabType = AnalysisLabType;
                APIHandeling.Post("Export_Constrains_API", DTO);

                return Json(  JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "getConstrainsPlants");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }



        //public long ID { get; set; }
        //public Nullable<short> ConstrainOwner_ID { get; set; }
        //public int CountryConstrain_Type { get; set; }
        //public Nullable<short> TransportCountry_ID { get; set; }
        //public long Item_ShortName_id { get; set; }
        //public bool IsExport { get; set; }
        //public Nullable<bool> IsStationAccreditation { get; set; }
        //public Nullable<bool> IsFarmAccreditation { get; set; }
        //public Nullable<bool> IsCompanyAccreditation { get; set; }
        //public Nullable<bool> IsAcceppted { get; set; }
        //public bool IsActive { get; set; }



        public JsonResult getPervExCountryConstrain(EX_CountryConstrainsDTO EX_CountryConstrain)
        {
            int ConstrainOwner_ID =(int) EX_CountryConstrain.ConstrainOwner_ID;
            int CountryConstrain_Type = EX_CountryConstrain.CountryConstrain_Type;
            long Item_ShortName_id = EX_CountryConstrain.Item_ShortName_id;
            long ItemCategories_ID = (long)EX_CountryConstrain.ItemCategories_ID;
            bool IsStationAccreditation = (bool)EX_CountryConstrain.IsStationAccreditation;
            bool IsFarmAccreditation = (bool)EX_CountryConstrain.IsFarmAccreditation;
            bool IsCompanyAccreditation = (bool)EX_CountryConstrain.IsCompanyAccreditation;


            var res = APIHandeling.getData("Export_Constrains_API?ConstrainOwner_ID=" + ConstrainOwner_ID
                + "&countryconstrain_type=" + CountryConstrain_Type
                + "&Item_ShortName_id=" + Item_ShortName_id + "&ItemCategories_ID=" + ItemCategories_ID
              +  "&IsStationAccreditation=" + IsStationAccreditation + "&IsFarmAccreditation=" + IsFarmAccreditation
                + "&IsCompanyAccreditation=" + IsCompanyAccreditation
                ); 
            var lst = res.Content.ReadAsAsync<ExCountryConstainDisplay>().Result;
            return Json(lst, JsonRequestBehavior.AllowGet);

        }
    }
}