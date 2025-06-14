using PlantQuar.DTO.DTO.DataEntry.Items.ItemData;
using PlantQuar.DTO.DTO.Import.DataEntry;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.API;
using PlantQuar.WEB.App_Start;
using PlantQuar.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace PlantQuar.WEB.Areas.DE_Import.Controllers
{
    public class Im_InitiatorsController : BaseController
    {
        string apiName = "Im_Initiator_API";
        /// <summary>
        /// sayedPC
        /// </summary>
        /// <returns></returns>
        // GET: DE_Import/Im_Initiators
        public ActionResult Index()
        {
            return View();
        }

        //GetShortNameDetails
        public JsonResult GetShortNameDetails(int ShortName = 0)
        {
            try
            {
                var res = APIHandeling.getData("Item_ShortName_API?ShortName=" + ShortName);
                var dto = res.Content.ReadAsAsync<Item_ShortNameDTO>().Result;
                return Json(new
                {
                    Result = "OK",
                    purpose = dto.Purpose_Name,
                    subpart = dto.SubPart_Name,
                    status = dto.Status_Name
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetShortNameDetails");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        public JsonResult GetQualitiveGrp()
        {
            try
            {
                var res = APIHandeling.getData("Item_ShortName_API?qualG=1");
                var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetQualitiveGrp");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        public JsonResult GetInitiatorStatus()
        {
            try
            {
                var res = APIHandeling.getData("Im_Initiator_API?Syscode=16");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetInitiatorStatus");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        //************************************************//
        public JsonResult ListInitiators(long shortName = 0, int qualGrp = 0, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                string apiStr = "Im_Initiator_API?shortNameId=" + shortName+ "&pageSize="+ jtPageSize +"&index="+ jtStartIndex;

                if (qualGrp > 0) apiStr = "Im_Initiator_API?qualGrpId=" + qualGrp + "&pageSize=" + jtPageSize + "&index=" + jtStartIndex;

                var res = APIHandeling.getData(apiStr);
                var lst = res.Content.ReadAsAsync<Dictionary<string, object>>().Result;

                if (shortName == 0) Session["ShortNameId"] = null;
                else Session["ShortNameId"] = shortName;

                if (qualGrp == 0) Session["QualGrpId"] = null;
                else Session["QualGrpId"] = qualGrp;

                var StatusCode = lst.ElementAt(0).Value;
                var obj = lst.ElementAt(1).Value;

                JavaScriptSerializer ser = new JavaScriptSerializer();
                var myObj = ser.Deserialize<Dictionary<string, object>>(obj.ToString());

                var count = myObj.ElementAt(0).Value;
                var Lobj = myObj.ElementAt(1).Value;

                return Json(new { Result = "OK", Records = Lobj, TotalRecordCount = count });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "ListInitiators");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }


        [HttpPost]
        public JsonResult CreateInitiator(byte? itemType, int? familyId, int? groupId, Im_InitiatorDTO model, HttpPostedFileBase Picture1)
        {
            try
            {
                var allErrors = ModelState.Values.SelectMany(x => x.Errors).ToList();

                if (ModelState.IsValid)
                {
                    User_Session Current = User_Session.GetInstance;
                    model.User_Creation_Id = (short)Session["UserId"];
                    model.User_Creation_Date = DateTime.Now;

                    if (Session["ShortNameId"] == null) model.Item_ShortName_ID = null;
                    else model.Item_ShortName_ID = long.Parse(Session["ShortNameId"].ToString());

                    if (Session["QualGrpId"] == null) model.QualitativeGroup_Id = null;
                    else model.QualitativeGroup_Id = short.Parse(Session["QualGrpId"].ToString());

                    ////////////      Image
                    if (Picture1 != null)
                    {
                        FileUpload_SaveDataController fileUpload = new FileUpload_SaveDataController();
                        string _Path = fileUpload.Get_Uplood_Imge("Item_" + model.ID, Picture1, "Import", "Initiator", Request.Url.AbsoluteUri.ToString());

                        model.AttachmentPath = _Path;
                        //var DomainPath = ConfigurationManager.AppSettings["Path_NetworkShare"].ToString();

                        //string fname = "Import_" + "Import_" + model.ID;
                        //// HttpPostedFileBase file;
                        //fname = Path.GetFileName(Picture1.FileName);
                        //NetworkShare.DisconnectFromShare(DomainPath + @"\\plant", true); //Disconnect in case we are currently connected with our credentials;
                        //NetworkShare.ConnectToShare(DomainPath + @"\\plant", "administrator", "asd@123"); //Connect with the new credentials
                        //if (!Directory.Exists(DomainPath + @"\\plant\\Import"))
                        //    Directory.CreateDirectory(DomainPath + @"\\plant\\Import");
                        //string Path_year = Create_Year("\\plant\\Import\\");
                        //Picture1.SaveAs(Path.Combine(DomainPath + Path_year + fname));

                        //model.Picture = Path_year + fname;
                        //model.AttachmentPath = Path_year + fname;
                    }

                    //check Repeated Data
                    var res = APIHandeling.Post(apiName, model);

                    return ((int)res.StatusCode != 409) ? Json(new { Result = "OK", Record = model })
                      : Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });
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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "CreateInitiator");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }




        }
        //[HttpPost]
        //public JsonResult CreateInitiator(Im_InitiatorDTO model)
        //{
        //    try
        //    {
        //        var allErrors = ModelState.Values.SelectMany(x => x.Errors).ToList();

        //        if (ModelState.IsValid)
        //        {
        //            User_Session Current = User_Session.GetInstance;
        //            model.User_Creation_Id =(short)Session["UserId"];
        //            model.User_Creation_Date = DateTime.Now;

        //            if (Session["ShortNameId"] == null) model.Item_ShortName_ID = null;
        //            else model.Item_ShortName_ID = long.Parse(Session["ShortNameId"].ToString());

        //            if (Session["QualGrpId"] == null) model.QualitativeGroup_Id = null;
        //            else model.QualitativeGroup_Id = short.Parse(Session["QualGrpId"].ToString());
        //            //imge
        //            ////////////      Image
        //            if (Picture1 != null)
        //            {

        //                var DomainPath = ConfigurationManager.AppSettings["Path_NetworkShare"].ToString();

        //                string fname = "Import_" + "Import_" + model.ID;
        //                // HttpPostedFileBase file;
        //                fname = Path.GetFileName(Picture1.FileName);
        //                NetworkShare.DisconnectFromShare(DomainPath + @"\\plant", true); //Disconnect in case we are currently connected with our credentials;
        //                NetworkShare.ConnectToShare(DomainPath + @"\\plant", "administrator", "asd@123"); //Connect with the new credentials
        //                if (!Directory.Exists(DomainPath + @"\\plant\\Import"))
        //                    Directory.CreateDirectory(DomainPath + @"\\plant\\Import");
        //                string Path_year = Create_Year("\\plant\\Import\\");
        //                Picture1.SaveAs(Path.Combine(DomainPath + Path_year + fname));

        //                model.Picture = Path_year + fname;
        //                model.AttachmentPath = Path_year + fname;
        //            }

        //            //check Repeated Data
        //            var res = APIHandeling.Post(apiName, model);

        //            return ((int)res.StatusCode != 409) ? Json(new { Result = "OK", Record = model })
        //              : Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });
        //        }
        //        else
        //        {
        //            return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex.HResult == -2146233087)
        //        {
        //            return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RelatedData) });
        //        }
        //        else
        //        {
        //            APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "CreateInitiator");
        //            return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
        //        }
        //    }

        //}

        [HttpPost]
        public JsonResult UpdateInitiator(Im_InitiatorDTO model, HttpPostedFileBase Picture1)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    User_Session Current = User_Session.GetInstance;
                    model.User_Updation_Id=(short)Session["UserId"];
                    model.User_Updation_Date = DateTime.Now;
                    ////////////      Image
                    if (Picture1 != null)
                    {
                        FileUpload_SaveDataController fileUpload = new FileUpload_SaveDataController();
                        string _Path = fileUpload.Get_Uplood_Imge( "Item_" + model.ID, Picture1, "Import", "Initiator", Request.Url.AbsoluteUri.ToString());

                        model.AttachmentPath = _Path;
                        //var DomainPath = ConfigurationManager.AppSettings["Path_NetworkShare"].ToString();

                        //string fname = "Import_" + "Import_" + model.ID;
                        //// HttpPostedFileBase file;
                        //fname = Path.GetFileName(Picture1.FileName);
                        //NetworkShare.DisconnectFromShare(DomainPath + @"\\plant", true); //Disconnect in case we are currently connected with our credentials;
                        //NetworkShare.ConnectToShare(DomainPath + @"\\plant", "administrator", "asd@123"); //Connect with the new credentials
                        //if (!Directory.Exists(DomainPath + @"\\plant\\Import"))
                        //    Directory.CreateDirectory(DomainPath + @"\\plant\\Import");
                        //string Path_year = Create_Year("\\plant\\Import\\");
                        //Picture1.SaveAs(Path.Combine(DomainPath + Path_year + fname));

                        //model.Picture = Path_year + fname;
                        //model.AttachmentPath = Path_year + fname;
                    }
                    //check Repeated Data
                    var res = APIHandeling.Put(apiName, model);
                    return ((int)res.StatusCode != 409) ? Json(new { Result = "OK" })
                      : Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });
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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "UpdateInitiator");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        [HttpPost]
        public JsonResult DeleteInitiator(long ID)
        {
            try
            {
                DeleteParameters obj = new DeleteParameters();
                obj.id = ID;
                User_Session Current = User_Session.GetInstance;
                obj.Userid = (short)Session["UserId"];
                obj._DateNow = DateTime.Now;
                APIHandeling.Delete(apiName, obj);

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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "DeleteInitiator");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        private string Create_Year(string Path_year)
        {
            var DomainPath = ConfigurationManager.AppSettings["Path_NetworkShare"].ToString();
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            if (!Directory.Exists(DomainPath + "\\" + Path_year + "\\" + year))
            {
                Directory.CreateDirectory(DomainPath + "\\" + Path_year + "\\" + year);
            }

            if (!Directory.Exists(DomainPath + "\\" + Path_year + "\\" + year + "\\" + month))
            {
                Directory.CreateDirectory(DomainPath + "\\" + Path_year + "\\" + year + "\\" + month);
            }
            return Path_year + "\\" + year + "\\" + month + "\\";
        }
    }
}