using PlantQuar.DTO.DTO.Admin;
using PlantQuar.DTO.DTO.Export_Certificate;
using PlantQuar.DTO.DTO.Export_CheckRequest;
using PlantQuar.DTO.DTO.Export_Constrains;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.Export_Certificate.Controllers
{
    public class ChangeCountryCertificateController : Controller
    {
        // GET: Export_Certificate/ChangeCountryCertificate

        string apiName = "ChangeCountryCertificate_API";
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetCountriesName()
        {
            try
            {
                var Countries_Name = APIHandeling.getData(apiName);
                var lst = Countries_Name.Content.ReadAsAsync<List<CustomOption>>().Result;
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
        public JsonResult GetPortType()
        {
            try
            {
                var PortType = APIHandeling.getData(apiName + "?portType=1");
                var lst = PortType.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "PortType");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }

        }

        public JsonResult GetCountryName(string CheckRequestNumber)
        {
            try
            {
                var res = APIHandeling.getData(apiName + "?CheckRequestNumber=" + CheckRequestNumber);
                var lst = res.Content.ReadAsAsync<List<ChangeCountryCertificateDTO>>().Result;
                return Json(lst, JsonRequestBehavior.AllowGet);
                //if (lst != null)
                //{
                //    return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
                //}
                //else { return Json(new { Result = "Empty" }); }




            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetGeshniCommittee");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }


        public JsonResult GetchangeImportCountry(string CheckRequestNumber, int newImportPortType, int newImportCountryID, int newImportPortID, int currentImportCountryID, int currentImportPortID)
        {
            try
            {
                var User_Id = (short)Session["UserId"];
                var res = APIHandeling.getData(apiName + "?RequestNumber=" + CheckRequestNumber + "&newImportPortType=" + newImportPortType + "&newImportCountryID=" + newImportCountryID + "&newImportPortID=" + newImportPortID + "&currentImportCountryID=" + currentImportCountryID + "&currentImportPortID=" + currentImportPortID + "&User_Id=" + User_Id);
                var lst = res.Content.ReadAsAsync<List<ChangeCountryCertificateDTO>>().Result;

                if (lst != null)
                {
                    return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
                }
                else { return Json(new { Result = "Empty" }); }
                //if (lst.FirstOrDefault().ReqPortType_ID== 9)
                //{
                //    return Json(new { Result = "Import", Options = lst }, JsonRequestBehavior.AllowGet);
                //} else if (lst.FirstOrDefault().ReqPortType_ID== 11)
                //{
                //    return Json(new { Result = "Passenger", Options = lst }, JsonRequestBehavior.AllowGet);
                //}
                //else { return Json(new { Result = "Empty" }); }
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetGeshniCommittee");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        public JsonResult GetchangePassengerCountry(string CheckRequestNumber, int newPassengerPortType, int newPassengerCountryID, int newPassengerPortID, int currentPassengerCountryID, int currentPassengerPortID)
        {
            try
            {
                var User_Id = (short)Session["UserId"];
                var res = APIHandeling.getData(apiName + "?RequestNumber=" + CheckRequestNumber + "&newPassengerPortType=" + newPassengerPortType + "&newPassengerCountryID=" + newPassengerCountryID + "&newPassengerPortID=" + newPassengerPortID + "&currentPassengerCountryID=" + currentPassengerCountryID + "&currentPassengerPortID=" + currentPassengerPortID + "&User_Id=" + User_Id);
                var lst = res.Content.ReadAsAsync<List<ChangeCountryCertificateDTO>>().Result;

                if (lst != null)
                {
                    return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
                }
                else { return Json(new { Result = "Empty" }); }
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetGeshniCommittee");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
    }
}