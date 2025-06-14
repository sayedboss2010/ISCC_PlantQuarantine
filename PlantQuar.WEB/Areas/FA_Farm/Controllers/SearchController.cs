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

namespace PlantQuar.WEB.Areas.FA_Farm.Controllers
{
    public class SearchController : BaseController
    {

        string apiName = "Farm_Constrain_Search_API";

        // GET: FA_Farm/Search
        public ActionResult Index()
        {

            try
            {
                var Fees_Process = APIHandeling.getData(apiName + "?Item_ID=1");
                var lst = Fees_Process.Content.ReadAsAsync<List<CustomOption>>().Result;
                // var lst1 = lst[0];
                //ViewBag.ddd = new SelectList(lst, "Value", "DisplayText");
                ViewBag.ddd = lst;
                // ViewBag.sss = lst1;
                ;

                // return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetOutlet_ID");
                // return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }




            return View();
        }


        [HttpPost]

        public ActionResult GetItems(int countryID)
        {
            try
            {
                var Fees_Process = APIHandeling.getData(apiName + "?countryID=" + countryID + "&Item_ID=1");
                var lst = Fees_Process.Content.ReadAsAsync<List<CustomOption>>().Result;
                //ViewBag.ddd = new SelectList(lst, "Value", "DisplayText");
                //ViewBag.ddd = lst;
                return Json(new SelectList(lst, "Value", "DisplayText"));
                //  return Json(new SelectList(lst1, "Value", "DisplayText"));
                // return Json(lst, JsonRequestBehavior.AllowGet);
                // return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetOutlet_ID");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }



            //   
            //  return Json(new { Result = "OK", Options = lst });

        }
        [HttpPost]

        public ActionResult getCountryItemData(int countryID, int Item_ID)
        {
            try
            {

                var Fees_Process = APIHandeling.getData(apiName + "?countryID=" + countryID + "&Item_ID=" + Item_ID + "&Item_ID1=1&Item_ID2=1");
                var lst = Fees_Process.Content.ReadAsAsync<List<Farm_Constrain_TextDTO>>().Result;
                lst.RemoveAt(0);


                return Json(lst, JsonRequestBehavior.AllowGet);

                //return Json(new SelectList(lst));

            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetOutlet_ID");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }




        }
        [HttpPost]

        public ActionResult GetFarmCode(String FarmCode)
        {
            try
            {

                // , String FarmCode1,
                var Fees_Process = APIHandeling.getData(apiName + "?FarmCode=" + FarmCode + "&FarmCode1=''&Item_ID=1");
                var lst = Fees_Process.Content.ReadAsAsync<List<Farm_Constrain_TextDTO>>().Result;
                lst.RemoveAt(0);
                //ViewBag.ddd = new SelectList(lst, "Value", "DisplayText");
                //ViewBag.ddd = lst;
                return Json(lst, JsonRequestBehavior.AllowGet);
                //  return Json(new SelectList(lst1, "Value", "DisplayText"));
                // return Json(lst, JsonRequestBehavior.AllowGet);
                // return Json(new { Result = "OK", Options = lst });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetOutlet_ID");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }



            //   
            //  return Json(new { Result = "OK", Options = lst });

        }
    }
}