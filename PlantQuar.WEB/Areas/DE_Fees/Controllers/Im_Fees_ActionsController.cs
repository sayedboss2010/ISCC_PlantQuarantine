using PlantQuar.DTO.DTO.DataEntry.Fees;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.DE_Fees.Controllers
{
    public class Im_Fees_ActionsController : Controller
    {

        string apiName = "Im_Fees_Actions_API";

        // GET: DE_Fees/Im_Fees_Actions
        public ActionResult Index()
        {
            var res = APIHandeling.getData(apiName + "?process=1");
            var modelLst = res.Content.ReadAsAsync<List<Im_Fees_ActionsDTO>>().Result;

            //new task
            var res_Fees_Money = APIHandeling.getData(apiName + "?index=1");
            var Fees_Money_lst = res_Fees_Money.Content.ReadAsAsync<List<Im_Fees_ActionsDTO>>().Result;   //is CustomOption change with Dto i will created it & wih files related
            ViewBag.Fees_Money_List= Fees_Money_lst;
            return View(modelLst);
        }

        [HttpPost]
        public JsonResult InsertFeesMoney(  List<Fees_ActionDTO> CheckedItemsList)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    foreach (var item in CheckedItemsList)
                    {
                        item.IsActive = true;
                        item.IsMandatory = true;
                        item.User_Creation_Id = (short)Session["UserId"];
                        item.User_Creation_Date = DateTime.Now;
                    }
                    var res = APIHandeling.Post(apiName , CheckedItemsList);

                    return ((int)res.StatusCode != 409) ? Json(new { Result = "OK", Record = CheckedItemsList })
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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "CreateTBLRows");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }


       }
}


