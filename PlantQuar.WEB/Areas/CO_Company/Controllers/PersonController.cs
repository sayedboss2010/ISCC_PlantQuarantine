using PlantQuar.DTO.DTO.Common;
using PlantQuar.DTO.DTO.Company;
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

namespace PlantQuar.WEB.Areas.CO_Company.Controllers
{
    public class PersonController : BaseController
    {
        // GET: CO_Company/Person
        string apiName = "Person_API";
        public ActionResult Index()
        {
            return View();
        }

        //LOAD SEARCH
        public JsonResult listPerson
        (string txt_AR_BTNSearch = "", int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = "")
        {
            try
            {

                var res = APIHandeling.getData(apiName + "?arName=" + txt_AR_BTNSearch + "&pageSize=" + jtPageSize.ToString() + "&index=" + jtStartIndex.ToString() + "&jtSorting=" + jtSorting.ToString());

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

        public ActionResult Details(long id = 0)
        {
            var model = getPersonByID(id);
            var res = APIHandeling.getData(apiName + "?countryId=" + model.Country_ID +"");
            ViewBag.CountryList = res.Content.ReadAsAsync<List<CustomOption>>().Result;
            Session["id"] = model.ID;
            return View(model);
        }
        //save
        [HttpPost]
        public ActionResult SavePerson(PersonDTO model)
        {
            User_Session Current = User_Session.GetInstance;

            model.User_Updation_Id=(short)Session["UserId"];
            model.User_Updation_Date = DateTime.Now;

            APIHandeling.Put(apiName, model);
            return RedirectToAction("Index", "Person", new { area = "CO_Company", id = model.ID });
        }

        private PersonDTO getPersonByID(long id)
        {
            var res = APIHandeling.getData(apiName + "?details=1&Id=" + id);
            return res.Content.ReadAsAsync<PersonDTO>().Result;
        }
        
    }
}