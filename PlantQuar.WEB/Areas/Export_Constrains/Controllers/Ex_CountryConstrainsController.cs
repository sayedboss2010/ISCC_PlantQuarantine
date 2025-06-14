using PlantQuar.DTO.DTO.Export_Constrains;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using PlantQuar.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.Export_Constrains.Controllers
{
    public class Ex_CountryConstrainsController : BaseController
    {
        // GET: Export_Constrains/Ex_CountryConstrains

        string apiName = "Ex_CountryConstrain_API";
        public ActionResult Index()
        {
            Fill_Lists();
            return View();
        }

        private void Fill_Lists()
        {
            try
            {
                //var AnalysisType = APIHandeling.getData(apiName + "?AnalysisType=1");
                //ViewBag.AnalysisType = AnalysisType.Content.ReadAsAsync<List<CustomOption>>().Result;

                var Union = APIHandeling.getData(apiName + "?Union=1");
                ViewBag.Union = Union.Content.ReadAsAsync<List<CustomOption>>().Result;

                var Country = APIHandeling.getData(apiName + "?Country=1");
                ViewBag.Country = Country.Content.ReadAsAsync<List<CustomOption>>().Result;


                //var Item = APIHandeling.getData("Group_API?List=1");
                //ViewBag.ItemList = Item.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                //var group = APIHandeling.getData("Group_API?List=1");
                //ViewBag.groupLst = group.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                //var FamilyLst = APIHandeling.getData("Family_API?List=1");
                //ViewBag.FamilyLst = FamilyLst.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;




                //var res = APIHandeling.getData("Plant_API?In_Item_ShortName=1");
                //ViewBag.PlantLst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;

               
                //var Item = APIHandeling.getData(apiName + "?Item=1");
                //ViewBag.Item = Item.Content.ReadAsAsync<List<CustomOption>>().Result;
             //   var Farm_Constrain_Text = APIHandeling.getData(apiName + "?Text=1");
                //ViewBag.Farm_Constrain_Text = Farm_Constrain_Text.Content.ReadAsAsync<List<CustomOption>>().Result;
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Fill_Lists");
            }
        }

        public JsonResult GetCountriesUnion_Name(int CountriesUnion_Id = 0)
        {
            try
            {
                var Union_Name = APIHandeling.getData(apiName + "?CountriesUnion_Id=" + CountriesUnion_Id);
                var lst = Union_Name.Content.ReadAsAsync<List<CustomOption>>().Result;
                //return Json(lst.ToList(), JsonRequestBehavior.AllowGet);
                //ViewBag.Union_Name = lst;

                return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Country_Name");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }

        }

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
                string SubPart_Name = "", Status_Name="", Purpose_Name="";
                if (data["SubPart_Name"] != null)
                {
                    SubPart_Name = data["SubPart_Name"].ToString();
                } 
                if (data["Status_Name"] != null)
                {
                    Status_Name = data["Status_Name"].ToString();
                }
                if (data["Purpose_Name"] != null)
                {
                    Purpose_Name = data["Purpose_Name"].ToString();
                }
                return Json(new { SubPart_Name = SubPart_Name, Status_Name = Status_Name, Purpose_Name = Purpose_Name }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "plantId_ShortName");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        #endregion

        #region التحاليل


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
        #endregion

        #region النصوص


        public JsonResult Get_EX_Constrain_Text_Jtable(int type)
        {
            try
            {
                var res = APIHandeling.getData("EX_Constrain_Text_API?EX_Constrain_Type_id=" + type);
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetAnalysisTypes");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        public JsonResult Get_EX_Constrain_Type_Jtable(int List = 0)
        {
            try
            {
                var res = APIHandeling.getData("EX_Constrain_Type_API?List=" + List);
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetAnalysisLab");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        public JsonResult Get_EX_Constrain_Text_Detiles(int type)
        {
            try
            {
                var res = APIHandeling.getData("EX_Constrain_Text_API?details=0&&Id=" + type);
                var lst = res.Content.ReadAsAsync<Dictionary<string, string>>().Result;
                //var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { InSide_Certificate_Ar = lst["InSide_Certificate_Ar"].ToString(), InSide_Certificate_En = lst["InSide_Certificate_En"].ToString() }, JsonRequestBehavior.AllowGet);
                //return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetAnalysisTypes");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        #endregion

        public JsonResult postExConstrain
              (Ex_CountryConstrainDTO EX_CountryConstrain
           , List<Ex_CountryConstrain_TextDTO> CountryConstrain_TextDTO
           , List<Ex_CountryConstrain_AnalysisLabTypeDTO> AnalysisLabType
           , List<Ex_CountryConstrain_ArrivalPortDTO> ConstraintAirPortInternational
            ,List<Ex_CountryConstrain_TreatmentDTO> Constraint_Treatment)
        {
            try
            {
                EX_CountryConstrain.User_Creation_Id = (short)Session["UserId"];
                EX_CountryConstrain.User_Creation_Date = DateTime.Now;

                //Ex_CountryConstrainDTO DTO = new ConstrainCountryDTO();
                EX_CountryConstrain.CountryConstrain_TextDTO = CountryConstrain_TextDTO;
                EX_CountryConstrain.AnalysisLabType = AnalysisLabType;
                //DTO.CountryConstrainsDTO = EX_CountryConstrain;
                EX_CountryConstrain.ConstraintAirPortInternational = ConstraintAirPortInternational;
                EX_CountryConstrain.Constraint_Treatment = Constraint_Treatment;

                APIHandeling.Post("Ex_CountryConstrain_API", EX_CountryConstrain);

                return Json(JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "getConstrainsPlants");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        public JsonResult getPervExCountryConstrain(Ex_CountryConstrainDTO EX_CountryConstrain)
        {
            
            int Import_Country_ID = 0;
            if (EX_CountryConstrain.Import_Country_ID != null)
            {
                Import_Country_ID = (int)EX_CountryConstrain.Import_Country_ID;
            }
           
            long Item_ShortName_id = EX_CountryConstrain.Item_ShortName_id;

            long ItemCategories_ID=0;
            if (EX_CountryConstrain.ItemCategories_ID != null)
            {
                 ItemCategories_ID = (long)EX_CountryConstrain.ItemCategories_ID;
            }
            bool IsStationAccreditation = (bool)EX_CountryConstrain.IsStationAccreditation;
            bool IsFarmAccreditation = (bool)EX_CountryConstrain.IsFarmAccreditation;
            bool IsCompanyAccreditation = (bool)EX_CountryConstrain.IsCompanyAccreditation;
            


            short TransportCountry_ID = 0;
            if (EX_CountryConstrain.TransportCountry_ID != null)
            {
                TransportCountry_ID = (short)EX_CountryConstrain.TransportCountry_ID; ;
            }
            //if (EX_CountryConstrain.TransportCountry_ID != null)
            //{
            //    TransportCountry_ID = (short)EX_CountryConstrain.TransportCountry_ID;
            //}
            var res = APIHandeling.getData(apiName+ "?Import_Country_ID=" + Import_Country_ID
                + "&TransportCountry_ID="+ TransportCountry_ID
                + "&Item_ShortName_id=" + Item_ShortName_id 
                + "&ItemCategories_ID=" + ItemCategories_ID
              + "&IsStationAccreditation=" + IsStationAccreditation 
              + "&IsFarmAccreditation=" + IsFarmAccreditation
                + "&IsCompanyAccreditation=" + IsCompanyAccreditation
                );
            var lst = res.Content.ReadAsAsync<Ex_CountryConstrainDTO>().Result;
            if (lst != null)
            {
                return Json(lst, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }

        }

        //Hadeer
        public JsonResult GetScientific_Name(long Item_Id = 0)
        {
            try
            {
                var res = APIHandeling.getData("Item_ShortName_API?ScientificNByIt=1&itemId=" + Item_Id);
                // var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                Session["PlantId"] = Item_Id;
                // return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);

                var data = res.Content.ReadAsAsync<Dictionary<string, string>>().Result;


                return Json(new { Scientific_Name = data["Scientific_Name"].ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "plantId_Scientific_Name");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
    }
}