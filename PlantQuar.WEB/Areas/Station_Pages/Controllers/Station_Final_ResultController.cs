using PlantQuar.DTO.DTO.Station_Pages;
using PlantQuar.WEB.App_Start;
using PlantQuar.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.Station_Pages.Controllers
{
    public class Station_Final_ResultController : BaseController
    {
        // GET: Station_Pages/Station_Final_Result
        public ActionResult Index(long stationId)
        {

            var res = APIHandeling.getData("Station_Final_Result_API?stationIdNew=" + stationId);
            var model = res.Content.ReadAsAsync<Station_Final_Result_New_DTO>().Result;//object
            ViewBag.stationId = stationId;
            if (model != null)
            {
                return View(model);
            }
            else
            {
                return View();
            }

        }

        public ActionResult Indexnew(long stationId)
        {
            var res = APIHandeling.getData("Station_Final_Result_API?stationId=" + stationId);
            var model = res.Content.ReadAsAsync<List<Station_Final_Result_DTO>>().Result;//object
            ViewBag.stationId = stationId;
            if (model != null)
            {
                return View(model);
            }
            else
            {
                return View();
            }
        }

        public JsonResult Save_Station_Requeste_Quarantine(long Station_Requst_ID, string Notes_Quarantine
            , DateTime? StartDate_Quarantine, DateTime? EndDate_Quarantine, bool IsAccepted_Quarantine
            , long Station_Accreditation_Data_ID, long Station_ID)
        {

            if (Station_Requst_ID != null)
            {

                Station_Accreditation_Request_DTO req = new Station_Accreditation_Request_DTO();
                req.ID = Station_Requst_ID;
                req.Notes_Quarantine = Notes_Quarantine;
                req.StartDate = StartDate_Quarantine;
                req.EndDate = EndDate_Quarantine;
                req.IsAccepted = IsAccepted_Quarantine;
                req.User_Updation_Id = (short)Session["UserId"];
                req.Station_Accreditation_Data_ID = Station_Accreditation_Data_ID;
                req.Station_ID = Station_ID;
                var res2 = APIHandeling.Put("Station_Final_Result_API?Insert_req=1", req);
                var list = res2.Content.ReadAsAsync<Dictionary<string, object>>().Result;
                if (list != null)
                    return Json("Exist", JsonRequestBehavior.AllowGet);
                else
                    return Json("error", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return null;
            }
        }
    }
}