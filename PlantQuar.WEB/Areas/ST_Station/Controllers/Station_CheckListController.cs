using PlantQuar.DTO.DTO.Station;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace PlantQuar.WEB.Areas.ST_Station.Controllers
{
    public class Station_CheckListController : Controller
    {
        // GET: ST_Station/Station_CheckList
        string apiName = "Station_CheckListAPI";
        public ActionResult Index()
        {
            return View();
        }
        //LOAD SEARCH
        public JsonResult listStation_CheckList
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

        public ActionResult Station_CheckListAddEdit(long id = 0)
        {
            
            if (id > 0)
            {
                var model = getStation_CheckListByID(id);
                Session["id"] = model.ID;
                return View(model);
            }
            else
            {
                return View(new Station_CheckListDTO());
            }
        }

        //save
        [HttpPost]
        public ActionResult SaveStation_CheckList(Station_CheckListDTO model)
        {
            User_Session Current = User_Session.GetInstance;
            model.User_Updation_Id = (short)Session["UserId"];
            model.User_Updation_Date = DateTime.Now;
            if (model.ID > 0)
            {
                //edit
                ViewBag.ID = model.ID;
                //model.User_Updation_Id=(short)Session["UserId"];
                //model.User_Updation_Date = DateTime.Now;

                APIHandeling.Put(apiName, model);
                return RedirectToAction("Index", "Station_CheckList", new { area = "ST_Station", id = model.ID });
            }
            else
            {
                //add
                model.User_Creation_Id =(short)Session["UserId"];
                model.User_Creation_Date = DateTime.Now;
                var res = APIHandeling.Post(apiName, model);

                var countryLst = res.Content.ReadAsAsync<Station_CheckListDTO>().Result;//object                                
                model.ID = countryLst.ID;
                return RedirectToAction("Index", "Station_CheckList", new { area = "ST_Station" });
            }

        }

        public ActionResult DeleteStation_CheckList(long id)
        {
            try
            {
                DeleteParameters obj = new DeleteParameters();
                obj.id = id;

                User_Session Current = User_Session.GetInstance;
                obj.Userid = (short)Session["UserId"];
                obj._DateNow = DateTime.Now;

                APIHandeling.Delete(apiName, obj);

                return RedirectToAction("Index", "Station_CheckList", new { area = "ST_Station" });
            }
            catch (Exception ex)
            {
                if (ex.HResult == -2146233087)
                {
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RelatedData) });
                }
                else
                {
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "DeleteStation_CheckList");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        private Station_CheckListDTO getStation_CheckListByID(long id)
        {
            var res = APIHandeling.getData(apiName + "?&Id=" + id);
            return res.Content.ReadAsAsync<Station_CheckListDTO>().Result;
        }

       
    }
}