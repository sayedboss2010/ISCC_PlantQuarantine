using PlantQuar.DTO.DTO.Export_Constrains;
using PlantQuar.DTO.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.Export_Constrains.Controllers
{
    public class PlantConstrainController : Controller
    {
        // GET: Export_Constrains/PlantConstrain
        public ActionResult Plant_Constrain()
        {
            return View();
        }

        public JsonResult ListPlantsRows(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            if (Session["DB_PlantConstrainRows"] != null)
            {
                var dbConstrain = Session["DB_PlantConstrainRows"] as CustomCountryConstrain;
                if (dbConstrain != null && dbConstrain.plants != null)
                {
                    var plantIndex = int.Parse(Session["PlantConRows_Index"].ToString());

                    List<CustomPlantConstrain_Rows> plants = new List<CustomPlantConstrain_Rows>();

                    foreach (var item in dbConstrain.plants.PlantConstrain_Rows)
                    {
                        item.index = plantIndex;
                        plantIndex++;
                    }
                    plants.AddRange(dbConstrain.plants.PlantConstrain_Rows);

                    Session["PlantConRows_Index"] = plantIndex;
                    Session["DB_PlantConstrainRows"] = null;
                    Session["PlantsConstrain_Rows"] = plants;
                }
            }

            var plantsList = Session["PlantsConstrain_Rows"] as List<CustomPlantConstrain_Rows>;
            return Json(new { Result = "OK", Records = plantsList.OrderBy(p => p.index), TotalRecordCount = plantsList.Count });
        }
        public JsonResult CreatePlantsRows(CustomPlantConstrain_Rows model)
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

            var plantsList = Session["PlantsConstrain_Rows"] as List<CustomPlantConstrain_Rows>;

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
        public JsonResult UpdatePlantsRows(CustomPlantConstrain_Rows model)
        {
            var plantsList = Session["PlantsConstrain_Rows"] as List<CustomPlantConstrain_Rows>;
            //model.PlantId = (long)Session["PlantId"];
            //model.statusId = (byte)Session["PlantStatusId"];
            //model.purposeId = (byte)Session["PlantPurposeId"];
            //model.plantPartId = (byte)Session["PlantPartType"];
            //try { model.PlantCatId = (long)Session["CategoryId"]; } catch { model.PlantCatId = null; }

            var plantRepeat = plantsList.Where(p => (p.ConstrainText_Ar == model.ConstrainText_Ar || p.ConstrainText_En == model.ConstrainText_En) && p.index != model.index).Count();

            if (plantRepeat == 0)
            {
                CustomPlantConstrain_Rows update = plantsList.Find(p => p.index == model.index || p.Id == model.Id);
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
        public JsonResult DeletePlantsRows(int index)
        {
            var plantsList = Session["PlantsConstrain_Rows"] as List<CustomPlantConstrain_Rows>;

            CustomPlantConstrain_Rows delete = plantsList.Find(p => p.index == index);
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
        //treatments plant
        public JsonResult ListPlantsRows_treatments(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            if (Session["DB_PlantConstrainTreatment"] != null)
            {
                var dbConstrain = Session["DB_PlantConstrainTreatment"] as CustomCountryConstrain;
                if (dbConstrain != null && dbConstrain.plants != null)
                {
                    var plantIndextre = int.Parse(Session["PlantConRows_IndexTreatments"].ToString());

                    List<CustomPlantConstrain_Treatments> treatments = new List<CustomPlantConstrain_Treatments>();

                    foreach (var item in dbConstrain.plants.PlantConstrain_Treatments)
                    {
                        item.index = plantIndextre;
                        plantIndextre++;
                    }
                    treatments.AddRange(dbConstrain.plants.PlantConstrain_Treatments);

                    Session["PlantConRows_IndexTreatments"] = plantIndextre;
                    Session["DB_PlantConstrainTreatment"] = null;
                    Session["PlantsConstrain_Treatments"] = treatments;
                }
            }

            var plantsListTreatments = Session["PlantsConstrain_Treatments"] as List<CustomPlantConstrain_Treatments>;
            return Json(new { Result = "OK", Records = plantsListTreatments.OrderBy(p => p.index), TotalRecordCount = plantsListTreatments.Count });
        }
        public JsonResult CreatePlantsRows_treatments(CustomPlantConstrain_Treatments model)
        {
            model.index = int.Parse(Session["PlantConRows_IndexTreatments"].ToString());
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

            var plantsListtr = Session["PlantsConstrain_Treatments"] as List<CustomPlantConstrain_Treatments>;

            var plantRepeattr = plantsListtr.Where(p => p.TreatmentMaterial_ID == model.TreatmentMaterial_ID && p.TreatmentMethod == model.TreatmentMethod && p.TreatmentType_ID == model.TreatmentType_ID).Count();

            if (plantRepeattr == 0)
            {
                plantsListtr.Add(model);
                Session["PlantsConstrain_Treatments"] = plantsListtr;

                Session["PlantConRows_IndexTreatments"] = int.Parse(Session["PlantConRows_IndexTreatments"].ToString()) + 1;
                return Json(new { Result = "OK", Record = plantsListtr.OrderBy(p => p.index), TotalRecordCount = plantsListtr.Count });
            }
            else
            {
                return Json(new { Result = "Error", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });
            }
        }
        public JsonResult UpdatePlantsRows_treatments(CustomPlantConstrain_Treatments model)
        {
            var plantsListtr = Session["PlantsConstrain_Treatments"] as List<CustomPlantConstrain_Treatments>;
            //model.PlantId = (long)Session["PlantId"];
            //model.statusId = (byte)Session["PlantStatusId"];
            //model.purposeId = (byte)Session["PlantPurposeId"];
            //model.plantPartId = (byte)Session["PlantPartType"];
            //try { model.PlantCatId = (long)Session["CategoryId"]; } catch { model.PlantCatId = null; }

            var plantRepeattr = plantsListtr.Where(p => p.TreatmentMaterial_ID == model.TreatmentMaterial_ID && p.TreatmentMethod == model.TreatmentMethod && p.TreatmentType_ID == model.TreatmentType_ID
            && p.TheDose == model.TheDose && p.Exposure_Day == model.Exposure_Day && p.Exposure_Hour == model.Exposure_Hour && p.Exposure_Minute == model.Exposure_Minute && p.index != model.index).Count();

            if (plantRepeattr == 0)
            {
                CustomPlantConstrain_Treatments update = plantsListtr.Find(p => p.index == model.index || p.Id == model.Id);
                model.TreatmentConstrain_ID = update.TreatmentConstrain_ID;
                plantsListtr.Remove(update);
                plantsListtr.Add(model);

                Session["PlantsConstrain_Treatments"] = plantsListtr;

                return Json(new { Result = "OK", Record = plantsListtr.OrderBy(p => p.index) });
            }
            else
            {
                return Json(new { Result = "Error", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });
            }
        }
        public JsonResult DeletePlantsRows_treatments(int index)
        {
            var plantsList = Session["PlantsConstrain_Treatments"] as List<CustomPlantConstrain_Treatments>;

            CustomPlantConstrain_Treatments delete = plantsList.Find(p => p.index == index);
            plantsList.Remove(delete);
            Session["PlantsConstrain_Treatments"] = plantsList;

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
        //plant analysis
        public JsonResult ListPlants_Analysis(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            if (Session["DB_PlantConstrainAnalysis"] != null)
            {
                var dbConstrain = Session["DB_PlantConstrainAnalysis"] as CustomCountryConstrain;
                if (dbConstrain != null && dbConstrain.plants != null)
                {
                    var plantIndextre = int.Parse(Session["PlantConRows_IndexAnalysis"].ToString());

                    List<CustomPlantConstrain_Analysis> Analysis = new List<CustomPlantConstrain_Analysis>();

                    foreach (var item in dbConstrain.plants.PlantConstrain_Analysis)
                    {
                        item.index = plantIndextre;
                        plantIndextre++;
                    }
                    Analysis.AddRange(dbConstrain.plants.PlantConstrain_Analysis);

                    Session["PlantConRows_IndexAnalysis"] = plantIndextre;
                    Session["DB_PlantConstrainAnalysis"] = null;
                    Session["PlantsConstrain_Analysis"] = Analysis;
                }
            }

            var plantsListAnalysis = Session["PlantsConstrain_Analysis"] as List<CustomPlantConstrain_Analysis>;
            return Json(new { Result = "OK", Records = plantsListAnalysis.OrderBy(p => p.index), TotalRecordCount = plantsListAnalysis.Count });
        }
        public JsonResult CreatePlants_Analysis(CustomPlantConstrain_Analysis model)
        {
            model.index = int.Parse(Session["PlantConRows_IndexAnalysis"].ToString());

            //if(model.IsAnalysis_IsTreatment == 1)
            //{
            //    var res = APIHandeling.getData("AnalysisLabType?Id=" + model.AnalysisLabTypeID);
            //    var anaTypeLab = res.Content.ReadAsAsync<AnalysisLabTypeDTO>().Result;

            //    model.AnalysisLab_ID = (int)anaTypeLab.AnalysisLabID;
            //    model.AnalysisType_ID = (int)anaTypeLab.AnalysisTypeID;
            //}
            // analysislabid == analysis lab type id

            var plantsListtr = Session["PlantsConstrain_Analysis"] as List<CustomPlantConstrain_Analysis>;

            var plantRepeattr = plantsListtr.Where(p => p.AnalysisLab_ID == model.AnalysisLab_ID).Count();

            if (plantRepeattr == 0)
            {
                plantsListtr.Add(model);
                Session["PlantsConstrain_Analysis"] = plantsListtr;

                Session["PlantConRows_IndexAnalysis"] = int.Parse(Session["PlantConRows_IndexAnalysis"].ToString()) + 1;
                return Json(new { Result = "OK", Record = plantsListtr.OrderBy(p => p.index), TotalRecordCount = plantsListtr.Count });
            }
            else
            {
                return Json(new { Result = "Error", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });
            }
        }
        public JsonResult UpdatePlants_Analysis(CustomPlantConstrain_Analysis model)
        {
            var plantsListtr = Session["PlantsConstrain_Analysis"] as List<CustomPlantConstrain_Analysis>;

            var plantRepeattr = plantsListtr.Where(p => p.AnalysisLab_ID == model.AnalysisLab_ID && p.index != model.index).Count();

            if (plantRepeattr == 0)
            {
                CustomPlantConstrain_Analysis update = plantsListtr.Find(p => p.index == model.index || p.Id == model.Id);
                model.AnalysisConstrain_ID = update.AnalysisConstrain_ID;
                plantsListtr.Remove(update);
                plantsListtr.Add(model);

                Session["PlantsConstrain_Analysis"] = plantsListtr;

                return Json(new { Result = "OK", Record = plantsListtr.OrderBy(p => p.index) });
            }
            else
            {
                return Json(new { Result = "Error", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });
            }
        }
        public JsonResult DeletePlants_Analysis(int index)
        {
            var plantsList = Session["PlantsConstrain_Analysis"] as List<CustomPlantConstrain_Analysis>;

            CustomPlantConstrain_Analysis delete = plantsList.Find(p => p.index == index);
            plantsList.Remove(delete);
            Session["PlantsConstrain_Analysis"] = plantsList;

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
        //plant ports
        public JsonResult ListPlants_ports(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            if (Session["DB_PlantConstrainPorts"] != null)
            {
                var dbConstrain = Session["DB_PlantConstrainPorts"] as CustomCountryConstrain;
                if (dbConstrain != null && dbConstrain.plants != null)
                {
                    var plantIndextre = int.Parse(Session["PlantConRows_IndexPorts"].ToString());

                    List<CustomPlantConstrain_ArrivalPorts> Ports = new List<CustomPlantConstrain_ArrivalPorts>();

                    foreach (var item in dbConstrain.plants.PlantConstrain_ArrivalPorts)
                    {
                        item.index = plantIndextre;
                        plantIndextre++;
                    }
                    Ports.AddRange(dbConstrain.plants.PlantConstrain_ArrivalPorts);

                    Session["PlantConRows_IndexPorts"] = plantIndextre;
                    Session["DB_PlantConstrainPorts"] = null;
                    Session["PlantsConstrain_Ports"] = Ports;
                }
            }

            var plantsListPorts = Session["PlantsConstrain_Ports"] as List<CustomPlantConstrain_ArrivalPorts>;
            return Json(new { Result = "OK", Records = plantsListPorts.OrderBy(p => p.index), TotalRecordCount = plantsListPorts.Count });
        }
        public JsonResult CreatePlants_ports(CustomPlantConstrain_ArrivalPorts model)
        {
            model.index = int.Parse(Session["PlantConRows_IndexPorts"].ToString());


            var plantsListtr = Session["PlantsConstrain_Ports"] as List<CustomPlantConstrain_ArrivalPorts>;

            var plantRepeattr = plantsListtr.Where(p => p.PortInternationalID == model.PortInternationalID).Count();

            if (plantRepeattr == 0)
            {
                plantsListtr.Add(model);
                Session["PlantsConstrain_Ports"] = plantsListtr;

                Session["PlantConRows_IndexPorts"] = int.Parse(Session["PlantConRows_IndexPorts"].ToString()) + 1;
                return Json(new { Result = "OK", Record = plantsListtr.OrderBy(p => p.index), TotalRecordCount = plantsListtr.Count });
            }
            else
            {
                return Json(new { Result = "Error", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });
            }
        }
        public JsonResult UpdatePlants_ports(CustomPlantConstrain_ArrivalPorts model)
        {
            var plantsListtr = Session["PlantsConstrain_Ports"] as List<CustomPlantConstrain_ArrivalPorts>;

            var plantRepeattr = plantsListtr.Where(p => p.PortInternationalID == model.PortInternationalID && p.index != model.index).Count();

            if (plantRepeattr == 0)
            {
                CustomPlantConstrain_ArrivalPorts update = plantsListtr.Find(p => p.index == model.index || p.Id == model.Id);
                model.ArrivalConstrain_ID = update.ArrivalConstrain_ID;
                plantsListtr.Remove(update);
                plantsListtr.Add(model);

                Session["PlantsConstrain_Ports"] = plantsListtr;

                return Json(new { Result = "OK", Record = plantsListtr.OrderBy(p => p.index) });
            }
            else
            {
                return Json(new { Result = "Error", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });
            }
        }
        public JsonResult DeletePlants_ports(int index)
        {
            var plantsList = Session["PlantsConstrain_Ports"] as List<CustomPlantConstrain_ArrivalPorts>;

            CustomPlantConstrain_ArrivalPorts delete = plantsList.Find(p => p.index == index);
            plantsList.Remove(delete);
            Session["PlantsConstrain_Ports"] = plantsList;

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