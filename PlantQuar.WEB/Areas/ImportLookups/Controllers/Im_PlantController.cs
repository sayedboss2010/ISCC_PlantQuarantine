using PlantQuar.DTO.DTO;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using PlantQuar.Web.Controllers;

namespace PlantQuar.Web.Areas.ImportLookups.Controllers
{
    public class Im_PlantController : BaseController
    {
        // GET: ExportRequest/Ex_Plants
        public ActionResult Im_Plant()
        {
            return View();
        }
        //********PLANT DATA
        [HttpPost]
        public JsonResult Plant_List()
        {
            try
            {
                var res = APIHandeling.getData("Plant?plant=1");
                var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Plant_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        [HttpPost]
        public JsonResult PlantPartsList(int plantId = 0)
        {
            try
            {
                var res = APIHandeling.getData("PlantPart?allowed=true&plantid=" + plantId);
                var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "PlantPartsList");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        [HttpPost]
        public JsonResult PlantCategoryList(int plantId = 0)
        {
            try
            {
                var res = APIHandeling.getData("PlantCategory?plantId=" + plantId);
                var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "PlantPartsList");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        public JsonResult GetPlantShortName(long plantId = 0, byte purposeId = 0, byte statusId = 0, byte partType = 0)
        {
            try
            {
                var res = APIHandeling.getData("PlantShortName?plantId=" + plantId + "&purposeId=" + purposeId + "&statusId=" + statusId + "&partType=" + partType);
                var data = res.Content.ReadAsAsync<Dictionary<string, string>>().Result;
                if (data["ExportStatus"] == "True")
                {
                    return Json(new { shortName = data["shortName"].ToString(), hsCode = data["HSCODE"].ToString(), state = 1 }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { shortName = data["shortName"].ToString(), hsCode = data["HSCODE"].ToString(), state = 0 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "plantId_ShortName");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        //*********************************************
        #region Constrains
        public JsonResult GetPlantConstrain
            (int plantId = 0, int purposeId = 0, int statusId = 0, int partType = 0, int importerCountryId = 0, int transitCountryId = 0)
        {
            var res = APIHandeling.getData
                ("Export_CheckRequest?plantId=" + plantId + "&purposeId=" + purposeId + "&statusId=" + statusId + "&partType=" + partType + "&importerCountryId=" + importerCountryId + "&transitCountryId=" + transitCountryId);
            var lst = res.Content.ReadAsAsync<List<Custom_Constrains>>().Result;

            return Json(new { Result = "OK", Records = lst }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Plants
        public JsonResult ListPlantsRows(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            var plantsList = Session["Plants"] as List<Custom_ExPlants>;
            return Json(new { Result = "OK", Records = plantsList.OrderBy(p => p.index), TotalRecordCount = plantsList.Count });
        }
        public JsonResult CreatePlantsRows(Custom_ExPlants model)
        {
            model.index = int.Parse(Session["Plant_Index"].ToString());

            var dd =
              GetPlantShortName(model.Plant_ID, model.Purpose_ID, model.ProductStatus_ID, model.PlantPartType_ID).Data.ToString();
            var data = dd.Split('=');

            model.PlantShortName = data[1].Split(',')[0];
            model.HSCODE = data[2].Split(',')[0];

            var plantsList = Session["Plants"] as List<Custom_ExPlants>;

            var plantRepeat = plantsList.Where(p => p.Plant_ID == model.Plant_ID && p.ProductStatus_ID == model.ProductStatus_ID && p.PlantPartType_ID == model.PlantPartType_ID && p.Purpose_ID == model.Purpose_ID).Count();

            if (plantRepeat == 0)
            {
                plantsList.Add(model);
                Session["Plants"] = plantsList;

                Session["Plant_Index"] = int.Parse(Session["Plant_Index"].ToString()) + 1;
                return Json(new { Result = "OK", Record = plantsList.OrderBy(p => p.index), TotalRecordCount = plantsList.Count });
            }
            else
            {
                return Json(new { Result = "Error", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });
            }
        }
        public JsonResult UpdatePlantsRows(Custom_ExPlants model)
        {
            var plantsList = Session["Plants"] as List<Custom_ExPlants>;

            var plantRepeat = plantsList.Where(p => p.Plant_ID == model.Plant_ID && p.ProductStatus_ID == model.ProductStatus_ID && p.PlantPartType_ID == model.PlantPartType_ID && p.Purpose_ID == model.Purpose_ID && p.index != model.index).Count();

            if (plantRepeat == 0)
            {
                Custom_ExPlants update = plantsList.Find(p => p.index == model.index);
                plantsList.Remove(update);
                plantsList.Add(model);

                Session["Plants"] = plantsList;

                return Json(new { Result = "OK", Record = plantsList.OrderBy(p => p.index) });
            }
            else
            {
                return Json(new { Result = "Error", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });
            }
        }
        public JsonResult DeletePlantsRows(int index)
        {
            var plantsList = Session["Plants"] as List<Custom_ExPlants>;

            Custom_ExPlants delete = plantsList.Find(p => p.index == index);
            plantsList.Remove(delete);
            Session["Plants"] = plantsList;

            return Json(new { Result = "OK", Record = plantsList.OrderBy(p => p.index) });
        }
        //**************************************************//
        //Lots
        public JsonResult ListPlants_LotRows(int indexPlant, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            var plantsList = Session["Plants"] as List<Custom_ExPlants>;
            Custom_ExPlants plant = plantsList.Find(p => p.index == indexPlant);
            var lots = plant.lotData;

            return Json(new { Result = "OK", Records = lots.OrderBy(p => p.LotIndex), TotalRecordCount = lots.Count });
        }
        public JsonResult CreatePlants_LotRows(int indexPlant, LotData model)
        {
            model.LotIndex = int.Parse(Session["PlantLot_Index"].ToString());

            model.Gross_Weight = WeightCalculation.ConvertWeightToKilo(model.Gross_Weight_Ton, model.Gross_Weight_Kilo, model.Gross_Weight_Gram);
            model.Net_Weight = WeightCalculation.ConvertWeightToKilo(model.Net_Weight_Ton, model.Net_Weight_Kilo, model.Net_Weight_Gram);
            model.Package_Weight = WeightCalculation.ConvertWeightToKilo(model.Package_Weight_Ton, model.Package_Weight_Kilo, model.Package_Weight_Gram);

            var plantsList = Session["Plants"] as List<Custom_ExPlants>;
            Custom_ExPlants plant = plantsList.Find(p => p.index == indexPlant);
            var lots = plant.lotData;

            var plantLotRepeat = lots.Where(p => p.Lot_Number == model.Lot_Number).Count();

            if (plantLotRepeat == 0)
            {
                plantsList.Remove(plant);

                lots.Add(model);
                plant.lotData = lots;
                plantsList.Add(plant);

                Session["Plants"] = plantsList;
                Session["PlantLot_Index"] = int.Parse(Session["PlantLot_Index"].ToString()) + 1;

                return Json(new { Result = "OK", Record = lots.OrderBy(p => p.LotIndex), TotalRecordCount = lots.Count });
            }
            else
            {
                return Json(new { Result = "Error", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });
            }
        }
        public JsonResult UpdatePlants_LotRows(int indexPlant, LotData model)
        {
            var plantsList = Session["Plants"] as List<Custom_ExPlants>;
            Custom_ExPlants plant = plantsList.Find(p => p.index == indexPlant);
            var lots = plant.lotData;

            model.Gross_Weight = WeightCalculation.ConvertWeightToKilo(model.Gross_Weight_Ton, model.Gross_Weight_Kilo, model.Gross_Weight_Gram);
            model.Net_Weight = WeightCalculation.ConvertWeightToKilo(model.Net_Weight_Ton, model.Net_Weight_Kilo, model.Net_Weight_Gram);
            model.Package_Weight = WeightCalculation.ConvertWeightToKilo(model.Package_Weight_Ton, model.Package_Weight_Kilo, model.Package_Weight_Gram);

            var plantLotRepeat = lots.Where(p => p.Lot_Number == model.Lot_Number && p.LotIndex != model.LotIndex).Count();

            if (plantLotRepeat == 0)
            {
                plantsList.Remove(plant);

                LotData update = lots.Find(p => p.LotIndex == model.LotIndex);
                lots.Remove(update);
                lots.Add(model);
                plant.lotData = lots;

                plantsList.Add(plant);

                Session["Plants"] = plantsList;

                return Json(new { Result = "OK", Record = lots.OrderBy(p => p.LotIndex) });
            }
            else
            {
                return Json(new { Result = "Error", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });
            }
        }
        public JsonResult DeletePlants_LotRows(int indexPlant, LotData model)
        {
            var plantsList = Session["Plants"] as List<Custom_ExPlants>;
            Custom_ExPlants plant = plantsList.Find(p => p.index == indexPlant);
            var lots = plant.lotData;

            plantsList.Remove(plant);

            LotData delete = lots.Find(p => p.LotIndex == model.LotIndex);
            lots.Remove(delete);
            plant.lotData = lots;

            plantsList.Add(plant);
            Session["Plants"] = plantsList;

            return Json(new { Result = "OK", Record = lots.OrderBy(p => p.LotIndex) });
        }
        #endregion
    }

}