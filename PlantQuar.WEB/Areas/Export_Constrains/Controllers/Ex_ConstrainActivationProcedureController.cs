
using PlantQuar.DTO.DTO.Export_Constrains;
using PlantQuar.DTO.HelperClasses;

using PlantQuar.WEB.App_Start;
using PlantQuar.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;

namespace PlantQuar.Web.Areas.Export_Constrains.Controllers
{
    public class Ex_ConstrainActivationProcedureController : BaseController
    {
        // GET: Export_Constrains/Ex_ConstrainActivationProcedure
        public ActionResult Index()
        {
            LoadData();
            return View();
        }

        public void LoadData()
        {
            //ViewBag.ConstrainTypeLst
            var res = APIHandeling.getData("A_SystemCode_API?Syscode=1");
            ViewBag.ConstrainTypeLst = res.Content.ReadAsAsync<List<CustomOption>>().Result;

            //ViewBag.CountriesLst
            res = APIHandeling.getData("Country_API?AddEdit=1");
            ViewBag.CountriesLst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;

            //ViewBag.UnionsLst
            res = APIHandeling.getData("Union_API?AddEdit=1");
            ViewBag.UnionsLst = res.Content.ReadAsAsync<List<CustomOption>>().Result;

            //***********************************//
            ////ViewBag.PlantLst
            //res = APIHandeling.getData("Plant_API?plant=1");
            //ViewBag.PlantLst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;

            ////ViewBag.PlantPartLst
            //res = APIHandeling.getData("PlantPart?allowed=true&plantid=0");
            //ViewBag.PlantPartLst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;

            ////ViewBag.PlantCategoryLst
            //res = APIHandeling.getData("PlantCategory?plantId=0");
            //ViewBag.PlantCategoryLst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;

            //***********************************//
            //ViewBag.ProductLst
            res = APIHandeling.getData("Product?plantId=0");
            ViewBag.ProductLst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;

            //***********************************//
            //ViewBag.StatusLst
            res = APIHandeling.getData("ProductStatus?AddEdit=1");
            ViewBag.StatusLst = res.Content.ReadAsAsync<List<CustomOption>>().Result;

            //ViewBag.PurposeLst
            res = APIHandeling.getData("PlantPurpose?AddEdit=1");
            ViewBag.PurposeLst = res.Content.ReadAsAsync<List<CustomOption>>().Result;

            //***********************************//
            //ViewBag.AliveLst
            res = APIHandeling.getData("LiableItems?type=14");
            ViewBag.AliveLst = res.Content.ReadAsAsync<List<CustomOption>>().Result;

            //ViewBag.BiologicalPhaseLst
            res = APIHandeling.getData("Biological_Phase?AddEdit=1");
            ViewBag.BiologicalPhaseLst = res.Content.ReadAsAsync<List<CustomOption>>().Result;

            //ViewBag.AliveStatusLst
            res = APIHandeling.getData("LiableItems_Status?AddEdit=1");
            ViewBag.AliveStatusLst = res.Content.ReadAsAsync<List<CustomOption>>().Result;

            //ViewBag.NotAliveLst
            res = APIHandeling.getData("LiableItems?type=15");
            ViewBag.NotAliveLst = res.Content.ReadAsAsync<List<CustomOption>>().Result;

            //***********************************//
            //Session["ProcedurePlantsConstrain_Rows"] = new List<ProcedureCustomPlantConstrain_Rows>();
            //Session["ProcedurePlantConRows_Index"] = 1;
            //Session["ProcedureDB_PlantConstrainRows"] = new ProcedureCustomCountryConstrain();
            //Session["ProcedureDB_PlantConstrainTreatment"] = new ProcedureCustomCountryConstrain();
            //Session["ProcedurePlantsConstrain_Treatments"] = new List<ProcedureCustomPlantConstrain_Treatments>();
            //Session["ProcedurePlantConRows_IndexTreatments"] = 1;
            //Session["ProcedureDB_PlantConstrainAnalysis"] = new ProcedureCustomCountryConstrain();
            //Session["ProcedurePlantsConstrain_Analysis"] = new List<ProcedureCustomPlantConstrain_Analysis>();
            //Session["ProcedurePlantConRows_IndexAnalysis"] = 1;
            //Session["ProcedureDB_PlantConstrainPorts"] = new ProcedureCustomCountryConstrain();
            //Session["ProcedurePlantsConstrain_Ports"] = new List<ProcedureCustomPlantConstrain_ArrivalPorts>();
            //Session["ProcedurePlantConRows_IndexPorts"] = 1;

            //Session["ProcedureDeletedConstrain"] = new List<DeleteParameters>();
        }

        public JsonResult GetPlantShortName(long plantId = 0, byte purposeId = 0, byte statusId = 0, long partType = 0, long catId = 0)
        {
            try
            {
                var res = APIHandeling.getData("PlantShortName?plantId=" + plantId + "&purposeId=" + purposeId + "&statusId=" + statusId + "&partType=" + partType);
                var data = res.Content.ReadAsAsync<Dictionary<string, string>>().Result;

                Session["ProcedurePlantId"] = plantId;
                Session["ProcedurePlantPurposeId"] = purposeId;
                Session["ProcedurePlantStatusId"] = statusId;

                res = APIHandeling.getData("PlantPart?plantPartId=" + partType);
                Session["ProcedurePlantPartType"] = res.Content.ReadAsAsync<byte>().Result;

                Session["ProcedureCategoryId"] = catId;

                return Json(new { shortName = data["shortName"].ToString(), hsCode = data["HSCODE"].ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "plantId_ShortName");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        //*************************************// 
       
        public JsonResult getConstrainsPlants
            (long plantId = 0, byte purposeId = 0, byte statusId = 0, long partType = 0, long catId = 0,short ownerImportId =0, short ownerTransitId = 0)
        {
            try
            {
                var res = APIHandeling.getData("PlantPart?plantPartId=" + partType);
                byte partTypeId = res.Content.ReadAsAsync<byte>().Result;

                res = APIHandeling.getData
                   ("Export_ConstrainsProcedure?plantId=" + plantId + "&purposeId=" + purposeId + "&statusId=" + statusId + "&partType=" + partTypeId + "&catId=" + catId + "&ownerImportId=" + ownerImportId + "&ownerTransitId=" + ownerTransitId);

                var lst = res.Content.ReadAsAsync<Ex_ConstrainActivationProcedureDTO>().Result;

                Session["ProcedurePlantsConstrain_Rows"] = new List<ProcedureCustomPlantConstrain_Rows>();
                Session["ProcedurePlantConRows_Index"] = 1;
                Session["ProcedurePlantsConstrain_Treatments"] = new List<ProcedureCustomPlantConstrain_Treatments>();
                Session["ProcedurePlantConRows_IndexTreatments"] = 1;
                Session["ProcedurePlantsConstrain_Analysis"] = new List<ProcedureCustomPlantConstrain_Analysis>();
                Session["ProcedurePlantConRows_IndexAnalysis"] = 1;
                Session["ProcedurePlantsConstrain_Ports"] = new List<ProcedureCustomPlantConstrain_ArrivalPorts>();
                Session["ProcedurePlantConRows_IndexPorts"] = 1;

                Session["ProcedureDB_PlantConstrainRows"] = lst;
                Session["ProcedureDB_PlantConstrainTreatment"] = lst;
                Session["ProcedureDB_PlantConstrainAnalysis"] = lst;
                Session["ProcedureDB_PlantConstrainPorts"] = lst;

                return Json(lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "getConstrainsPlants");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
       public ActionResult constrainSave(Ex_ConstrainActivationProcedureDTO countryConstrain)
        {
            //ask if that check true
            if (Session["ProcedurePlantId"] != null)
            {
                var texts = Session["ProcedurePlantsConstrain_Rows"] as List<ProcedureCustomPlantConstrain_Rows>;
                if (texts.Count > 0)
                {
                    Procedureplants plantt = new Procedureplants();

                    plantt.PlantId = (long)Session["ProcedurePlantId"];
                    plantt.statusId = (byte)Session["ProcedurePlantStatusId"];
                    plantt.purposeId = (byte)Session["ProcedurePlantPurposeId"];
                    plantt.plantPartId = (byte)Session["ProcedurePlantPartType"];
                    try { plantt.PlantCatId = (long)Session["ProcedureCategoryId"]; } catch { plantt.PlantCatId = null; }

                    var rows = Session["ProcedurePlantsConstrain_Rows"] as List<ProcedureCustomPlantConstrain_Rows>;

                    plantt.PlantConstrain_Rows = rows.Where(d => d.IsSelected == true).ToList();

                   var anal = Session["ProcedurePlantsConstrain_Analysis"] as List<ProcedureCustomPlantConstrain_Analysis>;
                    plantt.PlantConstrain_Analysis = anal.Where(d => d.IsSelected == true).ToList();

                    var prt = Session["ProcedurePlantsConstrain_Ports"] as List<ProcedureCustomPlantConstrain_ArrivalPorts>;
                    plantt.PlantConstrain_ArrivalPorts = prt.Where(d => d.IsSelected == true).ToList();

                    var trt = Session["ProcedurePlantsConstrain_Treatments"] as List<ProcedureCustomPlantConstrain_Treatments>;
                    plantt.PlantConstrain_Treatments=trt.Where(d => d.IsSelected == true).ToList();

                    countryConstrain.plants = plantt;
                }

            }
           
           
            countryConstrain.User_Creation_Id = (short)Session["UserId"];
            countryConstrain.User_Creation_Date = DateTime.Now;

            APIHandeling.Post("Export_ConstrainsProcedure", countryConstrain);

            if (Session["Export_ConstrainsProcedureDeletedConstrain"] != null)
            {
                var deleteList = Session["Export_ConstrainsProcedureDeletedConstrain"] as List<DeleteParameters>;
                if (deleteList.Count > 0)
                {
                    APIHandeling.Put("Export_Constrains", deleteList);
                }
            }

            return RedirectToAction("Index", "Ex_ConstrainActivationProcedure", new { area = "Export_ConstrainActivationProcedure" });
        }

    }
}