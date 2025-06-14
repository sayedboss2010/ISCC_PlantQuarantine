using PlantQuar.DTO.DTO.Farm.FarmRequest;
using PlantQuar.DTO.DTO.Station;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.API;
using PlantQuar.WEB.App_Start;
using PlantQuar.WEB.Controllers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.ST_Station.Controllers
{
    public class ExportRequestController : BaseController
    {
        // GET: ST_Station/ExportRequest
        public ActionResult Index()
        {
            Custome_ExCheckRequest model = new Custome_ExCheckRequest();
            model.gov_List = getGovs();
            model.center_List = getCentersDrop(0);
            model.generalAdmin_List = getGeneralAdmin();
            model.outlet_List = new SelectList(getOutlet(0).Data as IEnumerable, "Value", "DisplayText");
            model.country_List = getCountries();
            model.portInternational_List = new SelectList(getPortInternational(0, 0).Data as IEnumerable, "Value", "DisplayText");
            model.portNational_List = new SelectList(getPortNational(0).Data as IEnumerable, "Value", "DisplayText");
            model.shipmentMean_List = getShipmentMeans();
            model.transportationMean_List = getTransportMeans();
            model.company_List = getCompanies();
            model.exporterType_List = getExporterType();
            model.publicOrg_List = getPublicOrg();
            model.personIdType_List = getPersonIdTypes();
            model.station_List = getStations();
            model.IsStation = true;

            model.portType_List = getPortType();

            Session["Plants"] = model.plants = new List<Custom_ExPlants>();
            Session["Plant_Index"] = 1;
            Session["PlantLot_Index"] = 1;

            Session["Products"] = model.products = new List<Custom_ExProducts>();
            Session["Product_Index"] = 1;
            Session["ProductLot_Index"] = 1;

            Session["Alive"] = model.aliveItems = new List<Custom_ExAliveLiableItems>();
            Session["Alive_Index"] = 1;
            Session["AliveLot_Index"] = 1;

            Session["NotAlive"] = model.notAliveItems = new List<Custom_ExNotAliveLiableItems>();
            Session["NotAlive_Index"] = 1;
            Session["NotAliveLot_Index"] = 1;

            Session["Fees"] = model.feesList = new List<Custom_CheckRequest_Fees>();

            Session["Attachments"] = new List<Custom_Attatchment>();
            Session["Attachments_Index"] = 1;

            Session["AttachmentPath_Content"] = new Dictionary<int, HttpPostedFileBase>();

            Session["ImportCompanies"] = new List<Custom_ExImportCompanies>();
            Session["ImportCompanies_Index"] = 1;
            return View(model);
        }
        //***********************************//
        #region Fill Drops
        //***********FILL PAGE DROPS*************//
        public SelectList getGovs()
        {
            var res = APIHandeling.getData("Governate_API?List=1");
            var govLst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
            return (new SelectList(govLst, "Value", "DisplayText"));
        }
        public SelectList getCentersDrop(int govID = 0)
        {
            var res = APIHandeling.getData("Centers_API?GovId=" + govID);
            var centerLst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
            return (new SelectList(centerLst, "Value", "DisplayText"));
        }
        public SelectList getGeneralAdmin()
        {
            var res = APIHandeling.getData("GeneralAdmin_API?AddEdit=1");
            var govLst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
            return (new SelectList(govLst, "Value", "DisplayText"));
        }
        public JsonResult getGeneralAdminJson(int json)
        {
            var res = APIHandeling.getData("GeneralAdmin_API?AddEdit=1");
            var govLst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
            return Json(govLst, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getCentersByGov(int govID = 0)
        {
            var res = APIHandeling.getData("Centers_API?add=1&GovId=" + govID);
            var centerLst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
            return Json(centerLst, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getOutlet(int govID = 0, int generalAdminId = 0)
        {
            var res = APIHandeling.getData("Outlet_API?govID=" + govID + "&generalAdminId=" + generalAdminId);
            var outletLst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
            return Json(outletLst, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getOutletByGeneralAdmin(int generalAdminId = 0)
        {
            var res = APIHandeling.getData("Outlet_API?generalAdminId=" + generalAdminId);
            var outletLst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
            return Json(outletLst, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getOutletByCenter(int centerID = 0, int generalAdminId = 0)
        {
            var res = APIHandeling.getData("Outlet_API?centerID=" + centerID + "&generalAdminId=" + generalAdminId);
            var outletLst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
            return Json(outletLst, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getGeneralAdminByCenter(int centerID = 0)
        {
            var res = APIHandeling.getData("GeneralAdmin_API?centerID=" + centerID);
            var grlAdmin = res.Content.ReadAsAsync<List<CustomOption>>().Result;
            return Json(grlAdmin, JsonRequestBehavior.AllowGet);
        }
        public SelectList getCountries()
        {
            var res = APIHandeling.getData("Country_API?AddEdit=1");
            var countryLst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
            return (new SelectList(countryLst, "Value", "DisplayText"));
        }
        public JsonResult getPortInternational(int countryId = 0, int portType = 0)
        {
            var res = APIHandeling.getData("PortInternational_API?countryID=" + countryId + "&portType=" + portType);
            var portInterLst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
            return Json(portInterLst, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getPortNational(int govID = 0)
        {
            var res = APIHandeling.getData("PortNational_API?govID=" + govID);
            var portNationalLst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
            return Json(portNationalLst, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getPortNationalByOutlet(int outlet = 0)
        {
            var res = APIHandeling.getData("PortNational_API?outlet=" + outlet);
            var portNationalLst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
            return Json(portNationalLst, JsonRequestBehavior.AllowGet);
        }
        public SelectList getShipmentMeans()
        {
            var res = APIHandeling.getData("ShipmentMean_API?AddEdit=1");
            var shipmentMeanLst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
            return (new SelectList(shipmentMeanLst, "Value", "DisplayText"));
        }
        public SelectList getTransportMeans()
        {
            var res = APIHandeling.getData("TransportMean_API?AddEdit=1");
            var transMeanLst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
            return (new SelectList(transMeanLst, "Value", "DisplayText"));
        }
        public SelectList getCompanies()
        {
            var res = APIHandeling.getData("Company_National_API?activityId=76");//تصدير 76
            var companyLst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
            return (new SelectList(companyLst, "Value", "DisplayText"));
        }
        public JsonResult getCompanyAddress(int companyId = 0)
        {
            var res = APIHandeling.getData("Company_National_API?Id=" + companyId);
            var companyObj = res.Content.ReadAsAsync<CompanyNationalDTO>().Result;

            return Json(new { Result = "OK", Records = companyObj }, JsonRequestBehavior.AllowGet);
        }
        public SelectList getExporterType()
        {
            var res = APIHandeling.getData("A_SystemCode_API?AddEdit=1&Sysnum=3");
            var exporterTypes = res.Content.ReadAsAsync<List<CustomOption>>().Result;
            exporterTypes.RemoveAt(0);
            return (new SelectList(exporterTypes, "Value", "DisplayText"));
        }
        public SelectList getPublicOrg()
        {
            var res = APIHandeling.getData("Public_Organization_API?national=true");
            var publicOrgLst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
            return (new SelectList(publicOrgLst, "Value", "DisplayText"));
        }
        public JsonResult getPublicOrgAddress(int publicOrgId = 0)
        {
            var res = APIHandeling.getData("Public_Organization_API?Id=" + publicOrgId);
            var publicOrgObj = res.Content.ReadAsAsync<Public_OrganizationDTO>().Result;

            return Json(new { Result = "OK", Records = publicOrgObj }, JsonRequestBehavior.AllowGet);
        }
        public SelectList getPersonIdTypes()
        {
            var res = APIHandeling.getData("A_SystemCode_API?AddEdit=1&Sysnum=12");
            var personIdTypes = res.Content.ReadAsAsync<List<CustomOption>>().Result;
            return (new SelectList(personIdTypes, "Value", "DisplayText"));
        }

        /*******************************************/
        public SelectList getStations()
        {
            var res = APIHandeling.getData("Station_API?Accridated=1");
            var personIdTypes = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
            return (new SelectList(personIdTypes, "Value", "DisplayText"));
        }
        public SelectList getPortType()
        {
            var res = APIHandeling.getData("PortType_API?AddEdit=1");
            var exporterTypes = res.Content.ReadAsAsync<List<CustomOption>>().Result;
            exporterTypes.RemoveAt(0);
            return (new SelectList(exporterTypes, "Value", "DisplayText"));
        }
        #endregion        
        //***********************************//
        #region Fill Table Drops              
        [HttpPost]
        public JsonResult PackageType_List()
        {
            try
            {
                var res = APIHandeling.getData("PackageType_API?AddEdit=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "PackageType_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        [HttpPost]
        public JsonResult PackageMaterial_List()
        {
            try
            {
                var res = APIHandeling.getData("PackageMaterial_API?AddEdit=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "PackageMaterial_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        [HttpPost]
        public JsonResult getGovernates()
        {
            try
            {
                var res = APIHandeling.getData("Governate_API?List=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "getGovernates");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        [HttpPost]
        public JsonResult getCenters(int govID = 0)
        {
            try
            {
                var res = APIHandeling.getData("Centers_API?GovId=" + govID);
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "getCenters");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        [HttpPost]
        public JsonResult getVillages(int centerID = 0)
        {
            try
            {
                var res = APIHandeling.getData("Village_API?CenterId=" + centerID);
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "getVillages");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        [HttpPost]
        public JsonResult Farms_List(int villageID = 0)
        {
            try
            {
                var res = APIHandeling.getData("FarmData_API?VillageId=" + villageID);
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Farms_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        #endregion              
        //***********************************//
        //SAVE  FORM SUBMIT  
        [HttpPost]
        public ActionResult checkRequestSave(Custome_ExCheckRequest checkReqModel)
        {
            //if (ModelState.IsValid)
            //{
            var filesAttached = Session["Attachments"] as List<Custom_Attatchment>;
            if (filesAttached.Count > 0)
            {
                checkReqModel.plants = Session["Plants"] as List<Custom_ExPlants>;
                checkReqModel.products = Session["Products"] as List<Custom_ExProducts>;
                checkReqModel.aliveItems = Session["Alive"] as List<Custom_ExAliveLiableItems>;
                checkReqModel.notAliveItems = Session["NotAlive"] as List<Custom_ExNotAliveLiableItems>;
                checkReqModel.feesList = Session["Fees"] as List<Custom_CheckRequest_Fees>;
                checkReqModel.ImpCompanies = Session["ImportCompanies"] as List<Custom_ExImportCompanies>;

                Dictionary<int, HttpPostedFileBase> dd = new Dictionary<int, HttpPostedFileBase>();
                dd = Session["AttachmentPath_Content"] as Dictionary<int, HttpPostedFileBase>;
                FileUpload_SaveDataController upload = new FileUpload_SaveDataController();
                HttpPostedFileBase value;
                foreach (var item in filesAttached)
                {
                    if (item.AttachmentPath != null)
                    {
                        dd.TryGetValue(item.Index, out value);
                        item.AttachmentPath = upload.Upload_File_Data(value, "ExportRequest");
                    }
                }

                checkReqModel.filesAttached = filesAttached;
                Session.Clear();
                Session.Abandon();

                //CALL API
              //  User_Session Current = User_Session.GetInstance;
                checkReqModel.User_Creation_Id =(short)Session["UserId"];
                checkReqModel.User_Creation_Date = DateTime.Now;

                var result = APIHandeling.Post("Export_CheckRequest_API", checkReqModel);

                return RedirectToAction("Index", "List_ExportRequests");
            }
            return null;
            //}
            //return null;
        }

        //***************************************//
        public JsonResult getStationCode(int id = 0)
        {
            var res = APIHandeling.getData("Station_API?id=" + id);
            var station = res.Content.ReadAsAsync<StationDTO>().Result;

            return Json(new { code = station.StationCode }, JsonRequestBehavior.AllowGet);
        }
    }
}