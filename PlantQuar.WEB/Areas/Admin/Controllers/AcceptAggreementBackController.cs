using PlantQuar.DTO.DTO.Admin;
using PlantQuar.DTO.DTO.Export_CheckRequest;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.Admin.Controllers
{
    public class AcceptAggreementBackController : Controller
    {
        // GET: Admin/AcceptAggreementBack
        string apiName = "AcceptAggreementBack_API";
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetRequestAggreement(string RequestNumber , int Request_type)
        {
            try
            {
                var User_Id = (short)Session["UserId"];
                var res = APIHandeling.getData(apiName + "?RequestNumber=" + RequestNumber + "&Request_type="+ Request_type+ "&User_Id="+ User_Id);
                var lst = res.Content.ReadAsAsync<AcceptAggreementBackDTO>().Result;

                if (lst != null)
                {
                    return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
                }
                else { return Json(new { Result = "Empty" }); }


                //var res = APIHandeling.Post("CheckRequestChangeGeshni_API", geshniPortsLst);
                //return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetGeshniCommittee");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }


        public JsonResult GetEmployeeGeshniChange(string requestNumber, int requesttype)
        {
            try
            {
                var res = APIHandeling.getData(apiName+"?requestNumber=" + requestNumber+ "&requesttype="+ requesttype);
                var lst = res.Content.ReadAsAsync<List<EmployeeGeshniChangeDTO>>().Result;
                //<List<A__plant_Error_SaveDTO>>().Result;
                if (lst != null)
                {
                    return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
                }
                else { return Json(new { Result = "Empty" }); }

            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetGeshniCommittee");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }


    }
}