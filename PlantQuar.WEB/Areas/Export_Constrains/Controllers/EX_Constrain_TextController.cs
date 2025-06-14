using PlantQuar.DTO.DTO.Export_Constrains;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using PlantQuar.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace PlantQuar.WEB.Areas.Export_Constrains.Controllers
{
    public class EX_Constrain_TextController : BaseController
    {
        // GET: Export_Constrains/EX_Constrain_Text
        string apiName = "EX_Constrain_Text_API";

        public ActionResult Index(string  message)
        {
            ViewBag.message = message;
            return View();
        }

        //LOAD SEARCH
        public JsonResult listEX_Constrain_Text
        (string txt_AR_BTNSearch = "", string txt_EN_BTNSearch = "", int jtStartIndex = 0
            , int jtPageSize = 0, string jtSorting = "", byte Fill_Lists_Type=0)
        {
            try
            {

                var res = APIHandeling.getData(apiName + "?arName=" + txt_AR_BTNSearch 
                    + "&enName=" + txt_EN_BTNSearch + "&pageSize=" + jtPageSize.ToString() 
                    + "&index=" + jtStartIndex.ToString() + "&jtSorting=" + jtSorting.ToString()+ "&Fill_Lists_Type="+ Fill_Lists_Type);
                var lst = res.Content.ReadAsAsync<List<EX_Constrain_Text_DTO>>().Result;
               
                    return Json(lst, JsonRequestBehavior.AllowGet);
                //    var lst = res.Content.ReadAsAsync<Dictionary<string, object>>().Result;//object
                //var StatusCode = lst.ElementAt(0).Value;
                //var obj = lst.ElementAt(1).Value;

                //JavaScriptSerializer ser = new JavaScriptSerializer();
                //var myObj = ser.Deserialize<Dictionary<string, object>>(obj.ToString());

                //var count = myObj.ElementAt(0).Value;
                //var Lobj = myObj.ElementAt(1).Value;
                //return Json(new { Result = "OK", Records = Lobj, TotalRecordCount = count }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                if (ex.HResult == -2146233087)
                {
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RelatedData) });
                }
                else
                {
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "listCenter");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        public ActionResult EX_ConstrainAddEdit(long id = 0, string message = "")
        {
            Fill_Lists();
            ViewBag.message = message;
            if (id > 0)
            {
                var model = getEX_Constrain_TextByID(id);
                Session["id"] = model.ID;
                return View(model);
            }
            else
            {
                return View(new EX_Constrain_Text_DTO());
            }
        }

        //save
        [HttpPost]
        public ActionResult SaveEX_Constrain_Text(EX_Constrain_Text_DTO model)
        {
            User_Session Current = User_Session.GetInstance;
            var msg = "";
            if (model.ID > 0)
            {
                //edit
                
                model.User_Updation_Id = (short)Session["UserId"];
                model.User_Updation_Date = DateTime.Now;

                var mynewobj = APIHandeling.Put(apiName, model);

                if ((int)mynewobj.StatusCode != 409)
                {
                    msg = "تم التعديل ";
                }
                else
                {
                    msg = "هذا السجل موجود من قبل ";
                }

                return RedirectToAction("EX_ConstrainAddEdit", "EX_Constrain_Text", new { area = "Export_Constrains", id = model.ID, message = msg });
            }
            else
            {
                //add
                model.User_Creation_Id = (short)Session["UserId"];
                model.User_Creation_Date = DateTime.Now;
                var res = APIHandeling.Post(apiName, model);

                var countryLst = res.Content.ReadAsAsync<EX_Constrain_Text_DTO>().Result;//object
                                                                                      
                //model.ID = countryLst.ID;
                if ((int)res.StatusCode != 409)
                {
                    msg = "تمت الاضافه";
                }
                else
                {
                    msg = "هذا السجل موجود من قبل ";
                }


                return RedirectToAction("Index", "EX_Constrain_Text", new { area = "Export_Constrains",  message = msg });
            }

        }

        public ActionResult DeleteEX_Constrain_Text(long id)
        {
            try
            {
                DeleteParameters obj = new DeleteParameters();
                obj.id = id;

                User_Session Current = User_Session.GetInstance;
                obj.Userid = (short)Session["UserId"];
                obj._DateNow = DateTime.Now;
                var msg = "";
                APIHandeling.Delete(apiName, obj);
                msg = "تم الحذف";
                return RedirectToAction("Index", "EX_Constrain_Text", new { area = "Export_Constrains", message = msg });
            }
            catch (Exception ex)
            {
                if (ex.HResult == -2146233087)
                {
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RelatedData) });
                }
                else
                {
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "DeleteEX_Constrain_Text");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        private EX_Constrain_Text_DTO getEX_Constrain_TextByID(long id)
        {
          
            var res = APIHandeling.getData(apiName + "?details=1&Id=" + id);
            var list = res.Content.ReadAsAsync<EX_Constrain_Text_DTO>().Result;
            return list;
        }

        [HttpPost]
        //public JsonResult ItemType_AddEDIT()
        public void Fill_Lists()
        {
            try
            {
                var EX_Constrain_Type = APIHandeling.getData("EX_Constrain_Type_API?List=1");
                ViewBag.EX_Constrain_Type = EX_Constrain_Type.Content.ReadAsAsync<List<CustomOption>>().Result;
                
                
            //    var EX_Constrain_Type = APIHandeling.getData("EX_Constrain_Type_API?List=1");
            //    ViewBag.EX_Constrain_Type = EX_Constrain_Type.Content.ReadAsAsync<List<CustomOption>>().Result;

            //url: '@Url.Action("EX_Constrain_Type_List", "EX_Constrain_Type", new { area = "Export_Constrains" })',
                            
            //                    $('#EX_Constrain_Type_List').empty();

                    //var EX_Constrain_Country_Item = APIHandeling.getData("EX_Constrain_Country_Item_API?List=1");
                    //ViewBag.EX_Constrain_Type = EX_Constrain_Country_Item.Content.ReadAsAsync<List<CustomOption>>().Result;
                    // var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;

                }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Fill_Lists");
                //return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        //public void Fill_Lists2()
        //{
        //    try
        //    {
        //        var res = APIHandeling.getData(apiName + "?List=1");
        //        ViewBag.Im_Constrain_Type = res.Content.ReadAsAsync<List<CustomOption>>().Result;

        //    }
        //    catch (Exception ex)
        //    {
        //        APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Fill_Lists");
        //    }
        //}
    }
}