using PlantQuar.DTO.DTO.DataEntry.Outlets;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using PlantQuar.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace PlantQuar.WEB.Areas.DE_Outlets.Controllers
{
    public class GeneralAdminController : BaseController
    {
        // GET: DE_Outlets/GeneralAdmin
        DateTime Date_Now = DateTime.Now;

        string apiName = "GeneralAdmin_API";
        //string apiContextName = "GeneralAdmin";
        public ActionResult Index()
        {
            var model = new GeneralAdminDTO();
            var res = APIHandeling.getData("ContactType_API?List=1");
            var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
            ViewBag.ContactTypelst = lst.ToList();
            return View(model);
        }

        [HttpGet]
        public ActionResult AllGeneralAdminExcell()
        {
            var res = APIHandeling.getData(apiName + "?pageSize=-1&index=-1");

            var lst = res.Content.ReadAsAsync<List<GeneralAdminDTO>>().Result;//object

            //var StatusCode = lst.ElementAt(0).Value;
            //var obj = lst.ElementAt(1).Value;

            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        //LOAD SEARCH
        public JsonResult listGeneral_Admin
           (string txt_AR_BTNSearch = "", string txt_EN_BTNSearch = "", int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = "")
        {
            try
            {
                var res = APIHandeling.getData(apiName + "?arName=" + txt_AR_BTNSearch + "&enName=" + txt_EN_BTNSearch + "&pageSize=" + jtPageSize.ToString() + "&index=" + jtStartIndex.ToString() + "&jtSorting=" + jtSorting.ToString());


                var lst = res.Content.ReadAsAsync<Dictionary<string, object>>().Result;//object

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
                if (ex.HResult == -2146233087)
                {
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RelatedData) });
                }
                else
                {

                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "listGeneral_Admin");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        //NEW
        [HttpPost]
        public JsonResult CreateGeneral_Admin(GeneralAdminDTO model)
        {

            try
            {
                var allErrors = ModelState.Values.SelectMany(x => x.Errors).ToList();

                if (ModelState.IsValid)
                {
                    User_Session Current = User_Session.GetInstance;

                    short user_id = (short)Session["UserId"];
                    DateTime Date_Now = DateTime.Now;
                    model.User_Creation_Id = user_id;
                    model.User_Creation_Date = Date_Now;

                    var data = APIHandeling.Post(apiName, model);
                    if ((int)data.StatusCode != 409)
                    {


                        var dto = data.Content.ReadAsAsync<GeneralAdminDTO>().Result;
                        byte GeneralAdminID = dto.ID;
                        //add in hager contact table
                        var Contacts = model.Contacts.Where(a => a.DeleteCheck != 1);
                        APIHandeling.Post("HagrContact?user_id=" + user_id + "&OutlitAdmin=13&Date_Now=" + Date_Now.ToString("yyyy-MM-dd hh:mm:ss") + "&ContactOwnerID=" + GeneralAdminID, Contacts);
                        return Json(new { Result = "OK", Record = model });
                    }
                    else
                        return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });
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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "CreateGeneral_Admin");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
            // return Json("");
        }

        //UPDATE
        [HttpPost]
        public JsonResult UpdateGeneral_Admin(GeneralAdminDTO model)
        {
            try
            {
                var allErrors = ModelState.Values.SelectMany(x => x.Errors).ToList();
                if (ModelState.IsValid)
                {
                    User_Session Current = User_Session.GetInstance;

                    short user_id = (short)Session["UserId"];
                    DateTime Date_Now = DateTime.Now;
                    model.User_Updation_Id = user_id;
                    model.User_Updation_Date = Date_Now;
                    var data = APIHandeling.Put(apiName, model);
                    // APIHandeling.Put("HagrContact?user_id=" + user_id + "&OutlitAdmin=13&Date_Now=" + Date_Now.ToString("yyyy-MM-dd hh:mm:ss") + "&ContactOwnerID=" + model.ID, model.Contacts);
                    //check Repeated Data
                    var res = APIHandeling.Put("HagrContact?user_id=" + user_id + "&OutlitAdmin=13&Date_Now=" + Date_Now.ToString("yyyy-MM-dd hh:mm:ss") + "&ContactOwnerID=" + model.ID, model.Contacts);
                    return ((int)data.StatusCode != 409) ? Json(new { Result = "OK", Record = model })
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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "UpdateGeneral_Admin");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        //DELETE
        [HttpPost]
        public JsonResult DeleteGeneral_Admin(int ID)
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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "DeleteGeneral_Admin");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }


        public JsonResult GetContactList(long ContactOwnerID)
        {
            try
            {
                /* Dictionary<string, string> dicData = new Dictionary<string, string>();
                 var res = APIHandeling.getDataByParamter("HagrContact", dicData);
                 var datacontacts = res.Content.ReadAsAsync<List<HagrContactDTO>>().Result;
                 res = APIHandeling.getDataByParamter("ContactType", dicData);
                 var datacontacttype = res.Content.ReadAsAsync<List<ContactTypeDTO>>().Result;
                 var data = datacontacts.ToList().Join
                      (datacontacttype.ToList(),
                           C => C.ContactType_ID,
                           T => T.ID,
                           (C, T) => new { C, T }
                          ).Where(CT => CT.C.User_Deletion_Id =="" && CT.T.User_Deletion_Id =="" && CT.C.ContactOwnerID == GeneralAdminId).
                          Select(CT => new HagrContactDTO
                          {
                              Value = CT.C.Value,
                              ID = CT.C.ID
                          }).ToList();*/

                var res = APIHandeling.getData("HagrContact?ContactOwnerID=" + ContactOwnerID+ "&outletadmin=12");
                var data = res.Content.ReadAsAsync<List<HagrContactDTO>>().Result;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult General_Admin_List()
        {
            try
            {
                var res = APIHandeling.getData(apiName + "?List=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "General_Admin_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        [HttpPost]
        public JsonResult General_Admin_AddEDIT()
        {
            try
            {
                var res = APIHandeling.getData(apiName + "?AddEdit=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "General_Admin_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
    }
}