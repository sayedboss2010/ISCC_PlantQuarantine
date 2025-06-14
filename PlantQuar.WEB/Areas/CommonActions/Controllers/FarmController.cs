using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.CommonActions.Controllers
{
    public class FarmController : Controller
    {
        // GET: CommonActions/Farm
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult PlantCategory_AddEDIT_Plant(int Item_ID = 0)
        {
            try
            {

                var res = APIHandeling.getData("FarmPlant_API?Item_IDsayed=" + Item_ID);
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;
                return Json(new { Result = "OK", Options = lst });

                //var res = APIHandeling.getData("ItemPart_API", "Get_PlantCategory_List?plantId=" + Plant_ID);
                //var lst = res.Content.ReadAsAsync<List<CustomOptionLongId>>().Result;
                //if (IsJtable)
                //{
                //    return Json(new { Result = "OK", Options = lst });
                //}
                //else
                //{
                //    return Json(lst, JsonRequestBehavior.AllowGet);
                //}
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "PlantCategory_AddEDIT");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
    }
}