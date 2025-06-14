using PlantQuar.DTO.DTO.DataEntry.GovToVillage;
using PlantQuar.DTO.DTO.DataEntry.LookUp;
using PlantQuar.DTO.DTO.DataEntry.Outlets;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using PlantQuar.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace PlantQuar.WEB.Areas.DE_Outlets.Controllers
{
    public class OutletController : BaseController
    {
        // GET: DE_Outlets/Outlet
        string apiName = "Outlet_API";

        public ActionResult Index()
        {
            var model = new OutletDTO();
            var res = APIHandeling.getData("ContactType_API?List=1");
            var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
            ViewBag.ContactTypelst = lst.ToList();
            return View(model);
        }


        [HttpGet]
        public ActionResult AllOutletExcell()
        {
            var res = APIHandeling.getData(apiName + "?pageSize=-1&index=-1");

            var lst = res.Content.ReadAsAsync<List<OutletDTO>>().Result;//object

            //var StatusCode = lst.ElementAt(0).Value;
            //var obj = lst.ElementAt(1).Value;

            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        //LOAD SEARCH
        public JsonResult listOutlet
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

                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "listOutlet");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        //NEW
        [HttpPost]
        public JsonResult CreateOutlet(OutletDTO model)
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
                        var dto = data.Content.ReadAsAsync<OutletDTO>().Result;
                        long OutletID = dto.ID;
                        //add in hager contact table
                        var Contacts = model.Contacts.Where(a => a.DeleteCheck != 1).ToList();
                        if (Contacts.Count != 0)
                        {
                            APIHandeling.Post("HagrContact?user_id=" + user_id + "&OutlitAdmin=12&Date_Now=" + Date_Now.ToString("yyyy-MM-dd hh:mm:ss") + "&ContactOwnerID=" + OutletID, Contacts);
                        }
                        //add in hager center_outlet table
                        //            model.CenterID.Remove(0);
                        //            var centers = model.CenterID;

                        //            if (centers.Count != 0)
                        //            {
                        //                APIHandeling.Post("Center_Outlet?user_id=" + user_id + "&Date_Now=" + Date_Now.ToString("yyyy-MM-dd hh:mm:ss") + "&Outlet_ID=" + OutletID, centers);
                        //            }

                    }


                    //return Json(new { Result = "OK", Record = model });
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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "CreateOutlet");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        //UPDATE
        [HttpPost]
        public JsonResult UpdateOutlet(OutletDTO model)
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
                    if ((int)data.StatusCode != 409)
                    {
                        if (model.Contacts.Count != 0)
                        {
                            var res = APIHandeling.Put("HagrContact?user_id=" + user_id + "&OutlitAdmin=12&Date_Now=" + Date_Now.ToString("yyyy-MM-dd hh:mm:ss") + "&ContactOwnerID=" + model.ID, model.Contacts);
                        }
                        //update in hager center_outlet table
                        //model.CenterID.Remove(0);
                        //var centers = model.CenterID;

                        //if (centers.Count != 0)
                        //{
                        //    APIHandeling.Put("Center_Outlet?user_id=" + user_id + "&Date_Now=" + Date_Now.ToString("yyyy-MM-dd hh:mm:ss") + "&Outlet_ID=" + model.ID, centers);
                        //}
                        return Json(new { Result = "OK" });
                    }
                    else
                    {
                        return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });
                    }
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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "UpdateOutlet");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }
        //public JsonResult CreateOutlet(OutletDTO model)
        //{
        //    try
        //    {
        //        var allErrors = ModelState.Values.SelectMany(x => x.Errors).ToList();

        //        if (ModelState.IsValid)
        //        {
        //            User_Session Current = User_Session.GetInstance;

        //            short user_id = (short)Session["UserId"];
        //            DateTime Date_Now = DateTime.Now;
        //            model.User_Creation_Id = user_id;
        //            model.User_Creation_Date = Date_Now;

        //            var data = APIHandeling.Post(apiName, model);
        //            var dto = data.Content.ReadAsAsync<OutletDTO>().Result;
        //            long OutletID = dto.ID;
        //            //add in hager contact table
        //            var Contacts = model.Contacts.Where(a => a.DeleteCheck != 1).ToList();
        //            if (Contacts.Count != 0)
        //            {
        //                APIHandeling.Post("HagrContact?user_id=" + user_id + "&OutlitAdmin=12&Date_Now=" + Date_Now.ToString("yyyy-MM-dd hh:mm:ss") + "&ContactOwnerID=" + OutletID, Contacts);
        //            }
        //            //add in hager center_outlet table
        //            model.CenterID.Remove(0);
        //            var centers = model.CenterID;

        //            if (centers.Count != 0)
        //            {
        //                APIHandeling.Post("Center_Outlet?user_id=" + user_id + "&Date_Now=" + Date_Now.ToString("yyyy-MM-dd hh:mm:ss") + "&Outlet_ID=" + OutletID, centers);
        //            }
        //            return Json(new { Result = "OK", Record = model });
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
        //             APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "CreateOutlet");
        //            return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
        //        }
        //    }
        //}

        ////UPDATE
        //[HttpPost]
        //public JsonResult UpdateOutlet(OutletDTO model)
        //{
        //    try
        //    {
        //        var allErrors = ModelState.Values.SelectMany(x => x.Errors).ToList();
        //        if (ModelState.IsValid)
        //        {
        //            User_Session Current = User_Session.GetInstance;
        //            short user_id = (short)Session["UserId"];
        //            DateTime Date_Now = DateTime.Now;
        //            model.User_Updation_Id = user_id;
        //            model.User_Updation_Date = Date_Now;
        //            var data = APIHandeling.Put(apiName, model);
        //            if ((int)data.StatusCode != 409)
        //            {
        //                if (model.Contacts.Count != 0)
        //                {
        //                    var res = APIHandeling.Put("HagrContact?user_id=" + user_id + "&OutlitAdmin=12&Date_Now=" + Date_Now.ToString("yyyy-MM-dd hh:mm:ss") + "&ContactOwnerID=" + model.ID, model.Contacts);
        //                }
        //                //update in hager center_outlet table
        //                model.CenterID.Remove(0);
        //                var centers = model.CenterID;

        //                //if (centers.Count != 0)
        //                //{
        //                //    APIHandeling.Put("Center_Outlet?user_id=" + user_id + "&Date_Now=" + Date_Now.ToString("yyyy-MM-dd hh:mm:ss") + "&Outlet_ID=" + model.ID, centers);
        //                //}
        //                return Json(new { Result = "OK" });
        //            }
        //            else
        //            {
        //                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
        //            }
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
        //             APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "UpdateOutlet");
        //            return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
        //        }
        //    }
        //}

        //DELETE
        [HttpPost]
        public JsonResult DeleteOutlet(long ID)
        {
            try
            {
                DeleteParameters obj = new DeleteParameters();
                obj.id = ID;
                User_Session Current = User_Session.GetInstance;

                obj.Userid = (short)Session["UserId"];
                obj._DateNow = DateTime.Now;//.ToString("yyyy-MM-dd hh:mm:ss");
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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "DeleteOutlet");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }
        public JsonResult GetContactList(int OutletId)
        {
            try
            {
                Dictionary<string, string> dicData = new Dictionary<string, string>();
                var res = APIHandeling.getDataByParamter("HagrContact", dicData);
                var datacontacts = res.Content.ReadAsAsync<List<HagrContactDTO>>().Result;
                res = APIHandeling.getDataByParamter("ContactType", dicData);
                var datacontacttype = res.Content.ReadAsAsync<List<ContactTypeDTO>>().Result;
                var data = datacontacts.ToList().Join
                     (datacontacttype.ToList(),
                          C => C.ContactType_ID,
                          T => T.ID,
                          (C, T) => new { C, T }
                         ).Where(CT => CT.C.User_Deletion_Id == null && CT.T.User_Deletion_Id == null && CT.C.ContactOwnerID == OutletId).
                         Select(CT => new HagrContactDTO
                         {
                             Value = CT.C.Value,
                             ID = CT.C.ID
                         }).ToList();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
        //center outlet
        public JsonResult listOutletCenters
           (long outlet_ID, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = "")
        {
            try
            {
                var res = APIHandeling.getData(apiName + "?outlet_ID=" + outlet_ID + "&pageSize=" + jtPageSize.ToString() + "&index=" + jtStartIndex.ToString() + "&jtSorting=" + jtSorting.ToString());


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

                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "listOutlet");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }
       
        [HttpPost]
        public JsonResult CreateCenterOutlet(Center_OutletDTO model, long outlet_ID)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    
                    User_Session Current = User_Session.GetInstance;
                    short user_id = (short)Session["UserId"];
                    DateTime Date_Now = DateTime.Now;
                    model.User_Updation_Id = user_id;
                    model.User_Updation_Date = Date_Now;
                    model.Outlet_ID = outlet_ID;
                     var data = APIHandeling.Post(apiName+ "?Update=0", model);
                    if ((int)data.StatusCode != 409)
                    {
                        //return Json(data, JsonRequestBehavior.AllowGet);
                        return ((int)data.StatusCode != 409) ? Json(new { Result = "OK", Record = model })
                      : Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });

                    }
                    else
                    {
                        return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });
                    }
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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "UpdateStationActivty");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        ////DELETE
        /// <summary>
        /// 
       [HttpPost]
        public JsonResult DeleteCenterOutlet(short ID)
        {
            try
            {
                Center_OutletDTO obj = new Center_OutletDTO();
                obj.Center_ID = ID;
                obj.Outlet_ID = null;
                var data = APIHandeling.Post(apiName + "?Update=0", obj);
               // var data = APIHandeling.Put(apiName + "?Update=0", obj);

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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "DeleteStationActivty");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }
    

        public JsonResult listOutlet_Gov  (short? Gov_ID)
        {
            try
            {
                var res = APIHandeling.getData("Center_Outlet_API?Gov_ID=" + Gov_ID);
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "listOutlet_Gov");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }

        }
    }
}