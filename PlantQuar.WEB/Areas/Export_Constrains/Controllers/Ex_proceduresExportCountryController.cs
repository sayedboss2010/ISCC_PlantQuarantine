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
    public class Ex_proceduresExportCountryController : Controller
    {
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

        public JsonResult postExConstrain
               (EX_CountryConstrainsDTO EX_CountryConstrain
            , List<Ex_CountryConstrain_TextDTO> CountryConstrain_TextDTO
            , List<Ex_CountryConstrain_AnalysisLabTypeDTO> AnalysisLabType
            , List<Ex_CountryConstrain_ArrivalPortDTO> ConstraintAirPortInternational)
        {
            try
            {
                EX_CountryConstrain.User_Creation_Id = (short)Session["UserId"];
                EX_CountryConstrain.User_Creation_Date = DateTime.Now;








                ConstrainCountryDTO DTO = new ConstrainCountryDTO();
                DTO.CountryConstrain_TextDTO = CountryConstrain_TextDTO;
                EX_CountryConstrain.CountryConstrain_Type = 2;
                DTO.CountryConstrainsDTO = EX_CountryConstrain;
                DTO.ConstraintAirPortInternational = ConstraintAirPortInternational;
                DTO.AnalysisLabType = AnalysisLabType;
                APIHandeling.Post("Export_Constrains_API", DTO);

                return Json(JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "getConstrainsPlants");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        public JsonResult getPervExCountryConstrain(EX_CountryConstrainsDTO EX_CountryConstrain)
        {
            int ConstrainOwner_ID = (int)EX_CountryConstrain.ConstrainOwner_ID;
            long TransportCountry_ID = (long)EX_CountryConstrain.TransportCountry_ID;
            long Item_ShortName_id = EX_CountryConstrain.Item_ShortName_id;
            long ItemCategories_ID = (long)EX_CountryConstrain.ItemCategories_ID;
            bool IsStationAccreditation = (bool)EX_CountryConstrain.IsStationAccreditation;
            bool IsFarmAccreditation = (bool)EX_CountryConstrain.IsFarmAccreditation;
            bool IsCompanyAccreditation = (bool)EX_CountryConstrain.IsCompanyAccreditation;


            var res = APIHandeling.getData("Export_Constrains_API?ConstrainOwner_ID=" + ConstrainOwner_ID
                + "&TransportCountry_ID=" + TransportCountry_ID
                + "&Item_ShortName_id=" + Item_ShortName_id + "&ItemCategories_ID=" + ItemCategories_ID
              + "&IsStationAccreditation=" + IsStationAccreditation + "&IsFarmAccreditation=" + IsFarmAccreditation
                + "&IsCompanyAccreditation=" + IsCompanyAccreditation
                );
            var lst = res.Content.ReadAsAsync<TakenEx_CounstrainDataDTO>().Result;



            return Json(new { Opt=lst.yesExCountryConstainDisplay ,Opt2=lst.noExCountryConstainDisplay},
                JsonRequestBehavior.AllowGet);

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


    }
}