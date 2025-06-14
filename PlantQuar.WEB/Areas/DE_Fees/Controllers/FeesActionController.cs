using PlantQuar.DTO.DTO.DataEntry.Fees;
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

namespace PlantQuar.WEB.Areas.DE_Fees.Controllers
{
    public class FeesActionController : BaseController
    {
        // GET: DE_Fees/FeesAction
        string apiName = "FeesAction_API";
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AllFeesActionExcell()
        {
            var res = APIHandeling.getData(apiName + "?pageSize=-1&index=-1");

            var lst = res.Content.ReadAsAsync<List<Fees_ActionDTO>>().Result;//object

            //var StatusCode = lst.ElementAt(0).Value;
            //var obj = lst.ElementAt(1).Value;

            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        //LOAD SEARCH
        public JsonResult listFeesAction
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

        public ActionResult FeesActionAddEdit(long id = 0)
        {
            Fill_Lists();
            var shiftType = APIHandeling.getData("A_SystemCode_API?AddEdit=1&Sysnum=26");
            var ShiftTypes = shiftType.Content.ReadAsAsync<List<CustomOption>>().Result;

            ViewBag.ShiftTypes = ShiftTypes;

            if (id > 0)
            {
                var model = getFeesActionByID(id);
                Session["id"] = model.ID;
                return View(model);
            }
            else
            {
                return View(new Fees_ActionDTO());
            }
        }

        //save
        [HttpPost]
        public ActionResult SaveFeesAction(Fees_ActionDTO model)
        {
            User_Session Current = User_Session.GetInstance;

            if (model.ID > 0)
            {
                //edit
                ViewBag.ID = model.ID;
                model.User_Updation_Id=(short)Session["UserId"];
                model.User_Updation_Date = DateTime.Now;

                APIHandeling.Put(apiName, model);
                return RedirectToAction("Index", "FeesAction", new { area = "DE_Fees"/*, id = model.ID*/ });
            }
            else
            {
                //add
                model.User_Creation_Id =(short)Session["UserId"];
                model.User_Creation_Date = DateTime.Now;
                var res = APIHandeling.Post(apiName, model);

                var countryLst = res.Content.ReadAsAsync<Fees_ActionDTO>().Result;//object                                
                model.ID = countryLst.ID;
                return RedirectToAction("Index", "FeesAction", new { area = "DE_Fees" });
            }

        }

        public ActionResult DeleteFeesAction(long id)
        {
            try
            {
                DeleteParameters obj = new DeleteParameters();
                obj.id = id;

                User_Session Current = User_Session.GetInstance;
                obj.Userid = (short)Session["UserId"];
                obj._DateNow = DateTime.Now;

                APIHandeling.Delete(apiName, obj);

                return RedirectToAction("Index", "FeesAction", new { area = "DE_Fees" });
            }
            catch (Exception ex)
            {
                if (ex.HResult == -2146233087)
                {
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RelatedData) });
                }
                else
                {
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "DeleteFeesAction");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        private Fees_ActionDTO getFeesActionByID(long id)
        {
            var res = APIHandeling.getData(apiName + "?details=1&Id=" + id);
            return res.Content.ReadAsAsync<Fees_ActionDTO>().Result;
        }

        public void Fill_Lists()
        {
            try
            {
                var Fees_Process = APIHandeling.getData(apiName + "?process=1");
                ViewBag.Fees_Process = Fees_Process.Content.ReadAsAsync<List<CustomOption>>().Result;

                var Fees_Type = APIHandeling.getData(apiName + "?type=1");
                ViewBag.Fees_Type = Fees_Type.Content.ReadAsAsync<List<CustomOption>>().Result;
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Fill_Lists");
            }
        }

        public JsonResult GetFeesTypeActionByFeesProcessId(int Fees_Process_ID)
        {
            try
            {
                var Fees_Process = APIHandeling.getData(apiName + "?Fees_Process_ID=" + Fees_Process_ID);
                var lst = Fees_Process.Content.ReadAsAsync<List<CustomOption>>().Result;
                ViewBag.Fees_Process = lst;
                return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetFeesTypeActionByFeesProcessId");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }

        }

        public JsonResult GetItems()
        {
            try
            {
                var Items = APIHandeling.getData(apiName + "?item=1");
                var lst = Items.Content.ReadAsAsync<List<CustomOption>>().Result;
                ViewBag.Fees_Process = lst;
                return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetItems");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }

        }

        public JsonResult GetShiftTiming()
        {
            try
            {
                var ShiftTiming = APIHandeling.getData(apiName + "?shift=1");
                var lst = ShiftTiming.Content.ReadAsAsync<List<CustomOption>>().Result;
                ViewBag.Fees_Process = lst;
                return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetShiftTiming");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }

        }

        public JsonResult GetTreatmentMethods()
        {
            try
            {
                var TreatmentMethods = APIHandeling.getData(apiName + "?treatment=1");
                var lst = TreatmentMethods.Content.ReadAsAsync<List<CustomOption>>().Result;
                ViewBag.Fees_Process = lst;
                return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetTreatmentMethods");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }

        }
    }
}