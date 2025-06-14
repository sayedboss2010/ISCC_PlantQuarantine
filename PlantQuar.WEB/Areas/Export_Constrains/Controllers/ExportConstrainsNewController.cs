using PlantQuar.DTO.DTO.Export_Constrains;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.Export_Constrains.Controllers
{
    public class ExportConstrainsNewController : Controller
    {
        // GET: Export_Constrains/ExportConstrainsNew
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
                //ViewBag.PlantLst
                res = APIHandeling.getData("Plant_API?plant=1");
                ViewBag.PlantLst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;

                //***********************************//
                Session["PlantsConstrain_Rows"] = new List<EX_CustomItemConstrain_Rows>();
                //Session["PlantConRows_Index"] = 1;
                Session["DB_PlantConstrainRows"] = new ExportConstrainsNewDTO();
                //Session["DB_PlantConstrainTreatment"] = new CustomCountryConstrain();
                //Session["PlantsConstrain_Treatments"] = new List<CustomPlantConstrain_Treatments>();
                //Session["PlantConRows_IndexTreatments"] = 1;
                //Session["DB_PlantConstrainAnalysis"] = new CustomCountryConstrain();
                //Session["PlantsConstrain_Analysis"] = new List<CustomPlantConstrain_Analysis>();
                //Session["PlantConRows_IndexAnalysis"] = 1;
                //Session["DB_PlantConstrainPorts"] = new CustomCountryConstrain();
                //Session["PlantsConstrain_Ports"] = new List<CustomPlantConstrain_ArrivalPorts>();
                //Session["PlantConRows_IndexPorts"] = 1;





               // Session["DeletedConstrain"] = new List<DeleteParameters>();
            }

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
                    return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
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
        //
        public JsonResult getConstrainsPlants
     (long ShortName_ID = 0, long catId = 0, int constrainType = 0, int owner = 0)
        {
            try
            {
                //var res = APIHandeling.getData("PlantPart_API?plantPartId=" + partType);
                //byte partTypeId = res.Content.ReadAsAsync<byte>().Result;

                var res = APIHandeling.getData
                   ("ExportConstrainsNew_API?ShortName_ID=" + ShortName_ID + "&catId=" + catId + "&constrainType=" + constrainType + "&owner=" + owner);

                var lst = res.Content.ReadAsAsync<ExportConstrainsNewDTO>().Result;

                Session["PlantsConstrain_Rows"] = new List<EX_CustomItemConstrain_Rows>();
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


        [HttpPost]
            public ActionResult constrainSave( List<ExportConstrainsNewDTO>  countryConstrain)
            {
                ////ask if that check true
                //if (Session["PlantId"] != null)
                //{
                //    var texts = Session["PlantsConstrain_Rows"] as List<EX_CustomItemConstrain_Rows>;
                //    if (texts.Count > 0)
                //    {
                //        plants plantt = new plants();

                //        plantt.Item_ShortName_id = (long)Session["PlantId"];
                //        plantt.ItemCategories_ID = (long)Session["PlantStatusId"];
                //        plantt.IsCertificate_Addtion = (bool)Session["PlantPurposeId"];


                //        plantt.PlantConstrain_Rows = Session["PlantsConstrain_Rows"] as List<EX_CustomItemConstrain_Rows>;
                //        plantt.PlantConstrain_Analysis = Session["PlantsConstrain_Analysis"] as List<CustomPlantConstrain_Analysis>;
                //        plantt.PlantConstrain_ArrivalPorts = Session["PlantsConstrain_Ports"] as List<CustomPlantConstrain_ArrivalPorts>;

                //        plantt.PlantConstrain_Treatments = Session["PlantsConstrain_Treatments"] as List<CustomPlantConstrain_Treatments>;
                //        countryConstrain.plants = plantt;
                //    }

                //}

                //countryConstrain.User_Creation_Id = (short)Session["UserId"];
                //countryConstrain.User_Creation_Date = DateTime.Now;

                //APIHandeling.Post("Export_Constrains_API", countryConstrain);

                //if (Session["DeletedConstrain"] != null)
                //{
                //    var deleteList = Session["DeletedConstrain"] as List<DeleteParameters>;
                //    if (deleteList.Count > 0)
                //    {
                //        APIHandeling.Put("Export_Constrains", deleteList);
                //    }
                //}

                return RedirectToAction("Index", "ExportConstrains", new { area = "Export_Constrains" });
            }
        //*************************************//

        public JsonResult ListItemRows(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            if (Session["DB_PlantConstrainRows"] != null)
            {
                var dbConstrain = Session["DB_PlantConstrainRows"] as ExportConstrainsNewDTO;
                if (dbConstrain != null && dbConstrain.List_ItemConstrain_Rows != null)
                {
                    var plantIndex = int.Parse(Session["PlantConRows_Index"].ToString());

                    List<EX_CustomItemConstrain_Rows> plants = new List<EX_CustomItemConstrain_Rows>();

                    foreach (var item in dbConstrain.List_ItemConstrain_Rows)
                    {
                        item.index = plantIndex;
                        plantIndex++;
                    }
                    plants.AddRange(dbConstrain.List_ItemConstrain_Rows);

                    Session["PlantConRows_Index"] = plantIndex;
                    Session["DB_PlantConstrainRows"] = null;
                    Session["PlantsConstrain_Rows"] = plants;
                }
            }

            var plantsList = Session["PlantsConstrain_Rows"] as List<EX_CustomItemConstrain_Rows>;
            return Json(new { Result = "OK", Records = plantsList.OrderBy(p => p.index), TotalRecordCount = plantsList.Count });
        }
        public JsonResult CreateItemRows(EX_CustomItemConstrain_Rows model)
        {
            model.index = int.Parse(Session["PlantConRows_Index"].ToString());
            //model.PlantId = (long)Session["PlantId"];
            //model.statusId = (byte)Session["PlantStatusId"];
            //model.purposeId = (byte)Session["PlantPurposeId"];
            //model.plantPartId = (byte)Session["PlantPartType"];
            //try { model.PlantCatId = (long)Session["CategoryId"]; } catch { model.PlantCatId = null; }

            //if(model.IsAnalysis_IsTreatment == 1)
            //{
            //    var res = APIHandeling.getData("AnalysisLabType?Id=" + model.AnalysisLabTypeID);
            //    var anaTypeLab = res.Content.ReadAsAsync<AnalysisLabTypeDTO>().Result;

            //    model.AnalysisLab_ID = (int)anaTypeLab.AnalysisLabID;
            //    model.AnalysisType_ID = (int)anaTypeLab.AnalysisTypeID;
            //}

            var plantsList = Session["PlantsConstrain_Rows"] as List<EX_CustomItemConstrain_Rows>;

            var plantRepeat = plantsList.Where(p => (p.ConstrainText_Ar == model.ConstrainText_Ar || p.ConstrainText_En == model.ConstrainText_En)).Count();

            if (plantRepeat == 0)
            {
                plantsList.Add(model);
                Session["PlantsConstrain_Rows"] = plantsList;

                Session["PlantConRows_Index"] = int.Parse(Session["PlantConRows_Index"].ToString()) + 1;
                return Json(new { Result = "OK", Record = plantsList.OrderBy(p => p.index), TotalRecordCount = plantsList.Count });
            }
            else
            {
                return Json(new { Result = "Error", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });
            }
        }
        public JsonResult UpdateItemRows(EX_CustomItemConstrain_Rows model)
        {
            var plantsList = Session["PlantsConstrain_Rows"] as List<EX_CustomItemConstrain_Rows>;
            //model.PlantId = (long)Session["PlantId"];
            //model.statusId = (byte)Session["PlantStatusId"];
            //model.purposeId = (byte)Session["PlantPurposeId"];
            //model.plantPartId = (byte)Session["PlantPartType"];
            //try { model.PlantCatId = (long)Session["CategoryId"]; } catch { model.PlantCatId = null; }

            var plantRepeat = plantsList.Where(p => (p.ConstrainText_Ar == model.ConstrainText_Ar || p.ConstrainText_En == model.ConstrainText_En) && p.index != model.index).Count();

            if (plantRepeat == 0)
            {
                EX_CustomItemConstrain_Rows update = plantsList.Find(p => p.index == model.index || p.Id == model.Id);
                model.countryConstraintext_Id = update.countryConstraintext_Id;
                plantsList.Remove(update);
                plantsList.Add(model);

                Session["PlantsConstrain_Rows"] = plantsList;

                return Json(new { Result = "OK", Record = plantsList.OrderBy(p => p.index) });
            }
            else
            {
                return Json(new { Result = "Error", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });
            }
        }
        public JsonResult DeleteItemRows(int index)
        {
            var plantsList = Session["PlantsConstrain_Rows"] as List<EX_CustomItemConstrain_Rows>;

            EX_CustomItemConstrain_Rows delete = plantsList.Find(p => p.index == index);
            plantsList.Remove(delete);
            Session["PlantsConstrain_Rows"] = plantsList;

            if (delete.Id > 0)
            {
                DeleteParameters obj = new DeleteParameters();
                //for new changes in constrains
                //obj.id = delete.countryConstrain_Id;

                obj.Userid = (short)Session["UserId"];
                obj._DateNow = DateTime.Now;

                var deleteList = Session["DeletedConstrain"] as List<DeleteParameters>;
                deleteList.Add(obj);

                Session["DeletedConstrain"] = deleteList;
            }

            return Json(new { Result = "OK", Record = plantsList.OrderBy(p => p.index) });
        }
    }
    }