using PlantQuar.DTO.DTO.Import.Constrains;
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

namespace PlantQuar.WEB.Areas.Im_Constrains.Controllers
{
    public class Im_CountryConstrainController : BaseController
    {
        // GET: Im_Constrains/Im_CountryConstrain
        string apiName = "Im_CountryConstrain_API";
        public ActionResult Index(string message)
        {
            ViewBag.message = message;
            return View();
        }

        //LOAD SEARCH
        public JsonResult listIm_CountryConstrains
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
                return Json(new { Result = "OK", Records = Lobj, TotalRecordCount = count }, JsonRequestBehavior.AllowGet);
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
        
        public ActionResult Im_CountryConstrainAddEdit(long id = 0,string message="")
        {
            Fill_Lists();
            ViewBag.message = message;
            if (id > 0)
            {
                var model = getIm_CountryConstrainByID(id);
                Session["id"] = model.ID;
                return View(model);
            }
            else
            {
                return View(new Im_CountryConstrain_TextDTO());
            }
        }

        //save
        [HttpPost]
        public ActionResult SaveIm_CountryConstrain(Im_CountryConstrain_TextDTO model)
        {
            User_Session Current = User_Session.GetInstance;
            var msg = "";
            if (model.ID > 0)
            {
                //edit
                model.User_Updation_Id=(short)Session["UserId"];
                model.User_Updation_Date = DateTime.Now;

                var mynewobj = APIHandeling.Put(apiName, model);
               
                if((int)mynewobj.StatusCode != 409)
                {
                    msg = "تم التعديل ";
                }
                else
                {
                    msg = "هذا السجل موجود من قبل ";
                }
                
                return RedirectToAction("Im_CountryConstrainAddEdit", "Im_CountryConstrain", new { area = "Im_Constrains", id = model.ID, message=msg });
            }
            else
            {
                //add
                model.User_Creation_Id =(short)Session["UserId"];
                model.User_Creation_Date = DateTime.Now;
                var res = APIHandeling.Post(apiName, model);

                var countryLst = res.Content.ReadAsAsync<Im_CountryConstrain_TextDTO>().Result;//object                                
                model.ID = countryLst.ID;
                if ((int)res.StatusCode != 409)
                {
                    msg = "تمت الاضافه";
                }
                else
                {
                    msg = "هذا السجل موجود من قبل ";
                }
               

                return RedirectToAction("Index", "Im_CountryConstrain", new { area = "Im_Constrains" , message =msg});
            }

        }
        
        public ActionResult DeleteIm_CountryConstrains(long id)
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
                return RedirectToAction("Index", "Im_CountryConstrain", new { area = "Im_Constrains" , message =msg});
            }
            catch (Exception ex)
            {
                if (ex.HResult == -2146233087)
                {
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RelatedData) });
                }
                else
                {
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "DeleteIm_CountryConstrains");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }
        
        private Im_CountryConstrain_TextDTO getIm_CountryConstrainByID(long id)
        {
            var res = APIHandeling.getData(apiName + "?details=1&Id=" + id);
            return res.Content.ReadAsAsync<Im_CountryConstrain_TextDTO>().Result;
        }

        public void Fill_Lists()
        {
            try
            {
                var res = APIHandeling.getData(apiName + "?List=1");
                ViewBag.Im_Constrain_Type = res.Content.ReadAsAsync<List<CustomOption>>().Result;

            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Fill_Lists");
            }
        }

    }
}