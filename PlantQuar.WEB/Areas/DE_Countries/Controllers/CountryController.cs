using PlantQuar.DTO.DTO.DataEntry.Countries;
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

namespace PlantQuar.WEB.Areas.DE_Countries.Controllers
{
    public class CountryController : BaseController
    {
        // GET: DE_Countries/Country                   
        string apiName = "Country_API";

        // GET: Countries/Country
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AllCountryExcell()
        {
            var res = APIHandeling.getData(apiName + "?pageSize=-1&index=-1");

            var lst = res.Content.ReadAsAsync<List<CountryDTO>>().Result;//object

            //var StatusCode = lst.ElementAt(0).Value;
            //var obj = lst.ElementAt(1).Value;

            return Json(lst, JsonRequestBehavior.AllowGet);
        }
        //LOAD SEARCH
        [HttpPost]
        public JsonResult listCountries(string txt_AR_BTNSearch = "", string txt_EN_BTNSearch = "", int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = "")
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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "listCountries");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        //Insert
        [HttpPost]
        public JsonResult CreateCountries(CountryDTO model)
        {
            //List<int> ListUnions_Id,CountryDTO model
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
                    if ((int)res.StatusCode != 409)
                    {
                        if (model.ListUnions_Id > 0)
                        {
                            var country = res.Content.ReadAsAsync<CountryDTO>().Result;
                            short countryID = country.ID;
                            List<short> unionn = new List<short>();
                            unionn.Add((short)model.ListUnions_Id);
                            APIHandeling.Post("Union_Country_API?user_id=" + (short)Session["UserId"] + "&Date_Now=" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "&CountryID=" + countryID, unionn);
                            // model.ListUnions_Id.Remove(0);
                            //if (model.ListUnions_Id.Count != 0)
                            //{
                            //    APIHandeling.Post("Union_Country_API?user_id=" + (short)Session["UserId"] + "&Date_Now=" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "&CountryID=" + countryID, model.ListUnions_Id);
                            //}
                            return Json(new { Result = "OK", Record = model });
                        }
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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "CreateCountries");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        //UPDATE
        [HttpPost]
        public JsonResult UpdateCountries(CustomCountry_UnionList model)
        {
            try
            {
                var allErrors = ModelState.Values.SelectMany(x => x.Errors).ToList();

                if (ModelState.IsValid)
                {

                    User_Session Current = User_Session.GetInstance;

                    model.User_Updation_Id=(short)Session["UserId"];
                    model.User_Updation_Date = DateTime.Now;

                    //var mynewobj = APIHandeling.Put("Country?ListArrTest=1", model);
                    //var lst = mynewobj.Content.ReadAsAsync<CustomCountry_UnionList>().Result;


                    //return Json(new { Result = "OK", Record = model });
                    //check Repeated Data
                    // model.ListUnions_Id.Remove(0);
                    List<int> unionn = new List<int>();
                    if(model.ListUnions_Id != null)
                    {
                        unionn.Add((int)model.ListUnions_Id);
                    }
                    
                    model.ListUnions_Id2 = unionn;
                    var res = APIHandeling.Put("Country_API?ListArrTest=1", model);
                    if ((int)res.StatusCode != 409)
                    {
                        /*  if (model.ListUnions_Id.Count != 0)
                          {
                              APIHandeling.Put("Union_Country?user_id=" + (short)Session["UserId"] + "&Date_Now=" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "&CountryID=" + model.ID, model.ListUnions_Id);
                          }*/
                        return Json(new { Result = "OK" });
                    }
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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "UpdateCountries");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

        //DELETE
        [HttpPost]
        public JsonResult DeleteCountries(Int16 ID)
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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "DeleteCountries");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

    }
}