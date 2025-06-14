using PlantQuar.DTO.DTO.Export_Constrains;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using PlantQuar.WEB.Controllers;
using System.Web.Script.Serialization;
namespace PlantQuar.WEB.Areas.Export_Constrains.Controllers
{
    public class EX_Constrain_Country_ItemController : BaseController
    {
        // GET: Export_Constrains/EX_Constrain_Country_Item
        string apiName = "EX_Constrain_Country_Item_API";
        
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AllEX_Constrain_Country_ItemExcell()
        {
            var res = APIHandeling.getData(apiName + "?pageSize=-1&index=-1");

            var lst = res.Content.ReadAsAsync<List<EX_Constrain_Country_ItemDTO>>().Result;//object

            //var StatusCode = lst.ElementAt(0).Value;
            //var obj = lst.ElementAt(1).Value;

            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        //LOAD SEARCH
        [HttpPost]
        public JsonResult listEX_Constrain_Country_Item
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

                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "listLevel");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        //Create EX_Constrain_Country_Item
        [HttpPost]
        public JsonResult CreateEX_Constrain_Country_Item(EX_Constrain_Country_ItemDTO model)
        {
            try
            {
                var allErrors = ModelState.Values.SelectMany(x => x.Errors).ToList();

                if (ModelState.IsValid)
                {
                    User_Session Current = User_Session.GetInstance;
                    model.User_Creation_Id =(short)Session["UserId"];
                    model.User_Creation_Date = DateTime.Now;

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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "CreateLevel");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        //UPDATE EX_Constrain_Country_Item
        [HttpPost]
        public JsonResult UpdateEX_Constrain_Country_Item(EX_Constrain_Country_ItemDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    User_Session Current = User_Session.GetInstance;

                    model.User_Updation_Id=(short)Session["UserId"];
                    model.User_Updation_Date = DateTime.Now;

                    //check Repeated Data
                    var res = APIHandeling.Put(apiName, model);
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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "UpdateEX_Constrain_Country_Item");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        //DELETE EX_Constrain_Country_Item
        [HttpPost]
        public JsonResult DeleteEX_Constrain_Country_Item(byte ID)
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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "DeleteEX_Constrain_Country_Item");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        //CHECK CODE
        [HttpPost]
        public JsonResult EX_Constrain_Country_Item_List( int List=0)
        {
            try
            {
                //db_Plant.Configuration.ProxyCreationEnabled = false;
                var res = APIHandeling.getData(apiName + "?List="+ List);
                var data = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = data });
                //var continentals = lst.Select(c => new CustomOption { DisplayText = c.Name_Ar, Value = c.ID }).OrderByDescending(c => c.DisplayText).ToList();
                //return Json(new { Result = "OK", Options = continentals.OrderBy(a => a.Value).ToList() });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "EX_Constrain_Country_Item_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        /// <summary>
        /// //Eslam
        /// </summary>
        [HttpPost]
        public JsonResult EX_Constrain_Country_Item_List2(int EX_Constrain_Type_ID = 0)
        {
            try
            {
                //db_Plant.Configuration.ProxyCreationEnabled = false;
                var res = APIHandeling.getData(apiName + "?EX_Constrain_Type_ID=" + EX_Constrain_Type_ID);
                var data = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = data });
                //var continentals = lst.Select(c => new CustomOption { DisplayText = c.Name_Ar, Value = c.ID }).OrderByDescending(c => c.DisplayText).ToList();
                //return Json(new { Result = "OK", Options = continentals.OrderBy(a => a.Value).ToList() });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "EX_Constrain_Country_Item_List");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }


        [HttpPost]
        public JsonResult EX_Constrain_Country_Item_AddEDIT()
        {
            try
            {
                var res = APIHandeling.getData(apiName + "?AddEdit=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });
                //var continentals = lst.Select(c => new CustomOption { DisplayText = c.Name_Ar, Value = c.ID }).OrderByDescending(c => c.DisplayText).ToList();
                //return Json(new { Result = "OK", Options = continentals.OrderBy(a => a.Value).ToList() });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Level_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }


    }
}