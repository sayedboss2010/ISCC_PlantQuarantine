using PlantQuar.DTO.DTO.Farm.Print;
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
    public class Farm_PrintController : BaseController
    {
        // GET: FA_Farm/Farm_Print

        private string apiName = "Farm_Print_API";
        public ActionResult Index(long Farm_ID, long RequestId)
        {
            
             var user_Login =(short)Session["UserId"];
            var _FullName = Session["FullName"]; 
            if (user_Login != 0)
            {
                var res = APIHandeling.getData(apiName + "?Farm_ID=" + Farm_ID + "&User_Creation_Id=" + user_Login+ "&RequestId="+  RequestId);
                var lst = res.Content.ReadAsAsync<Farm_Data_Print_DTO>().Result;//object 
                ViewBag.FullName = _FullName;
                return View(lst);
            }
            else
                return Redirect("/Home");


        }

        public JsonResult GetFarms_Print(long Farm_ID)
        {
   
            var res = APIHandeling.getData(apiName + "?Farm_ID=" + Farm_ID);
            var lst = res.Content.ReadAsAsync<Farm_Data_Print_DTO>().Result;//object

            //var StatusCode = lst.ElementAt(0).Value;
            //var obj = lst.ElementAt(1).Value;

            //JavaScriptSerializer ser = new JavaScriptSerializer();
            //var myObj = ser.Deserialize<Dictionary<string, object>>(obj.ToString());

            //var count = myObj.ElementAt(0).Value;
            //var Lobj = myObj.ElementAt(1).Value;

            return Json(lst, JsonRequestBehavior.AllowGet);
        }
     
    }
}