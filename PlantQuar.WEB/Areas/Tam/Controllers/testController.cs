using ASPNET_MVC_Samples.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.Tam.Controllers
{
    public class testController : Controller
    {
        // GET: Tam/test
        public ActionResult Index()
        {
            List<DataPoint> dataPoints = new List<DataPoint>{
                new DataPoint(10, 22),
                //new DataPoint(20, 36),
                //new DataPoint(30, 42),
                //new DataPoint(40, 51),
                //new DataPoint(50, 46),
            };

            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

            return View();
            
        }
        //[HttpPost]
        //public ActionResult Get_Data_Im_CheckRequest(short Id)
        //{
        //    try
        //    {
        //        var Fees_Process = APIHandeling.getData(apiName + "?User_Id=" + Id + "");
        //        var lst = Fees_Process.Content.ReadAsAsync<User>().Result;


        //        return Json(lst, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetOutlet_ID");
        //        return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
        //    }
        //}



        //public ActionResult Column()
        //{
        //    //Below code can be used to include dynamic data in Chart. Check view page and uncomment the line "dataPoints: @Html.Raw(ViewBag.DataPoints)"
        //    //ViewBag.DataPoints = JsonConvert.SerializeObject(DataService.GetRandomDataForCategoryAxis(10), _jsonSetting);

        //    return View();
        //}
        //JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };

    }
}