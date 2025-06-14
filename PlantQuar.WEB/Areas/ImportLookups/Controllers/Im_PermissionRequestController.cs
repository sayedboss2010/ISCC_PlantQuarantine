using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PlantQuar.Web.Controllers;
using System.Net.Http;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.DTO.DTO;
using PlantQuar.Web.App_Start;
using System.Collections;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace PlantQuar.Web.Areas.ImportLookups.Controllers
{
    public class Im_PermissionRequestController : BaseController
    {
        string apiName = "Im_PermissionRequest";
        // GET: ImportLookups/Im_PermissionRequest
        public ActionResult Index()

        {
            //string guid = Guid.NewGuid().ToString();
            //ViewBag.ImPermission_Number = guid;          
            ViewBag.ImporterType_Id = getImporterType();
            ViewBag.company_List = getCompanies();

            ViewBag.country_List = getCountries();
            ViewBag.PublicOrg_List = getPublicOrg();
            ViewBag.personIdType_List = getPersonIdTypes();

            ViewBag.ExportCountry_Id = getExportCountry_Id();
            ViewBag.Shipment_Mean_Id = getShipmentMeans();
            ViewBag.Transport_Mean_Id = getTransportMeans();

            ViewBag.Im_OpertaionType = getIm_OpertaionType();

            ViewBag.PortInternational = new SelectList(getPortInternational(0).Data as IEnumerable, "Value", "DisplayText");
            ViewBag.PortInternational_T = new SelectList(getPortInternational_T().Data as IEnumerable, "Value", "DisplayText");
            ViewBag.PortNational = new SelectList(getPortNational().Data as IEnumerable, "Value", "DisplayText");
            return View();
        }


        [HttpPost]
        public ActionResult checkIm_RequestSave(Custome_ImCheckRequest model)
        {
            try
            {
                var allErrors = ModelState.Values.SelectMany(x => x.Errors).ToList();

                if (ModelState.IsValid)
                {
                    User_Session Current = User_Session.GetInstance;
                    var List_Alive = Session["List_Alive"] as List<Con_Ex_Im_LiableItems_AliveDTO>;
                    var List_NotAlive = Session["List_NotAlive"] as List<Con_Ex_Im_LiableItems_NotAliveDTO>;
                    model.aliveItems = List_Alive;
                    model.NotaliveItems = List_NotAlive;
                    var res = APIHandeling.Post(apiName, model);
                    //var x = model.ID_PermissionItems;
                    return RedirectToAction("Index");

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
                    APIHandeling.Insert_Error( Request.Url.AbsoluteUri.ToString(), ex.Message, "CreateTBLRows");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }


        public ActionResult Im_Plant()
        {
            return PartialView();
        }
        public ActionResult Im_Product()
        {
            return View();
        }
        public ActionResult Live_Organism()
        {
            return View();
        }
        public ActionResult NonLive_Organism()
        {
            return View();
        }
        //LOAD SEARCH
        [HttpPost]
        public JsonResult Mylist
       (string txt_AR_BTNSearch = null, string txt_EN_BTNSearch = null, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                return Json(new { Result = "OK", Records = "", TotalRecordCount = 2 });
            }
            catch (Exception ex)
            {
                if (ex.HResult == -2146233087)
                {
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RelatedData) });
                }
                else
                {
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        //Insert
        [HttpPost]
        public JsonResult CreateRow(object model)
        {
            try
            {
                var allErrors = ModelState.Values.SelectMany(x => x.Errors).ToList();

                if (ModelState.IsValid)
                {
                    //complete code!!!!!!!!!!!!!!!
                    return Json(new { Result = "OK", Record = model });
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
                    // db_Plant.Sp_plant_Error_Insert(Request.Url.AbsoluteUri.ToString(), ex.Message, "CreateProduct");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        //UPDATE
        [HttpPost]
        public JsonResult UpdateRow(object model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Json(new { Result = "OK" });
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
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        //DELETE
        [HttpPost]
        public JsonResult DeleteRow(int ID)
        {
            try
            {
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
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }

        }
        #region Fill Drops
        //***********FILL PAGE DROPS*************//
      

        public SelectList getGovs()
        {
            var res = APIHandeling.getData("Governate?AddEdit=1");
            var govLst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
            return (new SelectList(govLst, "Value", "DisplayText"));
        }
        public SelectList getImporterType()
        {
            var res = APIHandeling.getData("A_SystemCode?AddEdit=1&Sysnum=3");
            var exporterTypes = res.Content.ReadAsAsync<List<CustomOption>>().Result;
            exporterTypes.RemoveAt(0);
            return (new SelectList(exporterTypes, "Value", "DisplayText"));
        }

        //Get Companies
        public SelectList getCompanies()
        {
            var res = APIHandeling.getData("Company_National?AddEdit=1");
            var companyLst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
            return (new SelectList(companyLst, "Value", "DisplayText"));
        }

        //Get Company Addresss
        public JsonResult getCompanyAddress(int companyId)
        {
            var res = APIHandeling.getData("Company_National?Id=" + companyId);
            var companyObj = res.Content.ReadAsAsync<CompanyNationalDTO>().Result;

            return Json(new { Result = "OK", Records = companyObj }, JsonRequestBehavior.AllowGet);
        }
        //Get Organization Addresss
        public JsonResult getPublicOrgAddress(int publicOrgId = 0)
        {
            var res = APIHandeling.getData("Public_Organization?Id=" + publicOrgId);
            var publicOrgObj = res.Content.ReadAsAsync<Public_OrganizationDTO>().Result;

            return Json(new { Result = "OK", Records = publicOrgObj }, JsonRequestBehavior.AllowGet);
        }

        //الجنسيه
        public SelectList getCountries()
        {
            var res = APIHandeling.getData("Country?AddEdit=1");
            var countryLst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
            return (new SelectList(countryLst, "Value", "DisplayText"));
        }

        //نوع الهوية
        public SelectList getPersonIdTypes()
        {
            var res = APIHandeling.getData("A_SystemCode?AddEdit=1&Sysnum=12");
            var personIdTypes = res.Content.ReadAsAsync<List<CustomOption>>().Result;
            return (new SelectList(personIdTypes, "Value", "DisplayText"));
        }

        //هيئه
        public SelectList getPublicOrg()
        {
            var res = APIHandeling.getData("Public_Organization?AddEdit=1");
            var publicOrgLst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
            return (new SelectList(publicOrgLst, "Value", "DisplayText"));
        }


        //Get Im_RequestPort Country
        public SelectList getExportCountry_Id()
        {
            var res = APIHandeling.getData("Country?List=1");
            var countryLst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
            return (new SelectList(countryLst, "Value", "DisplayText"));
        }

        //ميناء الشحن
        public JsonResult getPortInternational(int ExportCountry_Id = 0, bool IsJtable = false)
        {
            var res = APIHandeling.getData("PortInternational?CountryID=" + ExportCountry_Id);
            var portInterLst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
            if (IsJtable)
            {
                return Json(new { Records = portInterLst }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(portInterLst, JsonRequestBehavior.AllowGet);
            }
        }

        //ميناء العبور
        public JsonResult getPortInternational_T(bool IsJtable = false)
        {
            var res = APIHandeling.getData("PortInternational?PortInternational_T=1");
            var portInterLst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
            if (IsJtable)
            {
                return Json(new { Records = portInterLst }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(portInterLst, JsonRequestBehavior.AllowGet);
            }
        }

        //ميناء الوصول
        public JsonResult getPortNational(bool IsJtable = false)
        {
            var res = APIHandeling.getData("PortNational?PortNational=1");
            var portNationalLst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
            if (IsJtable)
            {
                return Json(new { Records = portNationalLst }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(portNationalLst, JsonRequestBehavior.AllowGet);
            }
        }

        //وسيله الشحن
        public SelectList getShipmentMeans()
        {
            var res = APIHandeling.getData("ShipmentMean?List=1");
            var shipmentMeanLst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
            return (new SelectList(shipmentMeanLst, "Value", "DisplayText"));
        }

        //وسيله النقل
        public SelectList getTransportMeans()
        {
            var res = APIHandeling.getData("Transport_Mean?List=1");
            var transMeanLst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
            return (new SelectList(transMeanLst, "Value", "DisplayText"));
        }

        //نوع العملية         
        public SelectList getIm_OpertaionType()
        {
            var res = APIHandeling.getData("Im_OpertaionType?List=1");
            var transMeanLst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
            return (new SelectList(transMeanLst, "Value", "DisplayText"));
        }
        #endregion
    }
}