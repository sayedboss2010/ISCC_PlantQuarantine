using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Admin;
using PlantQuar.DTO.DTO.Export_CheckRequest;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using PlantQuar.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.Export_CheckRequest.Controllers
{
    public class CheckRequestChangeGeshniController : BaseController
    {
        // GET: Export_CheckRequest/CheckRequestChangeGeshni
        string apiName = "CheckRequestChangeGeshni_API";
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetGeshniCommittee(string CheckRequestNumber)
        {
            try
            {
                var res = APIHandeling.getData(apiName+"?CheckRequestNumber=" + CheckRequestNumber);
                var lst = res.Content.ReadAsAsync<GeshniCommitteesDTO>().Result;

                if (lst!=null)
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


        public JsonResult GetEmployeeGeshniChange(string requestNumber)
        {
            try
            {
                var res = APIHandeling.getData(apiName + "?requestNumber=" + requestNumber);
                var lst = res.Content.ReadAsAsync < List<EmployeeGeshniChangeDTO>>().Result;
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
        public JsonResult GetGeshniPorts()
        {
            try
            {
                var res = APIHandeling.getData(apiName+"?port=1");
                var lst = res.Content.ReadAsAsync<List<CustomOption>>().Result;


                return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetGeshniPorts");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        public JsonResult GetGeshniStation()
        {
            try
               {
                var res = APIHandeling.getData(apiName + "?station=1");
                var lst = res.Content.ReadAsAsync< List<CustomOptionLongId>>().Result;


                return Json(new { Result = "OK", Options = lst }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetGeshniStation");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        public JsonResult GetNewPortGeshni( string Ex_CheckRequest_Number ,int NewPortGeshni_Id )
        {
            try
            {
                //  var ex_CheckRequest_Number = geshniPortsLst.Select(a => a.Ex_CheckRequest_Number).FirstOrDefault();
                // var newPortGeshni_Id = geshniPortsLst.Select(a => a.NewPortGeshni_Id).FirstOrDefault();
                var geshniPortsLst = new GeshniPortsDTO();
                geshniPortsLst.NewPortGeshni_Id = NewPortGeshni_Id;
                geshniPortsLst.Ex_CheckRequest_Number = Ex_CheckRequest_Number;
                geshniPortsLst.User_Creation_Id = (short)Session["UserId"];
                geshniPortsLst.User_Creation_Date = DateTime.Now;


                //var res = APIHandeling.Post(apiName+ "?req=1", geshniPortsLst);//& ex_CheckRequest_Number="+ ex_CheckRequest_Number+ "&newPortGeshni_Id="+ newPortGeshni_Id);
                var res = APIHandeling.Post("CheckRequestChangeGeshni_API", geshniPortsLst);
                return Json(new { Result = "OK" });
                //var lst = res.Content.ReadAsAsync<List<GeshniPortsDTO>>().Result;

                //var lst = res.Content.ReadAsAsync<CustomOption>().Result;

                // return Json("OK", JsonRequestBehavior.AllowGet);
               // return Json(new { Result = "OK" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetNewPortGeshni");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }
        public JsonResult GetNewStationGeshni(string Ex_CheckRequest_Number, long NewStationGeshni_Id)
        {
            try
            {
                var geshniStationLst = new GeshniStationDTO();
                geshniStationLst.NewStationGeshni_Id = NewStationGeshni_Id;
                geshniStationLst.Ex_CheckRequest_Number = Ex_CheckRequest_Number;
                geshniStationLst.User_Creation_Id = (short)Session["UserId"];
                geshniStationLst.User_Creation_Date = DateTime.Now; 

                var res = APIHandeling.Post(apiName+"?req1=1", geshniStationLst);//& ex_CheckRequest_Number="+ ex_CheckRequest_Number+ "&newPortGeshni_Id="+ newPortGeshni_Id);
                //var lst = res.Content.ReadAsAsync<GeshniStationDTO> ().Result;

                //var lst = res.Content.ReadAsAsync<CustomOption>().Result;

               // return Json("OK", JsonRequestBehavior.AllowGet);
                return Json(new { Result = "OK" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                APIHandeling.Insert_Error(Request.Url.AbsoluteUri.ToString(), ex.Message, "GetNewStationGeshni");
                return Json(new { Result = "ERROR", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.ErrorHappened) });
            }
        }

        /////////////////////////////



    }
}