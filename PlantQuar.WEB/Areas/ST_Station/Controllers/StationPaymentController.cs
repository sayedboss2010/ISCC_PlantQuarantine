using PlantQuar.DTO.DTO.Farm.FarmRequest;
using PlantQuar.DTO.DTO.Station;
using PlantQuar.DTO.DTO.StationNew;
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

namespace PlantQuar.WEB.Areas.ST_Station.Controllers
{
    public class StationPaymentController : BaseController
    {
        // GET: ST_Station/StationPayment
        string apiName = "StationPayment_API";
        // GET: Payment/StationPayment
        public ActionResult Index(long station_committee_Id)
        {
            try
            {
                var res = APIHandeling.getData(apiName + "?station_committee_Id=" + station_committee_Id);

                var lst = res.Content.ReadAsAsync<Dictionary<string, object>>().Result;//object

                var StatusCode = lst.ElementAt(0).Value;
                var obj = lst.ElementAt(1).Value;

                JavaScriptSerializer ser = new JavaScriptSerializer();
                var myObj = ser.Deserialize<Dictionary<string, object>>(obj.ToString());

                var stationCode = myObj.ElementAt(0).Value;
                var payments = myObj.ElementAt(1).Value;
                var feesAmount = myObj.ElementAt(2).Value;//
                var totalamount = myObj.ElementAt(3).Value;

                ViewBag.stationCode = stationCode;
                ViewBag.payments = payments;
                ViewBag.feesAmount = feesAmount;
                ViewBag.totalamount = totalamount;
                ViewBag.station_committee_Id = station_committee_Id;
                return View();
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "Index");
                return null;
            }
        }
        [HttpPost]
        public JsonResult SaveStationCommitteePayment(long stationcommitee_Id, decimal moneyamount, decimal totalRequire)
        {
            try
            {
                Station_Accreditation_PaymentDTO ex_Im_PaymentDTO = new Station_Accreditation_PaymentDTO();
                ex_Im_PaymentDTO.Amount = moneyamount;
                ex_Im_PaymentDTO.Station_Committee_ID = stationcommitee_Id;

                ex_Im_PaymentDTO.User_Creation_Date = DateTime.Now;
                ex_Im_PaymentDTO.IS_OnlineOffline = false;

                ex_Im_PaymentDTO.totalRequire = totalRequire;
                User_Session Current = User_Session.GetInstance;
                ex_Im_PaymentDTO.User_Creation_Id = (short)Session["UserId"];

                Dictionary<string, string> dicData = new Dictionary<string, string>();
                var res = APIHandeling.Post(apiName, ex_Im_PaymentDTO);
                var lst = res.Content.ReadAsAsync<Ex_Im_Payment_GetDataDTO>().Result;

                return (lst != null) ? Json(new { Result = 1, Payment_Data = lst }) : Json(new { Result = -1 });
            }
            catch (Exception ex)
            {
                if (ex.HResult == -2146233087)
                {
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RelatedData) });
                }
                else
                {
                    APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetCommittee");
                    return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
                }
            }
        }

    }
}