using PlantQuar.DTO.DTO.Farm.FarmConstrain;
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

namespace PlantQuar.WEB.Areas.FA_Farm.Controllers
{
    public class Farm_Constrain_TextController : BaseController
    {
        // GET: FA_Farm/Farm_Constrain_Text
        string apiName = "Farm_Constrain_Text_API";
        public ActionResult Index()
        {
            //return View();
            var res = APIHandeling.getData(apiName + "?IndecCheck=1");
            var modelLst = res.Content.ReadAsAsync<List<Farm_Constrain_Text_CheckList_DTO>>().Result;

            //new task
            //var res_Fees_Money = APIHandeling.getData(apiName + "?index=1");
            //var Fees_Money_lst = res_Fees_Money.Content.ReadAsAsync<List<Farm_Constrain_Text_CheckList_DTO>>().Result;   //is CustomOption change with Dto i will created it & wih files related
            //ViewBag.Fees_Money_List = Fees_Money_lst;
            return View(modelLst);
        }
        //LOAD SEARCH
        public ActionResult listFarm_Constrain_Text
        (string txt_AR_BTNSearch = "", string txt_EN_BTNSearch = "", int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = "")
        {
            try
            {

                var res = APIHandeling.getData(apiName + "?arName=" + txt_AR_BTNSearch + "&enName=" + txt_EN_BTNSearch + "&pageSize=" + jtPageSize.ToString() + "&index=" + jtStartIndex.ToString() + "&jtSorting=" + jtSorting.ToString());

                var lst = res.Content.ReadAsAsync<Dictionary<string, object>>().Result;//object
                var modelLst = res.Content.ReadAsAsync<List<Farm_Constrain_Text_CheckList_DTO>>().Result;
                return View(modelLst);
                //var StatusCode = lst.ElementAt(0).Value;
                //var obj = lst.ElementAt(1).Value;

                //JavaScriptSerializer ser = new JavaScriptSerializer();
                //var myObj = ser.Deserialize<Dictionary<string, object>>(obj.ToString());

                //var count = myObj.ElementAt(0).Value;
                //var Lobj = myObj.ElementAt(1).Value;
                // return Json(new { Result = "OK", Records = Lobj, TotalRecordCount = count }, JsonRequestBehavior.AllowGet);
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

        public ActionResult Farm_Constrain_TextAddEdit(long id = 0, string name = "")
        {
            Fill_Lists();

            if (id > 0)
            {

                var model = getFarm_Constrain_TextByID(id, name);
                model.IsUpdated = true;

                Session["id"] = model.ID;
                return View(model);
            }
            else
            {
                return View(new Farm_Constrain_TextDTO());
            }
        }

        //save
        [HttpPost]
        public ActionResult SaveFarm_Constrain_Text(Farm_Constrain_TextDTO model)
        {
            User_Session Current = User_Session.GetInstance;

            if (model.ID > 0)
            {
                //edit
                ViewBag.ID = model.ID;
                model.User_Updation_Id = (short)Session["UserId"];
                model.User_Updation_Date = DateTime.Now;
                model.IsUpdated = true;
                APIHandeling.Put(apiName, model);
                return RedirectToAction("Index", "Farm_Constrain_Text", new { area = "FA_Farm", id = model.ID });
            }
            else
            {
                //add
                model.User_Creation_Id = (short)Session["UserId"];
                model.User_Creation_Date = DateTime.Now;
                var res = APIHandeling.Post(apiName, model);

                var countryLst = res.Content.ReadAsAsync<Farm_Constrain_TextDTO>().Result;//object                                
                //model.ID = countryLst.ID;
                return RedirectToAction("Index", "Farm_Constrain_Text", new { area = "FA_Farm" });
            }

        }

        //public ActionResult DeleteFarm_Constrain_Text(long id)
        //{
        //    try
        //    {
        //        DeleteParameters obj = new DeleteParameters();
        //        obj.id = id;

        //        User_Session Current = User_Session.GetInstance;
        //        obj.Userid = (short)Session["UserId"];
        //        obj._DateNow = DateTime.Now;

        //        APIHandeling.Delete(apiName, obj);

        //        return RedirectToAction("Index", "Farm_Constrain_Text", new { area = "FA_Farm" });
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex.HResult == -2146233087)
        //        {
        //            return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RelatedData) });
        //        }
        //        else
        //        {
        //            APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "DeleteFarm_Constrain_Text");
        //            return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
        //        }
        //    }
        //}
        public ActionResult DeleteFarm_Constrain_Text(long id, string constrain)
        {
            try
            {
                Farm_Constrain_Text_CheckList_DTO obj = new Farm_Constrain_Text_CheckList_DTO();
                obj.ID = id;
                obj.Constrain_Type = constrain;
                // User_Session Current = User_Session.GetInstance;
                obj.User_Deletion_Id = (short)Session["UserId"];
                obj.User_Deletion_Date = DateTime.Now;
                //obj.User_Creation_Id = -1;
                obj.IsDelete = true;
                APIHandeling.Delete(apiName + "?delete=1&", obj);

                return RedirectToAction("Index", "Farm_Constrain_Text", new { area = "FA_Farm" });
            }
            catch (Exception ex)
            {
                if (ex.HResult == -2146233087)
                {
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RelatedData) });
                }
                else
                {
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "DeleteFarm_Constrain_Text");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }
        private Farm_Constrain_TextDTO getFarm_Constrain_TextByID(long id, string Constrain_Type)
        {
            var res = APIHandeling.getData(apiName + "?details=1&Id=" + id + "&Constrain_Type=" + Constrain_Type);
            return res.Content.ReadAsAsync<Farm_Constrain_TextDTO>().Result;
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
    }
}