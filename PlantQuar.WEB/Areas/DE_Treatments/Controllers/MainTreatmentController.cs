using PlantQuar.DTO.DTO.DataEntry.Treatments;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.DE_Treatments.Controllers
{
    public class MainTreatmentController : Controller
    {
        // GET: DE_Treatments/MainTreatment

        // GetTreatmentMethod_List
        public ActionResult Index()
        {
            return View();
        }



        public ActionResult getTreatmentMain()
        {

            short id = 0;
             var res1 = APIHandeling.getData( "TreatmentMethod_API?methodList=" + id);
            var lst1 = res1.Content.ReadAsAsync<List<TreatmentMethodDTO>>().Result;
          

            return Json(lst1, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteFeesAction(long id)
        {

 

            try
            {
                DeleteParameters obj = new DeleteParameters();
                obj.id = id;
               // User_Session Current = User_Session.GetInstance;
                obj.Userid = (short)Session["UserId"];
                obj._DateNow = DateTime.Now;
                APIHandeling.Delete("TreatmentMethod_API", obj);
              //  return RedirectToAction("Index");
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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "DeleteTreatmentMethod");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }

        }
         public ActionResult SearchMainTreatment(string arabic, string english)
        {

            

            try
            {

                short id = 0;
                var res1 = APIHandeling.getData("TreatmentMethod_API?arName=" + arabic
                    + "&enName="+ english);
                var lst1 = res1.Content.ReadAsAsync<List<TreatmentMethodDTO>>().Result;


                return Json(lst1, JsonRequestBehavior.AllowGet);

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
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "DeleteTreatmentMethod");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }

        }

    }
}