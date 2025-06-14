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
    public class Farm_ConstrainController : BaseController
    {
        // GET: FA_Farm/Farm_Constrain
        string apiName = "Farm_Constrain_API";
        public ActionResult Index()
        {
            var Union = APIHandeling.getData(apiName + "?Union=1");
            ViewBag.Union = Union.Content.ReadAsAsync<List<CustomOption>>().Result;
            var Country = APIHandeling.getData(apiName + "?Country=1");
            ViewBag.Country = Country.Content.ReadAsAsync<List<CustomOption>>().Result;
            var Item = APIHandeling.getData(apiName + "?Item=1");
            ViewBag.Item = Item.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
            return View();
        }
        //LOAD SEARCH
        public ActionResult Farm_ConstrainAddEdit(long id = 0)
        {
            Fill_Lists();

            if (id > 0)
            {
                var model = getFarm_ConstrainByID(id);
                Session["id"] = model.ID;
                return View(model);
            }
            else
            {
                return View(new Farm_ConstrainDTO());
            }
        }
        public JsonResult listFarm_Constrain
        (short? Country_Id, long? Item_ID,string txt_AR_BTNSearch = "", string txt_EN_BTNSearch = "", int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = "")
        {
            try
            {
                var res = APIHandeling.getData(apiName + "?Country_Id=" + Country_Id + "&Item_ID=" + Item_ID + "&" +
                    "" +
                    "arName=" + txt_AR_BTNSearch + "&enName=" + txt_EN_BTNSearch 
                    + "&pageSize=" + jtPageSize.ToString() + "&index=" + jtStartIndex.ToString() + "&jtSorting=" + jtSorting.ToString());
                var lst = res.Content.ReadAsAsync<Dictionary<string, object>>().Result;//object
                var StatusCode = lst.ElementAt(0).Value;
                var obj = lst.ElementAt(1).Value;

                JavaScriptSerializer ser = new JavaScriptSerializer();
                var myObj = ser.Deserialize<Dictionary<string, object>>(obj.ToString());
                //Item_Name(myObj.Values.);
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



        //save
        [HttpPost]
        public ActionResult SaveFarm_Constrain(Farm_ConstrainDTO model)
        {
            User_Session Current = User_Session.GetInstance;
            model.Is_Preview = true;


            //edit 
            if (model.ID > 0)
            {
                ViewBag.ID = model.ID;

                var constrain = new Farm_ConstrainDTO();
                constrain.ID = model.ID;
                constrain.Item_ID = model.Item_ID;
                constrain.Country_Id = model.Country_Id;
                constrain.Union_Id = model.Union_Id;
                constrain.Farm_Constrain_Text_ID = model.Farm_Constrain_Text_ID;
                constrain.AnalysisType_ID = model.AnalysisType_ID;
                constrain.Is_Preview = model.Is_Preview;
                constrain.IsActive = model.IsActive;
                constrain.AnalysisType_Name = model.AnalysisType_Name;
                constrain.Country_Name = model.Country_Name;
                constrain.Union_Name = model.Union_Name;
                constrain.Farm_Constrain_Text_Name = model.Farm_Constrain_Text_Name;
                constrain.Item_Name = model.Item_Name;

                model.User_Updation_Id=(short)Session["UserId"];
                model.User_Updation_Date = DateTime.Now;

                //check Repeated Data
                var res = APIHandeling.Put(apiName, model);


                return RedirectToAction("Index", "Farm_Constrain", new { area = "FA_Farm", id = model.ID });
            }
            //add
            else
            {
                //add

                ViewBag.ID = model.ID;
                model.User_Creation_Id =(short)Session["UserId"];
                model.User_Creation_Date = DateTime.Now;
                var res2 = APIHandeling.Post(apiName, model);
                //var res = APIHandeling.Put(apiName + "?process=1", model);

                var farm_Constrain = res2.Content.ReadAsAsync<Farm_ConstrainDTO>().Result;//object                                
                if (farm_Constrain != null)
                {
                    model.ID = farm_Constrain.ID;
                }
                return RedirectToAction("Index", "Farm_Constrain", new { area = "FA_Farm" });
            }

        }
        [HttpPost]
        public JsonResult UpdateFarm_Constrain(Farm_ConstrainDTO farm_Constrain)
        {
            User_Session Current = User_Session.GetInstance;
            Session["Admins"] = (short)Session["UserId"];
            var allErrors = ModelState.Values.SelectMany(x => x.Errors).ToList();

            if (ModelState.IsValid)
            {
                var model = new Farm_ConstrainDTO();
                model.ID = farm_Constrain.ID;
                model.Item_ID = farm_Constrain.Item_ID;
                model.Country_Id = farm_Constrain.Country_Id;
                model.Union_Id = farm_Constrain.Union_Id;
                model.Farm_Constrain_Text_ID = farm_Constrain.Farm_Constrain_Text_ID;
                model.AnalysisType_ID = farm_Constrain.AnalysisType_ID;
                model.Is_Preview = farm_Constrain.Is_Preview;
                model.IsActive = farm_Constrain.IsActive;
                model.AnalysisType_Name = farm_Constrain.AnalysisType_Name;
                model.Country_Name = farm_Constrain.Country_Name;
                model.Union_Name = farm_Constrain.Union_Name;
                model.Farm_Constrain_Text_Name = farm_Constrain.Farm_Constrain_Text_Name;
                model.Item_Name = farm_Constrain.Item_Name;

                model.User_Updation_Id=(short)Session["UserId"];
                model.User_Updation_Date = DateTime.Now;

                //check Repeated Data
                var res = APIHandeling.Put(apiName, model);
                return ((int)res.StatusCode != 409) ? Json(new { Result = "OK", Record = model })
                  : Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });
            }
            else
            {
                return Json(new { Result = "Error", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });
            }

           // return Json(new { Result = "Error", Message = "يجب حفظ بيانات المزرعة أولا" });
        }
        public ActionResult DeleteFarm_Constrain(long id)
        {
            try
            {
                DeleteParameters obj = new DeleteParameters();
                obj.id = id;

                User_Session Current = User_Session.GetInstance;
                obj.Userid = (short)Session["UserId"];
                obj._DateNow = DateTime.Now;
                try
                {
                    APIHandeling.Delete(apiName, obj);
                }
                catch (Exception)
                {

                    throw;
                }


                return RedirectToAction("Index", "Farm_Constrain", new { area = "FA_Farm" });
            }
            catch (Exception ex)
            {
                if (ex.HResult == -2146233087)
                {
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RelatedData) });
                }
                else
                {
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "DeleteFarm_Constrain");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        private Farm_ConstrainDTO getFarm_ConstrainByID(long id)
        {
            var res = APIHandeling.getData(apiName + "?details=1&Id=" + id);
            return res.Content.ReadAsAsync<Farm_ConstrainDTO>().Result;
        }

        public void Fill_Lists()
        {
            try
            {
                var AnalysisType = APIHandeling.getData(apiName + "?AnalysisType=1");
                ViewBag.AnalysisType = AnalysisType.Content.ReadAsAsync<List<CustomOption>>().Result;
                
                var Union = APIHandeling.getData(apiName + "?Union=1");
                ViewBag.Union = Union.Content.ReadAsAsync<List<CustomOption>>().Result;
                var Country = APIHandeling.getData(apiName + "?Country=1");
                ViewBag.Country = Country.Content.ReadAsAsync<List<CustomOption>>().Result;
                var Item = APIHandeling.getData(apiName + "?Item=1");
                ViewBag.Item = Item.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                var Farm_Constrain_Text = APIHandeling.getData(apiName + "?Text=1");
                ViewBag.Farm_Constrain_Text = Farm_Constrain_Text.Content.ReadAsAsync<List<CustomOption>>().Result;
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Fill_Lists");
            }
        }

        public JsonResult AnalysisType_Name(int AnalysisType_ID)
        {
            try
            {
                var AnalysisType = APIHandeling.getData(apiName + "?AnalysisType_ID=" + AnalysisType_ID);
                var lst = AnalysisType.Content.ReadAsAsync<List<CustomOption>>().Result;
                ViewBag.AnalysisType = lst;
                return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "AnalysisType_Name");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }

        }
        public JsonResult Country_Name(int Country_ID)
        {
            try
            {
                var Country_Name = APIHandeling.getData(apiName + "?Country_ID=" + Country_ID);
                var lst = Country_Name.Content.ReadAsAsync<List<CustomOption>>().Result;
                ViewBag.Country_Name = lst;
                return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Country_Name");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }

        }
        public JsonResult Union_Name()
        {
            try
            {
                var Union_Name = APIHandeling.getData(apiName + "?Union_ID=1");
                var lst = Union_Name.Content.ReadAsAsync<List<CustomOption>>().Result;
                ViewBag.Union_Name = lst;
                return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Country_Name");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }

        }


        public JsonResult GetCountriesUnion_Name(int CountriesUnion_Id = 0)
        {
            try
            {
                var Union_Name = APIHandeling.getData(apiName + "?CountriesUnion_Id=" + CountriesUnion_Id);
                var lst = Union_Name.Content.ReadAsAsync<List<CustomOption>>().Result;
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
        public JsonResult Farm_Constrain_Text_Name(int Farm_Constrain_Text_ID)
        {
            try
            {
                var Farm_Constrain_Text_Name = APIHandeling.getData(apiName + "?Farm_Constrain_Text_ID=" + Farm_Constrain_Text_ID);
                var lst = Farm_Constrain_Text_Name.Content.ReadAsAsync<List<CustomOption>>().Result;
                ViewBag.Farm_Constrain_Text_Name = lst;
                return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Farm_Constrain_Text_Name");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }

        }
        public string Item_Name(int Item_ID)
        {
            try
            {
                var Item_Name = APIHandeling.getData(apiName + "?Item_ID=" + Item_ID);
                var lst = Item_Name.Content.ReadAsAsync<List<CustomOption>>().Result;
                ViewBag.Item_Name = lst;
                var name = ViewBag.Item_Name;
                return name;
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Item_Name");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) }).ToString();
            }

        }
    }
}